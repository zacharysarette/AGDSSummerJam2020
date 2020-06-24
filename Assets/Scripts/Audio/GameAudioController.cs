using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioController : MonoBehaviour
{
    public AudioClip DroppingCrates;
    public AudioClip Crash;

    public void playDroppingCrates()
    {
        AudioManager.Instance.PlaySfx(DroppingCrates);
    }

    public void playCrash()
    {
        AudioManager.Instance.PlaySfx(Crash);
    }
}
