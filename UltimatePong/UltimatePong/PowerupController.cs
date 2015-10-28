using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace UltimatePong
{
    class PowerupController
    {
        Random random = new Random(DateTime.Now.Millisecond + DateTime.Now.Second);

        Entity powerup = new Entity(barTexture, new Rectangle(), 100, 100, new Point(random.Next(150, 650), random.Next(150, 650);
    }
}
