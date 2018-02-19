using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.GameEngine
{
    public class Board
    {
        #region Public Variables

        public GameHolder config = new GameHolder();

        public List<Tile> BoardState;// { get; private set; }

        #endregion

        #region Private Variables

        List<int> defaultStones = new List<int> { 0, 0, 0, 10, 15, 21, 30, 40, 50 };
        List<int> defaultCapstones = new List<int> { 0, 0, 0, 0, 0, 1, 1, 1, 2 };

        #endregion

        #region Constructor

        public Board(GameHolder g)
        {
            if (g != null)
                config = g;

            if (config.StonesCount == 0)
                config.SetStonesCount(defaultStones[config.BoardSize]);

            if (config.CapstonesCount == 0)
                config.SetCapstonesCount(defaultCapstones[config.BoardSize]);

            BoardState = new List<Tile>();

            int numberOfTiles = config.BoardSize * config.BoardSize;
            for (int i = 0; i < numberOfTiles; i++)
            {
                this.BoardState.Add(new Tile());
            }
        }

        #endregion

    }

}
