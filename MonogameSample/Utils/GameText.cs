using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameSample.Utils
{
    class GameText
    {
        private static SpriteFont font;
        // for now, only queue one string
        public static string Text { get; set; }

        public static void Load(ContentManager content)
        {
            font = content.Load<SpriteFont>("Arial");
        }

        public static void Update(string text)
        {
            Text = text;
        }

        public static void Clear()
        {
            Text = default;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if(Text == default) { return; }
            float height = font.MeasureString(Text).Y;
            Vector2 pos = new Vector2(8, Camera.Instance.ScreenHeight - height - 8);
            spriteBatch.DrawString(font, Text, pos, Color.White);
        }
    }
}
