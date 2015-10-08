using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace UltimatePong
{
    interface InputController
    {
        bool quit { get; }
        //float X { get; }
        //float Y { get; }
        Point TopBar { get; }
        Point BotBar { get; }
        Point LeftBar { get; }
        Point RightBar { get; }


        void Update(float gameTime);
    }

    class InputManagement : InputController
    {
        KeyboardState ks;

        private Keys[,] controls;

       Vector2 barMovement { get; set; }

        public InputManagement(Keys[,] controls)
        {
            this.controls = controls;
           // this.entity = entity;
        }

        public Point TopBar
        {
            get
            {
                Rectangle r = new Rectangle();
                r.Location = new Point(0, 0);
                int speed = 10;

               
                if (ks.IsKeyDown(Keys.W))//speed-up
                    speed = 20;
                else
                    speed = 10;
                if (ks.IsKeyDown(Keys.Q))//left
                    r.Location = r.Location + new Point(-speed, 0);
                if (ks.IsKeyDown(Keys.E))//right
                    r.Location = r.Location + new Point(speed, 0);
                return r.Location;
            }
        }
        public Point BotBar
        {
            get
            {
                Rectangle r = new Rectangle();
                r.Location = new Point(0, 0);
                int speed = 10;


                if (ks.IsKeyDown(Keys.S))//speed-up
                    speed = 20;
                else
                    speed = 10;
                if (ks.IsKeyDown(Keys.A))//left
                    r.Location = r.Location + new Point(-speed, 0);
                if (ks.IsKeyDown(Keys.D))//right
                    r.Location = r.Location + new Point(speed, 0);
                return r.Location;
            }
        }
        public Point LeftBar
        {
            get
            {
                Rectangle r = new Rectangle();
                r.Location = new Point(0, 0);
                int speed = 10;


                if (ks.IsKeyDown(Keys.S))//speed-up
                    speed = 20;
                else
                    speed = 10;
                if (ks.IsKeyDown(Keys.A))//left
                    r.Location = r.Location + new Point(0,-speed);
                if (ks.IsKeyDown(Keys.D))//right
                    r.Location = r.Location + new Point(0,speed);
                return r.Location;
            }
        }
        public Point RightBar
        {
            get
            {
                Rectangle r = new Rectangle();
                r.Location = new Point(0, 0);
                int speed = 10;


                if (ks.IsKeyDown(Keys.K))//speed-up
                    speed = 20;
                else
                    speed = 10;
                if (ks.IsKeyDown(Keys.L))//left
                    r.Location = r.Location + new Point(0, -speed);
                if (ks.IsKeyDown(Keys.J))//right
                    r.Location = r.Location + new Point(0, speed);
                return r.Location;
            }
        }

        public bool quit
        {
            get
            {
                if (ks.IsKeyDown(Keys.Escape))//speed-up
                    return true;
                else
                    return false;
            }
        }

        public void Update(float gameTime)
        {
            ks = Keyboard.GetState();
        }
    }
}
