using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    public enum StoneType { FLAT_STONE, CAP_STONE }
    public enum StonePosition { FLAT, STANDING}
    
    public class Stone : MonoBehaviour
    {
        public StoneType Type { get; private set; }
        public StonePosition Position;
        public GameObject Prefab { get; private set; }

        public Stone(StoneType _stoneType, GameObject _stonePrefab)
        {
            Type = _stoneType;
            Prefab = _stonePrefab;
        }

    }
}