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

    public void startgame()
    {
        Debug.Log("start!");
        SceneManager.LoadScene("SelectMenu");
    }
    public void gotooption()
    {
        Debug.Log("option");
    }
    public void exitbutton()
    {
        Debug.Log("Exit!");
    }
}
