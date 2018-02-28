using Com.aberrantGames.Tak.Utilities;
using System.Collections.Generic;
using UnityEngine;
using Color = System.Byte;

namespace Com.aberrantGames.Tak.GameEngine
{
    public enum MoveError { ErrOccupied, ErrIllegalSlide, ErrNoCapstone, ErrIllegalOpening }
    public enum MoveType { Pass = 1, PlaceFlat, PlaceStanding, PlaceCapstone, SlideLeft, SlideRight, SlideUp, SlideDown }

    public struct Move
    {
        public int X, Y;
        public MoveType Type;
        public Slide Slides;

        private List<Slide[]> slides;

        public void Init() { }
        public Slide[] CalculateSlides(int stack) { }
    }

    public static class MoveExtensions
    {
        struct Dircnt
        {
            public MoveType d;
            public int c;
        }

        public static bool Equal(this Move m, Move rhs) { }

        public static bool IsSlide(this Move m) { }

        public static CoordPair Dest(this Move m) { }

        public static Position Move(this Position p, Move m) { }

        public static Position MovePreallocated(this Position p, Move m, Position next) { }

        public static Move[] AllMoves(this Position p, Move[] moves) { }
    }
}


//    public struct Move
//    {
//        public MoveType Type;
//        public Slide Slide;
//        public int X, Y;

//        private List<List<Slide>> possibleSlides;

//        public Move(int x, int y, MoveType type, Slide slide)
//        {
//            X = x;
//            Y = y;
//            Type = type;
//            Slide = slide;

//            // initiate the list of possible slides
//            possibleSlides = new List<List<Slide>>(10);
//            for (int i = 0; i <= 8; i++)
//            {
//                possibleSlides[i] = CalculateSlides(i);
//            }
//        }

//        private List<Slide> CalculateSlides(int _stack)
//        {
//            List<Slide> retVal = new List<Slide>();
//            for (int i = (byte)1; i < (byte)_stack; i++)
//            {
//                retVal.Add(new Slide(Slide.MkSlides((int)i)));
//                foreach (List<Slide> sub in possibleSlides)
//                {
//                    foreach (Slide subSlide in sub)
//                    {
//                        retVal.Add(new Slide(subSlide.Prepend(i)));
//                    }
//                }
//            }
//            return retVal;
//        }
//    }

//    public static class MoveHelper
//    {
//        #region Extension Properties

//        public static CoordPair GetDest(this Move m)
//        {
//            switch (m.Type)
//            {
//                case MoveType.PlaceFlat:
//                case MoveType.PlaceStanding:
//                case MoveType.PlaceCapstone:
//                    return new CoordPair()
//                    {
//                        X = m.X,
//                        Y = m.Y
//                    };
//                case MoveType.SlideLeft:
//                    return new CoordPair()
//                    {
//                        X = m.X - m.Slide.Len,
//                        Y = m.Y
//                    };
//                case MoveType.SlideRight:
//                    return new CoordPair()
//                    {
//                        X = m.X + m.Slide.Len,
//                        Y = m.Y
//                    };
//                case MoveType.SlideUp:
//                    return new CoordPair()
//                    {
//                        X = m.X,
//                        Y = m.Y + m.Slide.Len,
//                    };
//                case MoveType.SlideDown:
//                    return new CoordPair()
//                    {
//                        X = m.X,
//                        Y = m.Y - m.Slide.Len,
//                    };
//                default:
//                    Debug.LogError("bad type");
//                    return new CoordPair();
//            }
//        }

//        public static bool IsSlide(this Move m)
//        {
//            return m.Type >= MoveType.SlideLeft;
//        }

//        public static bool Equal(this Move m, Move _rhs)
//        {
//            if (m.X != _rhs.X || m.Y != _rhs.Y)
//                return false;
//            if (m.Type != _rhs.Type)
//                return false;
//            if (!m.IsSlide())
//                return true;
//            if (!m.Slide.Equals(_rhs.Slide))
//                return false;

//            return true;
//        }

//        #region Extension Methods

//        public static List<Move> AllMoves(this Position p, List<Move> _moves)
//        {
//            Color next = p.ToMove();
//            bool cap = false;

//            if (next == p.White)
//                cap = p.WhiteCapstones > 0;
//            else
//                cap = p.BlackCapstones > 0;

//            for (int x = 0; x < p.cfg.Size; x++)
//            {
//                for (int y = 0; y < p.cfg.Size; y++)
//                {
//                    uint i = (uint)(y * p.cfg.Size + x);

//                    if (p.Height[i] == 0)
//                    {
//                        _moves.Add(new Move() { X = x, Y = y, Type = MoveType.PlaceFlat });
//                        if (p.turn >= 2)
//                        {
//                            _moves.Add(new Move() { X = x, Y = y, Type = MoveType.PlaceStanding });
//                            if (cap)
//                                _moves.Add(new Move() { X = x, Y = y, Type = MoveType.PlaceCapstone });
//                        }
//                        continue;
//                    }

//                    if (p.turn < 2)
//                        continue;

//                    if ((next == Stone.White) && (p.White & (uint)(1 << (int)i)) == 0)
//                        continue;
//                    else if ((next == Stone.Black) && (p.Black & (uint)(1 << (int)i)) == 0)
//                        continue;

//                    Dircnt[] dirs = new Dircnt[4];
//                    dirs[0] = new Dircnt() { d = MoveType.SlideLeft, c = x };
//                    dirs[0] = new Dircnt() { d = MoveType.SlideRight, c = x - 1 };
//                    dirs[0] = new Dircnt() { d = MoveType.SlideDown, c = y };
//                    dirs[0] = new Dircnt() { d = MoveType.SlideUp, c = y - 1 };

//                    for (int d = 0; d < dirs.Length; d++)
//                    {
//                        uint h = p.Height[i];
//                        if (h > (uint)p.cfg.Size)
//                            h = (uint)p.cfg.Size;

//                        Slide mask = new Slide((1 << (4 * (int)dirs[d].c) - 1));

//                        //foreach (Slide s in slides[(int)h])
//                        //{
//                        //    if (s == mask)
//                        //        _moves.Add(new GameEngine.Move() { X = x, Y = y, Type = dirs[d].d, Slide = s });
//                        //}
//                    }
//                }
//            }
//            return _moves;
//        }

//        #endregion
//        #endregion
//    }
//}

