using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.GameEngine
{
    public class GameHolder : ScriptableObject
    {
        public string MatchName;
        public int BoardSize = 8;

        // TODO: setup (object): Function that returns the initial value of G.

        // TODO: moves (object): The keys are move names, and the values are pure functions that return the new value of G once the move has been processed.

        // TODO: flow (object): Arguments to customize the flow of the game.

        // TODO: flow.phases(array): Optional list of game phases.
    }
}