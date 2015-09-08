﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UltimatePong
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 ballPosition;
        Vector2 barPosition;
        Texture2D ball;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ballPosition = new Vector2(400.0f, 300.0f);
            ball = Content.Load<Texture2D>("ball1.png");


            ballPosition = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2);
            barPosition = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, 0.0f);

  

            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


        }


        protected override void UnloadContent()
        {
          
        }

        protected override void Update(GameTime gameTime)
        {

            var keyBoardstate = Keyboard.GetState();


            if (keyBoardstate.IsKeyDown(Keys.Escape))
                Exit();


            //ball controlls
            if (keyBoardstate.IsKeyDown(Keys.W))





            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                ypos = ypos - 10;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                ypos = ypos + 10;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                xpos = xpos - 10;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                xpos = xpos + 10;

            ballPosition = new Vector2(xpos, ypos);



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.TransparentBlack);
            
            spriteBatch.Begin();
            spriteBatch.Draw(bar, barPosition, Color.White);
            spriteBatch.Draw(ball, ballPosition, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
