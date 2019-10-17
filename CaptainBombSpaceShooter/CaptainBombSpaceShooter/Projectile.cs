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
    public class Projectile
    {
        public Rectangle boundingArea;
        public Texture2D texture;
        public Vector2 position;
        public bool isVisible;
        public float speed;

        public Projectile(Texture2D newTexture)
        {
            speed = 9;
            texture = newTexture;
            isVisible = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
