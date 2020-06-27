using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioController : MonoBehaviour
{
    public AudioClip music;

    void Start()
    {  
        AudioManager.Instance.PlayMusicWithCrossFade(music, 0.5f);
    }

}
