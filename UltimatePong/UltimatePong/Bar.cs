using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UltimatePong;


namespace UltimatePong
{
    class Bar : Game 
    {
        SpriteBatch spriteBatch;

        //Bar properties
        public Rectangle bar;
        public Texture2D barTexture;

        public int barSpeed = 400;
        public int barLength;
        public int barWidth;

        //Start position bar
        int barXPos;
        int barYPos;



        public Bar(SpriteBatch spriteBatch,Texture2D barTexture, int barXPos, int barYPos, int barLength, int barWidth)
        {
            
            this.barTexture = barTexture;
            this.spriteBatch = spriteBatch;
            this.barLength = barLength;
            this.barWidth = barWidth;

            this.barXPos = barXPos;
            this.barYPos = barYPos;
            
            bar = new Rectangle(barXPos,barYPos, barLength, barWidth);
        }








        public void DrawBar()
        {
            spriteBatch.Draw(barTexture,bar,Color.White);
        }



    }
}
