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
    public class Explosion
    {
        public Texture2D texture;
        public Vector2 position;
        public bool isVisible;
        private float timer;
        private float interval;
        private Vector2 origin;
        private int currentFrame;
        private int spriteWidth;
        private int spriteHeight;
        private Rectangle sourceRectangle;

        public Explosion(Texture2D newTexture, Vector2 newPosition)
        {
            position = newPosition;
            texture = newTexture;
            timer = 0f;
            interval = 30f;
            currentFrame = 1;
            spriteWidth = 120;
            spriteHeight = 128;
            isVisible = true;
        }

        public void Update(GameTime gameTime)
        {
            // Creating Animation
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                currentFrame++;
                timer = 0f;
            }

            // Animation has 17 frames
            if (currentFrame == 17)
            {
                isVisible = false;
                currentFrame = 0;
            }
            sourceRectangle = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
                spriteBatch.Draw(texture, position, sourceRectangle, Color.White, 0f,
                    origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
