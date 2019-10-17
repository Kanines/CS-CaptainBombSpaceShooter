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
    public class Enemy : Creature
    {
        public Texture2D projectileTexture;
        public Vector2 position;
        public int health { get; set; }
        public List<Projectile> projectileList;
        private int shootDelay { get; set; }

        public Enemy(Texture2D newTexture, Vector2 newPosition, Texture2D newProjectileTexture)
        {
            projectileList = new List<Projectile>();
            texture = newTexture;
            projectileTexture = newProjectileTexture;
            health = 5;
            position = newPosition;
            shootDelay = 40;
            speed = 2;
            isVisible = true;
        }

        public void Update(GameTime gameTime)
        {

            boundingArea = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            position.Y = position.Y + speed;

            if (position.Y >= 950)
                isVisible = false;

            EnemyShoot();
            UpdateProjectiles();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);

            foreach (Projectile p in projectileList)
                p.Draw(spriteBatch);

        }
        public void UpdateProjectiles()
        {
            // Projectile movement
            foreach (Projectile p in projectileList)
            {
                p.boundingArea = new Rectangle((int)p.position.X, (int)p.position.Y, p.texture.Width,
                    p.texture.Height);
                p.position.Y = p.position.Y + p.speed;

                if (p.position.Y >= 950)
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
        public void EnemyShoot()
        {
            // Shoot only if shootDelay resets
            if (shootDelay >= 0)
                shootDelay--;

            if (shootDelay <= 0)
            {
                Projectile newProjectile = new Projectile(projectileTexture);
                newProjectile.position = new Vector2(position.X + texture.Width / 2 - 5, position.Y + 35);

                newProjectile.isVisible = true;

                if (projectileList.Count() < 20)
                    projectileList.Add(newProjectile);

                // Reset shootDelay

                if (shootDelay == 0)
                    shootDelay = 40;
            }
        }
    }
}
