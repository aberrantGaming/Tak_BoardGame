using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.GameEngine
{
    public enum MoveType { PlaceFlat, PlaceStanding, PlaceCapstone, SlideLeft, SlideRight, SlideUp, SlideDown }

    public enum MoveError { ErrOccupied, ErrIllegalSlide, ErrNoCapstone, ErrIllegalOpening }
    
    public struct Move
    {
        int X, Y;
        public MoveType MoveType;
        Object Slides;                          // TO DO:  Define Slides object

        public bool Equals(Move rhs)
        {
            if (X != rhs.X || Y != rhs.Y)
                return false;

            if (MoveType != rhs.MoveType)
                return false;

            if (!IsSlide())
                return true;

            //if (Slides != rhx.Slides)
            //    return false;

            return true;
        }

        public bool IsSlide()
        {
            return ((int)MoveType >= 4);
        }

        public int[] Destination()
        {
            int[] retVal = new int[]{};

            switch (MoveType)
            {
                case (MoveType.PlaceFlat):
                case (MoveType.PlaceStanding):
                case (MoveType.PlaceCapstone):
                    retVal = new int[] { X, Y };
                    break;
                // TODO : Uncomment after defining Slides object
                //case (MoveType.SlideLeft):
                //    retVal = new int[] { X - Slides.Len(), Y };
                //    break;
                //case (MoveType.SlideRight):
                //    retVal = new int[] { X + Slides.Len(), Y };
                //    break;
                //case (MoveType.SlideUp):
                //    retVal = new int[] { X, Y + Slides.Len() };
                //    break;
                //case (MoveType.SlideDown):
                //    retVal = new int[] { X, Y - Slides.Len() };
                //    break;
            }
            return retVal;
        }
        

    }
}
