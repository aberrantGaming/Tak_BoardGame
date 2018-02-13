using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    [CreateAssetMenu(menuName = "Collectables/Gameboard")]
    public class Board : ScriptableObject
    {

        #region Public Properties

        public int Pieces { get { return NumberOfNormalPieces; } }
        public int Capstones { get { return NumberOfCapstones; } }

        #endregion

        #region Public Setters
        
        [SerializeField] private int boardSize = 5;
        public int BoardSize { get { return boardSize; } }

        [SerializeField] private string levelName = "Standard";
        public string LevelName { get { return levelName; } }

        [SerializeField] private List<Tile> tiles;
        public List<Tile> Tiles { get { return tiles; } }

        #endregion

        #region Public Variables

        public string GameboardName = "_BoardName";
        public string GameboardDesc = "_BoardDescription";

        public GameObject GameboardPrefab;

        #endregion


        private void Awake()
        {
            PopulateTilesArray();
        }

        private void PopulateTilesArray()
        {
            tiles = new List<Tile>();
            int volume = boardSize * boardSize;

            for (int i = 0; i < volume; i++)
            {
                tiles.Add(new Tile());
            }
        }

        private int NumberOfNormalPieces
        {
            get
            {
                if (boardSize == 3)
                    return 10;
                else if (boardSize == 4)
                    return 15;
                else if (boardSize == 5)
                    return 21;
                else if (boardSize == 6)
                    return 30;
                else if (boardSize == 7)
                    return 40;
                else if (boardSize == 8)
                    return 50;
                else
                    return 0;
            }
        }

        private int NumberOfCapstones
        {
            get
            {
                if (boardSize == 5)
                    return 1;
                else if (boardSize == 6)
                    return 1;
                else if (boardSize == 7)
                    return 2;
                else if (boardSize == 8)
                    return 2;
                else
                    return 0;
            }
        }

    }
}