
namespace Daylight.window
{
    partial class AboutWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutWindow));
            this.AboutWindow_AppNameLabel = new System.Windows.Forms.Label();
            this.AboutWindow_AppPurposeLabel = new System.Windows.Forms.Label();
            this.AboutWindow_CloseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AboutWindow_AppNameLabel
            // 
            this.AboutWindow_AppNameLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AboutWindow_AppNameLabel.Location = new System.Drawing.Point(12, 9);
            this.AboutWindow_AppNameLabel.Name = "AboutWindow_AppNameLabel";
            this.AboutWindow_AppNameLabel.Size = new System.Drawing.Size(360, 43);
            this.AboutWindow_AppNameLabel.TabIndex = 0;
            this.AboutWindow_AppNameLabel.Text = "AppName";
            this.AboutWindow_AppNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AboutWindow_AppPurposeLabel
            // 
            this.AboutWindow_AppPurposeLabel.Location = new System.Drawing.Point(12, 52);
            this.AboutWindow_AppPurposeLabel.Name = "AboutWindow_AppPurposeLabel";
            this.AboutWindow_AppPurposeLabel.Size = new System.Drawing.Size(360, 45);
            this.AboutWindow_AppPurposeLabel.TabIndex = 1;
            this.AboutWindow_AppPurposeLabel.Text = "Daylight changes your system theme to light at sunrise && to dark at sunset";
            this.AboutWindow_AppPurposeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AboutWindow_CloseButton
            // 
            this.AboutWindow_CloseButton.Location = new System.Drawing.Point(155, 126);
            this.AboutWindow_CloseButton.Name = "AboutWindow_CloseButton";
            this.AboutWindow_CloseButton.Size = new System.Drawing.Size(75, 23);
            this.AboutWindow_CloseButton.TabIndex = 2;
            this.AboutWindow_CloseButton.Text = "Close";
            this.AboutWindow_CloseButton.UseVisualStyleBackColor = true;
            this.AboutWindow_CloseButton.Click += new System.EventHandler(this.AboutWindow_CloseButton_Click);
            // 
            // AboutWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.AboutWindow_CloseButton);
            this.Controls.Add(this.AboutWindow_AppPurposeLabel);
            this.Controls.Add(this.AboutWindow_AppNameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label AboutWindow_AppNameLabel;
        private System.Windows.Forms.Label AboutWindow_AppPurposeLabel;
        private System.Windows.Forms.Button AboutWindow_CloseButton;
    }
}