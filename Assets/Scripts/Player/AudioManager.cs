using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    public AudioSource _voiceLines;
    public AudioSource _swordSource;
    public AudioSource _soundEffects;
    
    public List<AudioClip> _gruntsClips = new List<AudioClip>();
    public List<AudioClip> _walk = new List<AudioClip>();
    public List<AudioClip> _sword = new List<AudioClip>();
    
    public AudioClip _ChargeClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayWalkSound()
    {
        if (!_soundEffects.isPlaying)
        { 
            var i = Random.Range(0, _walk.Count);
            _soundEffects.clip = _walk[i];
            _soundEffects.Play();
        }
    }

    public void PlayAttackSound()
    {
        var i = 0;
        if (!_voiceLines.isPlaying)
        {
            i = Random.Range(0, _gruntsClips.Count);
            _voiceLines.clip = _gruntsClips[i];
            _voiceLines.Play();
        }

        _swordSource.Pause(); 
        i = Random.Range(0, _sword.Count);
        _swordSource.clip = _sword[i];
        _swordSource.Play();
       
    }

    public void PlayChargedAtackSound()
    {
        _swordSource.Pause();
        _swordSource.clip = _ChargeClip;
        _swordSource.Play();
    }
}
