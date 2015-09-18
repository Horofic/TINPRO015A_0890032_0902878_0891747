using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UltimatePong
{
    class Border : Game
    {

        public Rectangle border;
        SpriteBatch spriteBatch;
        public Texture2D borderTexture;

        int borderXPos;
        int borderYPos;

        int borderWidth  = 5;
        int borderLength = 800;

        String borderType;

        



        public Border(SpriteBatch spriteBatch, Texture2D borderTexture, int borderXPos, int borderYPos,String borderType)
        {
            this.spriteBatch = spriteBatch;
            this.borderTexture = borderTexture;
            this.borderXPos = borderXPos;
            this.borderYPos = borderYPos;

            this.borderType = borderType;

            border = new Rectangle(borderXPos,borderYPos,borderWidth,borderLength);
        }



        public void DrawBorder()
        {

            spriteBatch.Draw(borderTexture, border, Color.White);

        }

        public void updateBorder()
        {

            if (borderType == "Standing")
            {
                border.Width = borderWidth;
                border.Height = borderLength;
            }
            else if (borderType == "Lying")
            {
                border.Width = borderLength;
                border.Height = borderWidth;
            }
        }

    }
}
