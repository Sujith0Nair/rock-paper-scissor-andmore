using System;
using UnityEngine;
using Bootstrap.ScriptableObjects;

namespace Bootstrap
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannel gameEventChannel;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip musicClip;

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
        }

        private void OnEnable()
        {
            gameEventChannel.OnMusicToggleRequested += OnMusicToggleRequested;
        }

        private void OnDisable()
        {
            gameEventChannel.OnMusicToggleRequested -= OnMusicToggleRequested;
        }

        private void OnMusicToggleRequested()
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            else
            {
                audioSource.Play();
            }
        }
    }
}