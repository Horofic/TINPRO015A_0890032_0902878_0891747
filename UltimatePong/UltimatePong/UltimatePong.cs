using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace UltimatePong
{
    public class UltimatePong : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random = new Random(DateTime.Now.Millisecond+ DateTime.Now.Second);

        //sprites
        Texture2D spriteTexture;

        Texture2D barTexture;
        Texture2D borderTexture;

        SoundEffect bleepHigh;
        SoundEffect bleepLow;

        List<Ball> balls;

        //playing field properties
        const int fieldSize = 800;
        const int barToBorderDist = 16;
        const int borderWidth = 5;

        //ball properties
        float ballSpeed;
        float ballSpeedLimit;
        float ballSpeedInc;
        float ballXVelocity;
        float ballYVelocity;
        int ballSize;
        int ballStartPos;
        bool collision;
        float multiplier;
        int ballCenter;
        float maxOffset;
        float offset;
        int lastSpawnedDirection;
        bool pause;
        //pause
        int pauseTimer;
        int gameTime;

        //default bar properties
        const int barLength = 128;
        const int barWidth = 8;
        const float barSpeed = 400;
        const float bounceCorrection = 0.6f;

        //top bar properties
        Keys[] topBarKeys = new Keys[3];

        //bottom bar properties
        Keys[] bottomBarKeys = new Keys[3];

        //left bar properties
        Keys[] leftBarKeys = new Keys[3];

        //right bar properties
        Keys[] rightBarKeys = new Keys[3];

        //Player lives
        int[] playerLives;

        //Game Settings
        public int players;
        public int lives;
        public bool powerups;
        public bool classicBounce;
        private bool firstCycle;


        Powerup[] powerup;
        int powerupCount;

        SpriteFont font;

        //test
        //Border border;
        Border[] borders;
        //playerBars
        Bar[] playerBars;
        int lastHitBar;

        public UltimatePong(int playerAmount, int livesAmount, bool powerups, bool bounceType)
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = fieldSize;
            graphics.PreferredBackBufferHeight = fieldSize;
            Content.RootDirectory = "Content";

            this.players = playerAmount;
            this.lives = livesAmount;
            this.powerups = powerups;
            this.classicBounce = bounceType;

            //Printlines
            System.Console.WriteLine("players:" + players);
            System.Console.WriteLine("lives:" + lives);
            System.Console.WriteLine("powerups:" + powerups);
            System.Console.WriteLine("bounceType:" + bounceType);
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bleepHigh = Content.Load<SoundEffect>("bleep-high");
            bleepLow = Content.Load<SoundEffect>("bleep-low");
            //Ball texture
            spriteTexture = Content.Load<Texture2D>("ball1.png");
            //Border textures
            borderTexture = Content.Load<Texture2D>("bar.png");
            //Player Textures
            barTexture = Content.Load<Texture2D>("ball1.png");
            //Font texture
            font = Content.Load<SpriteFont>("font");

            base.Window.AllowUserResizing = false;

            //initialize bar controls
            topBarKeys[0] = Keys.T; //LEFT
            topBarKeys[1] = Keys.U; //RIGHT
            topBarKeys[2] = Keys.Y; //BOOST

            bottomBarKeys[0] = Keys.V; //LEFT
            bottomBarKeys[1] = Keys.N; //RIGHT
            bottomBarKeys[2] = Keys.B; //BOOST

            leftBarKeys[0] = Keys.A; //UP
            leftBarKeys[1] = Keys.D; //DOWN
            leftBarKeys[2] = Keys.S; //BOOST

            rightBarKeys[0] = Keys.L; //UP
            rightBarKeys[1] = Keys.J; //DOWN
            rightBarKeys[2] = Keys.K; //BOOST

            //initialize ball
            balls.Insert(0, new Ball(fieldSize, ballSize, ballSpeed, ballSpeedLimit, ballSpeedInc, bounceCorrection, false));
            firstCycle = true;

            //initialize bars
            //barKeys = new Keys[][]{  topBarKeys, bottomBarKeys, leftBarKeys, rightBarKeys }; //put all the inputs in 1 array

            int barStartPos = (fieldSize - barLength) / 2;

            //initialize player lives
            playerLives = new int[4] {lives,lives,lives,lives}; 

            //powerup
            powerup = new Powerup[3];
            powerupCount = 0;
            for(int i=0;i<powerup.Length;i++)
            powerup[i] = new Powerup(spriteBatch, spriteTexture, GraphicsDevice,i);

            //initialize players
            playerBars = new Bar[4];
            playerBars[0] = new Bar(spriteBatch, barTexture, barStartPos, barToBorderDist, topBarKeys, "Lying");//Topp bar
            playerBars[1] = new Bar(spriteBatch, barTexture, barStartPos, fieldSize - barToBorderDist - barWidth, bottomBarKeys, "Lying");//Bottom bar
            playerBars[2] = new Bar(spriteBatch, barTexture, barToBorderDist, barStartPos, leftBarKeys ,"Standing");//Left bar
            playerBars[3] = new Bar(spriteBatch, barTexture, fieldSize - barToBorderDist - barWidth, barStartPos, rightBarKeys ,"Standing");//Right bar

            //Inititialize borders
            borders = new Border[4];
            borders[0] = new Border(spriteBatch, borderTexture, 0, 0, "Lying");//Top border
            borders[1] = new Border(spriteBatch, borderTexture, 0, (fieldSize - borderWidth), "Lying");//Bottom border
            borders[2] = new Border(spriteBatch, borderTexture, 0, 0, "Standing");//Left border
            borders[3] = new Border(spriteBatch, borderTexture, (fieldSize - borderWidth), 0, "Standing");//Right border




            gameTime = 0;
            setPlayersAmount();

            base.Initialize();

        }
        
        private void setPlayersAmount()
        {
            switch(players)
            {
                case 2:
                    borders[0].borderTexture = Content.Load<Texture2D>("deadBar.png");
                    borders[1].borderTexture = Content.Load<Texture2D>("deadBar.png");
                    playerBars[0].bar.Offset(-800, -800);
                    playerBars[1].bar.Offset(-800, -800);
                    playerLives[0] = 0;
                    playerLives[1] = 0;
                    break;
                case 3:
                    borders[1].borderTexture = Content.Load<Texture2D>("deadBar.png");
                    playerBars[0].bar.Offset(-800, -800);
                    playerLives[0] = 0;
                    break;
                default:
                    break;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (firstCycle)
            {
                balls[0].spawnBall(0, ballSpeed, gameTime);
                firstCycle = false;
            }
            
            //bleepLow.Play();

            checkInput(gameTime);
            // Collision detection and ball movement
            foreach (Ball ball in balls)
            {
                int playerLostALife = ball.updateBall(gameTime, playerBars, borders, playerLives);//needs to be fixed and the returned int must be processed.
            }
                
            //move bars
            foreach (Bar bar in playerBars)
                bar.updateBar();
            //borders
            foreach (Border border in borders)
                border.updateBorder();

            //Power-ups
            if(powerups)
            powerupEvents(gameTime);
            if(pause)
                pauseBall(gameTime);
            base.Update(gameTime);
        }

        public void powerupEvents(GameTime gameTime)
        {
            if (powerupCount > 2)
                powerupCount = 0;

            powerup[powerupCount].startTimer(gameTime);
            for (int i = 0; i < playerBars.Length; i++)
                if (ball.Intersects(playerBars[i].bar))
                {
                    lastHitBar = i;
                    break;
                }
            int[] vel = new int[2];
            powerup[powerupCount].checkCollision(ref ball, ref playerBars, lastHitBar,ref playerLives,ref ballXVelocity,ref ballYVelocity);
            powerupCount++;
        }
        
        private void checkInput(GameTime gameTime)
        {
            var keyBoardstate = Keyboard.GetState();
            foreach (Bar bar in playerBars)
                bar.moveBar(gameTime);
            
            if (keyBoardstate.IsKeyDown(Keys.Escape))
                Exit();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.TransparentBlack);
            
            spriteBatch.Begin();
            //balls
            foreach (Ball ball in balls)
                ball.drawBall(spriteBatch, spriteTexture);
                //bars
                foreach (Bar bar in playerBars)
                    bar.DrawBar();
                //borders
                foreach (Border border in borders)
                    border.DrawBorder();
                //font
                if (players==4)
                spriteBatch.DrawString(font, playerLives[0].ToString(),new Vector2(390, 50), Color.White);
                if(players!=2)
                spriteBatch.DrawString(font, playerLives[1].ToString(), new Vector2(390, 710), Color.White);
                spriteBatch.DrawString(font, playerLives[2].ToString(), new Vector2(70, 383), Color.White);
                spriteBatch.DrawString(font, playerLives[3].ToString(), new Vector2(700, 383), Color.White);
                //powerup
                foreach(Powerup powerup in powerup)
                powerup.drawPowerup();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        

        protected void ResetBall(Rectangle player)
        {
            if(player == playerBars[0].bar)
            {
                playerLives[0] -= 1;
                lastSpawnedDirection = 0;
                if (playerLives[0] == 0)
                {
                    borders[0].borderTexture = Content.Load<Texture2D>("deadBar.png");
                    playerBars[0].bar.Offset(-800, -800);
                }
            }

           else if (player == playerBars[1].bar)
            {
                playerLives[1] -= 1;
                lastSpawnedDirection = 1;
                if (playerLives[1] == 0)
                {
                    borders[1].borderTexture = Content.Load<Texture2D>("deadBar.png");
                    playerBars[1].bar.Offset(-800, -800);
                }
            }

            else if (player == playerBars[2].bar)
            {
                playerLives[2] -= 1;
                lastSpawnedDirection = 2;
                if (playerLives[2] == 0)
                {
                    borders[2].borderTexture = Content.Load<Texture2D>("deadBar.png");
                    playerBars[2].bar.Offset(-800, -800);
                }
            }

            else if (player == playerBars[3].bar)
            {
                playerLives[3] -= 1;
                lastSpawnedDirection = 3;
                if (playerLives[3] == 0)
                {
                    borders[3].borderTexture = Content.Load<Texture2D>("deadBar.png");
                    playerBars[3].bar.Offset(-800, -800);
                }
            }
            // ballSpeed = 500.0f;
           // lastHitBar = -1;
            ball.Offset((-ball.X + ballStartPos), (-ball.Y + ballStartPos));
            pause = true;
        }
      
        public void pauseBall(GameTime gameTime)
        {
            ballXVelocity = ballYVelocity = ballSpeed = 0;
            if (this.gameTime < (int)gameTime.TotalGameTime.TotalSeconds)
            {
                this.gameTime = (int)gameTime.TotalGameTime.TotalSeconds;
                pauseTimer++;
                switch(pauseTimer)
                {
                    case 1:
                        foreach (Powerup powerup in powerup)
                            powerup.disabled(true);
                        break;
                    case 3:
                        ballXVelocity = ballYVelocity = ballSpeed = 500;
                        pauseTimer = 0;
                        pause = false;
                        
                        spawnBallDirection();
                        
                        break;
                    default:
                        break;
                }
            }
        }

        private void spawnBallDirection()
        {
            int[] playersAlive = new int[4];
            int chosenPlayer = 0;
            int death = 0;

            for (int i = 0; i < playerLives.Length; i++)
            {
                if (playerLives[i] > 0)
                {
                    playersAlive[i] = i;
                }
                else
                {
                    playersAlive[i] = -1;
                    death++;
                }
            }

            foreach(int p in playersAlive)
            Console.WriteLine(p);
            int hussle1;
            int hussle2;
            int temp;

            for(int i=0;i<3;i++)
            {
                hussle1 = random.Next(0, 4);
                hussle2 = random.Next(0, 4);
                temp = playersAlive[hussle1];
                playersAlive[hussle1] = playersAlive[hussle2];
                playersAlive[hussle2] = temp;
            }
            
            foreach(int player in playersAlive)
            {
                if (player != -1 && player != lastSpawnedDirection)
                {
                    chosenPlayer = player;
                    break;
                }
            }

            if (death==3)
                chosenPlayer = 4;

            switch (chosenPlayer)
            {
                case 0:
                    lastSpawnedDirection = 0;
                    ballXVelocity = 0;
                    ballYVelocity = -ballSpeed;
                    break;
                case 1:
                    lastSpawnedDirection = 1;
                    ballXVelocity = 0;
                    ballYVelocity = ballSpeed;
                    break;
                case 2:
                    lastSpawnedDirection = 2;
                    ballXVelocity = -ballSpeed;
                    ballYVelocity = 0;
                    break;
                case 3:
                    lastSpawnedDirection = 3;
                    ballXVelocity = ballSpeed;
                    ballYVelocity = 0;
                    break;
                case 4:
                    ballXVelocity = ballYVelocity = ballSpeed = 0;
                    foreach (Powerup powerup in powerup)
                        powerup.disabled(true);
                    break;
                default:
                    break;
            }
            if(chosenPlayer!=4)
                foreach (Powerup powerup in powerup)
                    powerup.disabled(false);
        }
    }
}
