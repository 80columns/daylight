namespace Daylight.window {
    partial class DaylightWindow {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }

                // dispose of custom components in the reverse order they were created
                if (this.p_Notification_Menu != null) {
                    this.p_Notification_Menu.Dispose();
                }

                if (this.p_Notification_Icon != null) {
                    this.p_Notification_Icon.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DaylightWindow));
            this.SettingsWindow_AutoDetectRadioButton = new System.Windows.Forms.RadioButton();
            this.SettingsWindow_CustomLocationRadioButton = new System.Windows.Forms.RadioButton();
            this.SettingsWindow_CustomTimesRadioButton = new System.Windows.Forms.RadioButton();
            this.SettingsWindow_SaveButton = new System.Windows.Forms.Button();
            this.SettingsWindow_CancelButton = new System.Windows.Forms.Button();
            this.SettingsWindow_LocationTextBox = new System.Windows.Forms.TextBox();
            this.SettingsWindow_SunriseTimepicker = new System.Windows.Forms.DateTimePicker();
            this.SettingsWindow_SunsetTimepicker = new System.Windows.Forms.DateTimePicker();
            this.SettingsWindow_SunriseLabel = new System.Windows.Forms.Label();
            this.SettingsWindow_SunsetLabel = new System.Windows.Forms.Label();
            this.SettingsWindow_LocationValidationButton = new System.Windows.Forms.Button();
            this.SettingsWindow_LocationLabel = new System.Windows.Forms.Label();
            this.SettingsWindow_LocationSourceContainer = new System.Windows.Forms.Panel();
            this.SettingsWindow_LocationSourceLabel = new System.Windows.Forms.Label();
            this.SettingsWindow_ErrorMessageLabel = new System.Windows.Forms.Label();
            this.SettingsWindow_LocationContainer = new System.Windows.Forms.Panel();
            this.SettingsWindow_SunriseSunsetContainer = new System.Windows.Forms.Panel();
            this.SettingsWindow_DateLabel = new System.Windows.Forms.Label();
            this.SettingsWindow_LocationSourceContainer.SuspendLayout();
            this.SettingsWindow_LocationContainer.SuspendLayout();
            this.SettingsWindow_SunriseSunsetContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // SettingsWindow_AutoDetectRadioButton
            // 
            this.SettingsWindow_AutoDetectRadioButton.AutoSize = true;
            this.SettingsWindow_AutoDetectRadioButton.Location = new System.Drawing.Point(6, 17);
            this.SettingsWindow_AutoDetectRadioButton.Name = "SettingsWindow_AutoDetectRadioButton";
            this.SettingsWindow_AutoDetectRadioButton.Size = new System.Drawing.Size(90, 19);
            this.SettingsWindow_AutoDetectRadioButton.TabIndex = 0;
            this.SettingsWindow_AutoDetectRadioButton.TabStop = true;
            this.SettingsWindow_AutoDetectRadioButton.Text = "Auto-Detect";
            this.SettingsWindow_AutoDetectRadioButton.UseVisualStyleBackColor = true;
            this.SettingsWindow_AutoDetectRadioButton.CheckedChanged += new System.EventHandler(this.SettingsWindow_AutoDetectButton_CheckedChanged);
            // 
            // SettingsWindow_CustomLocationRadioButton
            // 
            this.SettingsWindow_CustomLocationRadioButton.AutoSize = true;
            this.SettingsWindow_CustomLocationRadioButton.Location = new System.Drawing.Point(6, 42);
            this.SettingsWindow_CustomLocationRadioButton.Name = "SettingsWindow_CustomLocationRadioButton";
            this.SettingsWindow_CustomLocationRadioButton.Size = new System.Drawing.Size(116, 19);
            this.SettingsWindow_CustomLocationRadioButton.TabIndex = 1;
            this.SettingsWindow_CustomLocationRadioButton.TabStop = true;
            this.SettingsWindow_CustomLocationRadioButton.Text = "Custom Location";
            this.SettingsWindow_CustomLocationRadioButton.UseVisualStyleBackColor = true;
            this.SettingsWindow_CustomLocationRadioButton.CheckedChanged += new System.EventHandler(this.SettingsWindow_CustomLocationRadioButton_CheckedChanged);
            // 
            // SettingsWindow_CustomTimesRadioButton
            // 
            this.SettingsWindow_CustomTimesRadioButton.AutoSize = true;
            this.SettingsWindow_CustomTimesRadioButton.Location = new System.Drawing.Point(6, 67);
            this.SettingsWindow_CustomTimesRadioButton.Name = "SettingsWindow_CustomTimesRadioButton";
            this.SettingsWindow_CustomTimesRadioButton.Size = new System.Drawing.Size(101, 19);
            this.SettingsWindow_CustomTimesRadioButton.TabIndex = 2;
            this.SettingsWindow_CustomTimesRadioButton.TabStop = true;
            this.SettingsWindow_CustomTimesRadioButton.Text = "Custom Times";
            this.SettingsWindow_CustomTimesRadioButton.UseVisualStyleBackColor = true;
            this.SettingsWindow_CustomTimesRadioButton.CheckedChanged += new System.EventHandler(this.SettingsWindow_CustomTimesRadioButton_CheckedChanged);
            // 
            // SettingsWindow_SaveButton
            // 
            this.SettingsWindow_SaveButton.Location = new System.Drawing.Point(312, 311);
            this.SettingsWindow_SaveButton.Name = "SettingsWindow_SaveButton";
            this.SettingsWindow_SaveButton.Size = new System.Drawing.Size(75, 25);
            this.SettingsWindow_SaveButton.TabIndex = 8;
            this.SettingsWindow_SaveButton.Text = "Save";
            this.SettingsWindow_SaveButton.UseVisualStyleBackColor = true;
            this.SettingsWindow_SaveButton.Click += new System.EventHandler(this.SettingsWindow_SaveButton_Click);
            // 
            // SettingsWindow_CancelButton
            // 
            this.SettingsWindow_CancelButton.Location = new System.Drawing.Point(12, 311);
            this.SettingsWindow_CancelButton.Name = "SettingsWindow_CancelButton";
            this.SettingsWindow_CancelButton.Size = new System.Drawing.Size(75, 25);
            this.SettingsWindow_CancelButton.TabIndex = 7;
            this.SettingsWindow_CancelButton.Text = "Cancel";
            this.SettingsWindow_CancelButton.UseVisualStyleBackColor = true;
            this.SettingsWindow_CancelButton.Click += new System.EventHandler(this.SettingsWindow_CancelButton_Click);
            // 
            // SettingsWindow_LocationTextBox
            // 
            this.SettingsWindow_LocationTextBox.Location = new System.Drawing.Point(6, 17);
            this.SettingsWindow_LocationTextBox.Name = "SettingsWindow_LocationTextBox";
            this.SettingsWindow_LocationTextBox.Size = new System.Drawing.Size(283, 23);
            this.SettingsWindow_LocationTextBox.TabIndex = 3;
            // 
            // SettingsWindow_SunriseTimepicker
            // 
            this.SettingsWindow_SunriseTimepicker.CustomFormat = "h:mm tt";
            this.SettingsWindow_SunriseTimepicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.SettingsWindow_SunriseTimepicker.Location = new System.Drawing.Point(6, 32);
            this.SettingsWindow_SunriseTimepicker.Name = "SettingsWindow_SunriseTimepicker";
            this.SettingsWindow_SunriseTimepicker.ShowUpDown = true;
            this.SettingsWindow_SunriseTimepicker.Size = new System.Drawing.Size(177, 23);
            this.SettingsWindow_SunriseTimepicker.TabIndex = 5;
            this.SettingsWindow_SunriseTimepicker.Value = new System.DateTime(2021, 10, 6, 7, 0, 0, 0);
            // 
            // SettingsWindow_SunsetTimepicker
            // 
            this.SettingsWindow_SunsetTimepicker.CustomFormat = "h:mm tt";
            this.SettingsWindow_SunsetTimepicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.SettingsWindow_SunsetTimepicker.Location = new System.Drawing.Point(190, 32);
            this.SettingsWindow_SunsetTimepicker.Name = "SettingsWindow_SunsetTimepicker";
            this.SettingsWindow_SunsetTimepicker.ShowUpDown = true;
            this.SettingsWindow_SunsetTimepicker.Size = new System.Drawing.Size(177, 23);
            this.SettingsWindow_SunsetTimepicker.TabIndex = 6;
            this.SettingsWindow_SunsetTimepicker.Value = new System.DateTime(2021, 10, 6, 19, 0, 0, 0);
            // 
            // SettingsWindow_SunriseLabel
            // 
            this.SettingsWindow_SunriseLabel.AutoSize = true;
            this.SettingsWindow_SunriseLabel.Location = new System.Drawing.Point(6, 14);
            this.SettingsWindow_SunriseLabel.Name = "SettingsWindow_SunriseLabel";
            this.SettingsWindow_SunriseLabel.Size = new System.Drawing.Size(45, 15);
            this.SettingsWindow_SunriseLabel.TabIndex = 9;
            this.SettingsWindow_SunriseLabel.Text = "Sunrise";
            // 
            // SettingsWindow_SunsetLabel
            // 
            this.SettingsWindow_SunsetLabel.AutoSize = true;
            this.SettingsWindow_SunsetLabel.Location = new System.Drawing.Point(190, 14);
            this.SettingsWindow_SunsetLabel.Name = "SettingsWindow_SunsetLabel";
            this.SettingsWindow_SunsetLabel.Size = new System.Drawing.Size(42, 15);
            this.SettingsWindow_SunsetLabel.TabIndex = 10;
            this.SettingsWindow_SunsetLabel.Text = "Sunset";
            // 
            // SettingsWindow_LocationValidationButton
            // 
            this.SettingsWindow_LocationValidationButton.Location = new System.Drawing.Point(295, 16);
            this.SettingsWindow_LocationValidationButton.Name = "SettingsWindow_LocationValidationButton";
            this.SettingsWindow_LocationValidationButton.Size = new System.Drawing.Size(72, 25);
            this.SettingsWindow_LocationValidationButton.TabIndex = 4;
            this.SettingsWindow_LocationValidationButton.Text = "Validate";
            this.SettingsWindow_LocationValidationButton.UseVisualStyleBackColor = true;
            this.SettingsWindow_LocationValidationButton.Click += new System.EventHandler(this.SettingsWindow_LocationValidationButton_Click);
            // 
            // SettingsWindow_LocationLabel
            // 
            this.SettingsWindow_LocationLabel.AutoSize = true;
            this.SettingsWindow_LocationLabel.Location = new System.Drawing.Point(17, 121);
            this.SettingsWindow_LocationLabel.Name = "SettingsWindow_LocationLabel";
            this.SettingsWindow_LocationLabel.Size = new System.Drawing.Size(53, 15);
            this.SettingsWindow_LocationLabel.TabIndex = 12;
            this.SettingsWindow_LocationLabel.Text = "Location";
            // 
            // SettingsWindow_LocationSourceContainer
            // 
            this.SettingsWindow_LocationSourceContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SettingsWindow_LocationSourceContainer.Controls.Add(this.SettingsWindow_AutoDetectRadioButton);
            this.SettingsWindow_LocationSourceContainer.Controls.Add(this.SettingsWindow_CustomLocationRadioButton);
            this.SettingsWindow_LocationSourceContainer.Controls.Add(this.SettingsWindow_CustomTimesRadioButton);
            this.SettingsWindow_LocationSourceContainer.Location = new System.Drawing.Point(12, 18);
            this.SettingsWindow_LocationSourceContainer.Name = "SettingsWindow_LocationSourceContainer";
            this.SettingsWindow_LocationSourceContainer.Size = new System.Drawing.Size(375, 100);
            this.SettingsWindow_LocationSourceContainer.TabIndex = 13;
            // 
            // SettingsWindow_LocationSourceLabel
            // 
            this.SettingsWindow_LocationSourceLabel.AutoSize = true;
            this.SettingsWindow_LocationSourceLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.SettingsWindow_LocationSourceLabel.Location = new System.Drawing.Point(17, 9);
            this.SettingsWindow_LocationSourceLabel.Name = "SettingsWindow_LocationSourceLabel";
            this.SettingsWindow_LocationSourceLabel.Size = new System.Drawing.Size(92, 15);
            this.SettingsWindow_LocationSourceLabel.TabIndex = 14;
            this.SettingsWindow_LocationSourceLabel.Text = "Location Source";
            // 
            // SettingsWindow_ErrorMessageLabel
            // 
            this.SettingsWindow_ErrorMessageLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.SettingsWindow_ErrorMessageLabel.ForeColor = System.Drawing.Color.Black;
            this.SettingsWindow_ErrorMessageLabel.Location = new System.Drawing.Point(12, 268);
            this.SettingsWindow_ErrorMessageLabel.Name = "SettingsWindow_ErrorMessageLabel";
            this.SettingsWindow_ErrorMessageLabel.Size = new System.Drawing.Size(375, 40);
            this.SettingsWindow_ErrorMessageLabel.TabIndex = 15;
            // 
            // SettingsWindow_LocationContainer
            // 
            this.SettingsWindow_LocationContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SettingsWindow_LocationContainer.Controls.Add(this.SettingsWindow_LocationValidationButton);
            this.SettingsWindow_LocationContainer.Controls.Add(this.SettingsWindow_LocationTextBox);
            this.SettingsWindow_LocationContainer.Location = new System.Drawing.Point(12, 130);
            this.SettingsWindow_LocationContainer.Name = "SettingsWindow_LocationContainer";
            this.SettingsWindow_LocationContainer.Size = new System.Drawing.Size(375, 54);
            this.SettingsWindow_LocationContainer.TabIndex = 16;
            // 
            // SettingsWindow_SunriseSunsetContainer
            // 
            this.SettingsWindow_SunriseSunsetContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SettingsWindow_SunriseSunsetContainer.Controls.Add(this.SettingsWindow_SunriseLabel);
            this.SettingsWindow_SunriseSunsetContainer.Controls.Add(this.SettingsWindow_SunriseTimepicker);
            this.SettingsWindow_SunriseSunsetContainer.Controls.Add(this.SettingsWindow_SunsetTimepicker);
            this.SettingsWindow_SunriseSunsetContainer.Controls.Add(this.SettingsWindow_SunsetLabel);
            this.SettingsWindow_SunriseSunsetContainer.Location = new System.Drawing.Point(12, 196);
            this.SettingsWindow_SunriseSunsetContainer.Name = "SettingsWindow_SunriseSunsetContainer";
            this.SettingsWindow_SunriseSunsetContainer.Size = new System.Drawing.Size(375, 69);
            this.SettingsWindow_SunriseSunsetContainer.TabIndex = 17;
            // 
            // SettingsWindow_DateLabel
            // 
            this.SettingsWindow_DateLabel.AutoSize = true;
            this.SettingsWindow_DateLabel.Location = new System.Drawing.Point(17, 187);
            this.SettingsWindow_DateLabel.Name = "SettingsWindow_DateLabel";
            this.SettingsWindow_DateLabel.Size = new System.Drawing.Size(31, 15);
            this.SettingsWindow_DateLabel.TabIndex = 18;
            this.SettingsWindow_DateLabel.Text = "Date";
            // 
            // DaylightWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 348);
            this.Controls.Add(this.SettingsWindow_DateLabel);
            this.Controls.Add(this.SettingsWindow_LocationLabel);
            this.Controls.Add(this.SettingsWindow_LocationSourceLabel);
            this.Controls.Add(this.SettingsWindow_LocationSourceContainer);
            this.Controls.Add(this.SettingsWindow_CancelButton);
            this.Controls.Add(this.SettingsWindow_SaveButton);
            this.Controls.Add(this.SettingsWindow_LocationContainer);
            this.Controls.Add(this.SettingsWindow_ErrorMessageLabel);
            this.Controls.Add(this.SettingsWindow_SunriseSunsetContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DaylightWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Daylight";
            this.SettingsWindow_LocationSourceContainer.ResumeLayout(false);
            this.SettingsWindow_LocationSourceContainer.PerformLayout();
            this.SettingsWindow_LocationContainer.ResumeLayout(false);
            this.SettingsWindow_LocationContainer.PerformLayout();
            this.SettingsWindow_SunriseSunsetContainer.ResumeLayout(false);
            this.SettingsWindow_SunriseSunsetContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton SettingsWindow_AutoDetectRadioButton;
        private System.Windows.Forms.RadioButton SettingsWindow_CustomLocationRadioButton;
        private System.Windows.Forms.RadioButton SettingsWindow_CustomTimesRadioButton;
        private System.Windows.Forms.Button SettingsWindow_SaveButton;
        private System.Windows.Forms.Button SettingsWindow_CancelButton;
        private System.Windows.Forms.TextBox SettingsWindow_LocationTextBox;
        private System.Windows.Forms.DateTimePicker SettingsWindow_SunriseTimepicker;
        private System.Windows.Forms.DateTimePicker SettingsWindow_SunsetTimepicker;
        private System.Windows.Forms.Label SettingsWindow_SunriseLabel;
        private System.Windows.Forms.Label SettingsWindow_SunsetLabel;
        private System.Windows.Forms.Button SettingsWindow_LocationValidationButton;
        private System.Windows.Forms.Label SettingsWindow_LocationLabel;
        private System.Windows.Forms.Panel SettingsWindow_LocationSourceContainer;
        private System.Windows.Forms.Label SettingsWindow_LocationSourceLabel;
        private System.Windows.Forms.Label SettingsWindow_ErrorMessageLabel;
        private System.Windows.Forms.Panel SettingsWindow_LocationContainer;
        private System.Windows.Forms.Panel SettingsWindow_SunriseSunsetContainer;
        private System.Windows.Forms.Label SettingsWindow_DateLabel;
    }
}