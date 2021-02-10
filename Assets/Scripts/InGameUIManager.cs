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

    private float endTimer;

    void Start()
    {
        gm = gameManager.GetComponent<GM>();
        tempPhase = -1;

        blindImage.SetActive(false);

        endTimer = 0.0f;
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

    private void SetBoolean() //Called When Phase Changes
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
                endUI.transform.GetChild(0).gameObject.SetActive(true);
                endUI.transform.GetChild(1).gameObject.SetActive(false);
            }
            else if (gm.scoreR == gm.winScore)
            {
                endUI.transform.GetChild(0).gameObject.SetActive(false);
                endUI.transform.GetChild(1).gameObject.SetActive(true);
            }

            endUI.transform.GetChild(2).gameObject.SetActive(false);
            endUI.transform.GetChild(3).gameObject.SetActive(false);

            endTimer = 3.0f;
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
        if (endTimer > 0.0f)
        {
            endTimer -= Time.deltaTime;
            if (endTimer <= 0.0f)
            {
                endTimer = 0.0f;
                endUI.transform.GetChild(2).gameObject.SetActive(true);
                endUI.transform.GetChild(3).gameObject.SetActive(true);
            }
        }
    }

    private void SetScoreBoard()
    {
        if (scoreBoard.activeSelf)
        {
            scoreBoard.GetComponent<Text>().text = "<color=#ffffff>" + gm.scoreL + " : " + gm.scoreR + "</color>";
        }
    }

    private void SetTimerBoard()
    {
        if (timerBoard.activeSelf)
        {
            int sec = Mathf.RoundToInt(gm.timer * 100) - Mathf.FloorToInt(gm.timer) * 100;
            string tempStr = gm.timer.ToString("00") + " : " + sec.ToString("00");
            if (gm.timer < 10.0f)
            {
                tempStr = "<color=#ff0000>" + tempStr + "</color>";
            }
            else
            {
                tempStr = "<color=#ffffff>" + tempStr + "</color>";
            }
            timerBoard.GetComponent<Text>().text = tempStr;
        }
    }

    public void RestartB()
    {
        gm.SetPhase(-1);
    }

    public void TitleB()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
