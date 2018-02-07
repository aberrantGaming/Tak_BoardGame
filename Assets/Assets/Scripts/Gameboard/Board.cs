using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gameboard")]
public class Board : ScriptableObject {

    #region Properties
    [SerializeField]
    public int Size { get { return Mathf.Clamp(Size, 3, 8); } }
    public int Pieces { get { return NumberOfNormalPieces; } }
    public int Capstones { get { return NumberOfCapstones; } }
    
    #endregion

    #region Variables

    [SerializeField] private int boardSize = 5;
    public int BoardSize { get { return boardSize; } }

    [SerializeField] private string levelName = "Standard";      
    public string LevelName { get { return levelName; } }

    [SerializeField] private List<Tile> tiles;
    public List<Tile> Tiles { get { return tiles; } }

    #endregion

    private void Awake()
    {
        PopulateTilesArray();
    }

    private void PopulateTilesArray()
    {
        tiles = new List<Tile>();
        int volume = Size * Size;

        for (int i = 0; i < volume; i++)
        {
            tiles.Add(new Tile());
        }
    }

    private int NumberOfNormalPieces
    {
        get
        {
            if (Size == 3)
                return 10;
            else if (Size == 4)
                return 15;
            else if (Size == 5)
                return 21;
            else if (Size == 6)
                return 30;
            else if (Size == 7)
                return 40;
            else if (Size == 8)
                return 50;
            else
                return 0;                    
        }
    }

    private int NumberOfCapstones
    {
        get
        { 
            if (Size == 5)
                return 1;
            else if (Size == 6)
                return 1;
            else if (Size == 7)
                return 2;
            else if (Size == 8)
                return 2;
            else
                return 0;                    
        }
    }

}
