using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.GameEngine
{
    public class Board : MonoBehaviour
    {
        #region Public Variables

        public GameHolder config = new GameHolder();

        public List<Tile> BoardState;// { get; private set; }
        
        #endregion

        #region Private Variables

        List<int> defaultStones = new List<int> { 0, 0, 0, 10, 15, 21, 30, 40, 50 };
        List<int> defaultCapstones = new List<int> { 0, 0, 0, 0, 0, 1, 1, 1, 2 };
        
        private IDictionary<string, Transform> PrefabDictionary;

        #endregion

        #region Constructor

        public Board(GameHolder g, Dictionary<string, Transform> _prefabs)
        {
            if (g != null)
                config = g;

            if (_prefabs != null)
                PrefabDictionary = _prefabs;

            Init();
        }

        void Init()
        {

            if (config.StonesCount == 0)
                config.SetStonesCount(defaultStones[config.BoardSize]);

            if (config.CapstonesCount == 0)
                config.SetCapstonesCount(defaultCapstones[config.BoardSize]);


            BuildBoardFoundation();
            BuildBoardTiles();
        }

        void BuildBoardFoundation()
        {
            Transform boardFoundation =
                Instantiate(PrefabDictionary["BoardFoundation"], new Vector3(0,0,0), Quaternion.identity) as Transform;

            int boardSize = config.BoardSize;

            boardFoundation.transform.localScale =
                new Vector3(boardSize, boardFoundation.localScale.y, boardSize);
        }

        void BuildBoardTiles()
        {

            BoardState = new List<Tile>();

            bool toggle = true;
            int numberOfTiles = config.BoardSize * config.BoardSize;
            for (int i = 0; i < numberOfTiles; i++)
            {
                if (toggle)
                    this.BoardState.Add(new Tile(PrefabDictionary["TileLight"]));
                else
                    this.BoardState.Add(new Tile(PrefabDictionary["TileDark"]));

                toggle = !toggle;                
            }
            
            for (int row = 0; row < config.BoardSize; row++)
            {
                for (int col = 0; col < config.BoardSize; col++)
                {
                    int elementIndex = CalculateElementIndex(row, col, config.BoardSize);
                    Tile element = BoardState[elementIndex];

                    Transform elementPrefab = BoardState[elementIndex].tilePrefab;

                    Vector3 elementPosition = CalculateElementPosition(row, col);

                    Transform elementTransform =
                        Instantiate(elementPrefab, elementPosition, Quaternion.identity) as Transform;
                }
            }
        }

        int CalculateElementIndex(int row, int col, int boardSize)
        {
            return row * boardSize + col;
        }

        Vector3 CalculateElementPosition(int row, int col)
        {
            float x = row; //- offset;
            float z = col; //- offset;
            return new Vector3(x, 1, z);
        }

        #endregion

    }

}
