using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioController : MonoBehaviour
{
    public AudioClip DroppingCrates;
    public AudioClip Crash;

    private static GameAudioController instance;

    private void Awake() => instance = this;
    public static void playDroppingCrates() =>AudioManager.Instance.PlaySfx(instance.DroppingCrates);
    public static void playCrash() => AudioManager.Instance.PlaySfx(instance.Crash);
}
