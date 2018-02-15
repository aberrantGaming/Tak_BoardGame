using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    [System.Serializable]
    public class Tile : MonoBehaviour
    {

        public int tileID = -1;

        private Stone lastStoneAdded;
                
        public List<Stone> Stack { get; private set; }

        #region Properties

        /// <summary>
        /// The tile is controlled by the player who's tile was placed on it last.
        /// </summary>
        public StoneColor? ControllingPlayer {
            get {
                if (lastStoneAdded == null)
                    return null;

                return lastStoneAdded.StoneColor;
            }
            private set { }
        }

        /// <summary>
        /// The tile is Insurmountable if the last stone on the stack is a CAP_STONE. 
        /// </summary>
        public bool IsInsurmountable {
            get {
                if (lastStoneAdded == null)
                    return false;

                return (lastStoneAdded.StoneType == StoneType.CAP_STONE);
            }
            private set {  }
        }

        /// <summary>
        /// The tile is a Wall if the last stone on the stack is in the STANDING orientation. 
        /// </summary>
        public bool IsAWall {
            get {
                if (lastStoneAdded == null)
                    return false;

                return (lastStoneAdded.StoneOrientation == StoneOrientations.STANDING);
            }
            private set { }
        }

        #endregion

        #region MonoBehaviour Callbacks

        protected void Awake()
        {
            Stack = new List<Stone>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Allow a player, who controls this tile, to pickup a number of pieces from the top of the stack equal to their carry limit.
        /// </summary>
        public Queue<Stone> PickupStack(StoneColor requestingPlayer, int carryLimit)
        {
            if (requestingPlayer != ControllingPlayer)
                return null;

            Queue<Stone> requestedHand = new Queue<Stone>();
            requestedHand.Clear();

            for (int i = carryLimit; i < 0; i--)
            {
                requestedHand.Enqueue(Stack[Stack.Count - i]);
            }

            return requestedHand;
        }

        //public bool AddStoneToStack(Stone stoneToAdd)
        //{
        //    switch (stoneToAdd.StoneType)
        //    {
        //        case (StoneType.STONE):

        //            break;

        //        case (StoneType.CAP_STONE):
        //            if (IsInsurmountable)
        //                return false;
        //            break;
        //    }
        //}
        
        #endregion
    }
}