using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace CaptainBombSpaceShooter
{
    public class SoundManager
    {
        public SoundEffect playerShootSound;
        public SoundEffect explosionSound;
        public Song backgroundMusic;
        public Song gameoverSound;
        public Song menuSound;


        public SoundManager()
        {
            playerShootSound = null;
            explosionSound = null;
            backgroundMusic = null;
            gameoverSound = null;
            menuSound = null;

        }

        public void LoadContent(ContentManager Content)
        {
            playerShootSound = Content.Load<SoundEffect>("shootSound");
            explosionSound = Content.Load<SoundEffect>("explosionSound");
            backgroundMusic = Content.Load<Song>("backgroundTheme");
            gameoverSound = Content.Load<Song>("gameoverSound");
            menuSound = Content.Load<Song>("menuSound");

        }
    }
}
