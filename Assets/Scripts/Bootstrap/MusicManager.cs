using System;
using UnityEngine;
using Core.Utilities;
using UnityEngine.UI;

namespace Bootstrap
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip musicClip;
        [SerializeField] private Sprite muteSprite;
        [SerializeField] private Sprite unmuteSprite;
        [SerializeField] private Image iconImage;
        [SerializeField] private Button toggleButton;

        private void Start()
        {
            if (audioSource == null)
            {
                throw new NullReferenceException("audioSource is unassigned!");
            }

            if (musicClip == null)
            {
                throw new NullReferenceException("musicClip is unassigned!");
            }
            
            audioSource.clip = musicClip;
            audioSource.PlayDelayed(1f);
            toggleButton.onClick.RemoveAllAndAddNewListener(OnMusicToggleRequested);
        }

        private void OnMusicToggleRequested()
        {
            if (audioSource.isPlaying)
            {
                iconImage.sprite = unmuteSprite;
                audioSource.Pause();
            }
            else
            {
                iconImage.sprite = muteSprite;
                audioSource.Play();
            }
        }
    }
}