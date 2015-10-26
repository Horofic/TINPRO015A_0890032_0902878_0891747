using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UltimatePong
{
    struct BallEntity
    {
        public Rectangle rectangle { get; set; }
        public Texture2D appearance { get; private set; }





        public BallEntity(Texture2D appearance, Rectangle rectangle)
        {
            this.appearance = appearance;
            this.rectangle = rectangle;


        }


        public BallEntity CreateMoved(int deltaX, int deltaY)
        {
            return new BallEntity()
            {
                appearance = this.appearance,
                rectangle = new Rectangle(this.rectangle.X + deltaX, this.rectangle.Y + deltaY, this.rectangle.Width, this.rectangle.Height)
            };
        }
    }
}
