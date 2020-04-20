using System.Collections.Generic;
using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource = null;
        [SerializeField] private List<AudioClip> _audioClips = null;

        private int _currentClipNumber = -1;

        public void NextClip()
        {
            if (_audioClips.Count == 0)
            {
                Debug.LogError("No clips specified");
                return;
            }

            _currentClipNumber = (_currentClipNumber + 1) % _audioClips.Count;
            _audioSource.clip = _audioClips[_currentClipNumber];
            _audioSource.Play();
        }

        public void Pause()
        {
            _audioSource.Pause();
        }

        public void Unpause()
        {
            _audioSource.UnPause();
        }
    }
}