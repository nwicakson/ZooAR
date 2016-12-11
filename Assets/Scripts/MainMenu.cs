using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : MonoBehaviour
{
    public AudioClip sfx;
    public AudioClip backgroundMusic;
    private SoundManager soundManager;

    void Awake()
    {
        //TextAsset file = Resources.Load<TextAsset>("InitialSetting");
        //prettyJson = JsonMapper.ToObject(file.text);
    }

    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager.PlayBackground(backgroundMusic);
    }

    public void Play()
    {
        soundManager.PlaySoundEffect(sfx);
        SceneManager.LoadScene("Play");
    }

    public void Setting()
    {
        soundManager.PlaySoundEffect(sfx);
        SceneManager.LoadScene("Setting");
    }

    public void Exit()
    {
        soundManager.PlaySoundEffect(sfx);
        PlayerPrefs.Save();
        Application.Quit();
    }
}
