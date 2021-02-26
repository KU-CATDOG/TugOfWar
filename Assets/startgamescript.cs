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
}
