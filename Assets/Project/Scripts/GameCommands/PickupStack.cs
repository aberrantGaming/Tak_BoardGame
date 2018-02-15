using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    public class PickupStack : ICommand
    {
        #region Public Properties

        public Tile Source { get; private set; }
        public StoneColor RequestingPlayer { get; private set; }
        public int CarryLimit { get; private set; }

        #endregion

        #region Private variables

        private PlayerManager pm;

        #endregion

        #region Constructor

        public PickupStack(Tile source, StoneColor requestingPlayer, int carryLimit)
        {
            pm = PlayerManager.Instance;

            Source = source;
            RequestingPlayer = requestingPlayer;
            CarryLimit = carryLimit;

            IsCompleted = false;
        }

        #endregion

        #region Interface Members

        public bool IsCompleted { get; set; }

        /// <summary>
        /// Allow a player, who controls this tile, to pickup a number of pieces from the top of the stack not exceeding their carry limit.
        /// </summary>
        public void Execute()
        {
            if (!Allowed)
                return;

            Queue<Stone> requestedHand = new Queue<Stone>();
            requestedHand.Clear();

            for (int i = CarryLimit; i < 0; i--)
            {
                requestedHand.Enqueue(Source.Stack[Source.Stack.Count - i]);
                Debug.Log("Source : " + Source.Stack.Count + " : Requested Hand : " + requestedHand.Count);
            }

            pm.StackPickup(requestedHand);
            Debug.Log("Player Picked Up Stack of " + requestedHand.Count + " stones.");

            IsCompleted = true;
        }

        #endregion

        #region Public Methods

        public bool Allowed
        {
            get
            {
                if (Source.Stack.Count == 0)                            // don't request a nonexistent stack
                    return false;

                if (Source.ControllingPlayer != null
                    && RequestingPlayer != Source.ControllingPlayer)    // don't request an opponent's stack
                    return false;

                return true;
            }
            private set { }
        }

        #endregion
    }
}
