using System;
using Microsoft.Xna.Framework.Graphics;

namespace Dragon
{
    public class DFont
    {
        internal SpriteFont spriteFont;
        internal float spriteFontSize;

        public DFont(SpriteFont spriteFont, float? lineSpacingScale = null)
        {
            load(spriteFont, lineSpacingScale);
        }
        public DFont(string assetName, float? lineSpacingScale = null)
        {
            SpriteFont spriteFont = DGame.current.contentManager.loadSpriteFont(assetName);
            load(spriteFont, lineSpacingScale);
        }

        void load(SpriteFont someSpriteFont, float? someLineSpacingScale)
        {
            spriteFont = someSpriteFont;
            float scale = someLineSpacingScale ?? 1.533f;
            spriteFontSize = (float)Math.Round(spriteFont.LineSpacing / scale);
        }
    }
}
