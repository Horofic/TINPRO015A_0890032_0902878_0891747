using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace UltimatePong
{
    abstract class PowerupController
    {
        abstract public void powerupEvent();
        abstract public void Draw();
    }

    class GreenPowerupController : PowerupController
    {
        Entity powerup;
        SpriteBatch spriteBatch;
        Texture2D barTexture;
        public GreenPowerupController(Texture2D barTexture, SpriteBatch spriteBatch)
        {
            powerup = new Entity(barTexture, new Rectangle(), 100, 100, new Point(150, 650));
            this.spriteBatch = spriteBatch;
            this.barTexture = barTexture;
        }
 
        public override void powerupEvent()
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            spriteBatch.Draw(barTexture, powerup.rectangle, Color.Green);
        }
    }
}
