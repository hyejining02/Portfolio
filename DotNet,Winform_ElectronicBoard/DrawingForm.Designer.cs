
namespace ELEA_BOARD
{
    partial class DrawingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawingForm));
            this.BGColorChange = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.BGColorChange)).BeginInit();
            this.SuspendLayout();
            // 
            // BGColorChange
            // 
            this.BGColorChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BGColorChange.BackColor = System.Drawing.Color.Transparent;
            this.BGColorChange.Image = global::ELEA_BOARD.Properties.Resources.board_color;
            this.BGColorChange.Location = new System.Drawing.Point(720, 12);
            this.BGColorChange.Name = "BGColorChange";
            this.BGColorChange.Size = new System.Drawing.Size(130, 30);
            this.BGColorChange.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BGColorChange.TabIndex = 0;
            this.BGColorChange.TabStop = false;
            this.BGColorChange.Click += new System.EventHandler(this.BGColorChange_Click);
            // 
            // DrawingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 485);
            this.Controls.Add(this.BGColorChange);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DrawingForm";
            this.Text = "    ";
            this.Load += new System.EventHandler(this.DrawingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BGColorChange)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox BGColorChange;
    }
}