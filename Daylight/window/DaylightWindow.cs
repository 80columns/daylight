using Daylight.helper;
using Daylight.settings;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Daylight.window {
    public enum Theme {
        Dark = 0,
        Light = 1
    }

    public partial class DaylightWindow : Form {
        private Helper p_Helper;
        private Theme p_Theme;

        // declare app elements as class members so they can be properly disposed
        private NotifyIcon p_Notification_Icon;
        private ContextMenuStrip p_Notification_Menu;

        private (string Latitude, string Longitude)? p_Coordinates;
        private string p_Location;
        private TimeSpan p_Sunrise;
        private TimeSpan p_Sunset;

        private (string Latitude, string Longitude)? p_SettingsWindow_Coordinates;
        private CancellationTokenSource p_Wait_Cancellation_Token_Source;
        private bool p_Waiting_For_Time_Of_Day;
        private bool p_Configuring_Settings_Window;

        public DaylightWindow() {
            this.p_Helper = new Helper();
            this.p_Wait_Cancellation_Token_Source = new CancellationTokenSource();
            this.p_Waiting_For_Time_Of_Day = false;
            this.p_Configuring_Settings_Window = false;

            // create the app icon in the taskbar notification area
            this.CreateNotificationIcon();

            // built-in WinForms initialization
            InitializeComponent();
        }

        #region ThemeUpdates
        private void UpdateTheme() {
            SetWindowsTheme();
            SetAppTheme(this);
            SetIconTheme();
        }

        private void SetWindowsTheme() {
            using (
                var Key = Registry.CurrentUser.OpenSubKey(
                    name: AppSettings.Registry.Path.Themes_Personalization,
                    writable: true
                )
            ) {
                Key.SetValue(
                    name: AppSettings.Registry.Key.Apps_Light_Theme,
                    value: (int)this.p_Theme,
                    valueKind: RegistryValueKind.DWord
                );

                Key.SetValue(
                    name: AppSettings.Registry.Key.System_Light_Theme,
                    value: (int)this.p_Theme,
                    valueKind: RegistryValueKind.DWord
                );
            }
        }

        private void SetAppTheme(
            Control _Control
        ) {
            foreach (Control Child_Control in _Control.Controls) {
                SetAppTheme(Child_Control);
            }

            _Control.ForeColor = (this.p_Theme == Theme.Dark) ? AppSettings.Theme.Dark.Font : AppSettings.Theme.Light.Font;
            _Control.BackColor = (this.p_Theme == Theme.Dark) ? AppSettings.Theme.Dark.Background : AppSettings.Theme.Light.Background;
        }

        private void SetIconTheme() {
            this.p_Notification_Icon.Icon = (this.p_Theme == Theme.Dark) ? AppSettings.File.Icon.App.Light : AppSettings.File.Icon.App.Dark;
        }
        #endregion

        #region TimeDelays
        // wait until 12:30 am before pulling the sunrise/sunset times for the new day
        private async Task WaitUntilOhDarkThirtyAsync() {
            // sleep until 30 minutes after midnight before pulling the sunrise/sunset for the new day
            while (true) {
                try {
                    await Task.Delay(
                        millisecondsDelay: (int)AppSettings.Time.Oh_Dark_Thirty_Tomorrow.Subtract(DateTime.Now).TotalMilliseconds,
                        cancellationToken: this.p_Wait_Cancellation_Token_Source.Token
                    );

                    break;
                } catch (TaskCanceledException) {
                    this.p_Wait_Cancellation_Token_Source = new CancellationTokenSource();
                }
            }

            // pull the sunrise / sunset times for the new day
            (
                this.p_Sunrise,
                this.p_Sunset
            ) = await this.p_Helper.Location.GetSunriseAndSunsetAsync(_Coordinates: this.p_Coordinates);

            this.SettingsWindow_DateLabel.Text = DateTime.Now.ToLongDateString();

            // update the sunrise / sunset times in the settings window
            this.SettingsWindow_SunriseTimepicker.Value = this.p_Sunrise.GetDate();
            this.SettingsWindow_SunsetTimepicker.Value = this.p_Sunset.GetDate();

            await this.p_Helper.Log.LogAppAsync($"retrieved sunrise {this.p_Sunrise}, sunset {this.p_Sunset}, date is now {this.SettingsWindow_DateLabel.Text}");
        }

        // wait until sunrise to apply the light theme
        private async Task WaitUntilSunriseAsync() {
            while (true) {
                try {
                    await this.p_Helper.Log.LogAppAsync($"waiting until sunrise at {this.p_Sunrise}");

                    // sleep until the calculated sunrise time before setting the light theme
                    await Task.Delay(
                        millisecondsDelay: (int)this.p_Sunrise.Subtract(DateTime.Now.TimeOfDay).TotalMilliseconds,
                        cancellationToken: this.p_Wait_Cancellation_Token_Source.Token
                    );

                    break;
                } catch (TaskCanceledException) {
                    this.p_Wait_Cancellation_Token_Source = new CancellationTokenSource();
                }
            }
        }

        // wait until sunset to apply the dark theme
        private async Task WaitUntilSunsetAsync() {
            while (true) { 
                try {
                    await this.p_Helper.Log.LogAppAsync($"waiting until sunset at {this.p_Sunset}");

                    // sleep until the calculated sunset time before setting the dark theme
                    await Task.Delay(
                        millisecondsDelay: (int)this.p_Sunset.Subtract(DateTime.Now.TimeOfDay).TotalMilliseconds,
                        cancellationToken: this.p_Wait_Cancellation_Token_Source.Token
                    );

                    break;
                } catch (TaskCanceledException) {
                    this.p_Wait_Cancellation_Token_Source = new CancellationTokenSource();
                }
            }
        }

        private async Task WaitUntilNextTimeOfDayAsync() {
            this.p_Waiting_For_Time_Of_Day = true;

            while (true) {
                var Now = DateTime.Now;

                if (
                    Now.TimeOfDay >= this.p_Sunrise
                 && Now.TimeOfDay < this.p_Sunset
                ) {
                    await this.p_Helper.Log.LogAppAsync($"setting theme to light, then calling {nameof(WaitUntilSunsetAsync)}");

                    // it's daytime
                    this.p_Theme = Theme.Light;

                    this.UpdateTheme();
                    await this.WaitUntilSunsetAsync();
                } else if (
                    Now.TimeOfDay >= this.p_Sunset
                 && Now.TimeOfDay <= AppSettings.Time.Last_Time_Before_Midnight
                ) {
                    await this.p_Helper.Log.LogAppAsync($"setting theme to dark, then calling {nameof(WaitUntilOhDarkThirtyAsync)}");

                    // it's nighttime, before midnight
                    this.p_Theme = Theme.Dark;

                    this.UpdateTheme();
                    await this.WaitUntilOhDarkThirtyAsync();
                } else if (
                    Now.TimeOfDay < this.p_Sunrise
                 && Now.TimeOfDay >= AppSettings.Time.Midnight
                ) {
                    await this.p_Helper.Log.LogAppAsync($"setting theme to dark, then calling {nameof(WaitUntilSunriseAsync)}");

                    // it's nighttime, after midnight
                    this.p_Theme = Theme.Dark;

                    this.UpdateTheme();
                    await this.WaitUntilSunriseAsync();
                }
            }
        }
        #endregion

        #region WindowModifications
        private void CreateNotificationIcon() {
            // create the right-click menu for the notification icon
            this.p_Notification_Menu = new ContextMenuStrip();
            this.p_Notification_Menu.Items.AddRange(
                toolStripItems: new ToolStripMenuItem[] {
                    new ToolStripMenuItem(
                        text: "About",
                        image: null,
                        onClick: this.ShowAboutWindow
                    ),
                    new ToolStripMenuItem(
                        text: "Settings",
                        image: null,
                        onClick: this.ShowSettings
                    )
                }
            );

            this.p_Notification_Menu.Items.Add(new ToolStripSeparator());

            this.p_Notification_Menu.Items.Add(
                new ToolStripMenuItem(
                    text: "Exit",
                    image: null,
                    onClick: this.ExitApplication
                )
            );

            // create the windows taskbar notification icon
            this.p_Notification_Icon = new NotifyIcon {
                Text = AppSettings.App.Name,
                ContextMenuStrip = this.p_Notification_Menu,
                Visible = true,
                Icon = AppSettings.File.Icon.App.Dark
            };
        }

        private void ConfigureSettingsWindow() {
            this.p_Configuring_Settings_Window = true;

            // set an empty format for the datetime pickers
            this.SettingsWindow_SunriseTimepicker.CustomFormat = AppSettings.Time.Format.Empty;
            this.SettingsWindow_SunsetTimepicker.CustomFormat = AppSettings.Time.Format.Empty;

            // set the datetime pickers default values
            this.SettingsWindow_SunriseTimepicker.Value = ExtensionHelper.GenerateDate(_Hour: 7);
            this.SettingsWindow_SunsetTimepicker.Value = ExtensionHelper.GenerateDate(_Hour: 19);

            if (this.p_Helper.User_Settings.LocationSource == UserLocationSource.Internet) {
                // select the internet radio button
                this.SettingsWindow_AutoDetectRadioButton.Checked = true;

                // populate & disable the location text box
                this.SettingsWindow_LocationTextBox.Text = this.p_Location;
                this.SettingsWindow_LocationTextBox.Enabled = false;

                // disable the location validation button
                this.SettingsWindow_LocationValidationButton.Enabled = false;

                // disable the sunrise/sunset date pickers
                this.SettingsWindow_SunriseTimepicker.Enabled = false;
                this.SettingsWindow_SunsetTimepicker.Enabled = false;
            } else if (this.p_Helper.User_Settings.LocationSource == UserLocationSource.Custom) {
                // select the custom location radio button
                this.SettingsWindow_CustomLocationRadioButton.Checked = true;

                // enable the location text box
                this.SettingsWindow_LocationTextBox.Enabled = true;

                // enable the location validation button
                this.SettingsWindow_LocationValidationButton.Enabled = true;

                // disable the sunrise/sunset date pickers
                this.SettingsWindow_SunriseTimepicker.Enabled = false;
                this.SettingsWindow_SunsetTimepicker.Enabled = false;
            } else if (this.p_Helper.User_Settings.LocationSource == UserLocationSource.None) {
                // select the custom times radio button
                this.SettingsWindow_CustomTimesRadioButton.Checked = true;

                // disable the location text box
                this.SettingsWindow_LocationTextBox.Enabled = false;

                // disable the location validation button
                this.SettingsWindow_LocationValidationButton.Enabled = false;

                // enable the sunrise/sunset date pickers
                this.SettingsWindow_SunriseTimepicker.Enabled = true;
                this.SettingsWindow_SunsetTimepicker.Enabled = true;
            }

            this.p_Configuring_Settings_Window = false;
        }

        private void PopulateSettingsWindow() {
            this.SettingsWindow_SunriseTimepicker.CustomFormat = AppSettings.Time.Format.Hour_Minute_Meridiem;
            this.SettingsWindow_SunsetTimepicker.CustomFormat = AppSettings.Time.Format.Hour_Minute_Meridiem;

            // populate the location text box
            this.SettingsWindow_LocationTextBox.Text = this.p_Location;

            // set the date label value
            this.SettingsWindow_DateLabel.Text = DateTime.Now.ToLongDateString();

            // populate the sunrise/sunset date pickers
            this.SettingsWindow_SunriseTimepicker.Value = this.p_Sunrise.GetDate();
            this.SettingsWindow_SunsetTimepicker.Value = this.p_Sunset.GetDate();
        }
        #endregion

        #region WindowEvents
        protected override async void OnLoad(
            EventArgs _e
        ) {
            // load the user-configurable settings
            await this.p_Helper.CreateUserSettingsAsync();

            // set the configuration for the settings window based on the user settings
            this.ConfigureSettingsWindow();

            try {
                // check if we need to pull coordinates
                if (this.p_Helper.User_Settings.LocationSource != UserLocationSource.None) {
                    // get the coordinates either from the settings or the IP address
                    (
                        this.p_Coordinates,
                        this.p_Location
                    ) = await this.p_Helper.Location.GetCoordinatesAndLocationAsync();

                    await this.p_Helper.Log.LogAppAsync($"retrieved coordinates ({(this.p_Coordinates.HasValue ? $"{this.p_Coordinates.Value.Latitude}, {this.p_Coordinates.Value.Longitude}" : "null")}) and location '{this.p_Location}'");
                }

                // get the sunrise & sunset times for today
                (
                    this.p_Sunrise,
                    this.p_Sunset
                ) = await this.p_Helper.Location.GetSunriseAndSunsetAsync(this.p_Coordinates);

                await this.p_Helper.Log.LogAppAsync($"retrieved sunrise '{this.p_Sunrise}' and sunset '{this.p_Sunset}'");

                // set the location / sunrise / sunset values for the settings window after they've been retrieved above
                this.PopulateSettingsWindow();

                // start the waiting loop for sunrise/sunset/after midnight
                this.WaitUntilNextTimeOfDayAsync();
            } catch (Exception ex) {
                await this.p_Helper.Log.LogExceptionAsync(ex);

                // show the settings window
                this.Visible = true;
                this.ShowInTaskbar = true;

                this.SettingsWindow_ErrorMessageLabel.Text = $"Error: your location could not be found via {this.p_Helper.User_Settings.LocationSource.ToDisplayString()}. To continue, select a different location source.";
            }

            base.OnLoad(_e);
        }

        protected override void OnClosing(
            CancelEventArgs e
        ) {
            if (this.p_Waiting_For_Time_Of_Day == false) {
                var Exit_Result = MessageBox.Show(
                    text: "Without valid settings, the application will exit. Please confirm below if you'd like to continue.",
                    caption: "Do you want to exit?",
                    buttons: MessageBoxButtons.YesNo,
                    icon: MessageBoxIcon.Question,
                    defaultButton: MessageBoxDefaultButton.Button2
                );

                if (Exit_Result == DialogResult.No) {
                    e.Cancel = true;
                }
            } else {
                this.Visible = false;
                this.ShowInTaskbar = false;

                // restore the user settings if they were changed but not saved
                if (
                    (this.p_Helper.User_Settings.LocationSource == UserLocationSource.Internet && this.SettingsWindow_AutoDetectRadioButton.Checked == false)
                 || (this.p_Helper.User_Settings.LocationSource == UserLocationSource.Custom && this.SettingsWindow_CustomLocationRadioButton.Checked == false)
                 || (this.p_Helper.User_Settings.LocationSource == UserLocationSource.None && this.SettingsWindow_CustomTimesRadioButton.Checked == false)
                ) {
                    // set the configuration for the settings window based on the user settings
                    this.ConfigureSettingsWindow();

                    // set the location / sunrise / sunset values for the settings window
                    this.PopulateSettingsWindow();
                }

                e.Cancel = true;
            }
        }
        #endregion

        #region WindowButtonEvents
        private async void SettingsWindow_AutoDetectButton_CheckedChanged(
            object sender,
            EventArgs e
        ) {
            // the internet location source was just selected as the new location source,
            // and the current user settings location source is not the internet
            if (
                this.SettingsWindow_AutoDetectRadioButton.Checked
             && this.p_Configuring_Settings_Window == false
            ) {
                try {
                    this.Cursor = Cursors.AppStarting;

                    var Location = string.Empty;

                    this.SettingsWindow_ErrorMessageLabel.Text = string.Empty;

                    this.SettingsWindow_LocationTextBox.Enabled = false;
                    this.SettingsWindow_LocationValidationButton.Enabled = false;
                    this.SettingsWindow_SunriseTimepicker.Enabled = false;
                    this.SettingsWindow_SunsetTimepicker.Enabled = false;

                    if (this.p_Helper.User_Settings.LocationSource == UserLocationSource.Internet) {
                        // restore the auto-detect location settings currently in memory

                        // populate the location text box
                        this.SettingsWindow_LocationTextBox.Text = this.p_Location;

                        // populate the sunrise/sunset date pickers
                        this.SettingsWindow_SunriseTimepicker.Value = this.p_Sunrise.GetDate();
                        this.SettingsWindow_SunriseTimepicker.CustomFormat = AppSettings.Time.Format.Hour_Minute_Meridiem;

                        this.SettingsWindow_SunsetTimepicker.Value = this.p_Sunset.GetDate();
                        this.SettingsWindow_SunsetTimepicker.CustomFormat = AppSettings.Time.Format.Hour_Minute_Meridiem;
                    } else {
                        // pull the coordinates / location from the internet
                        (
                            this.p_SettingsWindow_Coordinates,
                            Location
                        ) = await this.p_Helper.Location.GetIpCoordinatesAsync();

                        await this.p_Helper.Log.LogAppAsync($"retrieved coordinates ({(this.p_SettingsWindow_Coordinates.HasValue ? $"{this.p_SettingsWindow_Coordinates.Value.Latitude}, {this.p_SettingsWindow_Coordinates.Value.Longitude}" : "null")}) and location '{Location}'");

                        // pull the sunrise / sunset times
                        var (
                            Sunrise,
                            Sunset
                        ) = await this.p_Helper.Location.GetSunriseAndSunsetAsync(_Coordinates: this.p_SettingsWindow_Coordinates);

                        await this.p_Helper.Log.LogAppAsync($"retrieved sunrise '{Sunrise}' and sunset '{Sunset}'");

                        // update the settings window

                        // populate the location text box
                        this.SettingsWindow_LocationTextBox.Text = Location;

                        // populate the sunrise/sunset date pickers
                        this.SettingsWindow_SunriseTimepicker.Value = Sunrise.GetDate();
                        this.SettingsWindow_SunriseTimepicker.CustomFormat = AppSettings.Time.Format.Hour_Minute_Meridiem;

                        this.SettingsWindow_SunsetTimepicker.Value = Sunset.GetDate();
                        this.SettingsWindow_SunsetTimepicker.CustomFormat = AppSettings.Time.Format.Hour_Minute_Meridiem;
                    }
                } catch (Exception ex) {
                    await this.p_Helper.Log.LogExceptionAsync(ex);

                    this.SettingsWindow_ErrorMessageLabel.Text = $"Error: your location could not be found via {UserLocationSource.Internet.ToDisplayString()}. To continue, select a different location source.";
                } finally {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private async void SettingsWindow_CustomLocationRadioButton_CheckedChanged(
            object sender,
            EventArgs e
        ) {
            // the custom location source was just selected as the new location source
            if (
                this.SettingsWindow_CustomLocationRadioButton.Checked
             && this.p_Configuring_Settings_Window == false
            ) {
                try {
                    this.Cursor = Cursors.AppStarting;

                    this.SettingsWindow_ErrorMessageLabel.Text = string.Empty;

                    this.SettingsWindow_LocationTextBox.Enabled = true;
                    this.SettingsWindow_LocationValidationButton.Enabled = true;
                    this.SettingsWindow_SunriseTimepicker.Enabled = false;
                    this.SettingsWindow_SunsetTimepicker.Enabled = false;

                    this.SettingsWindow_LocationTextBox.Text = string.Empty;

                    // display an empty value for the sunrise/sunset
                    // https://stackoverflow.com/a/8448499
                    this.SettingsWindow_SunriseTimepicker.CustomFormat = AppSettings.Time.Format.Empty;
                    this.SettingsWindow_SunsetTimepicker.CustomFormat = AppSettings.Time.Format.Empty;
                } catch (Exception ex) {
                    await this.p_Helper.Log.LogExceptionAsync(ex);

                    this.SettingsWindow_ErrorMessageLabel.Text = $"Error: the custom location '{this.SettingsWindow_LocationTextBox.Text}' could not be resolved. To continue, input and validate a different location.";
                } finally {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void SettingsWindow_CustomTimesRadioButton_CheckedChanged(
            object sender,
            EventArgs e
        ) {
            // the custom times source was just selected as the new sunrise/sunset source
            if (
                this.SettingsWindow_CustomTimesRadioButton.Checked
             && this.p_Configuring_Settings_Window == false
            ) {
                this.SettingsWindow_ErrorMessageLabel.Text = string.Empty;

                this.SettingsWindow_LocationTextBox.Enabled = false;
                this.SettingsWindow_LocationValidationButton.Enabled = false;
                this.SettingsWindow_SunriseTimepicker.Enabled = true;
                this.SettingsWindow_SunsetTimepicker.Enabled = true;

                this.p_SettingsWindow_Coordinates = null;

                // populate & disable the location text box
                this.SettingsWindow_LocationTextBox.Text = string.Empty;

                this.SettingsWindow_SunriseTimepicker.CustomFormat = AppSettings.Time.Format.Hour_Minute_Meridiem;
                this.SettingsWindow_SunsetTimepicker.CustomFormat = AppSettings.Time.Format.Hour_Minute_Meridiem;
            }
        }

        private async void SettingsWindow_LocationValidationButton_Click(
            object sender,
            EventArgs e
        ) {
            if (string.IsNullOrWhiteSpace(this.SettingsWindow_LocationTextBox.Text) == false) {
                try {
                    this.Cursor = Cursors.AppStarting;

                    this.SettingsWindow_ErrorMessageLabel.Text = string.Empty;

                    // pull the coordinates from the custom location
                    this.p_SettingsWindow_Coordinates = await this.p_Helper.Location.GetCustomLocationCoordinatesAsync(this.SettingsWindow_LocationTextBox.Text);

                    await this.p_Helper.Log.LogAppAsync($"retrieved coordinates ({(this.p_SettingsWindow_Coordinates.HasValue ? $"{this.p_SettingsWindow_Coordinates.Value.Latitude}, {this.p_SettingsWindow_Coordinates.Value.Longitude}" : "null")}) from custom location '{this.SettingsWindow_LocationTextBox.Text}'");

                    // pull the sunrise / sunset times
                    var (
                        Sunrise,
                        Sunset
                    ) = await this.p_Helper.Location.GetSunriseAndSunsetAsync(this.p_SettingsWindow_Coordinates);

                    await this.p_Helper.Log.LogAppAsync($"retrieved sunrise '{Sunrise}' and sunset '{Sunset}'");

                    // populate & disable the sunrise/sunset date pickers
                    this.SettingsWindow_SunriseTimepicker.Value = Sunrise.GetDate();
                    this.SettingsWindow_SunsetTimepicker.Value = Sunset.GetDate();

                    this.SettingsWindow_SunriseTimepicker.CustomFormat = AppSettings.Time.Format.Hour_Minute_Meridiem;
                    this.SettingsWindow_SunsetTimepicker.CustomFormat = AppSettings.Time.Format.Hour_Minute_Meridiem;
                } catch (Exception ex) {
                    await this.p_Helper.Log.LogExceptionAsync(ex);

                    this.SettingsWindow_ErrorMessageLabel.Text = $"Error: the custom location '{this.SettingsWindow_LocationTextBox.Text}' could not be resolved. To continue, input and validate a different location.";
                } finally {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void SettingsWindow_CancelButton_Click(
            object sender,
            EventArgs e
        ) {
            this.Close();
        }

        private async void SettingsWindow_SaveButton_Click(
            object sender,
            EventArgs e
        ) {
            if (
                this.SettingsWindow_SunriseTimepicker.CustomFormat == AppSettings.Time.Format.Empty
             && this.SettingsWindow_SunsetTimepicker.CustomFormat == AppSettings.Time.Format.Empty
            ) {
                this.SettingsWindow_ErrorMessageLabel.Text = "Error: cannot save invalid settings. To continue, select a valid location source.";
            } else {
                try {
                    // update the active location details
                    this.p_Coordinates = this.p_SettingsWindow_Coordinates;
                    this.p_Location = this.SettingsWindow_LocationTextBox.Text;
                    this.p_Sunrise = this.SettingsWindow_SunriseTimepicker.Value.TimeOfDay;
                    this.p_Sunset = this.SettingsWindow_SunsetTimepicker.Value.TimeOfDay;

                    // update the in-memory user settings
                    if (this.SettingsWindow_AutoDetectRadioButton.Checked) {
                        this.p_Helper.User_Settings.LocationSource = UserLocationSource.Internet;
                    } else if (this.SettingsWindow_CustomLocationRadioButton.Checked) {
                        this.p_Helper.User_Settings.LocationSource = UserLocationSource.Custom;
                        this.p_Helper.User_Settings.CustomLocation = this.SettingsWindow_LocationTextBox.Text;
                    } else if (this.SettingsWindow_CustomTimesRadioButton.Checked) {
                        this.p_Helper.User_Settings.LocationSource = UserLocationSource.None;
                        this.p_Helper.User_Settings.CustomSunriseTime = this.SettingsWindow_SunriseTimepicker.Value.TimeOfDay.ToString();
                        this.p_Helper.User_Settings.CustomSunsetTime = this.SettingsWindow_SunsetTimepicker.Value.TimeOfDay.ToString();
                    }

                    // write the in-memory user settings to the configuration file
                    await this.p_Helper.User_Settings.SaveAsync();

                    // start waiting for the next significant time of day if the app isn't already waiting, otherwise cancel the current waiting event
                    // so the time until the next significant time of day can be recalculated and waited
                    if (this.p_Waiting_For_Time_Of_Day) {
                        // cancel the wait token to reset the countdown until sunrise/sunset/30 minutes after midnight
                        this.p_Wait_Cancellation_Token_Source.Cancel();
                    } else {
                        this.WaitUntilNextTimeOfDayAsync();
                    }

                    this.Close();
                } catch (Exception ex) {
                    await this.p_Helper.Log.LogExceptionAsync(ex);

                    this.SettingsWindow_ErrorMessageLabel.Text = "Error: could not save settings. Please try again.";
                }
            }
        }
        #endregion

        #region ContextMenuEvents
        private void ShowSettings(
            object _Sender,
            EventArgs _e
        ) {
            this.Visible = true;
            this.ShowInTaskbar = true;
        }

        private void ShowAboutWindow(
            object _Sender,
            EventArgs _e
        ) {
            var About_Window = new AboutWindow(
                _App_Name: $"Daylight v{AppSettings.App.Version}",
                _Theme: this.p_Theme
            );

            About_Window.Show();
        }

        private void ExitApplication(
            object _Sender,
            EventArgs _e
        ) {
            this.p_Helper.Dispose();
            this.Dispose();

            Application.Exit();
        }
        #endregion
    }
}