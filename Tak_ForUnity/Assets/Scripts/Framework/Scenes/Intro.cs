using UnityEngine;
using UnityEngine.SceneManagement;

namespace aberrantGames.Tak.Scenes
{
    public class Intro : MonoBehaviour
    {
        public float DelayTimer = 3f;

        GameManager gm;

        private void Awake()
        {
            gm = GameManager.Instance;
            gm.OnStateChange += Gm_OnStateChange;
           
            gm.SetGameState(GameState.INTRO);
        }

        private void Gm_OnStateChange()
        {
            switch (gm.GameState)
            {
                case (GameState.INTRO): Invoke("LoadNextScene", DelayTimer); break;
            }
        }

        private void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}