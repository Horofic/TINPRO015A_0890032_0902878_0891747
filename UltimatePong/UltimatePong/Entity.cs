using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UltimatePong
{
    struct Entity
    {
        public Rectangle rectangle { get; set; }
        public Texture2D appearance { get; private set; }

        //public int speed;

        public int width { get { return rectangle.Width; } }
        public int height { get { return rectangle.Height; } }

        public float X { get { return rectangle.X; } set { X = value; } }
        public float Y { get { return rectangle.Y; } set { Y = value; } }


        public Entity(Texture2D appearance, Rectangle rectangle, int width, int height)
        {
            this.appearance = appearance;
            this.rectangle = rectangle;

            rectangle.Width = width;
            rectangle.Height = height;
        }


        public Entity CreateMoved(float deltaX, float deltaY)
        {
            return new Entity()
            {
                appearance = this.appearance,
                X = this.X + deltaX,
                Y = this.Y + deltaY,
            };
        }
    }
}
