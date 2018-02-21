using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.GameEngine
{
    public enum StoneColor { NULL, LIGHT, DARK }
    public enum StoneType { NULL, FLAT, STANDING, CAPSTONE }

    public class Stone : MonoBehaviour
    {
        public StoneColor Color { get; private set; }
        public StoneType Type { get; private set; }

        // Handle Instantiation from within the constructor
        public Stone(StoneColor _color, StoneType _type)
        {
            Color = _color;
            Type = _type;
        }
        public Stone()
        {
            Color = StoneColor.NULL;
            Type = StoneType.NULL;
        }

        public bool IsRoad()
        {
            return (Type.Equals(StoneType.FLAT) || Type.Equals(StoneType.CAPSTONE));
        }

        public string Notation()
        {
            string c = "";

            if (Color == StoneColor.LIGHT)
                c = "W";
            else
                c = "B";

            switch (Type) {
                case StoneType.CAPSTONE: c += "C"; break;
                case StoneType.STANDING: c += "S"; break;                
            }

            return c;
        }

        internal StoneColor FlipColor()
        {
            if (Color == StoneColor.NULL)
                return Color;

            if (Color == StoneColor.LIGHT)
                return StoneColor.DARK;
            else
                return StoneColor.LIGHT;
        }


    }
}
