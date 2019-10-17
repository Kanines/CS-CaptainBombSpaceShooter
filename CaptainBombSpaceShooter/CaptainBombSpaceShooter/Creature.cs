using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace CaptainBombSpaceShooter
{
    public class Creature
    {
        public Rectangle boundingArea { get; set; }
        public Texture2D texture { get; set; }
        public int speed { get; set; }
        public bool isVisible { get; set; }
    }
}
