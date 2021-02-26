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
    private GameObject bgi;
    private float t;

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

    private void Start()
    {
        bgi = GameObject.Find("Canvas").transform.Find("BackGroundImage").gameObject;
        bgi.transform.localPosition = new Vector3(0, 0, 0);
        t = 0;
    }

    private void Update()
    {
        t += Time.deltaTime;
        if (t < 2 || 2.5f < t && t < 3 || 7 < t && t < 7.5f || 8 < t && t < 10)
        {
            bgi.transform.localPosition += 20 * Vector3.right * Time.deltaTime;
        }
        else
        {
            bgi.transform.localPosition += 20 * Vector3.left * Time.deltaTime;
            if (t >= 10)
            {
                t = 0;
            }
        }
    }
}
