using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Dragon
{
    public class DLabelNode : DNode
    {
        DFont font;
        internal Vector2 anchorPoint;
        internal float fontSize;

        internal string text;
        Color color;
        SpriteEffects effects;
        float layerDepth;

        internal Vector2 drawOrigin;

        Vector2 textureSize => font.spriteFont?.MeasureString(text) ?? Vector2.One;

        internal static DFont defaultLoadFont;

        public DLabelNode(string text, DFont font, Color? color = null, float? fontSize = null)
        {
            load(font, fontSize, color, text);
        }

        public DLabelNode(string text, DFont font, float fontSize)
        {
            load(font, fontSize, null, text);
        }

        public DLabelNode(string text, string assetName, Color? color = null, float? fontSize = null)
        {
            DFont font = new DFont(assetName);
            load(font, fontSize, color, text);
        }

        public DLabelNode(string text, string assetName, float fontSize)
        {
            DFont font = new DFont(assetName);
            load(font, fontSize, null, text);
        }

        public DLabelNode(string text, Color? color = null, float? fontSize = null)
        {
            DFont font = defaultFont();
            load(font, fontSize, color, text);
        }

        public DLabelNode(string text, float fontSize)
        {
            DFont font = defaultFont();
            load(font, fontSize, null, text);
        }

        void load(DFont someFont, float? someFontSize, Color? someColor, string someText)
        {
            font = someFont;
            text = someText ?? "";
            position = Vector2.Zero;
            color = someColor ?? Color.White;
            rotation = 0;
            anchorPoint = new Vector2(0.5f, 0.5f);
            scale = Vector2.One;
            effects = SpriteEffects.None;
            layerDepth = 0;

            fontSize = someFontSize ?? 32;
        }

        DFont defaultFont()
        {
            if (defaultLoadFont != null)
            {
                return defaultLoadFont;
            }

            SpriteFont spriteFont = DGame.current.contentManager.loadSpriteFont(null);
            return new DFont(spriteFont);
        }

        internal override void draw(Vector2 currentPosition, float currentRotation, Vector2 currentScale, float currentAlpha)
        {
            beforeDraw(currentPosition, currentRotation, currentScale, currentAlpha);

            drawOrigin = textureSize * anchorPoint;
            Vector2 drawScale = fontSize / font.spriteFontSize * this.drawScale;
            Color drawColor = color * drawAlpha;
            DGame.current.spriteBatch.DrawString(font.spriteFont, text, drawPosition, drawColor, drawRotation, drawOrigin, drawScale, effects, layerDepth);
            drawChildren();
        }
    }
}
