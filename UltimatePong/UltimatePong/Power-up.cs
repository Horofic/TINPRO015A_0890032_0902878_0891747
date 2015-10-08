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

        //draws the powerup
        public Entity greenEvent(Entity bar)
        {
            return bar.CreateChangedProperties(0, 10);
        }
    }
}
