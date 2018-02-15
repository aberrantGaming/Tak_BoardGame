using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    [System.Serializable]
    public class Tile : MonoBehaviour
    {

        public int tileID;

        private Stone lastStoneAdded;

        public Queue<Stone> Stack { get; private set; }

        #region Properties

        public bool IsBlocked
        {
            get {
                if (lastStoneAdded != null && lastStoneAdded.StoneType != StoneType.FLAT_STONE)
                        return true;

                return false;
            }
            private set
            {

            }
        }

        #endregion

        #region MonoBehaviour Callbacks

        protected void Awake()
        {
            Stack = new Queue<Stone>();
        }

        #endregion

        #region Public Methods

        public void AddStoneToStack(Stone stoneToAdd)
        {
            if (lastStoneAdded.StoneType != StoneType.FLAT_STONE)
                return;

            Stack.Enqueue(stoneToAdd);
        }
        
        #endregion
    }
}