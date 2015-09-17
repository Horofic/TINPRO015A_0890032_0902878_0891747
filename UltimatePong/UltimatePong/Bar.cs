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
        Rectangle bar;
        Texture2D barTexture;

        public int barSpeed;
        public int barLenght;
        public int barWidth;

        //Start position bar
        public int barXPos;
        public int barYPos;

        int barStartPos;


        public Bar(SpriteBatch spriteBatch,Texture2D barTexture, int barXPos)
        {
            this.barTexture = barTexture;
            this.spriteBatch = spriteBatch;

            this.barXPos = barXPos;
            //this.barYPos = barYPos;
            
            bar = new Rectangle(barStartPos,16,barLenght,barWidth);
        }








        public void DrawBar()
        { 
            spriteBatch.Draw(barTexture,bar,Color.White);
        }



    }
}
