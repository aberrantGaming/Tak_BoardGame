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

    public struct Config
    {
        public int Size;
        public int Stones;
        public int Capstones;

        public bool BlackWinsTies;

        // public Board.Constants BoardConstants;
    }

    public class Game : MonoBehaviour
    {
        List<int> defaultStones = new List<int> { 0, 0, 0, 10, 15, 21, 30, 40, 50 };
        List<int> defaultCapstones = new List<int> { 0, 0, 0, 0, 0, 1, 1, 1, 2 };

        public Game(Config g)
        {
            if (g.Stones == 0)
                g.Stones = defaultStones[g.Size];
            if (g.Capstones == 0)
                g.Capstones = defaultCapstones[g.Size];

            // g.BoardConstants = bitboard.Precompute(int(g.Size));     // TO DO : Translate this line

            Position p = new Position() { };
        }
    }

}
