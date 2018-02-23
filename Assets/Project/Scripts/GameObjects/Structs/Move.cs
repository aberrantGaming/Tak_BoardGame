﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slides = System.Int32;

namespace Com.aberrantGames.Tak.GameEngine
{
    //TODO : Refactor this.

    //public enum MoveType { Pass, PlaceFlat, PlaceStanding, PlaceCapstone, SlideLeft, SlideRight, SlideUp, SlideDown }

    //public enum MoveError { ErrOccupied, ErrIllegalSlide, ErrNoCapstone, ErrIllegalOpening }

    //public bool Equals(Move rhs)
    //{
    //    if (X != rhs.X || Y != rhs.Y)
    //        return false;

    //    if (MoveType != rhs.MoveType)
    //        return false;

    //    if (!IsSlide())
    //        return true;

    //    //if (Slides != rhs.Slides)
    //    //    return false;

    //    return true;
    //}

    //public bool IsSlide()
    //{
    //    return ((int)MoveType >= 4);
    //}

    //public int[] Destination()
    //{
    //    int[] _retVal;

    //    switch (MoveType)
    //    {
    //        case (MoveType.PlaceFlat):
    //        case (MoveType.PlaceStanding):
    //        case (MoveType.PlaceCapstone):
    //            _retVal = new int[] {
    //                X,
    //                Y
    //            };
    //            break;

    //        case (MoveType.SlideLeft):
    //            _retVal = new int[] {
    //                X - Slides.Length,
    //                Y
    //            };
    //            break;

    //        case (MoveType.SlideRight):
    //            _retVal = new int[] {
    //                X + Slides.Length,
    //                Y
    //            };
    //            break;

    //        case (MoveType.SlideUp):
    //            _retVal = new int[] {
    //                X,
    //                Y + Slides.Length
    //            };
    //            break;

    //        case (MoveType.SlideDown):
    //            _retVal = new int[] {
    //                X,
    //                Y - Slides.Length
    //            };
    //            break;

    //        default:
    //            _retVal = new int[] { };
    //            break;
    //    }

    //    return _retVal;
    //}

    //public Position? MoveTo(Move m)
    //{
    //    return MovePreallocated(m, null);
    //}

    //public Position? MovePreallocated(Move m, Position? next)
    //{
    //    Stone placedStone = new Stone();

    //    Position newPosition;
    //    int dX, dY;

    //    if (next == null)
    //        newPosition = new Position();
    //    else
    //        newPosition = (Position)next;

    //    newPosition.CopyPosition(new Position(), newPosition);
    //    newPosition.move++;

    //    switch (m.MoveType)
    //    {
    //        case MoveType.Pass:
    //            newPosition.analyze();
    //            return newPosition;

    //        case MoveType.PlaceFlat:
    //            placedStone = new Stone(newPosition.ToMove(), StoneType.FLAT);
    //            break;

    //        case MoveType.PlaceStanding:
    //            placedStone = new Stone(newPosition.ToMove(), StoneType.STANDING);
    //            break;

    //        case MoveType.PlaceCapstone:
    //            placedStone = new Stone(newPosition.ToMove(), StoneType.CAPSTONE);
    //            break;

    //        case MoveType.SlideLeft:
    //            dX = -1;
    //            break;

    //        case MoveType.SlideRight:
    //            dX = 1;
    //            break;

    //        case MoveType.SlideUp:
    //            dY = -1;
    //            break;

    //        case MoveType.SlideDown:
    //            dY = 1;
    //            break;

    //        default:
    //            Debug.LogWarning("Invalid Move Type");
    //            return null;
    //    }

    //    // Flip the stones color for the first two turns
    //    if (newPosition.move < 2)
    //    {
    //        if (placedStone.Type != StoneType.FLAT)
    //        {
    //            ThrowWarning(MoveError.ErrIllegalOpening);
    //            return null;
    //        }
    //        placedStone = new Stone(placedStone.FlipColor(), placedStone.Type);
    //    }

    //    int i = m.X + m.Y * newPosition.cfg.BoardSize;
    //    if (placedStone != null)
    //    {
    //        if ((newPosition.Light | newPosition.Dark) + (int)(1 << i) != 0)
    //        {
    //            ThrowWarning(MoveError.ErrOccupied);
    //            return null;
    //        }

    //        byte? stones = new byte?();
    //        switch (placedStone.Type)
    //        {
    //            case StoneType.CAPSTONE:
    //                if (newPosition.ToMove() == StoneColor.DARK)
    //                    stones += newPosition.DarkCapstones;
    //                else
    //                    stones += newPosition.LightCapstones;
    //                newPosition.Caps |= (1 << i);
    //                break;

