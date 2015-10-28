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
        public Color color;
        abstract public PowerupResponse powerupEvent(int lastHitPlayer, ref List<Entity> tempBars, ref int[] playerlives);
        public void Draw()
        {
            spriteBatch.Draw(barTexture, powerup.rectangle, color);
        }
        public Random random = new Random(DateTime.Now.Millisecond + DateTime.Now.Second);
        public Entity powerup;
        public SpriteBatch spriteBatch;
        public Texture2D barTexture;
    }

    class GreenPowerupController : PowerupController
    {
        public GreenPowerupController(Texture2D barTexture, SpriteBatch spriteBatch)
        {
            powerup = new Entity(barTexture, new Rectangle(), 50, 50, new Point(random.Next(100,650), random.Next(100, 650)));
            this.spriteBatch = spriteBatch;
            this.barTexture = barTexture;
            color = Color.Green;
        }

        public override PowerupResponse powerupEvent(int lastHitPlayer, ref List<Entity> tempBars, ref int[] playerlives)
        {
            Console.WriteLine("GREEN HIT");
            int difference = 50;
            tempBars.Insert(lastHitPlayer, tempBars[lastHitPlayer].CreateChangedProperties(0, difference).CreateMoved(new Point(0, -(difference / 2))));
            tempBars.RemoveAt(lastHitPlayer+1);
            return PowerupResponse.done;
        }
    }
    class RedPowerupController : PowerupController
    {
        public RedPowerupController(Texture2D barTexture, SpriteBatch spriteBatch)
        {
            powerup = new Entity(barTexture, new Rectangle(), 50, 50, new Point(random.Next(100, 650), random.Next(100, 650)));
            this.spriteBatch = spriteBatch;
            this.barTexture = barTexture;
            color = Color.Red;
        }

        public override PowerupResponse powerupEvent(int lastHitPlayer, ref List<Entity> tempBars, ref int[] playerlives)
        {
            Console.WriteLine("RED HIT");
            int difference = 30;
            tempBars.Insert(lastHitPlayer, tempBars[lastHitPlayer].CreateChangedProperties(0, -difference).CreateMoved(new Point(0, (difference / 2))));
            tempBars.RemoveAt(lastHitPlayer+1);
            return PowerupResponse.done;
        }
    }
    class GoldPowerupController : PowerupController
    {
        public GoldPowerupController(Texture2D barTexture, SpriteBatch spriteBatch)
        {
            powerup = new Entity(barTexture, new Rectangle(), 30, 30, new Point(random.Next(100, 650), random.Next(100, 650)));
            this.spriteBatch = spriteBatch;
            this.barTexture = barTexture;
            color = Color.Gold;
        }

        public override PowerupResponse powerupEvent(int lastHitPlayer, ref List<Entity> tempBars, ref int[] playerlives)
        {
            Console.WriteLine("GOLD HIT");
            playerlives[lastHitPlayer] += 1;
            return PowerupResponse.done;
        }
    }
    class PinkPowerupController : PowerupController
    {
        public PinkPowerupController(Texture2D barTexture, SpriteBatch spriteBatch)
        {
            powerup = new Entity(barTexture, new Rectangle(), 30, 30, new Point(random.Next(100, 650), random.Next(100, 650)));
            this.spriteBatch = spriteBatch;
            this.barTexture = barTexture;
            color = Color.HotPink;
        }

        public override PowerupResponse powerupEvent(int lastHitPlayer, ref List<Entity> tempBars, ref int[] playerlives)
        {
            Console.WriteLine("PINK HIT");
            return PowerupResponse.addBall;
        }
    }
    class BluePowerupController : PowerupController
    {
        public BluePowerupController(Texture2D barTexture, SpriteBatch spriteBatch)
        {
            powerup = new Entity(barTexture, new Rectangle(), 30, 30, new Point(random.Next(100, 650), random.Next(100, 650)));
            this.spriteBatch = spriteBatch;
            this.barTexture = barTexture;
            color = Color.Gold;
        }

        public override PowerupResponse powerupEvent(int lastHitPlayer, ref List<Entity> tempBars, ref int[] playerlives)
        {
            Console.WriteLine("BLUE HIT");
            return PowerupResponse.changeBallDirection;
        }
    }
}
