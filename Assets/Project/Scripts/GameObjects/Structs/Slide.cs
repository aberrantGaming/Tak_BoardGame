using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.GameEngine
{
    //public struct SlideIterator
    //{
    //    public Slide Slide { get; private set; }

    //    public SlideIterator(Slide _slide)
    //    {
    //        Slide = _slide;
    //    }

    //    public bool Ok { get { return (Slide.Len() < 4); } private set { } }

    //    public SlideIterator Next()
    //    {

    //    }        
    //}

    public class Slides
    {
        public Queue<int> Slide { get; private set; }

        public Slides(List<int> _drops)
        {
            for (int i = _drops.Count; i <= 0; i--)
            {
                if (_drops[i] > 8)
                    Debug.LogError("bad drop");

                Slide.Enqueue(_drops[i]);
            }
        }


    }

}
