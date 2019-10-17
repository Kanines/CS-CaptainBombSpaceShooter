using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaptainBombSpaceShooter
{
    public class Settings
    {

        public enum DifficultyLevel
        {
            Easy,
            Medium,
            Hard
        }
        public const int windowWidth = 800;
        public const int windowHeight = 950;
        public const int minSpawnX = 0;
        public const int maxSpawnX = 750;
        public const int minSpawnY = -600;
        public const int maxSpawnY = -50;
        public const int enemyProjectileDamage = 20;
        public const int collisionDamage = 40;
        public const int playerProjectileDamage = 2;

    }
}
