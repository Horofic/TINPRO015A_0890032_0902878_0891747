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

        public int width { get { return width; } set { width = value; } }
        public int height { get { return height; } set { height = value; } }

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

        //Move by adding the poss
        public Entity CreateMoved(Point deltaPosition)
        {
            Rectangle temp = rectangle;
            temp.Location += deltaPosition;

            return new Entity()
            {
                appearance = this.appearance,
                rectangle = temp
                
            };
        }

        //Move by giving new pos
        public Entity CreateNewPos(Point newPosition)
        {
            Rectangle temp = rectangle;
            temp.Location = newPosition;

            return new Entity()
            {
                appearance = this.appearance,
                rectangle = temp

            };
        }

        //Change width/height
        public Entity CreateChangedProperties(int width, int height)
        {
            Rectangle temp = rectangle;
            temp.Width += width;
            temp.Height += height;

            return new Entity()
            {
                appearance = this.appearance,
                rectangle = temp

            };
        }

    }
}
