using System;
using System.Windows.Forms;

namespace Daylight.context {
    // https://www.mking.net/blog/setting-a-winforms-form-to-be-hidden-on-startup
    public class DefaultHiddenApplicationContext : ApplicationContext {
        /// <summary>
        /// The main application form.
        /// </summary>
        private Form _mainForm;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomApplicationContext"/> class.
        /// </summary>
        /// <param name="mainForm">The main form of the application.</param>
        public DefaultHiddenApplicationContext(
            Form mainForm
        ) {
            _mainForm = mainForm;

            if (_mainForm != null) {
                // Wire up the destroy events similar to how the base ApplicationContext
                // does things when a form is provided.
                _mainForm.HandleDestroyed += OnFormDestroy;

                // We still want to call Show() here, but we can at least hide it from the user
                // by setting Opacity to 0 while the form is being shown for the first time.
                _mainForm.Opacity = 0;
                _mainForm.Show();
                _mainForm.Hide();
                _mainForm.Opacity = 1;
            }
        }

        /// <summary>
        /// Handles the <see cref="Control.HandleDestroyed"/> event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        private void OnFormDestroy(
            object sender,
            EventArgs e
        ) {
            if (
                sender is Form form
             && form.RecreatingHandle == false
            ) {
                form.HandleDestroyed -= OnFormDestroy;
                OnMainFormClosed(sender, e);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        /// true if invoked from the <see cref="IDisposable.Dispose"/> method;
        /// false if invoked from the finalizer.
        /// </param>
        protected override void Dispose(
            bool disposing
        ) {
            if (
                disposing
             && _mainForm != null
            ) {
                if (_mainForm.IsDisposed == false) {
                    _mainForm.Dispose();
                }

                _mainForm = null;
            }

            base.Dispose(disposing);
        }
    }
}