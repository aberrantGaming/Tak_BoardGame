using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    public enum StoneType { STONE, CAP_STONE }
    public enum StoneColor { DARK, LIGHT }
    public enum StoneOrientations { FLAT, STANDING }    
    
    public class Stone : MonoBehaviour
    {
        public StoneType StoneType { get; private set; }
        public StoneColor StoneColor { get; private set; }
        public StoneOrientations StoneOrientation { get; private set; }

        public GameObject Prefab { get; private set; }

        public Stone(StoneType _stoneType, StoneColor _stoneColor, StoneOrientations _stoneOrientations, GameObject _stonePrefab)
        {
            StoneType = _stoneType;
            StoneColor = _stoneColor;
            StoneOrientation = _stoneOrientations;

            Prefab = _stonePrefab;
        }
    }
}