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
        abstract public void powerupEvent(int lastHitPlayer, Entity playerBar);
        abstract public void Draw();
        public Random random = new Random(DateTime.Now.Millisecond + DateTime.Now.Second);
        public Entity powerup;
    }

    class GreenPowerupController : PowerupController
    {
        //public Entity powerup;
        SpriteBatch spriteBatch;
        Texture2D barTexture;
        public GreenPowerupController(Texture2D barTexture, SpriteBatch spriteBatch)
        {
            powerup = new Entity(barTexture, new Rectangle(), 50, 50, new Point(random.Next(100,650), random.Next(100, 650)));
            this.spriteBatch = spriteBatch;
            this.barTexture = barTexture;
        }

        public override void powerupEvent(int lastHitPlayer, Entity playerBar)
        {
            Console.WriteLine("GREEN HIT");
        }

        public override void Draw()
        {
            spriteBatch.Draw(barTexture, powerup.rectangle, Color.Green);
        }
    }
    class RedPowerupController : PowerupController
    {
        //public Entity powerup;
        SpriteBatch spriteBatch;
        Texture2D barTexture;
        public RedPowerupController(Texture2D barTexture, SpriteBatch spriteBatch)
        {
            powerup = new Entity(barTexture, new Rectangle(), 50, 50, new Point(random.Next(100, 650), random.Next(100, 650)));
            this.spriteBatch = spriteBatch;
            this.barTexture = barTexture;
        }

        public override void powerupEvent(int lastHitPlayer, Entity playerBar)
        {
            Console.WriteLine("RED HIT");
        }

        public override void Draw()
        {
            spriteBatch.Draw(barTexture, powerup.rectangle, Color.Red);
        }
    }
    class GoldPowerupController : PowerupController
    {
        //public Entity powerup;
        SpriteBatch spriteBatch;
        Texture2D barTexture;
        public GoldPowerupController(Texture2D barTexture, SpriteBatch spriteBatch)
        {
            powerup = new Entity(barTexture, new Rectangle(), 30, 30, new Point(random.Next(100, 650), random.Next(100, 650)));
            this.spriteBatch = spriteBatch;
            this.barTexture = barTexture;
        }

        public override void powerupEvent(int lastHitPlayer, Entity playerBar)
        {
            Console.WriteLine("GOLD HIT");
        }

        public override void Draw()
        {
            spriteBatch.Draw(barTexture, powerup.rectangle, Color.Gold);
        }
    }
}
