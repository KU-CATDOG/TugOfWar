﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private GameObject seVolumeSlider;
    private GameObject bgmVolumeSlider;

    [HideInInspector]
    public float seVolume = 0.5f;
    [HideInInspector]
    public float bgmVolume = 0.2f;

    private AudioSource bgmPlayer;
    private AudioSource sePlayer;

    [SerializeField] AudioClip[] bgmClip;
    [SerializeField] AudioClip[] SEClip;
    [SerializeField] AudioClip buttonSoundClip;

    private Dictionary<string, AudioClip> audioClipDic;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        SetupVolumeSlider();
        SetupBGMPlayer();
        SetupSEPlayer();

        audioClipDic = new Dictionary<string, AudioClip>();
        audioClipDic.Add("buttonSoundClip", buttonSoundClip);
        foreach(AudioClip clip in SEClip)
        {
            audioClipDic.Add(clip.name, clip);
        }
    }

    private void SetupVolumeSlider()
    {
        GameObject tempObj = GameObject.Find("Canvas").transform.Find("OptionScreenImage").gameObject;
        seVolumeSlider = tempObj.transform.Find("SEVolumeSlider").gameObject;
        bgmVolumeSlider = tempObj.transform.Find("BGMVolumeSlider").gameObject;
    }

    private void SetupBGMPlayer()
    {
        bgmPlayer = transform.GetChild(0).GetComponent<AudioSource>();
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.loop = true;
    }

    private void SetupSEPlayer()
    {
        sePlayer = transform.GetChild(1).GetComponent<AudioSource>();
        sePlayer.volume = seVolume;
        sePlayer.loop = false;
    }

    void Start()
    {
        if (bgmPlayer != null)
        {
            bgmPlayer.clip = bgmClip[0];
            bgmPlayer.Play();
        }
    }
    
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "StartMenu")
        {
            if (GameObject.Find("Canvas").transform.Find("OptionScreenImage").gameObject.activeSelf)
            {
                seVolume = seVolumeSlider.GetComponent<Slider>().value;
                bgmVolume = bgmVolumeSlider.GetComponent<Slider>().value;
                SetupBGMPlayer();
                SetupSEPlayer();
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "StartMenu")
        {
            SetupVolumeSlider();
            SetupBGMPlayer();
            SetupSEPlayer();
        }
        else if (scene.name == "SelectMenu")
        {

        }
        else if (scene.name == "SelectExtra")
        {

        }
        else if (scene.name == "InGame")
        {
            if (bgmPlayer != null)
            {
                bgmPlayer.Stop();
                bgmPlayer.clip = bgmClip[1];
                bgmPlayer.Play();
            }
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /*===========================Can Be Used At Other Scripts==============================*/

    public void PlaySoundClip(AudioClip clip, float volumeRatio = 1f)
    {
        sePlayer.PlayOneShot(clip, sePlayer.volume * volumeRatio);
    }

    public void PlaySoundDic(string clipName, float volumeRatio = 1f)
    {
        if (audioClipDic.ContainsKey(clipName))
        {
            sePlayer.PlayOneShot(audioClipDic[clipName], sePlayer.volume * volumeRatio);
        }
        else
        {
            UnityEngine.Debug.Log(clipName + " 는 저장된 오디오클립에 존재하지 않습니다.");
        }
    }

    public void PlaySoundIdx(int clipIdx, float volumeRatio = 1f)
    {
        if (0 < clipIdx && clipIdx < SEClip.Length)
        {
            sePlayer.PlayOneShot(SEClip[clipIdx], sePlayer.volume * volumeRatio);
        }
        else
        {
            UnityEngine.Debug.Log(clipIdx + "번 오디오클립은 존재하지 않습니다.");
        }
    }

    public void PlayRandomSoundDic(string[] clipNameArray, float volumeRatio = 1f)
    {
        string clipName = clipNameArray[Random.Range(0, clipNameArray.Length)];

        if (audioClipDic.ContainsKey(clipName))
        {
            sePlayer.PlayOneShot(audioClipDic[clipName], sePlayer.volume * volumeRatio);
        }
        else
        {
            UnityEngine.Debug.Log(clipName + " 는 저장된 오디오클립에 존재하지 않습니다.");
        }
    }

    public void PlayRandomSoundIdx(int[] clipIdxArray, float volumeRatio = 1f)
    {
        int clipIdx = clipIdxArray[Random.Range(0, clipIdxArray.Length)];

        if (0 < clipIdx && clipIdx < SEClip.Length)
        {
            sePlayer.PlayOneShot(SEClip[clipIdx], sePlayer.volume * volumeRatio);
        }
        else
        {
            UnityEngine.Debug.Log(clipIdx + "번 오디오클립은 존재하지 않습니다.");
        }
    }

    public void playButtonSound()
    {
        PlaySoundClip(buttonSoundClip);
    }
}
