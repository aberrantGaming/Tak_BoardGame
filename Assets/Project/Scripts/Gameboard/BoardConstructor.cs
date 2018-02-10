using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    public class BoardConstructor : MonoBehaviour
    {
        #region Variables


        public Transform Prefab_Foundation;
        public Transform Prefab_Tile;
        public Color Player_Dark;
        public Color Player_Light;

        private const float elementSize = 1f;
        private float offset;
        private Board Gameboard;

        #endregion

        void Start()
        {

            // Set Gameboard according to selected difficulty
            Gameboard = Utils.ReadSelectedGameboard();

            Debug.Log("Level Name: " + Gameboard.LevelName
                + ", board size is: " + Gameboard.BoardSize);

            offset = (Gameboard.BoardSize / 2f) - (elementSize / 2f);     // adjust the offset for the difference in size between the board and tiles.

            BuildBoardFoundation();
            BuildBoardTiles();
        }

        void BuildBoardFoundation()
        {
            Transform boardFoundation =
                Instantiate(Prefab_Foundation, Vector3.zero, Quaternion.identity) as Transform;

            int boardSize = Gameboard.BoardSize;

            boardFoundation.transform.localScale =
                new Vector3(boardSize, boardFoundation.localScale.y, boardSize);
        }

        void BuildBoardTiles()
        {
            bool toggle = true;
            int boardSize = Gameboard.BoardSize;
            List<Tile> tiles = Gameboard.Tiles;

            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    int elementIndex = CalculateElementIndex(row, col, boardSize);
                    Tile element = tiles[elementIndex];

                    Transform elementPrefab = Prefab_Tile;

                    Vector3 elementPosition = CalculateElementPosition(row, col);

                    Transform elementTransform =
                        Instantiate(elementPrefab, elementPosition, Quaternion.identity) as Transform;

                    if (toggle)
                        elementTransform.GetComponentInChildren<Renderer>().material.color = Player_Light;
                    else
                        elementTransform.GetComponentInChildren<Renderer>().material.color = Player_Dark;

                    toggle = !toggle;
                }
            }
        }

        int CalculateElementIndex(int row, int col, int boardSize)
        {
            return row * boardSize + col;
        }

        Vector3 CalculateElementPosition(int row, int col)
        {
            float x = row - offset;
            float z = col - offset;
            return new Vector3(x, 1, z);
        }
    }
}