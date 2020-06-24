using System.Collections;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

  #region Static Instance
  private static AudioManager instance;
  public static AudioManager Instance
  {
    get
    {
      if (instance == null)
      {
        instance = FindObjectOfType<AudioManager>();
        if (instance == null)
        {
          instance = new GameObject(
            "Spawned AudioManager", typeof(AudioManager)
          ).GetComponent<AudioManager>();
        }
      }
      return instance;
    }
    private set
    {
      instance = value;
    }
  }
  #endregion

  private bool isFirstMusicSourcePlaying;

  #region Fields
  private AudioSource musicSource;
  private AudioSource musicSource2;
  private AudioSource sfxSource;

  #endregion

  private void Awake()
  {
    DontDestroyOnLoad(this.gameObject);

    musicSource = this.gameObject.AddComponent<AudioSource>();
    musicSource2 = this.gameObject.AddComponent<AudioSource>();
    sfxSource = this.gameObject.AddComponent<AudioSource>();

    musicSource.loop = true;
    musicSource2.loop = true;

  }

  public void PlayMusic(AudioClip musicClip)
  {
    AudioSource activeSource = getCurrentMusicSource();
    activeSource.clip = musicClip;
    activeSource.volume = 1;
    activeSource.Play();
  }

  public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1.0f)
  {
    AudioSource activeSource = getCurrentMusicSource();
    StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));
  }

public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1.0f)
{
  AudioSource activeSource = getCurrentMusicSource();
  AudioSource newSource = getNonCurrentMusicSource();

  isFirstMusicSourcePlaying = !isFirstMusicSourcePlaying;

  newSource.clip = musicClip;
  newSource.Play();
  StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));
}
  private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
  {
      if(!activeSource.isPlaying)
      {
        activeSource.Play();
      }

      float t = 0.0f;

      for (t = 0; t < transitionTime; t += Time.deltaTime)
      {
        activeSource.volume = (1 - (t / transitionTime));
        yield return null;
      }

      activeSource.Stop();
      activeSource.clip = newClip;
      activeSource.Play();

      for (t = 0; t < transitionTime; t += Time.deltaTime)
      {
        activeSource.volume = t / transitionTime;
        yield return null;
      }
  }

  private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTime)
  {
    float t = 0.0f;
    for (t = 0.0f; t <= transitionTime; t += Time.deltaTime)
    {
      original.volume = 1 - (t / transitionTime);
      newSource.volume = t / transitionTime;
      yield return null;
    }
    original.Stop();
  }

  public void PlaySfx(AudioClip clip)
  {
        if (!sfxSource.isPlaying)
            sfxSource.PlayOneShot(clip);     
  }

  public void PlaySfx(AudioClip clip, float volume)
  {
    sfxSource.PlayOneShot(clip, volume);
  }
  private AudioSource getCurrentMusicSource()
  {
    return isFirstMusicSourcePlaying ? musicSource : musicSource2;
  }
  
  private AudioSource getNonCurrentMusicSource()
  {
    return !isFirstMusicSourcePlaying ? musicSource : musicSource2;
  }

  public void SetMusicVolume(float volume)
  {
    musicSource.volume = volume;
    musicSource2.volume = volume;
  }
  public void SetSfxVolume(float volume)
  {
    sfxSource.volume = volume;
  }
}
