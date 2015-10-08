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

        public List<Entity> powerupEvent(List<Entity> tempBars, int powerupType,int lastHitBar)
        {
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
    }
}
