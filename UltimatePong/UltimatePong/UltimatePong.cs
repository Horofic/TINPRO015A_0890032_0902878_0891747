using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;

namespace UltimatePong
{

    enum BallMovementInstructionResult
    {
        Running,
        RunningAndPowerupHit,
        OutOfBounds,
        DoneAndPlayer0LostALife,
        DoneAndPlayer1LostALife,
        DoneAndPlayer2LostALife,
        DoneAndPlayer3LostALife
    }
    enum PowerupResponse
    {
        done,
        addBall,
        changeBallDirection
    }
    enum InstructionResult
    {
        Done,
        DoneAndCreatePowerup,
        Running,
        RunningAndCreatePowerup
    }

    public class UltimatePong : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        static Random random = new Random(DateTime.Now.Millisecond+ DateTime.Now.Second);
        SpriteFont fontPlayerLives;
        SpriteFont fontVictoryScreen;

        //sprites
        Texture2D spriteTexture;
        Texture2D barTexture;
        Texture2D borderTexture;

        //Sounds
        SoundEffect bleepHigh;
        SoundEffect bleepLow;

        //LISTS
        List<Entity> borders;
        List<BallController> balls;
        List<Entity> playerBars;
        List<PowerupController> powerupControllers;
        
        //playing field properties
        const int fieldSize = 800;
        const int barToBorderDist = 16;
        const int borderWidth = 5;

        //ball properties
        const float ballSpeed = 500.0f;
        const float ballSpeedLimit = 80000.0f;
        const float ballSpeedInc = 20.0f;
        const int ballSize = 16;
        const float bounceCorrection = 0.6f;
        int lastSpawnedDirection;

        //default bar properties
        const int barLength = 128;
        const int barWidth = 8;
        const float barSpeed = 400;

        //Player lives
        int[] playerLives;

        //Game Settings
        public int players;
        public int lives;
        public bool powerupEnabled;
        private bool firstCycle;
        private bool gameDone;
        private int maxPowerupCount;

        //input
        InputManagement input;

        //color array
        Color[] colorArray = {Color.White,Color.TransparentBlack,Color.Green,Color.Red,Color.Goldenrod };

        //Victory screen values
        int[] selectionLocation = {130, 500, 250, 600};
        int selection = 0;
        double selectionTimer = 0;
        public bool restart = false;

        //gameLogic for powerup
        Instruction gameLogic =
            new Wait(() => 5) +
            new Repeat(
                new CreatePowerup() +
                new Wait(() => 3));

        public UltimatePong(int playerAmount, int livesAmount, bool powerup, int maxPowerupCount)
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = fieldSize;
            graphics.PreferredBackBufferHeight = fieldSize;
            Content.RootDirectory = "Content";

            this.players = playerAmount;
            this.lives = livesAmount;
            this.powerupEnabled = powerup;
            this.gameDone = false;
            this.maxPowerupCount = maxPowerupCount;

            //Printlines
            System.Console.WriteLine("players:" + players);
            System.Console.WriteLine("lives:" + lives);
            System.Console.WriteLine("powerups:" + powerupEnabled);
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
            fontPlayerLives = Content.Load<SpriteFont>("font");
            fontVictoryScreen = Content.Load<SpriteFont>("fontVictoryScreen");

            base.Window.AllowUserResizing = false;

            //initialize ball
            balls = new List<BallController>();
            balls.Insert(0, new BallController(fieldSize, ballSize, ballSpeed, ballSpeedLimit, ballSpeedInc, bounceCorrection, spriteBatch, spriteTexture, bleepHigh, bleepLow));
            firstCycle = true;

            //bar start pos
            int barStartPos = (fieldSize - barLength) / 2;
            Point topBarPos = new Point(barStartPos, barToBorderDist);
            Point botBarPos = new Point(barStartPos, fieldSize - barToBorderDist - barWidth);
            Point leftBarPos = new Point(barToBorderDist, barStartPos);
            Point rightBarPos = new Point(fieldSize - barToBorderDist - barWidth, barStartPos);

