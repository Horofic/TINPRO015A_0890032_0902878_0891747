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
        UltimatePong game;
        public Form1(UltimatePong game)
        {
            this.game = game;
            InitializeComponent();
            comboBox1.SelectedIndex = 0;

        }


        private void button2_Click(object sender, EventArgs e)
        {
            StartButton.Enabled = false;
            // this.WindowState = FormWindowState.Minimized;
            
            game.Run();
            Close();
            //  using (var game = new UltimatePong())
            //  game.Run();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) //powerups
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //Players
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e) //lives
        {

        }

        private void label1_Click(object sender, EventArgs e) //lives TEXT
        {

        }

        private void Form1_Load(object sender, EventArgs e) //window
        {

        }
    }
}
