using Com.aberrantGames.Tak.Utilities;
using System.Collections.Generic;
using UnityEngine;
using Color = System.Byte;
using Piece = System.Byte;
using Slides = System.UInt32;

namespace Com.aberrantGames.Tak.GameEngine
{
    public enum MoveError { ErrOccupied, ErrIllegalSlide, ErrNoCapstone, ErrIllegalOpening }
    public enum MoveType { Pass = 1, PlaceFlat, PlaceStanding, PlaceCapstone, SlideLeft, SlideRight, SlideUp, SlideDown }

    public struct Move
    {
        public int X, Y;
        public MoveType Type;
        public Slides Slide;
    }

    public static class MoveExtensions
    {
        struct Dircnt
        {
            public MoveType d;
            public int c;
        }
        private static List<List<Slides>> slides;

        public static void Init()
        {
            slides = new List<List<Slides>>(10);

            for (int s = 0; s <= 8; s++)
            {
                slides[s] = CalculateSlides(s);
            }
        }
        public static List<Slides> CalculateSlides(int _stack)
        {
            List<Slides> retVal = new List<Slides>();

            for (int i = 1; i <= (byte)_stack; i++)
            {
                retVal.Add(GameEngine.Slide.MakeSlides((int)i));

                foreach (Slides sub in slides[_stack - (int)i])
                {
                    retVal.Add(sub.Prepend(i));
                }
            }

            return retVal;
        }
        public static bool Equal(this Move m, Move rhs)
        {
            if (m.X != rhs.X || m.Y != rhs.Y)
                return false;
            if (m.Type != rhs.Type)
                return false;
            if (!m.IsSlide())
                return true;
            if (m.Slide != rhs.Slide)
                return false;

            return true;
        }
        public static bool IsSlide(this Move m)
        {
            return m.Type >= MoveType.SlideLeft;
        }
        public static CoordPair Dest(this Move m)
        {
            switch (m.Type)
            {
                case MoveType.PlaceFlat:
                case MoveType.PlaceStanding:
                case MoveType.PlaceCapstone:
                    return new CoordPair()
                    {
                        X = m.X,
                        Y = m.Y
                    };
                case MoveType.SlideLeft:
                    return new CoordPair()
                    {
                        X = m.X - m.Slide.Len(),
                        Y = m.Y
                    };
                case MoveType.SlideRight:
                    return new CoordPair()
                    {
                        X = m.X + m.Slide.Len(),
                        Y = m.Y
                    };
                case MoveType.SlideUp:
                    return new CoordPair()
                    {
                        X = m.X,
                        Y = m.Y + m.Slide.Len(),
                    };
                case MoveType.SlideDown:
                    return new CoordPair()
                    {
                        X = m.X,
                        Y = m.Y - m.Slide.Len(),
                    };
                default:
                    Debug.LogError("bad type");
                    return new CoordPair();
            }

        }
        public static Position Move(this Position p, Move m)
        {
            return p.MovePreallocated(m, null);
        }
        public static Position MovePreallocated(this Position p, Move m, Position next)
        {
            if (next == null)
                next = Allocation.Alloc(p);
            else
                Allocation.CopyPosition(p, next);

            next.turn++;
            Piece place = new Piece();
            int dx = 0, dy = 0;
            switch (m.Type)
            {
                case (MoveType.Pass):
                    next.analyze();
                    return next;
                case (MoveType.PlaceFlat):
                    place = Stone.MakePiece(p.ToMove(), Stone.Flat);
                    break;
                case (MoveType.PlaceStanding):
                    place = Stone.MakePiece(p.ToMove(), Stone.Standing);
                    break;
                case (MoveType.PlaceCapstone):
                    place = Stone.MakePiece(p.ToMove(), Stone.Capstone);
                    break;
                case (MoveType.SlideLeft):
                    dx = -1;
                    break;
                case (MoveType.SlideRight):
                    dx = 1;
                    break;
                case (MoveType.SlideUp):
                    dy = 1;
                    break;
                case (MoveType.SlideDown):
                    dy = -1;
                    break;
                default:
                    Debug.LogError("invalid move type");
                    return null;
            }
            if (p.turn < 2)
            {
                if (place.Type() != Stone.Flat)
                {
                    Debug.LogError("illegal opening move");
                    return null;
                }
                place = Stone.MakePiece(place.Color().Flip(), place.Type());
            }
            uint i = (uint)(m.X + m.Y * p.Size());
            if (place != 0)
            {
                if (((p.White | p.Black) & (ulong)(1 << (int)i)) != 0)
                {
                    Debug.LogError("position is occupied");
                    return null;
                }
                byte? stones = new byte?();
                switch(place.Type())
                {
                    case (Stone.Capstone):
                        if (p.ToMove() == Stone.Black)
                            stones = next.BlackCapstones;
                        else
                            stones = next.WhiteCapstones;
                        next.Caps |= (uint)(1 << (int)i);
                        break;
                    case (Stone.Standing):
                        next.Standing |= (uint)(1 << (int)i);
                        goto case (Stone.Flat);
                    case (Stone.Flat):
                        if (place.Color() == Stone.Black)
                            stones = next.BlackStones;
                        else
                            stones = next.WhiteStones;
                        break;
                }
                if (stones <= 0)
                {
                    Debug.LogError("capstone has already been played");
                    return null;
                }
                stones--;
                if (place.Color() == Stone.White)
                    next.White |= (uint)(1 << (int)i);
                else
                    next.Black |= (uint)(1 << (int)i);
                next.Height[i]++;
                next.analyze();
                return next;
            }

            uint ct = 0;
            for (uint it = m.Slide.Iterator(); it.Ok(); it = it.Next())
            {
                int c = it.Elem();
                if (c == 0)
                {
                    Debug.LogError("illegal slide");
                    return null;
                }
                ct += (uint)c;
            }
            if ((ct > (uint)p.cfg.Size) || (ct < 1) || (ct > (uint)p.Height[i]))
            {
                Debug.LogError("illegal slide");
                return null;
            }
            if ((p.ToMove() == Stone.White) && ((p.White&(uint)(1 << (int)i)) == 0))
            {
                Debug.Log("illegal slide");
                return null;
            }
            if ((p.ToMove() == Stone.Black) && ((p.White&(uint)(1 << (int)i)) == 0))
            {
                Debug.LogError("illegal slide");
                return null;
            }

            Piece top = p.Top((int)m.X, (int)m.Y);
            ulong stack = p.Stacks[i] << 1;
            if (top.Color() == Stone.Black)
                stack |= 1;

            next.Caps &= ~(uint)(1 << (int)i);
            next.Standing &= ~(uint)(1 << (int)i);
            if ((uint)next.Height[i] == ct)
            {
                next.White &= ~(uint)(1 << (int)i);
                next.Black &= ~(uint)(1 << (int)i);
            }
            else
            {
                if ((stack & (uint)(1 << (int)ct)) == 0)
                {
                    next.White |= (uint)(1 << (int)i);
                    next.Black &= ~(uint)(1 << (int)i);
                }
                else
                {
                    next.Black |= (uint)(1 << (int)i);
                    next.White &= ~(uint)(1 << (int)i);
                }
            }

            // next.hash ^= next.HashAt(i);
            next.Stacks[i] >>= (int)ct;
            next.Height[i] -= (uint)ct;
            // next.hash ^= next.HashAt(i);

            int x = m.X, y = m.Y;
            for (uint it = m.Slide.Iterator(); it.Ok(); it = it.Next())
            {
                uint c = (uint)it.Elem();
                x += dx;
                y += dy;
                if (x < 0 || x >= (int)next.cfg.Size || y < 0 || y >= (int)next.cfg.Size)
                {
                    Debug.LogError("illegal slide");
                    return null;
                }
                if ((int)c < 1 || (uint)c > ct)
                {
                    Debug.LogError("illegal slide");
                    return null;
                }
                i = (uint)(x + y * p.Size());
                if ((next.Caps & (uint)(1 << (int)i)) != 0)
                {
                    Debug.LogError("illegal slide");
                    return null;
                }
                else if ((next.Standing & (uint)(1<<(int)i)) != 0)
                {
                    if (ct != 1 || top.Type() != Stone.Capstone)
                    {
                        Debug.LogError("illegal slide");
                        return null;
                    }
                    next.Standing &= ~(uint)(1 << (int)i);
                }

                // next.hash ^= next.HashAt(i);
                if ((next.White & (uint)(1 << (int)i)) != 0)
                    next.Stacks[i] <<= 1;
                else if ((next.Black & (uint)(1 << (int)i)) != 0)
                {
                    next.Stacks[i] <<= 1;
                    next.Stacks[i] |= 1;
                }                
                ulong drop = ((stack >> (int)(ct - (c-1))) & (uint)((1 << (int)(c -1 )) -1));
                next.Stacks[i] = ((next.Stacks[i] << (int)(c - 1)) | drop);
                next.Height[i] += c;
                // next.hash ^= next.HashAt(i);

                if ((stack & (uint)(1<<(int)(ct-c))) != 0)
                {
                    next.Black |= (uint)(1 << (int)i);
                    next.White &= ~(uint)(1 << (int)i);
                }
                else
                {
                    next.White |= (uint)(1 << (int)i);
                    next.Black &= ~(uint)(1 << (int)i);
                }

                ct -= (uint)c;
                if (ct == 0)
                {
                    switch(top.Type())
                    {
                        case (Stone.Capstone): next.Caps |= (uint)(1 << (int)i); break;
                        case (Stone.Standing): next.Standing |= (uint)(1 << (int)i); break;
                    }
                }
            }

            next.analyze();
            return next;
        }
        public static List<Move> AllMoves(this Position p, List<Move> moves)
        {
            Color next = p.ToMove();
            bool cap = false;

            if (next == p.White)
                cap = p.WhiteCapstones > 0;
            else
                cap = p.BlackCapstones > 0;

            for (int x = 0; x < p.cfg.Size; x++)
            {
                for (int y = 0; y < p.cfg.Size; y++)
                {
                    uint i = (uint)(y * p.cfg.Size + x);

                    if (p.Height[i] == 0)
                    {
                        moves.Add(new Move() { X = x, Y = y, Type = MoveType.PlaceFlat });
                        if (p.turn >= 2)
                        {
                            moves.Add(new Move() { X = x, Y = y, Type = MoveType.PlaceStanding });
                            if (cap)
                                moves.Add(new Move() { X = x, Y = y, Type = MoveType.PlaceCapstone });
                        }
                        continue;
                    }

                    if (p.turn < 2)
                        continue;

                    if ((next == Stone.White) && (p.White & (uint)(1 << (int)i)) == 0)
                        continue;
                    else if ((next == Stone.Black) && (p.Black & (uint)(1 << (int)i)) == 0)
                        continue;

                    Dircnt[] dirs = new Dircnt[4];
                    dirs[0] = new Dircnt() { d = MoveType.SlideLeft, c = x };
                    dirs[0] = new Dircnt() { d = MoveType.SlideRight, c = x - 1 };
                    dirs[0] = new Dircnt() { d = MoveType.SlideDown, c = y };
                    dirs[0] = new Dircnt() { d = MoveType.SlideUp, c = y - 1 };

                    for (int d = 0; d < dirs.Length; d++)
                    {
                        uint h = p.Height[i];
                        if (h > (uint)p.cfg.Size)
                            h = (uint)p.cfg.Size;

                        Slides mask = Slide.MakeSlides((1 << (4 * (int)dirs[d].c) - 1));

                        foreach (Slides s in slides[(int)h])
                        {
                            if (s == mask)
                                moves.Add(new Move() { X = x, Y = y, Type = dirs[d].d, Slide = s });
                        }
                    }
                }
            }
            return moves;
        }
    }
}