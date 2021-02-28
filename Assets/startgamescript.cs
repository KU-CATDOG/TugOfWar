using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startgamescript : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;

    public GameObject buttons;
    public GameObject optionScreen;
    public GameObject helpScreen;
    public GameObject helpScreen2;

    void Start()
    {
        buttons.SetActive(true);
        optionScreen.SetActive(false);
        helpScreen.SetActive(false);
        helpScreen2.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionScreen.activeSelf)
            {
                buttons.SetActive(true);
                optionScreen.SetActive(false);
            }
            if (helpScreen.activeSelf)
            {
                buttons.SetActive(true);
                helpScreen.SetActive(false);
            }
            else if (helpScreen2.activeSelf)
            {
                buttons.SetActive(true);
                helpScreen2.SetActive(false);
            }
        }
    }

    public void startgame()
    {
        SoundManager.instance.playButtonSound();

        Debug.Log("start!");
        SceneManager.LoadScene("SelectMenu");
    }
    public void gotooption()
    {
        SoundManager.instance.playButtonSound();

        Debug.Log("option");
        buttons.SetActive(false);
        optionScreen.SetActive(true);

        if (GameObject.Find("SoundManageObject") != null)
        {
            optionScreen.transform.Find("SEVolumeSlider").gameObject.GetComponent<Slider>().value = SoundManager.instance.seVolume;
            optionScreen.transform.Find("BGMVolumeSlider").gameObject.GetComponent<Slider>().value = SoundManager.instance.bgmVolume;
        }
    }
    public void exitbutton()
    {
        SoundManager.instance.playButtonSound();

        Debug.Log("Exit!");
        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
    public void closeOption()
    {
        SoundManager.instance.playButtonSound();

        buttons.SetActive(true);
        optionScreen.SetActive(false);
    }

    public void volumeChangeSound()
    {
        SoundManager.instance.playButtonSound();
    }

    public void OpenHelpScreen()
    {
        buttons.SetActive(false);
        helpScreen.SetActive(true);
    }

    public void CloseHelpScreen()
    {
        if (helpScreen.activeSelf)
        {
            buttons.SetActive(true);
            helpScreen.SetActive(false);
        }
        else if (helpScreen2.activeSelf)
        {
            buttons.SetActive(true);
            helpScreen2.SetActive(false);
        }
    }

    public void NextHelpPage()
    {
        helpScreen.SetActive(false);
        helpScreen2.SetActive(true);
    }

    public void PreviousHelpPage()
    {
        helpScreen.SetActive(true);
        helpScreen2.SetActive(false);
    }
}
