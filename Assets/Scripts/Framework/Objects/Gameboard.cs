using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aberrantGames.Tak
{
    public class Gameboard : MonoBehaviour
    {
        protected const string GameboardSpawn = "gbSPAWN";

        #region Public Variables

        public GameEngine.Position Position { get; set; }

        #endregion

        #region Private Variables

        private Collectables.BoardDesign design;

        private GameObject basePlane;

        private const float tileSize = 1f;
        private float tileOffset;

        #endregion

        public static Gameboard MakeGameboard(GameEngine.Position _game, Collectables.BoardDesign _boardDesign = null)
        {
            GameObject parent = GameObject.Find(GameboardSpawn);
            
            GameObject go = new GameObject("Gameboard");
                
            Gameboard ret = go.AddComponent<Gameboard>();

            if (parent != null)
                ret.transform.parent = parent.transform;

            ret.Position = _game;
            ret.design = _boardDesign;

            return ret;
        }

        public void DrawGameBoard()
        {
            int boardSize = Position.cfg.Size;
            tileOffset = (boardSize / 2f) - (tileSize / 2);

            Debug.Log(boardSize); 
            Debug.Log(tileOffset);

            // Draw the tiles
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    int elementIndex = CalculateElementIndex(row, col, boardSize);
                    Vector3 elementPosition = CalculateElementPosition(row, col);

                    Collectables.Tile element = design.Boards[boardSize][elementIndex];
                    GameObject elementPrefab = element.Prefab;
                    
                    Transform elementTransform =
                        Instantiate(elementPrefab.transform, elementPosition, Quaternion.identity) as Transform;

                    elementTransform.parent = gameObject.transform;
                    elementTransform.eulerAngles = new Vector3(90, 0, 0);
                    elementTransform.GetComponent<Renderer>().material = element.Material;
                }
            }
        }

        int CalculateElementIndex(int row, int col, int boardSize)
        {
            return row * boardSize + col;
        }

        Vector3 CalculateElementPosition(int row, int col)
        {
            float x = row - tileOffset;
            float z = col - tileOffset;
            return new Vector3(x, 1, z);
        }
    }
}