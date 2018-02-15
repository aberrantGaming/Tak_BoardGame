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

            Debug.Log("Player Picked Up Stack of " + CarryLimit + " stones.");

            IsCompleted = true;
        }
    }
}
