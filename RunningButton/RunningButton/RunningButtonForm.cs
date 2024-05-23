namespace RunningButton
{
    public class RunningButtonForm: Form
    {
        private Button button;
        Random random;
        public RunningButtonForm()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Text = "Running Button";
            this.button = new Button();
            this.Controls.Add(button);
            button.Text = "Press me";
            button.Location = new Point(450, 275);
            button.Size = new Size(100, 50);
            button.MouseEnter += new System.EventHandler(this.Mouse_Hover);
            button.Click += new System.EventHandler(this.Mouse_Click);
            random = new Random();
            this.MinimumSize = new Size(500, 500);
        }
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer? components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        /// <summary>
        /// Event that happens when user hovers on the button
        /// </summary>
        /// <param name="sender">parameter that contains a reference to the control that raised the event</param>
        /// <param name="e">parameter that contains the event data</param>
        private void Mouse_Hover(object sender, EventArgs e)
        {
            this.button.Location = new Point(random.Next(0, this.Width - 130), random.Next(0 , this.Height - 100));
        }

        /// <summary>
        /// Event that happens when user successfully clicks the button
        /// </summary>
        /// <param name="sender">parameter that contains a reference to the control that raised the event</param>
        /// <param name="e">parameter that contains the event data</param>
        private void Mouse_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>

    }
}
