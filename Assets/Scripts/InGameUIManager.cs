using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    public GameObject gameManager;
    private GM gm;

    private int tempPhase;

    public GameObject startUI;
    public GameObject inGameUI;
    public GameObject endUI;

    public GameObject scoreBoard;
    public GameObject timerBoard;

    public GameObject blindImage;

    void Start()
    {
        gm = gameManager.GetComponent<GM>();
        tempPhase = -1;

        blindImage.SetActive(false);
    }

    void Update()
    {
        if (tempPhase != gm.phase)
        {
            tempPhase = gm.phase;
            SetBoolean();
        }

        if (gm.phase == 0) //StartScreen
        {
            StartScreenUI();
        }
        else if (gm.phase == 2) //InGame
        {
            InGameScreenUI();
        }
        else if (gm.phase == 3) //EndGame
        {
            EndScreenUI();
        }
    }

    private void SetBoolean()
    {
        if (gm.phase == 0) //StartScreen
        {
            startUI.SetActive(true);
            inGameUI.SetActive(false);
            endUI.SetActive(false);
        }
        else if (gm.phase == 2) //InGame
        {
            startUI.SetActive(false);
            inGameUI.SetActive(true);
            endUI.SetActive(false);
        }
        else if (gm.phase == 3) //EndGame
        {
            startUI.SetActive(false);
            inGameUI.SetActive(false);
            endUI.SetActive(true);

            if (gm.scoreL == gm.winScore)
            {

            }
            else if (gm.scoreR == gm.winScore)
            {

            }
        }
    }

    private void StartScreenUI()
    {
        if (Input.anyKeyDown)
        {
            gm.SetPhase(1);
        }
    }

    private void InGameScreenUI()
    {
        SetScoreBoard();
        SetTimerBoard();
    }

    private void EndScreenUI()
    {

    }

    private void SetScoreBoard()
    {
        if (scoreBoard.activeSelf)
        {
            scoreBoard.GetComponent<Text>().text = gm.scoreL + " : " + gm.scoreR;
        }
    }

    private void SetTimerBoard()
    {
        if (timerBoard.activeSelf)
        {
            timerBoard.GetComponent<Text>().text = gm.timer.ToString("N0") + " 초";
        }
    }

    public void RestartB()
    {
        gm.SetPhase(0);
    }

    public void TitleB()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
