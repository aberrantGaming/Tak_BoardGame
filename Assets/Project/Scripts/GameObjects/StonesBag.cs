using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    public class StonesBag : MonoBehaviour
    {        
        IDictionary<StoneType, GameObject> StonesPrefabDark;
        IDictionary<StoneType, GameObject> StonesPrefabLight;

        PlayerManager pm;
        Queue<Stone> Stones;    // This object represents the pool that the player draws new stones from
        Queue<Stone> CapStones; // This object represents the pool that the player draws new stones from

        private void Awake()
        {
            pm = PlayerManager.Instance;
            
            InitPrefabDictionary();
        }

        public void PopulateBag(int numberOfStones, int numberOfCapstones)
        {
            
        }

        protected void InitPrefabDictionary()
        {
            StonesPrefabDark = new Dictionary<StoneType, GameObject>
            {
                { StoneType.STONE, pm.CurrentStones.FlatStoneDark },
                { StoneType.CAP_STONE, pm.CurrentStones.CapStoneDark }
            };

            StonesPrefabLight = new Dictionary<StoneType, GameObject>
            {
                { StoneType.STONE, pm.CurrentStones.FlatStoneLight },
                { StoneType.CAP_STONE, pm.CurrentStones.CapStoneLight }
            };
        }
    }
}