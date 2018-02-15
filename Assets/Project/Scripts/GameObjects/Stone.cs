using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    public enum StoneType { FLAT_STONE, STANDING_STONE, CAP_STONE }
    
    public class Stone : MonoBehaviour
    {
        public StoneType StoneType { get; private set; }

    }
}