using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    public class PlayerManager : MonoBehaviour
    {

        #region Private Variables
        
        bool PlayersTurn = true;       // TO DO: Replace this with a dynamic property

        Queue<Stone> StonesBag = new Queue<Stone>();    // This object represents the "deck" that the player draws new stones from

        Queue<Stone> HeldStack = new Queue<Stone>();    // This object represents what the player is "holding" in hand

        #endregion

        #region MonoBehaviour Callbacks

        protected void Update()
        {
            if (PlayersTurn)
                HandleInput();
        }

        #endregion

        #region Private Methods

        protected void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    Debug.Log("You Selected the " + hit.transform.name);
                }

                switch (hit.transform.name)
                {
                    // case (""): DrawStone(); break;

                }
            }
        }

        /// <summary>
        ///     Request a new Stone from the StonesBag
        /// </summary>
        private void DrawStone()
        {
            // Clear the current selection and draw a new stone.
            HeldStack.Clear();
            HeldStack.Enqueue(StonesBag.Dequeue());
        }

        private void PlaceStone(Tile destination)
        {
            if (!destination.IsBlocked)
                destination.Stack.Enqueue(HeldStack.Dequeue());
        }

        #endregion

    }
}
