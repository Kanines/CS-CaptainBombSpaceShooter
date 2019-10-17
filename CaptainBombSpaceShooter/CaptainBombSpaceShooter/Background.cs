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
    public class Background
    {
        public Texture2D texture;
        public Vector2 backgroundPosition1;
        public Vector2 backgroundPosition2;
        public int speed;

        public Background()
        {
            texture = null;
            backgroundPosition1 = new Vector2(0, 0);
            backgroundPosition2 = new Vector2(0, -950);
            speed = 5;
        }
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("backgroundImage");

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, backgroundPosition1, Color.White);
            spriteBatch.Draw(texture, backgroundPosition2, Color.White);

        }
        public void Update(GameTime gameTime)
        {
            // Moving Background
            backgroundPosition1.Y = backgroundPosition1.Y + speed;
            backgroundPosition2.Y = backgroundPosition2.Y + speed;

            // Background loop (repeat)
            if (backgroundPosition1.Y >= 950)
            {
                backgroundPosition1.Y = 0;
                backgroundPosition2.Y = -950;
            }
        }
    }
}
