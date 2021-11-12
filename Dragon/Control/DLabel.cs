//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.Xna.Framework;

//namespace Dragon
//{
//    class Label : DControl
//    {
//        internal static Color defaultColor = GameColors.fontWhite;
//        internal static FontName defaultFontName = FontName.SpriteFont;
//        internal static FontSize defaultFontSize = FontSize.size32;

//        HorizontalAlignment horizontalAlignment = HorizontalAlignment.left;
//        VerticalAlignment verticalAlignment = VerticalAlignment.top;

//        public Label(string text, float x = 0, float y = 0,
//                     HorizontalAlignment horizontalAlignment = HorizontalAlignment.left,
//                     VerticalAlignment verticalAlignment = VerticalAlignment.top,
//                     FontName fontName = FontName.Default,
//                     FontSize fontSize = FontSize.Default) : base((fontName == FontName.Default ?
//                                                                   defaultFontName :
//                                                                   fontName).ToString() +
//                                                                  (fontSize == FontSize.Default ?
//                                                                   defaultFontSize :
//                                                                   fontSize).GetHashCode(), text)
//        {
//            sketchPosition = new Vector2(x, y);
//            setAlignment(horizontalAlignment, verticalAlignment);
//            color = defaultColor;
//        }
//    }
//}
