using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;

namespace UltimatePong
{
    interface InputController
    {
        bool quit { get; }

    
        void Update(float gameTime);
    }
}
