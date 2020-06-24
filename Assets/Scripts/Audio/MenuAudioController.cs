using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioController : MonoBehaviour
{
    public AudioClip music;
    void Start()
    {
        AudioManager.Instance.PlayMusicWithFade(music);
    }
}
