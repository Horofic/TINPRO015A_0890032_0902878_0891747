using Microsoft.Xna.Framework;
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
        Vector2 bar2Position;
        Texture2D ball;
        Texture2D bar;
        Texture2D bar2;

        float xpos;
        float ypos;

        float xbarpos;
        float xbar2pos;
        int barspeed;
        int ballspeed;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            bar = Content.Load<Texture2D>("bar.png");
            bar2 = Content.Load<Texture2D>("bar.png");
            ball = Content.Load<Texture2D>("ball1.png");

            ballspeed = 10;
            barspeed = 10;
            xpos = (GraphicsDevice.Viewport.Bounds.Width - ball.Width) / 2;
            ypos = (GraphicsDevice.Viewport.Bounds.Height - ball.Height) / 2;

            xbarpos = (GraphicsDevice.Viewport.Bounds.Width - bar.Width) / 2;

           // barPosition = new Vector2((GraphicsDevice.Viewport.Bounds.Width - bar.Width) / 2 , 0.0f);



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
            var prevxBarPos = xbarpos;
            var prevxBar2Pos = xbar2pos;
            var prevxBallPos = xpos;
            var prevyBallPos = ypos;

            if (keyBoardstate.IsKeyDown(Keys.Escape))
                Exit();


            //ball controls
            if (keyBoardstate.IsKeyDown(Keys.W))
                ypos = ypos - ballspeed;
            if (keyBoardstate.IsKeyDown(Keys.S))
                ypos = ypos + ballspeed;
            if (keyBoardstate.IsKeyDown(Keys.A))
                xpos = xpos - ballspeed;
            if (keyBoardstate.IsKeyDown(Keys.D))
                xpos = xpos + ballspeed;

            //bar controls
            if (keyBoardstate.IsKeyDown(Keys.Left))
                xbarpos = xbarpos - barspeed;
            if (keyBoardstate.IsKeyDown(Keys.Right))
                xbarpos = xbarpos + barspeed;
            //bar2 controls
            if (keyBoardstate.IsKeyDown(Keys.End))
                xbar2pos = xbar2pos - barspeed;
            if (keyBoardstate.IsKeyDown(Keys.PageDown))
                xbar2pos = xbar2pos + barspeed;


            //bar boundries
            /* if (xbarpos <= 0)
                 xbarpos = 0;
             if (xbarpos > GraphicsDevice.Viewport.Bounds.Width - bar.Width)
                 xbarpos = GraphicsDevice.Viewport.Bounds.Width - bar.Width;*/
            if (xbarpos <= 0 || xbarpos > GraphicsDevice.Viewport.Bounds.Width - bar.Width)
                xbarpos = prevxBarPos;

            /* if (xbar2pos <= 0)
                 xbar2pos = 0;
             if (xbar2pos > GraphicsDevice.Viewport.Bounds.Width - bar2.Width)
                 xbar2pos = GraphicsDevice.Viewport.Bounds.Width - bar2.Width;*/
            if (xbar2pos <= 0 || xbar2pos > GraphicsDevice.Viewport.Bounds.Width - bar2.Width)
                xbar2pos = prevxBar2Pos;

            //ball boundries
            /* if (xpos <= 0)
                 xpos = 0;
             if (xpos >= GraphicsDevice.Viewport.Bounds.Width -ball.Width)
                 xpos = GraphicsDevice.Viewport.Bounds.Width - ball.Width;
             if (ypos < 0)
                 ypos = 0;
             if (ypos > GraphicsDevice.Viewport.Bounds.Height-ball.Height)
                 ypos = GraphicsDevice.Viewport.Bounds.Height-ball.Height;*/
            if (xpos <= 0 || xpos >= GraphicsDevice.Viewport.Bounds.Width - ball.Width || ypos < 0 || ypos > GraphicsDevice.Viewport.Bounds.Height - ball.Height)
            {
                xpos = prevxBallPos;
                ypos = prevyBallPos;
            }



            ballPosition = new Vector2(xpos, ypos);
            barPosition = new Vector2(xbarpos, 0.0f);
            bar2Position = new Vector2(xbar2pos, GraphicsDevice.Viewport.Bounds.Height - bar.Height);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.TransparentBlack);
            
            spriteBatch.Begin();
            spriteBatch.Draw(bar, barPosition, Color.White);
            spriteBatch.Draw(bar2, bar2Position, Color.White);
            spriteBatch.Draw(ball, ballPosition, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
