using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;

namespace Com.SeeSameGames.Tak
{
    public class SettingsUiInput : MonoBehaviour
    {
        #region Public Variables

        public TMPro.TMP_Dropdown SettingsResolutionDropdownGO;

        #endregion

        #region Private Variables

        protected UiController uc;      // access to the UiController component

        static string[] resPresentation;
        static Resolution[] availResolutions;

        #endregion

        #region MonoBehaviour Callbacks

        protected virtual void Start()
        {
            StoreAvailableResolutions();
            PopulateResolutionsDropdown();
        }

        #endregion

        #region Public Methods

        public virtual void OpenSettingsMenu()
        {
            uc.ToggleUiElement(SettingsUiCanvas, true);
        }

        public virtual void SetVolume(float volume)
        {
            uc.audioMixer.SetFloat("volume", volume);
        }

        public virtual void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        public virtual void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }

        public virtual void SetResolution(int resolutionIndex)
        {
            Resolution resolution = availResolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Caches a unique list resolutions available on the currently active display
        /// </summary>
        protected static void StoreAvailableResolutions()
        {
            var resolutionList = Screen.resolutions;

            // for some reason Unity contains duplicate entries for available resolutions so grab only unique ones
            var uniqueResolutions = new Dictionary<string, Resolution>();

            foreach (var resolution in resolutionList)
            {
                uniqueResolutions[resolution.ToString()] = resolution;
            }

            // sort unique keys by comparing width and then height of their representative resolutions
            var sortedKeys = uniqueResolutions.Keys.ToList();

            sortedKeys.Sort((a, b) =>
            {
                int diff = uniqueResolutions[a].width.CompareTo(uniqueResolutions[b].width);
                if (diff != 0) return diff;
                return uniqueResolutions[a].height.CompareTo(uniqueResolutions[b].height);
            });

            availResolutions = new Resolution[sortedKeys.Count];
            resPresentation = new string[sortedKeys.Count];

            for (int i = 0; i < sortedKeys.Count; i++)
            {
                resPresentation[i] = sortedKeys[i];
                availResolutions[i] = uniqueResolutions[sortedKeys[i]];
            }
        }

        protected virtual void PopulateResolutionsDropdown()
        {
            SettingsResolutionDropdownGO.ClearOptions();

            List<string> options = new List<string>();

            int currentResoltuionIndex = 0;
            for (int i = 0; i < availResolutions.Length; i++)
            {
                string option = availResolutions[i].width + " x " + availResolutions[i].height;
                options.Add(option);

                int diff = availResolutions[i].width.CompareTo(Screen.currentResolution.width) +
                    availResolutions[i].height.CompareTo(Screen.currentResolution.height);

                if (diff == 0)
                    currentResoltuionIndex = i;
            }

            SettingsResolutionDropdownGO.AddOptions(options);
            SettingsResolutionDropdownGO.value = currentResoltuionIndex;
            SettingsResolutionDropdownGO.RefreshShownValue();
        }

        #endregion

    }
}
