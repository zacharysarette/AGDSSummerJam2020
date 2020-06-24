using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioController : MonoBehaviour
{
    public AudioClip music;

    public AudioClip gameMusic;
    void Start()
    {
        AudioManager.Instance.PlayMusicWithFade(music);
    }

    public void playGameMusic()
    {
        AudioManager.Instance.PlayMusicWithCrossFade(gameMusic);
    }
}
