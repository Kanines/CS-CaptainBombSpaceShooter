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
    public class Player : Creature
    {

        public Vector2 position;
        public int health;
        public bool isColliding;
        public List<Projectile> projectileList;
        private float shootDelay;
        private Rectangle healthRectangle;
        private Vector2 healthBarPosition;
        private Texture2D projectileTexture;
        private Texture2D healthTexture;
        SoundManager soundManager = new SoundManager();

        public Player()
        {
            texture = null;
            position = new Vector2(350, 750);
            shootDelay = 10;
            speed = 6;
            health = 200;
            isColliding = false;
            projectileList = new List<Projectile>();
            healthBarPosition = new Vector2(10, 10);
        }

        public Player(int newHealth)
        {
            {
                texture = null;
                position = new Vector2(350, 750);
                shootDelay = 10;
                speed = 6;
                health = newHealth;
                isColliding = false;
                projectileList = new List<Projectile>();
                healthBarPosition = new Vector2(10, 10);
            }
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("playerImage");
            projectileTexture = Content.Load<Texture2D>("playerProjectileImage");
            healthTexture = Content.Load<Texture2D>("healthImage");
            soundManager.LoadContent(Content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.Draw(healthTexture, healthRectangle, Color.White);
            foreach (Projectile p in projectileList)
                p.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            // BoundingArea of our Player
            boundingArea = new Rectangle((int)position.X, (int)position.Y,
                texture.Width, texture.Height);

            healthRectangle = new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y,
                health, 50);

            // Player controls
            // Movement
            if (keyState.IsKeyDown(Keys.W))
                position.Y = position.Y - speed * 3 / 4;

            if (keyState.IsKeyDown(Keys.S))
                position.Y = position.Y + speed;

            if (keyState.IsKeyDown(Keys.A))
                position.X = position.X - speed;

            if (keyState.IsKeyDown(Keys.D))
                position.X = position.X + speed;

            // Shooting
            if (keyState.IsKeyDown(Keys.Space))
                Shoot();

            UpdateProjectiles();

            // Keep player in screen bounds
            if (position.X <= 0)
                position.X = 0;

            if (position.X >= 800 - texture.Width)
                position.X = 800 - texture.Width;

            if (position.Y <= 0)
                position.Y = 0;

            if (position.Y >= 950 - texture.Height + 70)
                position.Y = 950 - texture.Height + 70;
        }
        public void Shoot()
        {
            if (shootDelay >= 0)
                shootDelay--;

            // Shoot only if shootDelay resets
            if (shootDelay <= 0)
            {
                soundManager.playerShootSound.Play(0.7f, 0f, 0f);
                Projectile newProjectile = new Projectile(projectileTexture);
                newProjectile.position = new Vector2(position.X + 40 - newProjectile.texture.Width / 2,
                    position.Y + 30);

                newProjectile.isVisible = true;

                if (projectileList.Count() < 10)
                    projectileList.Add(newProjectile);
            }
            // Reset shootDelay
            if (shootDelay == 0)
                shootDelay = 10;
        }
        public void UpdateProjectiles()
        {
            // Projectile movement
            foreach (Projectile p in projectileList)
            {
                p.boundingArea = new Rectangle((int)p.position.X, (int)p.position.Y,
                    p.texture.Width, p.texture.Height);
                p.position.Y = p.position.Y - p.speed;

                if (p.position.Y <= 0)
                    p.isVisible = false;
            }
            // Projectile removal
            for (int i = 0; i < projectileList.Count; i++)
            {
                if (!projectileList[i].isVisible)
                {
                    projectileList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
