using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UltimatePong;

namespace UltimatePong
{
    public partial class Form1 : Form
    {
        public UltimatePong game;
        public bool pressed;

        public Form1(UltimatePong game)
        {
            InitializeComponent();

            pressed = false;
            this.game = game;
            PlayerAmountBox.SelectedIndex = 0;

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            game.setGameSettings(PlayerAmountBox.SelectedIndex + 2, Convert.ToInt32(LivesAmountBox.Value), PowerupsBox.Checked);
            pressed = true;
            Close();
        }
    }
}
