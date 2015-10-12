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
        bool selection { get; }
        bool confirmation { get; }
        Point moveBar(int bar, int barType);
        void Update(float gameTime);
    }

    class InputManagement : InputController
    {
        KeyboardState ks;
        private Keys[,] controls;
        Vector2 barMovement { get; set; }
        int normalSpeed = 8;
        int boostSpeed = 12;

        public InputManagement(Keys[,] controls)
        {
            this.controls = controls;
        }

        //returns delta location
        //barType 0 = Top/Bot , 1 = Left/Right
        public Point moveBar(int bar,int barType)
        {
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
      
        public bool quit
        {
            get
            {
                if (ks.IsKeyDown(Keys.Escape))//spawn balls
                    return true;
                else
                    return false;
            }
        }

        public bool test
        {
            get
            {
                if (ks.IsKeyDown(Keys.Z))
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
