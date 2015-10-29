namespace UltimatePong
{
    partial class Launcher
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
            this.StartButton = new System.Windows.Forms.Button();
            this.PowerupsBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LivesAmountBox = new System.Windows.Forms.NumericUpDown();
            this.PlayerAmountText = new System.Windows.Forms.Label();
            this.LivesAmountText = new System.Windows.Forms.Label();
            this.PlayerAmountBox = new System.Windows.Forms.ComboBox();
            this.powerupCount = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LivesAmountBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerupCount)).BeginInit();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(79, 171);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(121, 65);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "Start game";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // PowerupsBox
            // 
            this.PowerupsBox.AutoSize = true;
            this.PowerupsBox.Checked = true;
            this.PowerupsBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PowerupsBox.Location = new System.Drawing.Point(18, 73);
            this.PowerupsBox.Name = "PowerupsBox";
            this.PowerupsBox.Size = new System.Drawing.Size(73, 17);
            this.PowerupsBox.TabIndex = 2;
            this.PowerupsBox.Text = "Powerups";
            this.PowerupsBox.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.powerupCount);
            this.panel1.Controls.Add(this.LivesAmountBox);
            this.panel1.Controls.Add(this.PlayerAmountText);
            this.panel1.Controls.Add(this.LivesAmountText);
            this.panel1.Controls.Add(this.PlayerAmountBox);
            this.panel1.Controls.Add(this.PowerupsBox);
            this.panel1.Location = new System.Drawing.Point(28, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(226, 144);
            this.panel1.TabIndex = 3;
            // 
            // LivesAmountBox
            // 
            this.LivesAmountBox.Location = new System.Drawing.Point(103, 47);
            this.LivesAmountBox.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.LivesAmountBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LivesAmountBox.Name = "LivesAmountBox";
            this.LivesAmountBox.Size = new System.Drawing.Size(100, 20);
            this.LivesAmountBox.TabIndex = 1;
            this.LivesAmountBox.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // PlayerAmountText
            // 
            this.PlayerAmountText.AutoSize = true;
            this.PlayerAmountText.Location = new System.Drawing.Point(15, 19);
            this.PlayerAmountText.Name = "PlayerAmountText";
            this.PlayerAmountText.Size = new System.Drawing.Size(80, 13);
            this.PlayerAmountText.TabIndex = 8;
            this.PlayerAmountText.Text = "Players Amount";
            // 
            // LivesAmountText
            // 
            this.LivesAmountText.AutoSize = true;
            this.LivesAmountText.Location = new System.Drawing.Point(15, 49);
            this.LivesAmountText.Name = "LivesAmountText";
            this.LivesAmountText.Size = new System.Drawing.Size(71, 13);
            this.LivesAmountText.TabIndex = 7;
            this.LivesAmountText.Text = "Lives Amount";
            // 
            // PlayerAmountBox
            // 
            this.PlayerAmountBox.FormattingEnabled = true;
            this.PlayerAmountBox.Items.AddRange(new object[] {
            "2 Players",
            "3 Players",
            "4 Players"});
            this.PlayerAmountBox.Location = new System.Drawing.Point(103, 17);
            this.PlayerAmountBox.Name = "PlayerAmountBox";
            this.PlayerAmountBox.Size = new System.Drawing.Size(100, 21);
            this.PlayerAmountBox.TabIndex = 5;
            // 
            // powerupCount
            // 
            this.powerupCount.Location = new System.Drawing.Point(103, 73);
            this.powerupCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.powerupCount.Name = "powerupCount";
            this.powerupCount.Size = new System.Drawing.Size(100, 20);
            this.powerupCount.TabIndex = 9;
            this.powerupCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.StartButton);
            this.Name = "Launcher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ultimate Pong Launcher";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LivesAmountBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerupCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.CheckBox PowerupsBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox PlayerAmountBox;
        private System.Windows.Forms.Label LivesAmountText;
        private System.Windows.Forms.Label PlayerAmountText;
        private System.Windows.Forms.NumericUpDown LivesAmountBox;
        private System.Windows.Forms.NumericUpDown powerupCount;
    }
}