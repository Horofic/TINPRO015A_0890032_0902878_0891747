﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UltimatePong
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        //sprites
        Texture2D spriteTexture;
        Rectangle ball;
        Rectangle topBar;
        Rectangle bottomBar;
        Rectangle leftBar;
        Rectangle rightBar;

        Rectangle topBorder;
        Rectangle bottomBorder;
        Rectangle leftBorder;
        Rectangle rightBorder;

        
        

        //playing field properties
        const int fieldSize = 800;
        const int barToBorderDist = 16;
        const int borderWeight = 4;

        //ball properties
        float ballSpeed;
        int ballSize;

        //default bar properties
        const int barLength = 128;
        const int barWeight = 8;
        const float barSpeed = 400;


        //top bar properties
        float topBarSpeed;
        int topBarLength;


        //bottom bar properties
        float bottomBarSpeed;
        int bottomBarLength;


        //left bar properties
        float leftBarSpeed;
        int leftBarLength;


        //right bar properties
        float rightBarSpeed;
        int rightBarLength;

             
        

        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = fieldSize;
            graphics.PreferredBackBufferHeight = fieldSize;
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteTexture = Content.Load<Texture2D>("bar.png");

            base.Window.AllowUserResizing = false;



            //initialize bar properties
            topBarSpeed = barSpeed;
            bottomBarSpeed = barSpeed;
            leftBarSpeed = barSpeed;
            rightBarSpeed = barSpeed;

            topBarLength = barLength;
            bottomBarLength = barLength;
            leftBarLength = barLength;
            rightBarSpeed = barLength;


            //initialize ball
            ballSize = 16;
            ballSpeed = 400;

            int ballStartPos = (fieldSize - ballSize) / 2;
            ball = new Rectangle(ballStartPos, ballStartPos, ballSize, ballSize);


            //initialize bars
            int barStartPos = (fieldSize - barLength) / 2;
            topBar = new Rectangle(barStartPos, barToBorderDist, barLength, barWeight);
            bottomBar = new Rectangle(barStartPos, fieldSize-barToBorderDist-barWeight, barLength, barWeight);
            leftBar = new Rectangle(barToBorderDist, barStartPos, barWeight, barLength);
            rightBar = new Rectangle(fieldSize - barToBorderDist - barWeight, barStartPos, barWeight, barLength);




            base.Initialize();
        }


  
        protected override void Update(GameTime gameTime)
        {

            var keyBoardstate = Keyboard.GetState();
                      

            if (keyBoardstate.IsKeyDown(Keys.Escape))
                Exit();


            //ball controls, just for testing
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                ball.Offset(0, -ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Keyboard.GetState().IsKeyDown(Keys.S))
                ball.Offset(0, ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                ball.Offset(-ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                ball.Offset(ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);


            //Top bar controls
            if (keyBoardstate.IsKeyDown(Keys.Z))
                topBar.Offset(-topBarSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);

            if (keyBoardstate.IsKeyDown(Keys.X))
                topBar.Offset(topBarSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);



            //Bottom bar controls
            if (keyBoardstate.IsKeyDown(Keys.Left))
                bottomBar.Offset(-bottomBarSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);

            if (keyBoardstate.IsKeyDown(Keys.Right))
                bottomBar.Offset(bottomBarSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);

            if(ball.Intersects(topBar))
            {
                System.Console.WriteLine(ball.X);
                ball.Offset((-ball.X+394), (-ball.Y+394));
                topBar.Width = topBar.Width / 2;

            }
            /*
            //bar boundries

            if (topBarXPos <= 0 || topBarXPos > GraphicsDevice.Viewport.Bounds.Width - topBar.Width)
                topBarXPos = prev_topBarXPos;

    
            if (bottomBarXPos <= 0 || bottomBarXPos > GraphicsDevice.Viewport.Bounds.Width - bottomBar.Width)
                bottomBarXPos = prev_bottomBarXPos;

            //ball boundries
          
            if (ballXPos <= 0 || ballXPos >= GraphicsDevice.Viewport.Bounds.Width - ball.Width || ballYPos < 0 || ballYPos > GraphicsDevice.Viewport.Bounds.Height - ball.Height)
            {
                ballXPos = prev_ballXPos;
                ballYPos = prev_ballYPos;
            }


            ballPosition = new Vector2(ballXPos, ballYPos);
            topBarPosition = new Vector2(topBarXPos, 0.0f);
            bottomBarPosition = new Vector2(bottomBarXPos, GraphicsDevice.Viewport.Bounds.Height - bottomBar.Height);

    */
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.TransparentBlack);
            
            spriteBatch.Begin();
            spriteBatch.Draw(spriteTexture, ball, Color.White);
            spriteBatch.Draw(spriteTexture, topBar, Color.White);
            spriteBatch.Draw(spriteTexture, bottomBar, Color.White);
            spriteBatch.Draw(spriteTexture, leftBar, Color.White);
            spriteBatch.Draw(spriteTexture, rightBar, Color.White);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
