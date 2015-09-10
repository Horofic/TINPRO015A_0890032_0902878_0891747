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
            pressed = false;
            this.game = game;
            InitializeComponent();
            comboBox1.SelectedIndex = 0;

        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
               

                game.setGameSettings(comboBox1.SelectedIndex+2, Convert.ToInt32(lives.Value), checkBox1.Checked);
                pressed = true;
                Close();

            }
            catch (Exception)
            {
                System.Console.WriteLine("Invalid lives value!");
            }
           // StartButton.Enabled = false;
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //Players
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e) //lives
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) //powerups
        {

        }    

        private void label1_Click(object sender, EventArgs e) //lives TEXT
        {

        }

        private void Form1_Load(object sender, EventArgs e) //window
        {

        }

        private void lives_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