    //            case StoneType.STANDING:
    //                newPosition.Standing = newPosition.Standing | (1 << i);
    //                goto case (StoneType.FLAT);

    //            case StoneType.FLAT:
    //                if (placedStone.Color == StoneColor.DARK)
    //                    stones += newPosition.DarkStones;
    //                else
    //                    stones += newPosition.LightStones;
    //                break;
    //        }
    //        if (stones <= 0)
    //        {
    //            ThrowWarning(MoveError.ErrNoCapstone);
    //            return null;
    //        }

    //        stones--;
    //        if (placedStone.Color == StoneColor.LIGHT)
    //            newPosition.Light |= (1 << i);
    //        else
    //            newPosition.Dark |= (1 << i);

    //        newPosition.Height[i]++;
    //        newPosition.analyze();

    //        return newPosition;
    //    }

    //    int ct = 0;
    //    foreach (int elem in Slides.Slide)
    //    {
    //        if (elem == 0)
    //        {
    //            ThrowWarning(MoveError.ErrIllegalSlide);
    //            return null;
    //        }
    //        ct++;
    //    }

    //    if ((ct > (int)newPosition.cfg.BoardSize) || (ct < 1) || (ct > (int)newPosition.Height[i]))
    //    {
    //        ThrowWarning(MoveError.ErrIllegalSlide);
    //        return null;
    //    }
    //    if ((newPosition.ToMove() == StoneColor.LIGHT) && (newPosition.Light + (1 << i) == 0))
    //    {
    //        ThrowWarning(MoveError.ErrIllegalSlide);
    //        return null;
    //    }
    //    if ((newPosition.ToMove() == StoneColor.DARK) && (newPosition.Dark + (1 << i) == 0))
    //    {
    //        ThrowWarning(MoveError.ErrIllegalSlide);
    //        return null;
    //    }

    //    Stone top = newPosition.Top(m.X, m.Y);
    //    int stack = newPosition.Stacks[i] << 1;
    //    if (top.Color == StoneColor.DARK)
    //    {
    //        stack |= 1;
    //    }

    //    newPosition.Caps = newPosition.Caps ^ (1 << i);
    //    newPosition.Standing = newPosition.Caps ^ (1 << i);
    //    if ((int)newPosition.Height[i] == ct)
    //    {
    //        newPosition.Light = newPosition.Light ^ (1 << i);
    //        newPosition.Dark = newPosition.Dark ^ (1 << i);
    //    }
    //    else
    //    {
    //        if (stack & (1 << i))
    //        {

    //        }
    //    }

    //    newPosition.analyze();
    //    return newPosition;
    //}

    //#region Internal Methods

    //internal void ThrowWarning(MoveError error, string customErrMsg = "")
    //{
    //    switch (error)
    //    {
    //        case MoveError.ErrIllegalOpening:
    //            Debug.LogWarning("illegal opening move");
    //            break;
    //        case MoveError.ErrOccupied:
    //            Debug.LogWarning("position is occupied");
    //            break;
    //        case MoveError.ErrIllegalSlide:
    //            Debug.LogWarning("illegal slide");
    //            break;
    //        case MoveError.ErrNoCapstone:
    //            Debug.LogWarning("out of capstones");
    //            break;

    //        default:
    //            Debug.LogWarning(customErrMsg);
    //            break;
    //    }
    //}

    //#endregion
    
    public enum MoveType { Pass, PlaceFlat, PlaceStanding, PlaceCapstone, SlideLeft, SlideRight, SlideUp, SlideDown }
    public enum MoveError { ErrOccupied, ErrIllegalSlide, ErrNoCapstone, ErrIllegalOpening }

    public struct MoveDetail
    {
        MoveType Type;
        Slides Slides;
        int X, Y;
    }

    public class Move
    {
        #region Variables

        public Slide[][] Slides { get; private set; }

        public Position? p { get; private set; }
        public MoveDetail m { get; private set; }

        #endregion

        #region Properties

        public bool Equal() { }
        public bool IsSlide() { }
        public int[] Dest() { }

        #endregion

        #region Constructors

        public Move() { }

        #endregion

        #region Public Methods

        public Position? MoveTo(MoveDetail _move) { }
        public Position? MovePreallocated(MoveDetail _move, Position? _next) { }
        public Move[] AllMoves(Move[] _moves) { }

        #endregion
        
        #region Private Methods

        private Slides[] CalculateSlides(int _stack) { }

        #endregion
    }
}
