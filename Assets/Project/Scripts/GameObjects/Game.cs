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
        int Size;
        int Stones;
        int Capstones;

        bool BlackWinsTies;
    }

    public struct Position
    {
        Config cfg;
        byte WhiteStones;
        byte WhiteCapstones;
        byte BlackStones;
        byte BlackCapstones;

        int move;
    }

    public class Game : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            GenerateBoardFromTiles();
        }

        // Update is called once per frame
        void Update()
        {


        }

        GenerateBoardFromTiles(Config cfg, Tile[,] board, int move)
        {

        }

    }

}
