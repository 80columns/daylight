using Daylight.settings;
using System.Windows.Forms;

namespace Daylight.window {
    public partial class AboutWindow : Form {
        private Theme p_Theme;

        public AboutWindow(
            string _App_Name,
            Theme _Theme
        ) {
            InitializeComponent();

            this.AboutWindow_AppNameLabel.Text = _App_Name;
            this.p_Theme = _Theme;

            SetAppTheme(this);
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

        private void AboutWindow_CloseButton_Click(
            object sender,
            System.EventArgs e
        ) {
            this.Close();
        }
    }
}