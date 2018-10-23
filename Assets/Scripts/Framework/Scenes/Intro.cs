using System.Collections;
using System.Collections.Generic;
using aberrantGames.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using aberrantGames.Utilities.Patterns;

namespace aberrantGames.Tak.Scenes
{
    public class Intro : MonoBehaviour
    {
        public float DelayTimer = 3f;

        GameManager gm;
        Player pm;
        ObjectPooler op;

        private void Awake()
        {
            gm = GameManager.Instance;
            gm.OnStateChange += Gm_OnStateChange;

            op = (ObjectPooler)ObjectPooler.Instance;            
            
            gm.SetGameState(GameState.INTRO);
        }

        private void Gm_OnStateChange()
        {
            switch (gm.GameState)
            {
                case (GameState.INTRO): Invoke("LoadNextScene", DelayTimer); break;
            }
        }

        private void Pm_OnStateChange() { }



        private void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
