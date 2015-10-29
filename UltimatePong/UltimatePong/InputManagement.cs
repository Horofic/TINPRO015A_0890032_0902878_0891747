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
        bool exit { get; }
        bool selection { get; }
        bool confirmation { get; }
        Point moveBar(int bar);
        void Update(float gameTime);
    }

    class InputManagement : InputController
    {
        KeyboardState ks;
        private Keys[,] controls = new Keys[4, 3];

        Vector2 barMovement { get; set; }
        int normalSpeed = 8;
        int boostSpeed = 12;

        public InputManagement()
        {
            //this.controls = controls;
            controls[0, 0] = Keys.T; //LEFT
            controls[0, 1] = Keys.U; //RIGHT
            controls[0, 2] = Keys.Y; //BOOST

            //BotBar controls
            controls[1, 0] = Keys.V; //LEFT
            controls[1, 1] = Keys.N; //RIGHT
            controls[1, 2] = Keys.B; //BOOST

            //LeftBar controls
            controls[2, 0] = Keys.A; //UP
            controls[2, 1] = Keys.D; //DOWN
            controls[2, 2] = Keys.S; //BOOST

            //RightBar controls
            controls[3, 0] = Keys.L; //UP
            controls[3, 1] = Keys.J; //DOWN
            controls[3, 2] = Keys.K; //BOOST
        }

        //returns delta location
        //barType 0 = Top/Bot , 1 = Left/Right
        public Point moveBar(int bar)
        {
            int barType = bar;
            if (barType < 2)
                barType = 0;
            else
                barType = 1;
            Rectangle r = new Rectangle();
            r.Location = new Point(0, 0);
            int speed = normalSpeed;

            int[] locationX = { speed,0 };
            int[] locationY = { 0,speed };

            if (ks.IsKeyDown(controls[bar, 2]))//speed-up
            {
                locationX[0] = boostSpeed;
                locationY[1] = boostSpeed;
            }
            else
            {
                locationX[0] = normalSpeed;
                locationY[1] = normalSpeed;
            }
            if (ks.IsKeyDown(controls[bar, 0]))//left/up
                r.Location = r.Location + new Point(-locationX[barType], -locationY[barType]);
            if (ks.IsKeyDown(controls[bar, 1]))//right/down
                r.Location = r.Location + new Point(locationX[barType], locationY[barType]);
            return r.Location;
            
        }
      
        public bool exit
        {
            get
            {
                if (ks.IsKeyDown(Keys.Escape))//spawn balls
                    return true;
                else
                    return false;
            }
        }

        public bool selection
        {
            get
            {
                if (ks.IsKeyDown(Keys.Up)||ks.IsKeyDown(Keys.Down))
                    return true;
                else
                    return false;
            }
        }

        public bool confirmation
        {
            get
            {
                if (ks.IsKeyDown(Keys.Enter))
                    return true;
                else
                    return false;
            }
        }
        //need to make pause input and events

        public void Update(float gameTime)
        {
            ks = Keyboard.GetState();
        }
    }
}
