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

        public int width { get { return rectangle.Width; } set { width = value; } }
        public int height { get { return rectangle.Height; } set { height = value; } }

        public Entity(Texture2D appearance, Rectangle rectangle, int width, int height, Point pos)
        {
            this.appearance = appearance;
            rectangle.Width = width;
            rectangle.Height = height;
            rectangle.X = pos.X;
            rectangle.Y = pos.Y;
            this.rectangle = rectangle;
        }

        //Contructor overload use this one for a ball
        public Entity(Texture2D appearance, Rectangle rectangle)
        {
            this.appearance = appearance;
            this.rectangle = rectangle;
        }

        //Move by adding the pos
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

        //Overload for CreateMoved
        public Entity CreateMoved(int deltaX, int deltaY)
        {
            return new Entity()
            {
                appearance = this.appearance,
                rectangle = new Rectangle(this.rectangle.X + deltaX, this.rectangle.Y + deltaY, this.rectangle.Width, this.rectangle.Height)
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
