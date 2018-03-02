using UnityEngine;
using Color = System.Byte;
using Piece = System.Byte;
using Type = System.Byte;

namespace Com.aberrantGames.Tak.GameEngine
{
    public static class Stones
    {
        public const Color White = 1 << 7;
        public const Color Black = 1 << 6;
        public const Color NoColor = 0;
        public const Type Flat = 1;
        public const Type Standing = 2;
        public const Type Capstone = 3;
        public const byte ColorMask = 3 << 6;
        public const byte TypeMask = 1 << 2 - 1;
        
        /// <summary>
        /// Create a new piece by type and color
        /// </summary>
        /// <param name="_color"></param>
        /// <param name="_kind"></param>
        /// <returns>Piece</returns>
        public static Piece MakePiece(Color _color, Type _kind)
        {
            return (Piece)((byte)_color | (byte)_kind);
        }

        /// <summary>
        /// Get the color of this piece
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Color Color(this Piece p)
        {
            return (Color)((byte)p & ColorMask);
        }

        /// <summary>
        /// Get the type of this piece
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Type Type(this Piece p)
        {
            return (Type)((byte)p & TypeMask);
        }

        /// <summary>
        /// Determine if this piece is a valid road
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool IsRoad(this Piece p)
        {
            return (p.Type() == Flat) || (p.Type() == Capstone);
        }

        /// <summary>
        /// Get the notation of this piece
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string StringNotation(this Piece p)
        {
            string n = "";
            if (p.Color() == White)
                n = "W";
            else
                n = "B";
            switch (p.Type())
            {
                case (Capstone): n += "C"; break;
                case (Standing): n += "S"; break;
            }
            return n;
        }

        /// <summary>
        /// Get the name of this color
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string StringColor(this Color c)
        {
            switch (c)
            {
                case (White): return "White";
                case (Black): return "Black";
                case (NoColor): return "No Color";
                default:
                    Debug.LogError("bad color" + (int)c);
                    return "";
            }
        }

        /// <summary>
        /// Get the color opposite of this color
        /// </summary>
        /// <returns>Color</returns>
        public static Color Flip(this Color c)
        {
            switch (c)
            {
                case (White): return Black;
                case (Black): return White;
                case (NoColor): return NoColor;
                default:
                    Debug.LogError("bad color" + (int)c);
                    return 0;
            }
        }
    }
}
