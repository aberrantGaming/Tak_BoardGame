using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    public interface ICommand
    {
        bool IsCompleted { get; set; }

        void Execute();
    }
}
