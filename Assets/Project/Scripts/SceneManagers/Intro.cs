using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.aberrantGames.Tak.Scenes
{
    public class Intro : MonoBehaviour
    {
        public float DelayTimer = 3f;

        GameManager gm;
        PlayerManager pm;
        ObjectPooler op;

        private void Awake()
        {
            gm = GameManager.Instance;
            gm.OnStateChange += Gm_OnStateChange;

            pm = PlayerManager.Instance;
            pm.OnStateChange += Pm_OnStateChange;

            op = ObjectPooler.Instance;            
            
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
