using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioController : MonoBehaviour
{
    public AudioClip DroppingCrates;
    public AudioClip RubyPickup;
    public AudioClip Hurt;
    public AudioClip Dig;
    public AudioClip Buzz;
    public AudioClip GameMusic;

    private static GameAudioController instance;

    private void Awake() => instance = this;

    private void Start() => playGameMusic();
    public static void playDroppingCrates() =>AudioManager.Instance.PlaySfx(instance.DroppingCrates);
    public static void playRubyPickup() => AudioManager.Instance.PlaySfx(instance.RubyPickup, 0.7f);
    public static void playHurt() => AudioManager.Instance.PlaySfx(instance.Hurt);
    public static void playDig() => AudioManager.Instance.PlaySfx(instance.Dig, 0.4f);
    public static void playBuzz() => AudioManager.Instance.PlaySfx(instance.Buzz);
    public static void playGameMusic() => AudioManager.Instance.PlayMusicWithCrossFade(instance.GameMusic, 0.5f);

}
