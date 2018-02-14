using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    public class ArenaUiInput : UiInput
    {        
        public void Concede_OnButtonPress()
        {
            uc.ConcedeMatch();
        }
    }
}
