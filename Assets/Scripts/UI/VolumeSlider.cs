using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer mix;

    public void SetMusicVolume(float musicValue)
    {
        mix.SetFloat("MusicVol", Mathf.Log10(musicValue) * 100);
    }

    public void SetSFXVolume(float sfxValue)
    {
        mix.SetFloat("SFXVol", Mathf.Log10(sfxValue) * 30);
    }
}
