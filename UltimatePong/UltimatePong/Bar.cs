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
        //changeble bar values
        public int barSpeed;
        public int barLength;
        public int barWidth = 8;
        String barType;
        //Controls of the bar(player)
        public Keys[] controls;
        int barSpeedMultiplier;
        //Start position bar
        int barXPos;
        int barYPos;
        Border[] borders;


        public Bar(Border[] borders,SpriteBatch spriteBatch,Texture2D barTexture, int barXPos, int barYPos,  Keys[] controls, String barType)
        {
            this.borders = borders;
               
            this.barTexture = barTexture;
            this.spriteBatch = spriteBatch;

            barSpeed = 400;
            barSpeedMultiplier = 0;

            this.barType = barType;

            this.controls = controls;

            this.barXPos = barXPos;
            this.barYPos = barYPos;
            barLength = 128;
            bar = new Rectangle(barXPos,barYPos, barLength, barWidth);
        }

        public void DrawBar()
        {
           
            spriteBatch.Draw(barTexture,bar,Color.White);

        }

        public void updateBar()
        {
            if (barType == "Standing")
            {
                bar.Width = barWidth;
                bar.Height = barLength;
            }
            else if(barType == "Lying")
            {
                bar.Width = barLength;
                bar.Height = barWidth;
            }
        }

        public void moveBar(GameTime gameTime)
        {
            var keyBoardState = Keyboard.GetState();


            if (keyBoardState.IsKeyDown(controls[2]))
                barSpeedMultiplier = 3;
            else
                barSpeedMultiplier = 0;

            if (barType == "Standing")
            {
                if (keyBoardState.IsKeyDown(controls[0]))
                    bar.Offset(0, (-barSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) - barSpeedMultiplier);

                if (keyBoardState.IsKeyDown(controls[1]))
                    bar.Offset(0, (barSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) + barSpeedMultiplier);
                if (bar.Intersects(borders[0].border))
                    bar.Y = 5;
                else if (bar.Intersects(borders[1].border))
                    bar.Y = 800-barLength;
            }
            else if (barType == "Lying")
            {
                if (keyBoardState.IsKeyDown(controls[0]))
                    bar.Offset((-barSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) - barSpeedMultiplier, 0);

                if (keyBoardState.IsKeyDown(controls[1]))
                    bar.Offset((barSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) + barSpeedMultiplier, 0);

                if (bar.Intersects(borders[2].border))
                    bar.X = 5;
                else if (bar.Intersects(borders[3].border))
                    bar.X = 800 - barLength;
            }
        }

    }
}
