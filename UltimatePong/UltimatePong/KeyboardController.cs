using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace UltimatePong
{
    class KeyboardController : InputController
    {
        KeyboardState ks;
        Rectangle entity;
        private Keys[] controls;
        int speed = 400;
        int speedMultiplier;

        public KeyboardController(Rectangle entity, Keys[] controls)
        {
            this.entity = entity;
            this.controls = controls;
        }

        public bool quit
        {
            get
            {
                return ks.IsKeyDown(Keys.Escape);
            }
        }


        public void PlayerMovement()
        {
            if(ks.IsKeyDown(controls[0]))
                entity.Offset(0, (-speed - speedMultiplier));

            if (ks.IsKeyDown(controls[1]))
                entity.Offset(0, (speed - speedMultiplier));

            if (ks.IsKeyDown(controls[2]))
                speedMultiplier = 3;
            else
                speedMultiplier = 0;
        }


        public void Update(float gameTime)
        {
            ks = Keyboard.GetState();
        }
    }
}
