using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    public class PickupStack : ICommand
    {
        public Tile Source { get; private set; }
        public StoneColor RequestingPlayer { get; private set; }
        public int CarryLimit { get; private set; }

        public bool IsCompleted { get; set; }

        private PlayerManager pm;

        public bool IsLegal
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

        public PickupStack(Tile source, StoneColor requestingPlayer, int carryLimit)
        {
            pm = PlayerManager.Instance;

            Source = source;
            RequestingPlayer = requestingPlayer;
            CarryLimit = carryLimit;

            IsCompleted = false;
        }
        
        public void Execute()
        {
            if (!IsLegal)
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
    }
}
