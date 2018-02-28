using Com.aberrantGames.Tak.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.GameEngine
{
    public enum MoveError { ErrOccupied, ErrIllegalSlide, ErrNoCapstone, ErrIllegalOpening }
    public enum MoveType { Pass = 1, PlaceFlat, PlaceStanding, PlaceCapstone, SlideLeft, SlideRight, SlideUp, SlideDown }

    public struct Move
    {
        #region Variables

        public MoveType Type;
        public Slide Slide;
        public int X, Y;
        
        public static List<List<Slide>> possibleSlides;

        #endregion

        #region Properties

        public bool IsSlide
        {
            get { return Type >= MoveType.SlideLeft; }
            private set { }
        }

        public CoordPair Dest
        {
            get
            {
                switch (Type)
                {
                    case MoveType.PlaceFlat:
                    case MoveType.PlaceStanding:
                    case MoveType.PlaceCapstone:
                        return new CoordPair()
                        {
                            X = X,
                            Y = Y
                        };
                    case MoveType.SlideLeft:
                        return new CoordPair()
                        {
                            X = X - Slide.Len,
                            Y = Y
                        };
                    case MoveType.SlideRight:
                        return new CoordPair()
                        {
                            X = X + Slide.Len,
                            Y = Y
                        };
                    case MoveType.SlideUp:
                        return new CoordPair()
                        {
                            X = X,
                            Y = Y + Slide.Len,
                        };
                    case MoveType.SlideDown:
                        return new CoordPair()
                        {
                            X = X,
                            Y = Y - Slide.Len,
                        };
                    default:
                        Debug.LogError("bad type");
                        return new CoordPair();
                }
            }
        }

        #endregion

        #region Constructors

        public Move(int _x, int _y, MoveType _type, Slide _slide)
        {
            X = _x;
            Y = _y;
            Type = _type;
            Slide = _slide;
            
            // initiate the list of possible slides
            possibleSlides = new List<List<Slide>>(10);
            for (int i = 0; i <= 8; i++)
            {
                possibleSlides[i] = CalculateSlides(i);
            }
        }

        #endregion

        public bool Equal(Move _rhs)
        {
            if (X != _rhs.X || Y != _rhs.Y)
                return false;
            if (Type != _rhs.Type)
                return false;
            if (!IsSlide)
                return true;
            if (!Slide.Equals(_rhs.Slide))
                return false;

            return true;
        }
        
        #region Private Methods

        private List<Slide> CalculateSlides(int _stack)
        {
            List<Slide> retVal = new List<Slide>();
            for (int i = (byte)1; i < (byte)_stack; i++)
            {
                retVal.Add(new Slide(Slide.MkSlides((int)i)));
                foreach (List<Slide> sub in possibleSlides)
                {
                    foreach (Slide subSlide in sub)
                    {
                        retVal.Add(new Slide(subSlide.Prepend(i)));
                    }
                }
            }

            return retVal;
        }

        #endregion
    }
}