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
        //Entity entity;
        //Rectangle entity;
        private Keys[,] controls;

       Vector2 barMovement { get; set; }

        public KeyboardController(Keys[,] controls)
        {
            this.controls = controls;
           // this.entity = entity;
        }

        public bool quit
        {
            get
            {
                return ks.IsKeyDown(Keys.Escape);
            }
        }

        public float X
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public float Y
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Vector2 PlayerMovement()
        {
            return Vector2.Zero;          
            //top player
            /*
            if(ks.IsKeyDown(controls[0, 0]))
                entity.Offset((-speed - speedMultiplier),0);
            if (ks.IsKeyDown(controls[0, 1]))
                entity.Offset((speed - speedMultiplier),0);
            if (ks.IsKeyDown(controls[0, 2]))
                speedMultiplier = 3;
            else
                speedMultiplier = 0;
            //bottom player
            if (ks.IsKeyDown(controls[1, 0]))
                entity.Offset((-speed - speedMultiplier),0);
            if (ks.IsKeyDown(controls[1, 1]))
                entity.Offset((speed - speedMultiplier),0);
            if (ks.IsKeyDown(controls[1, 2]))
                speedMultiplier = 3;
            else
                speedMultiplier = 0;
            //left player
            if (ks.IsKeyDown(controls[2, 0]))
                entity.Offset(0, (-speed - speedMultiplier));
            if (ks.IsKeyDown(controls[2, 1]))
                entity.Offset(0, (speed - speedMultiplier));
            if (ks.IsKeyDown(controls[2, 2]))
                speedMultiplier = 3;
            else
                speedMultiplier = 0;
            //right player
            if (ks.IsKeyDown(controls[3, 0]))
                entity.Offset(0, (-speed - speedMultiplier));
            if (ks.IsKeyDown(controls[3, 1]))
                entity.Offset(0, (speed - speedMultiplier));
            if (ks.IsKeyDown(controls[3, 2]))
                speedMultiplier = 3;
            else
                speedMultiplier = 0;*/
        }


        public void Update(float gameTime)
        {
            ks = Keyboard.GetState();
        }
    }
}
