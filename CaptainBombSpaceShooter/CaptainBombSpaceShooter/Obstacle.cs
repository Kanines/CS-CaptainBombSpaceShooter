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
    public class Obstacle : Creature
    {
        public Vector2 position;
        public Vector2 origin;
        private float rotationAngle;
        private float randomX, randomY;
        private int randomRotationDirection;
        Random random = new Random();

        public Obstacle(Texture2D newTexture, Vector2 newPosition)
        {
            position = newPosition;
            texture = newTexture;
            speed = 4;
            isVisible = true;

            randomX = random.Next(Settings.minSpawnX, Settings.maxSpawnX);
            randomY = random.Next(Settings.minSpawnY, Settings.maxSpawnY);
            randomRotationDirection = random.Next(0, 2);
        }

        public void Update(GameTime gameTime)
        {
            // Set bounding area for collision detection
            boundingArea = new Rectangle((int)position.X - 40, (int)position.Y, 80, 50);

            // Update origin for rotation
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;

            // Obstacle movement
            position.Y = position.Y + speed;
            if (position.Y >= 950)
                isVisible = false;

            // Obstacle rotation
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (randomRotationDirection == 1)
                rotationAngle += elapsed / 4;
            else
                rotationAngle -= elapsed / 4;

            float circle = MathHelper.Pi * 2;
            rotationAngle = rotationAngle % circle;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
                spriteBatch.Draw(texture, position, null, Color.White,
                    rotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
        }
    }
}
