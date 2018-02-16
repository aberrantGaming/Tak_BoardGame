using UnityEngine;
using UnityEngine.Audio;

namespace Com.SeeSameGames.Tak
{
    public class UiComponent : MonoBehaviour
    {
        #region Public Variables
        
        [HideInInspector]
        public bool 
            isPaused = false;

        [HideInInspector]
        public Animator animator;
        [HideInInspector]
        public AudioMixer audioMixer;

        #endregion

        public void Initialize()
        {
            // access components
            animator = GetComponent<Animator>();
            audioMixer = GetComponent<AudioMixer>();
        }
    }
}