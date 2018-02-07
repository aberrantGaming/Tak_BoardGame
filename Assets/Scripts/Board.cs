using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gameboard")]
public class Board : ScriptableObject {

    #region Properties
    [SerializeField]

    public int Size { get { return Mathf.Clamp(Size, 3, 8); } }

    #endregion

    #region Variables

    [SerializeField] private int boardSize;
    public int BoardSize { get { return boardSize; } }

    [SerializeField] private string levelName;
    public string LevelName { get { return levelName; } }

    [SerializeField] private List<Tile> tiles;
    public List<Tile> Tiles { get { return tiles; } }

    #endregion

    private void Awake()
    {
        tiles = new List<Tile>();
        int volume = Size * Size;

        for (int i = 0; i < volume; i++)
        {
            tiles.Add(new Tile());
        }
    }


}
