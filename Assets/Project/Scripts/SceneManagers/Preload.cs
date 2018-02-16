using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.SeeSameGames.Tak
{
    public class Preload : MonoBehaviour
    {
        void Start()
        {
            // Move to next scene after preload initialization finishes
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
