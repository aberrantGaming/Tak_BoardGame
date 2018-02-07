using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceTypes
{
    Flatstone,
    Capstone
}

public enum PieceColors
{
    Light,
    Dark
}

public class BoardConstructor : MonoBehaviour
{
    #region Variables

    public Board Gameboard;

    public Transform Prefab_Foundation;
    public Transform Prefab_Flatstone;
    public Transform Prefab_Capstone;
    public Material Player_Dark;
    public Material Player_Light;

    private const float elementSize = 1.0f;
    private float offset;
    
    private IDictionary<PieceTypes, Transform> stonePrefabs;
    private IDictionary<PieceColors, Material> stoneMaterials;
    
    #endregion

    // Use this for initialization
    void Start() {
        Debug.Log("Level Name: " + Gameboard.LevelName
            + ", board size is: " + Gameboard.Size);

        offset = (Gameboard.BoardSize / 2f) - (elementSize / 2f);     // adjust the offset for the difference in size between the board and tiles.

        InitiatePrefabsDictionary();
        InitiateMaterialsDictionary();
    }

    void InitiatePrefabsDictionary()
    {
        stonePrefabs = new Dictionary<PieceTypes, Transform>
        {
            { PieceTypes.Flatstone, Prefab_Flatstone },
            { PieceTypes.Capstone, Prefab_Capstone }
        };
    }

    void InitiateMaterialsDictionary()
    {
        stoneMaterials = new Dictionary<PieceColors, Material>
        {
            { PieceColors.Light, Player_Light },
            { PieceColors.Dark, Player_Dark }
        };
    }
}
