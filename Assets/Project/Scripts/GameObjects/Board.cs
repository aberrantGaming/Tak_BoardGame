using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.GameEngine
{
    public class Board
    {
        public GameHolder cfg;

        public List<Tile> BoardState { get; private set; }

        List<int> defaultStones = new List<int> { 0, 0, 0, 10, 15, 21, 30, 40, 50 };
        List<int> defaultCapstones = new List<int> { 0, 0, 0, 0, 0, 1, 1, 1, 2 };

        public Board(GameHolder g)
        {
            Debug.Log("Board Size : " + g.BoardSize);

            if (g.StonesCount == 0)
                g.SetStonesCount(defaultStones[g.BoardSize]);

            if (g.CapstonesCount == 0)
                g.SetCapstonesCount(defaultCapstones[g.BoardSize]);

            BoardState = new List<Tile>();

            int numberOfTiles = g.BoardSize * g.BoardSize;
            for (int i = 0; i < numberOfTiles; i++)
            {
                this.BoardState.Add(new Tile());
            }
        }
    }

}
