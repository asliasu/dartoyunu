namespace dart_oyunu
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            topPanel = new Panel();
            shotLabel = new Label();
            scoreLabel = new Label();
            archerPictureBox = new PictureBox();
            boardPanel = new Panel();
            newGameButton = new Button();
            shootTimer = new System.Windows.Forms.Timer(components);
            balloonTimer = new System.Windows.Forms.Timer(components);
            topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)archerPictureBox).BeginInit();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.FromArgb(33, 33, 33);
            topPanel.Controls.Add(shotLabel);
            topPanel.Controls.Add(scoreLabel);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(900, 70);
            topPanel.TabIndex = 0;
            // 
            // shotLabel
            // 
            shotLabel.AutoSize = true;
            shotLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            shotLabel.ForeColor = Color.White;
            shotLabel.Location = new Point(260, 20);
            shotLabel.Name = "shotLabel";
            shotLabel.Size = new Size(114, 25);
            shotLabel.TabIndex = 1;
            shotLabel.Text = "Atış: 0/10";
            // 
            // scoreLabel
            // 
            scoreLabel.AutoSize = true;
            scoreLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            scoreLabel.ForeColor = Color.White;
            scoreLabel.Location = new Point(24, 20);
            scoreLabel.Name = "scoreLabel";
            scoreLabel.Size = new Size(92, 25);
            scoreLabel.TabIndex = 0;
            scoreLabel.Text = "Puan: 0";
            // 
            // archerPictureBox
            // 
            archerPictureBox.BackColor = Color.FromArgb(230, 230, 230);
            archerPictureBox.Location = new Point(24, 104);
            archerPictureBox.Name = "archerPictureBox";
            archerPictureBox.Size = new Size(230, 420);
            archerPictureBox.TabIndex = 1;
            archerPictureBox.TabStop = false;
            // 
            // boardPanel
            // 
            boardPanel.BackColor = Color.FromArgb(245, 245, 245);
            boardPanel.BorderStyle = BorderStyle.FixedSingle;
            boardPanel.Location = new Point(450, 140);
            boardPanel.Name = "boardPanel";
            boardPanel.Size = new Size(400, 400);
            boardPanel.TabIndex = 2;
            boardPanel.Paint += boardPanel_Paint;
            boardPanel.MouseClick += boardPanel_MouseClick;
            // 
            // newGameButton
            // 
            newGameButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            newGameButton.Location = new Point(24, 610);
            newGameButton.Name = "newGameButton";
            newGameButton.Size = new Size(160, 40);
            newGameButton.TabIndex = 3;
            newGameButton.Text = "Yeni Oyun";
            newGameButton.UseVisualStyleBackColor = true;
            newGameButton.Click += newGameButton_Click;
            // 
            // shootTimer
            // 
            shootTimer.Interval = 500;
            shootTimer.Tick += shootTimer_Tick;
            // 
            // balloonTimer
            // 
            balloonTimer.Interval = 45;
            balloonTimer.Tick += balloonTimer_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(900, 700);
            Controls.Add(newGameButton);
            Controls.Add(boardPanel);
            Controls.Add(archerPictureBox);
            Controls.Add(topPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dart Oyunu";
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)archerPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel topPanel;
        private Label shotLabel;
        private Label scoreLabel;
        private PictureBox archerPictureBox;
        private Panel boardPanel;
        private Button newGameButton;
        private System.Windows.Forms.Timer shootTimer;
        private System.Windows.Forms.Timer balloonTimer;
    }
}
