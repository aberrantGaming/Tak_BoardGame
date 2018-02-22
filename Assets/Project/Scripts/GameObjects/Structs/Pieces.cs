using UnityEngine;
using Color = System.Byte;
using Piece = System.Byte;
using Type = System.Byte;

namespace Com.aberrantGames.Tak.GameEngine
{
    public struct Pieces
    {
        #region Constant Variables

        public const Color White = 1 << 7;
        public const Color Black = 1 << 6;
        public const Color NoColor = 0;

        public const Type Flat = 1;
        public const Type Standing = 2;
        public const Type Capstone = 3;

        private const byte colorMask = 3 << 6;
        private const byte typeMask = 1 << 2 - 1;

        #endregion

        #region Private Variables

        Piece p;

        #endregion

        #region Public Methods

        public Piece MakePiece(Color _color, Type _kind)
        {
            return (Piece)((byte)_color | (byte)_kind);
        }

        #endregion

        #region Piece Methods

        public Color Color()
        {
            return (Color)((byte)p & colorMask);
        }

        public Type Type()
        {
            return (Type)((byte)p & typeMask);
        }

        public bool IsRoad()
        {
            return Type() == Flat || Type() == Capstone;
        }

        public string String()
        {
            string n = "";
            if (Color() == White)
                n = "W";
            else
                n = "B";
            switch (Type())
            {
                case (Capstone): n += "C"; break;
                case (Standing): n += "S"; break;
            }
            return n;
        }

        #endregion

        #region Color Methods

        public string ColorString()
        {
            Color c = Color();
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

        public Color Flip()
        {
            Color c = Color();
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

        #endregion
    }
}
