using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.GameEngine
{
    public enum WinReason { ROAD_WIN, FLATS_WIN, RESIGNATION }

    public struct WinDetails
    {
        bool over;
        WinReason reason;
        Color winner;
        int WhiteFlats;
        int BlackFlats;
    }

    public class Game : MonoBehaviour
    {
        public 

        public Game()
        {

        }

    }

}
