namespace MancalaGUI
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.play = new System.Windows.Forms.Button();
            this.instructions = new System.Windows.Forms.Button();
            this.quit = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // play
            // 
            this.play.BackColor = System.Drawing.Color.Transparent;
            this.play.BackgroundImage = global::MancalaGUI.Properties.Resources.TwoPlayerButton;
            this.play.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.play.FlatAppearance.BorderSize = 0;
            this.play.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.play.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.play.Location = new System.Drawing.Point(62, 114);
            this.play.Name = "play";
            this.play.Size = new System.Drawing.Size(202, 44);
            this.play.TabIndex = 0;
            this.play.UseVisualStyleBackColor = false;
            this.play.Click += new System.EventHandler(this.play_Click);
            // 
            // instructions
            // 
            this.instructions.BackColor = System.Drawing.Color.Transparent;
            this.instructions.BackgroundImage = global::MancalaGUI.Properties.Resources.InstructionsButton;
            this.instructions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.instructions.FlatAppearance.BorderSize = 0;
            this.instructions.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.instructions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.instructions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.instructions.Location = new System.Drawing.Point(62, 183);
            this.instructions.Name = "instructions";
            this.instructions.Size = new System.Drawing.Size(202, 44);
            this.instructions.TabIndex = 1;
            this.instructions.UseVisualStyleBackColor = false;
            this.instructions.Click += new System.EventHandler(this.instructions_Click);
            // 
            // quit
            // 
            this.quit.BackColor = System.Drawing.Color.Transparent;
            this.quit.BackgroundImage = global::MancalaGUI.Properties.Resources.quitMenuButton;
            this.quit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.quit.FlatAppearance.BorderSize = 0;
            this.quit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.quit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.quit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.quit.Location = new System.Drawing.Point(62, 255);
            this.quit.Name = "quit";
            this.quit.Size = new System.Drawing.Size(202, 44);
            this.quit.TabIndex = 2;
            this.quit.UseVisualStyleBackColor = false;
            this.quit.Click += new System.EventHandler(this.quit_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "rockBlue.png");
            this.imageList1.Images.SetKeyName(1, "rockGreen.png");
            this.imageList1.Images.SetKeyName(2, "rockPurple.png");
            this.imageList1.Images.SetKeyName(3, "rockRed.png");
            this.imageList1.Images.SetKeyName(4, "rockYellow.png");
            this.imageList1.Images.SetKeyName(5, "rockGreen.png");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MancalaGUI.Properties.Resources.MenuBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(321, 408);
            this.Controls.Add(this.quit);
            this.Controls.Add(this.instructions);
            this.Controls.Add(this.play);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mancala";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button play;
        private System.Windows.Forms.Button instructions;
        private System.Windows.Forms.Button quit;
        private System.Windows.Forms.ImageList imageList1;
    }
}

