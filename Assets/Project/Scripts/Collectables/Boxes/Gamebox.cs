using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    [CreateAssetMenu(menuName = "Collectables/Gamebox")]
    public class Gamebox : ScriptableObject
    {
        #region Public Variables

        public GameObject BoxPrefab;
        public string BoxName = "_BoxName";
        public string BoardDesc = "_BoxDescription";

        #endregion

    }
}