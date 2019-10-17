using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CaptainBombSpaceShooter
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public enum GameState
        {
            Menu,
            Playing,
            GameOver
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random = new Random();

        private Texture2D menuTexture;
        private Texture2D gameoverTexture;

        Player player1 = new Player();

        // Polymorphism example (overloading)
        // Create Player with extra health
        Player playerWithExtraHealth = new Player(400);

        Background background1 = new Background();
        Hud hud = new Hud();
        SoundManager soundManager = new SoundManager();

        List<Obstacle> obstacleList = new List<Obstacle>();
        List<Enemy> enemyList = new List<Enemy>();
        List<Explosion> explosionList = new List<Explosion>();

        GameState gameState = GameState.Menu;

        // Set difficulty level
        Settings.DifficultyLevel difficulty = Settings.DifficultyLevel.Medium;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = Settings.windowWidth;
            graphics.PreferredBackBufferHeight = Settings.windowHeight;
            this.Window.Title = "Captain Bomb C# Shooter";
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player1.LoadContent(Content);
            playerWithExtraHealth.LoadContent(Content);
            hud.LoadContent(Content);
            background1.LoadContent(Content);
            soundManager.LoadContent(Content);
            menuTexture = Content.Load<Texture2D>("menuImage");
            gameoverTexture = Content.Load<Texture2D>("gameoverImage");
            MediaPlayer.IsRepeating = false;
            MediaPlayer.Play(soundManager.menuSound);

        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape))
                this.Exit();

            switch (gameState)
            {
                case GameState.Playing:
                    {

                        background1.speed = 5;

                        // Check for collisions
                        foreach (Enemy e in enemyList)
                        {
                            // Collision between Enemy and Player1
                            if (e.boundingArea.Intersects(player1.boundingArea))
                            {
                                soundManager.explosionSound.Play(0.45f, 0f, 0f);
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosionSprite"),
                                        new Vector2(e.position.X + e.texture.Width / 2, e.position.Y + e.texture.Height - 10)));
                                player1.health -= Settings.collisionDamage;
                                e.isVisible = false;
                            }
                            // Collision between EnemyProjectiles and Player1
                            for (int i = 0; i < e.projectileList.Count; i++)
                            {
                                if (player1.boundingArea.Intersects(e.projectileList[i].boundingArea))
                                {
                                    player1.health -= Settings.enemyProjectileDamage;
                                    e.projectileList[i].isVisible = false;
                                }
                            }
                            // Collision between Player1Projectiles and Enemy
                            for (int i = 0; i < player1.projectileList.Count; i++)
                            {
                                if (player1.projectileList[i].boundingArea.Intersects(e.boundingArea))
                                {
                                    soundManager.explosionSound.Play(0.45f, 0f, 0f);
                                    explosionList.Add(new Explosion(Content.Load<Texture2D>("explosionSprite"),
                                        new Vector2(e.position.X + e.texture.Width / 2, e.position.Y + e.texture.Height - 10)));
                                    player1.projectileList[i].isVisible = false;
                                    e.isVisible = false;
                                    hud.playerScore += 10;
                                    //e.health -= playerProjectileDamage;
                                }
                            }
                            e.Update(gameTime);
                        }
                        foreach (Obstacle o in obstacleList)
                        {
                            // Collision between Obstacles and Player1
                            if (o.boundingArea.Intersects(player1.boundingArea))
                            {
                                soundManager.explosionSound.Play(0.45f, 0f, 0f);
                                explosionList.Add(new Explosion(Content.Load<Texture2D>("explosionSprite"),
                                        new Vector2(o.position.X - 5, o.position.Y + 30)));
                                player1.health -= 20;
                                o.isVisible = false;
                            }
                            // Collision between Player1Projectiles and Obstacles
                            for (int i = 0; i < player1.projectileList.Count; i++)
                            {
                                if (o.boundingArea.Intersects(player1.projectileList[i].boundingArea))
                                {
                                    soundManager.explosionSound.Play(0.45f, 0f, 0f);
                                    explosionList.Add(new Explosion(Content.Load<Texture2D>("explosionSprite"),
                                        new Vector2(o.position.X - 5, o.position.Y + 30)));
                                    hud.playerScore += 5;
                                    o.isVisible = false;
                                    player1.projectileList.ElementAt(i).isVisible = false;
                                }
                            }
                            o.Update(gameTime);
                        }

                        foreach (Explosion ex in explosionList)
                            ex.Update(gameTime);

                        // Player defeat
                        if (player1.health <= 0)
                        {
                            gameState = GameState.GameOver;
                            MediaPlayer.Stop();
                            MediaPlayer.IsRepeating = false;
                            MediaPlayer.Volume = 1f;
                            MediaPlayer.Play(soundManager.gameoverSound);
                        }
                        player1.Update(gameTime);
                        background1.Update(gameTime);
                        LoadObstacles();
                        LoadEnemies();
                        break;
                    }
                case GameState.Menu:
                    {
                        if (keyState.IsKeyDown(Keys.Enter))
                        {
                            MediaPlayer.Stop();
                            MediaPlayer.IsRepeating = true;
                            MediaPlayer.Volume = 0.7f;
                            MediaPlayer.Play(soundManager.backgroundMusic);
                            player1.health = 200;
                            gameState = GameState.Playing;
                        }
                        if (keyState.IsKeyDown(Keys.H))
                        {
                            MediaPlayer.Stop();
                            MediaPlayer.IsRepeating = true;
                            MediaPlayer.Volume = 0.7f;
                            MediaPlayer.Play(soundManager.backgroundMusic);
                            player1 = playerWithExtraHealth;
                            gameState = GameState.Playing;
                        }
                        background1.Update(gameTime);
                        background1.speed = 1;

                        break;
                    }
                case GameState.GameOver:
                    {
                        if (keyState.IsKeyDown(Keys.R))
                        {
                            MediaPlayer.Stop();
                            MediaPlayer.Play(soundManager.menuSound);
                            player1.position = new Vector2(350, 750);
                            enemyList.Clear();
                            obstacleList.Clear();
                            player1.projectileList.Clear();
                            explosionList.Clear();
                            player1.health = 200;
                            playerWithExtraHealth.health = player1.health * 2;
                            hud.playerScore = 0;
                            gameState = GameState.Menu;
                        }

                        break;
                    }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.Playing:
                    {
                        background1.Draw(spriteBatch);

                        foreach (Explosion ex in explosionList)
                            ex.Draw(spriteBatch);
                        foreach (Obstacle o in obstacleList)
                            o.Draw(spriteBatch);
                        foreach (Enemy e in enemyList)
                            e.Draw(spriteBatch);
                        player1.Draw(spriteBatch);
                        hud.Draw(spriteBatch);

                        break;
                    }
                case GameState.Menu:
                    {
                        background1.Draw(spriteBatch);
                        spriteBatch.Draw(menuTexture, new Vector2(0, 0), Color.White);

                        break;
                    }
                case GameState.GameOver:
                    {
                        background1.Draw(spriteBatch);
                        spriteBatch.Draw(gameoverTexture, new Vector2(0, 0), Color.White);
                        spriteBatch.DrawString(hud.playerScoreFont, "Your Final Score was: " + hud.playerScore.ToString(),
                            new Vector2(180, 50), Color.Red);

                        break;
                    }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
        public void LoadObstacles()
        {
            int randomY = random.Next(-600, -50);
            int randomX = random.Next(0, 750);
            int obstacleCount;

            if (difficulty == Settings.DifficultyLevel.Hard)
                obstacleCount = 16;
            else if (difficulty == Settings.DifficultyLevel.Medium)
                obstacleCount = 12;
            else
                obstacleCount = 8;

            // Create new obstacles
            if (obstacleList.Count() < obstacleCount)
            {
                obstacleList.Add(new Obstacle(Content.Load<Texture2D>("obstacleImage"),
                    new Vector2(randomX, randomY)));
            }

            // Obstacles removal
            for (int i = 0; i < obstacleList.Count; i++)
            {
                if (!obstacleList[i].isVisible)
                {
                    obstacleList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void LoadEnemies()
        {
            int randomY = random.Next(-600, -50);
            int randomX = random.Next(0, 750);
            int enemyCount;

            if (difficulty == Settings.DifficultyLevel.Hard)
                enemyCount = 14;
            else if (difficulty == Settings.DifficultyLevel.Medium)
                enemyCount = 10;
            else
                enemyCount = 6;

            // Create new enemies
            if (enemyList.Count() < enemyCount)
            {
                Enemy newEnemy = new Enemy(Content.Load<Texture2D>("enemyImage"),
                    new Vector2(randomX, randomY), Content.Load<Texture2D>("enemyProjectileImage"));

                if (difficulty == Settings.DifficultyLevel.Medium)
                    newEnemy.health = newEnemy.health * 2;

                else if (difficulty == Settings.DifficultyLevel.Hard)
                    newEnemy.health = newEnemy.health * 3;

                enemyList.Add(newEnemy);

            }

            // Enemies removal
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (!enemyList[i].isVisible)
                {
                    enemyList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void ManageExplosions()
        {
            for (int i = 0; i < explosionList.Count; i++)
            {
                if (!explosionList[i].isVisible)
                {
                    explosionList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
