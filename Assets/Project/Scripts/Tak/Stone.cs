using UnityEngine;
using Color = System.Byte;
using Piece = System.Byte;
using Type = System.Byte;

namespace Com.aberrantGames.Tak.GameEngine
{
    public class Stone
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

        protected Piece p;

        #endregion

        #region Properties

        public Piece Piece { get { return p; } set { p = value; } }

        public Color Color { get { return (Color)(Piece & colorMask); } private set { } }
        public Type Type { get { return (Type)(Piece & typeMask); } private set { } }
        public bool IsRoad { get { return Type == Flat || Type == Capstone; } private set { } }
        public string Notation { get { return NotationString(); } private set { } }
        public string ColorName { get { return ColorString(); } private set { } }

        #endregion

        #region Constructors

        public Stone(Piece _piece)
        {
            Piece = _piece;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Used to combine a Color and Type byte into a Piece
        /// </summary>
        /// <param name="_color"></param>
        /// <param name="_kind"></param>
        /// <returns>Piece</returns>
        public static Piece MakePiece(Color _color, Type _kind)
        {
            return (Piece)((byte)_color | (byte)_kind);
        }

        /// <summary>
        /// Used to get the color opposite to this stones color
        /// </summary>
        /// <returns>Color</returns>
        public Color FlipColor()
        {
            Color c = Color;
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

        #region Private Methods

        protected string NotationString()
        {
            string n = "";
            if (Color == White)
                n = "W";
            else
                n = "B";
            switch (Type)
            {
                case (Capstone): n += "C"; break;
                case (Standing): n += "S"; break;
            }
            return n;
        }

        protected string ColorString()
        {
            Color c = Color;
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

        #endregion
    }
}
