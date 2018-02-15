using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    [CreateAssetMenu(menuName = "Collectables/Stones Set")]
    public class Stones : ScriptableObject
    {
        #region Public Variables
        
        public string StonesName = "_StonesName";
        public string StonesDesc = "_StonesDescription";

        public Sprite StonesSprite;

        public GameObject CapStoneDark;
        public GameObject FlatStoneDark;

        public GameObject CapStoneLight;
        public GameObject FlatStoneLight;
        
        #endregion
    }
}