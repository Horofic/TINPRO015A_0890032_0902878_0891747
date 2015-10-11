using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UltimatePong
{
    class Power_up
    {
        public Power_up()
        {

        }

        /*TO_DO**
        * Blue: Bounce off ball to random direction
        * Purple: Invert controls for the lastHitBar
        * Gold: Life+1 for the lastHitBar
        * Make sure lastHitBar gets fixed
        * Idea: Portals for the ball
        * Idea: Hidden ball
        * Idea: obstacles on the field
        */

        int lastHitBar;
        public List<Entity> powerupEvent(List<Entity> tempBars, int powerupType,int lastHitBar, ref int[] playerLives)
        {
            this.lastHitBar = lastHitBar;
            switch(powerupType)
            {
                case 0:
                    int greenDifference = 20;
                    tempBars.Insert(lastHitBar, tempBars[lastHitBar].CreateChangedProperties(0, greenDifference).CreateMoved(new Point(0, -(greenDifference / 2))));
                    tempBars.RemoveAt(lastHitBar+1);
                    return tempBars;
                case 1:
                    int redDifference = 20;
                    tempBars.Insert(lastHitBar, tempBars[lastHitBar].CreateChangedProperties(0, -redDifference).CreateMoved(new Point(0, (redDifference / 2))));
                    tempBars.RemoveAt(lastHitBar+1);
                    return tempBars;
                case 2:
                    goldEvent(ref playerLives);
                    return tempBars;
                default:
                    return tempBars;
            }
        }

        public List<Entity> greenEvent(List<Entity> tempBars)
        {
            int difference = 50;
            tempBars.Insert(2, tempBars[2].CreateChangedProperties(0, difference).CreateMoved(new Point(0, -(difference/2))));
            tempBars.RemoveAt(3);
            return tempBars;
        }
        public List<Entity> redEvent(List<Entity> tempBars)
        {
            int difference = 30;
            tempBars.Insert(2, tempBars[2].CreateChangedProperties(0, -difference).CreateMoved(new Point(0, (difference / 2))));
            tempBars.RemoveAt(3);
            return tempBars;
        }
        public void goldEvent(ref int[] playerLives)
        {
            playerLives[lastHitBar] += 1;
        }
    }
}
