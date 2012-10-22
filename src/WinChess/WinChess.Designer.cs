namespace WinChess
{
    partial class WinChess
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinChess));
            this.btnStart = new System.Windows.Forms.Button();
            this.imlChess = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(231, 471);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // imlChess
            // 
            this.imlChess.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlChess.ImageStream")));
            this.imlChess.TransparentColor = System.Drawing.Color.Transparent;
            this.imlChess.Images.SetKeyName(0, "lightSquare.bmp");
            this.imlChess.Images.SetKeyName(1, "white_pawn.png");
            this.imlChess.Images.SetKeyName(2, "white_knight.png");
            this.imlChess.Images.SetKeyName(3, "white_bishop.png");
            this.imlChess.Images.SetKeyName(4, "white_rook.png");
            this.imlChess.Images.SetKeyName(5, "white_queen.png");
            this.imlChess.Images.SetKeyName(6, "white_king.png");
            this.imlChess.Images.SetKeyName(7, "black_pawn.png");
            this.imlChess.Images.SetKeyName(8, "black_knight.png");
            this.imlChess.Images.SetKeyName(9, "black_bishop.png");
            this.imlChess.Images.SetKeyName(10, "black_rook.png");
            this.imlChess.Images.SetKeyName(11, "black_queen.png");
            this.imlChess.Images.SetKeyName(12, "black_king.png");
            this.imlChess.Images.SetKeyName(13, "darkSquare.bmp");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WinChess.Properties.Resources.board;
            this.pictureBox1.Location = new System.Drawing.Point(50, 50);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(409, 404);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // WinChess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 497);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pictureBox1);
            this.Name = "WinChess";
            this.Text = "WinChess";
            this.Load += new System.EventHandler(this.WinChess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ImageList imlChess;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}