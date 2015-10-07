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

       // public int width { get { return rectangle.Width; } set { rectangle.Width = value; } }
       // public int height { get { return rectangle.Height; } set { height = value; }}

        public int X { get { return rectangle.X; } set { this.X = value; } }
        public int Y { get { return rectangle.Y; } set { this.Y = value; } }

        public Entity(Texture2D appearance, Rectangle rectangle, int width, int height, Point pos)
        {
            this.appearance = appearance;
            rectangle.Width = width;
            rectangle.Height = height;
            rectangle.X = pos.X;
            rectangle.Y = pos.Y;
            this.rectangle = rectangle;
        }

        public Entity CreateMoved(Point deltaPosition)
        {
            return new Entity()
            {
                appearance = this.appearance,
                rectangle = this.getRectangle(deltaPosition)
            };
        }

        public Rectangle getRectangle(Point deltaPosition)
        {
            Rectangle temp = rectangle;
            temp.Location += deltaPosition;
            return temp;
        }

    }
}
