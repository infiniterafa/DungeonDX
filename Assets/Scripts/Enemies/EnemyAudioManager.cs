using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyAudioManager : MonoBehaviour
{

    public AudioSource _soundEffects;
    public AudioSource _voiceLines;

    public List<AudioClip> _gruntsClips = new List<AudioClip>();
    public List<AudioClip> _walk = new List<AudioClip>();


    public void PlayWalkSound()
    {
        if (!_soundEffects.isPlaying)
        {
            _soundEffects.volume = 0.5f;
            _soundEffects.pitch = Random.Range(0.9f, 1.1f);
            _voiceLines.clip = _gruntsClips[Random.Range(0, _gruntsClips.Count)];
            _soundEffects.clip = _walk[Random.Range(0, _walk.Count)];
            _soundEffects.Play();
        }
    }

    public void StopWalkSound()
    {
        if (_soundEffects.isPlaying)
        {
            _soundEffects.clip = null;
            _soundEffects.Pause();
        }
    }

}
