using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public GameObject seVolumeSlider;
    public GameObject bgmVolumeSlider;

    [HideInInspector]
    public float seVolume;
    [HideInInspector]
    public float bgmVolume;

    [HideInInspector]
    public AudioSource bgmPlayer { get; private set; }
    [HideInInspector]
    public AudioSource sePlayer1 { get; private set; }
    [HideInInspector]
    public AudioSource sePlayer2 { get; private set; }

    public AudioClip bgmClip1;
    public AudioClip bgmClip2;


    void Awake()
    {
        if (SoundManager.instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
        seVolume = 0.5f;
        bgmVolume = 0.5f;
        bgmPlayer = transform.GetChild(0).GetComponent<AudioSource>();
        sePlayer1 = transform.GetChild(1).GetComponent<AudioSource>();
        sePlayer2 = transform.GetChild(2).GetComponent<AudioSource>();
        bgmPlayer.volume = bgmVolume;
        sePlayer1.volume = seVolume;
        sePlayer2.volume = seVolume;
    }

    
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "StartMenu")
        {
            if (seVolumeSlider.activeSelf)
            {
                seVolume = seVolumeSlider.GetComponent<Slider>().value;
                bgmVolume = bgmVolumeSlider.GetComponent<Slider>().value;
                bgmPlayer.volume = bgmVolume;
                sePlayer1.volume = seVolume;
                sePlayer2.volume = seVolume;
            }
        }

        else if (SceneManager.GetActiveScene().name == "SelectMenu")
        {

        }

        else if (SceneManager.GetActiveScene().name == "SelectExtra")
        {

        }

        else if (SceneManager.GetActiveScene().name == "InGame")
        {

        }
    }

    public void playSound(AudioSource audioPlayer, AudioClip clip)
    {
        audioPlayer.Stop();
        audioPlayer.clip = clip;
        audioPlayer.time = 0;
        audioPlayer.Play();
    }
}
