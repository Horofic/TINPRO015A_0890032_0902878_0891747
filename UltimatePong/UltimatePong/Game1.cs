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
        Texture2D ball;
        Texture2D bar;

        float xpos;
        float ypos;

        float xbarpos;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            bar = Content.Load<Texture2D>("bar.png");
            ball = Content.Load<Texture2D>("ball1.png");

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



            if (keyBoardstate.IsKeyDown(Keys.Escape))
                Exit();


            //ball controlls
            // TODO: Add your update logic here
            if (keyBoardstate.IsKeyDown(Keys.W))
                ypos = ypos - 10;
            if (keyBoardstate.IsKeyDown(Keys.S))
                ypos = ypos + 10;
            if (keyBoardstate.IsKeyDown(Keys.A))
                xpos = xpos - 10;
            if (keyBoardstate.IsKeyDown(Keys.D))
                xpos = xpos + 10;
            if (keyBoardstate.IsKeyDown(Keys.Left))
                xbarpos = xbarpos - 10;

            if (keyBoardstate.IsKeyDown(Keys.Right))
                xbarpos = xbarpos + 10;

            if (xbarpos <= 0)
                xbarpos = 0;
            if (xbarpos > GraphicsDevice.Viewport.Bounds.Width - bar.Width)
                xbarpos = GraphicsDevice.Viewport.Bounds.Width - bar.Width;

            if (xpos <= 0)
                xpos = 0;
            if (xpos >= GraphicsDevice.Viewport.Bounds.Width -ball.Width)
                xpos = GraphicsDevice.Viewport.Bounds.Width - ball.Width;

            if (ypos < 0)
                ypos = 0;
            if (ypos > GraphicsDevice.Viewport.Bounds.Height-ball.Height)
                ypos = GraphicsDevice.Viewport.Bounds.Height-ball.Height;


            ballPosition = new Vector2(xpos, ypos);
            barPosition = new Vector2(xbarpos, 0.0f);
            //bar controlls

            

            



            




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