            //Initialize Bar list
            playerBars = new List<Entity>();
            playerBars.Insert(0, new Entity(barTexture, new Rectangle(), 128, 8, topBarPos)); //Top Bar
            playerBars.Insert(1, new Entity(barTexture, new Rectangle(), 128, 8, botBarPos)); //Bot Bar
            playerBars.Insert(2, new Entity(barTexture, new Rectangle(), 8, 128, leftBarPos)); //Left Bar
            playerBars.Insert(3, new Entity(barTexture, new Rectangle(), 8, 128, rightBarPos)); //Right Bar

            //initialize player lives
            playerLives = new int[4] {lives,lives,lives,lives};

            //Initialize powerups
            powerupControllers = new List<PowerupController>();

            //Inititialize borders
            borders = new List<Entity>();
            borders.Insert(0,new Entity(borderTexture,  new Rectangle(), fieldSize, borderWidth, new Point(0, 0))); //Top Border
            borders.Insert(1, new Entity(borderTexture, new Rectangle(), fieldSize, borderWidth, new Point(0, fieldSize - borderWidth))); //Bot Border
            borders.Insert(2, new Entity(borderTexture, new Rectangle(), borderWidth, fieldSize, new Point(0, 0))); //Left Border
            borders.Insert(3, new Entity(borderTexture, new Rectangle(), borderWidth, fieldSize, new Point(fieldSize - borderWidth, 0))); //Right Border

            //Initialize input
            input = new InputManagement();

