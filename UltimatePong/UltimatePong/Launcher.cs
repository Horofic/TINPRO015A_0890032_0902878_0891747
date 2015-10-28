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
    public partial class Launcher : Form
    {
        public bool StartPressed;

        public int playerAmount;
        public int livesAmount;
        public bool powerups;

        public Launcher(int playerAmount, int livesAmount, bool powerups)
        {
            InitializeComponent();
            StartPressed = false;

            PlayerAmountBox.SelectedIndex = playerAmount - 2;
            LivesAmountBox.Value = livesAmount;
            PowerupsBox.Checked = powerups;
        }
        public Launcher()
        {
            InitializeComponent();
            StartPressed = false;
            PlayerAmountBox.SelectedIndex = 0;
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            playerAmount = PlayerAmountBox.SelectedIndex + 2;
            livesAmount = Convert.ToInt32(LivesAmountBox.Value);
            powerups = PowerupsBox.Checked;

            StartPressed = true;
            Close();
        }

    
    }
}
