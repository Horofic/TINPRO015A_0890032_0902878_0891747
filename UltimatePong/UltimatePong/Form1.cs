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
      //  UltimatePong game;
        public Form1()
        {
            //this.game = game;
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            StartButton.Enabled = false;
            this.WindowState = FormWindowState.Minimized;

            using (var game = new UltimatePong())
            game.Run();
            Close();
        }
    }
}
