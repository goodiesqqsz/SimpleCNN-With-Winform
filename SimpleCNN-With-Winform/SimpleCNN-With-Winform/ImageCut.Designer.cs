namespace SimpleCNN_With_Winform
{
    partial class ImageCut
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
            this.SuspendLayout();
            // 
            // ImageCut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ImageCut";
            this.Text = "ImageCut";
            this.Load += new System.EventHandler(this.ImageCut_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ImageCut_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ImageCut_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImageCut_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImageCut_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImageCut_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}