            Console.WriteLine("initialize");
            base.Initialize();
        }
        
        private void setPlayersAmount()
        {
            List<Entity> temp = playerBars;
            switch(players)
            {
                case 2:
                    playerLives[0] = 0;
                    playerLives[1] = 0;
                    temp.Insert(0,playerBars[0].CreateNewPos(new Point(-100,-100)));
                    temp.Insert(1, playerBars[1].CreateNewPos(new Point(-100, -100)));
                    temp.RemoveAt(2);
                    temp.RemoveAt(2);
                    break;
                case 3:
                    playerLives[0] = 0;
                    temp.Insert(0, playerBars[0].CreateNewPos(new Point(-100, -100)));
                    temp.RemoveAt(1);
                    break;
                default:
                    break;
            }
            playerBars = temp;
        }

        protected void firstSpawnBall(float deltaTime)
        {
            int direction = 0;
            switch(players)
            {
                case 2:
                    direction = random.Next(2, 4);
                    break;
                case 3:
                    direction = random.Next(1, 4);
                    break;
                case 4:
                    direction = random.Next(0, 4);
                    break;
            }
            lastSpawnedDirection = direction;
            balls[0].spawnBall(direction, deltaTime);
            firstCycle = false;
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            double elapsedTime = gameTime.TotalGameTime.TotalMilliseconds;

            List<PowerupController> updatedPowerupControllers = powerupControllers;
            List<Entity> updatedPlayerBars = new List<Entity>();
            List<BallController> updatedBalls = new List<BallController>();

            if (firstCycle)
            {
                setPlayersAmount();
                firstSpawnBall(deltaTime);
            }
    
            input.Update(deltaTime);
            if (input.exit)
            {
                base.Exit();
            }

           

            //BAR ENTITIES to be moved
            for(int i=0;i<4;i++)
            {
                Entity tempOldBar = playerBars[i]; //temp bar to save previous pos
                Entity tempNewBar = tempOldBar.CreateMoved(input.moveBar(i));
                if(i < 2)
                {
                    if (tempNewBar.rectangle.Intersects(borders[2].rectangle))
                    {
                        if (tempNewBar.rectangle.Left > tempOldBar.rectangle.Left)
                            updatedPlayerBars.Insert(i, tempNewBar);
                        else
                            updatedPlayerBars.Insert(i, tempOldBar);
                    }
                    else if(tempNewBar.rectangle.Intersects(borders[3].rectangle))
                    {
                        if (tempNewBar.rectangle.Right < tempOldBar.rectangle.Right)
                            updatedPlayerBars.Insert(i, tempNewBar);
                        else
                            updatedPlayerBars.Insert(i, tempOldBar);
                    }
                    else
                        updatedPlayerBars.Insert(i, tempNewBar);
                }
                else
                {
                    if (tempNewBar.rectangle.Intersects(borders[0].rectangle))
                    {
                        if (tempNewBar.rectangle.Top > tempOldBar.rectangle.Top)
                            updatedPlayerBars.Insert(i, tempNewBar);
                        else
                            updatedPlayerBars.Insert(i, tempOldBar);
                    }
                    else if (tempNewBar.rectangle.Intersects(borders[1].rectangle))
                    {
                        if (tempNewBar.rectangle.Bottom < tempOldBar.rectangle.Bottom)
                            updatedPlayerBars.Insert(i, tempNewBar);
                        else
                            updatedPlayerBars.Insert(i, tempOldBar);
                    }
                    else
                        updatedPlayerBars.Insert(i, tempNewBar);
                }
            }

            //creation of a new list of balls

            foreach (BallController ball in balls)
            {
                switch (ball.updateBall(deltaTime, elapsedTime, playerBars, borders, powerupControllers, playerLives))
                {
                    case BallMovementInstructionResult.Running:
                        updatedBalls.Add(ball);
                        break;
                    case BallMovementInstructionResult.DoneAndPlayer0LostALife:
                        playerLives[0] -= 1;
                        break;
                    case BallMovementInstructionResult.DoneAndPlayer1LostALife:
                        playerLives[1] -= 1;
                        break;
                    case BallMovementInstructionResult.DoneAndPlayer2LostALife:
                        playerLives[2] -= 1;
                        break;
                    case BallMovementInstructionResult.DoneAndPlayer3LostALife:
                        playerLives[3] -= 1;
                        break;
                    case BallMovementInstructionResult.RunningAndPowerupHit:

                        if (ball.lastHitPlayer != -1)
                        {
                            switch (ball.hitPowerup.powerupEvent(ball.lastHitPlayer, ref updatedPlayerBars, ref playerLives))
                            {
                                case PowerupResponse.addBall:
                                    BallController tmpBallController = new BallController(fieldSize, ballSize, ballSpeed, ballSpeedLimit, ballSpeedInc, bounceCorrection, spriteBatch, spriteTexture, bleepHigh, bleepLow);
                                    tmpBallController.spawnBall(spawnBallDirection(), elapsedTime);
                                    updatedBalls.Add(tmpBallController);
                                    break;
                                case PowerupResponse.changeBallDirection:
                                    ball.simpleBounce(random.Next(0, 3));
                                    break;
                                default:
                                    break;
                            }
                        }
                        
                        updatedBalls.Add(ball);
                        updatedPowerupControllers.Remove(ball.hitPowerup);
                        break;


                    case BallMovementInstructionResult.OutOfBounds:

                    default:
                        break;
                }
            }
            //create powerup
            switch (gameLogic.Execute(deltaTime))
            {
                case InstructionResult.DoneAndCreatePowerup:
                case InstructionResult.RunningAndCreatePowerup:
                    if (updatedPowerupControllers.Count < maxPowerupCount)
                    {
                        switch (random.Next(0,5))
                        {
                            case 0:
                                updatedPowerupControllers.Add(new GreenPowerupController(barTexture, spriteBatch));
                                break;
                            case 1:
                                updatedPowerupControllers.Add(new RedPowerupController(barTexture, spriteBatch));
                                break;
                            case 2:
                                updatedPowerupControllers.Add(new GoldPowerupController(barTexture, spriteBatch));
                                break;
                            case 3:
                                updatedPowerupControllers.Add(new PinkPowerupController(barTexture, spriteBatch));
                                break;
                            case 4:
                                updatedPowerupControllers.Add(new BluePowerupController(barTexture, spriteBatch));
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        updatedPowerupControllers.RemoveAt(0);
                    }
                    break;
            }

            //check the amount of players still alive
            int playersAlive = 0;
            for (int i = 0; i < 4; i++)
            {
                if (playerLives[i] > 0)
                    playersAlive++;
                else             //move player to void if dead

                {
                    updatedPlayerBars.Insert(i, playerBars[i].CreateNewPos(new Point(900, 900)));
                    updatedPlayerBars.RemoveAt(i + 1);
                }
            }

            if (playersAlive < 2)
            {
                updatedBalls.Clear();
                powerupControllers.Clear();
                gameDone = true;
            }
            else if (updatedBalls.Count < 1)
            {
                updatedBalls.Insert(0, new BallController(fieldSize, ballSize, ballSpeed, ballSpeedLimit, ballSpeedInc, bounceCorrection, spriteBatch, spriteTexture,bleepHigh,bleepLow));
                updatedBalls[0].spawnBall(spawnBallDirection(), elapsedTime);
            }
          

            //inputs for victory screen selection
            if (gameDone)
            {
                if (input.selection && gameTime.TotalGameTime.TotalMilliseconds - selectionTimer > 200)
                {
                    selectionTimer = gameTime.TotalGameTime.TotalMilliseconds;
                    selection += 2;
                    if (selection > 2)
                        selection = 0;
                }
                if(input.confirmation)
                {
                    if(selection == 0)
                    {
                        restart = true;
                        base.Exit();
                    }
                    else 
                        base.Exit();
                }
            }

            //update Entities
            balls = updatedBalls;
            playerBars = updatedPlayerBars;
            powerupControllers = updatedPowerupControllers;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.TransparentBlack);
            spriteBatch.Begin();
            //balls
            foreach (BallController ball in balls)
                ball.drawBall();
            //font
            if (players==4)
                spriteBatch.DrawString(fontPlayerLives, playerLives[0].ToString(),new Vector2(390, 50), Color.White);
            if(players!=2)
                spriteBatch.DrawString(fontPlayerLives, playerLives[1].ToString(), new Vector2(390, 710), Color.White);
            spriteBatch.DrawString(fontPlayerLives, playerLives[2].ToString(), new Vector2(70, 383), Color.White);
            spriteBatch.DrawString(fontPlayerLives, playerLives[3].ToString(), new Vector2(700, 383), Color.White);
            //entities border
            for(int i=0;i<borders.Count;i++)
            {
                Color color;
                if (playerLives[i] != 0)
                    color = Color.TransparentBlack;
                else
                    color = Color.White;
                spriteBatch.Draw(barTexture, borders[i].rectangle, color);
            }
            //entities bar
            foreach (Entity bar in playerBars)
                spriteBatch.Draw(barTexture, bar.rectangle, Color.White);
            //powerups
            foreach (PowerupController powerup in powerupControllers)
                powerup.Draw();

            if(gameDone)
            {
                spriteBatch.DrawString(fontVictoryScreen, "Winner!", new Vector2(240, 200), Color.White);
                String[] winningPlayer = { "Player 4", "Player 3", "Player 1", "Player 2" };
                int win;
                for (win = 0; win < playerLives.Length; win++)
                    if (playerLives[win] > 0)
                        break;
                spriteBatch.DrawString(fontVictoryScreen, winningPlayer[win], new Vector2(218, 300), Color.White);
                spriteBatch.DrawString(fontVictoryScreen, "Play Again", new Vector2(183, 500), Color.White);
                spriteBatch.DrawString(fontVictoryScreen, "Quit", new Vector2(303, 600), Color.White);
                spriteBatch.DrawString(fontVictoryScreen, ">", new Vector2(selectionLocation[selection], selectionLocation[selection + 1]), Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private int spawnBallDirection()
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

            lastSpawnedDirection = chosenPlayer;
            return chosenPlayer;
        }
    }
}
