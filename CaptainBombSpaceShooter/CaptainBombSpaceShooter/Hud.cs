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
    public class Hud
    {
        public int playerScore;
        public SpriteFont playerScoreFont;
        public Vector2 playerScorePosition;
        public bool showHud;

        public Hud()
        {
            playerScore = 0;
            showHud = true;
            playerScoreFont = null;
            playerScorePosition = new Vector2(600, 10);
        }

        public void LoadContent(ContentManager Content)
        {
            playerScoreFont = Content.Load<SpriteFont>("font");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (showHud)
                spriteBatch.DrawString(playerScoreFont, "Score: " + playerScore, playerScorePosition, Color.Red);
        }
    }
}
