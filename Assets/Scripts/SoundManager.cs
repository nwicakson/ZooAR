using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource backgroundMusic;
    public AudioSource soundEffect;
    private static SoundManager instance = null;
    private float lowPitchRange = .95f, highPitchRange = 1.05f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlayBackground(AudioClip clip)
    {
        backgroundMusic.clip = clip;
        backgroundMusic.Play();
    }

    public void StopBackground()
    {
        backgroundMusic.Stop();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        soundEffect.pitch = randomPitch;
        soundEffect.clip = clip;
        soundEffect.Play();
    }

}
