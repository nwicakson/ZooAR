﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : MonoBehaviour
{
    public AudioClip sfx;
    public AudioClip backgroundMusic;
    private SoundManager soundManager;

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

    public void History()
    {
        soundManager.PlaySoundEffect(sfx);
        SceneManager.LoadScene("History");
    }

    public void Exit()
    {
        soundManager.PlaySoundEffect(sfx);
        PlayerPrefs.Save();
        Application.Quit();
    }
}
