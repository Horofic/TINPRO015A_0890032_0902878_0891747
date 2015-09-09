using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UltimatePong
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D spriteTexture;
        Rectangle ball;
        Rectangle topBar;
        Rectangle bottomBar;

        int topBarSpeed;
        int bottomBarSpeed;
        int ballSpeed;

        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: Add your initialization logic here
            spriteTexture = Content.Load<Texture2D>("bar.png");

            base.Window.AllowUserResizing = false;

            ballSpeed = 400;
            topBarSpeed = 400;
            bottomBarSpeed = 400;

            //positions need some cleaning up
            ball = new Rectangle(256, 256, 16, 16);
            topBar = new Rectangle(0, 0, 64, 8);
            bottomBar = new Rectangle(0, 794, 64, 8);
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

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
