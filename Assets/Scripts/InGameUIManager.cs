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

    public GameObject timerBoard;
    public Sprite scoreOnSprite;
    public Sprite scoreOffSprite;
    public Sprite readySprite;
    public Sprite startSprite;

    public GameObject blindImage;

    private float endTimer;
    private float readyTimer;

    private List<int> tempExtraLstL;
    private List<int> tempExtraLstR;

    private List<GameObject> extraObjLstL;
    private List<GameObject> extraObjLstR;

    public List<GameObject> extraPrefabLst;

    private List<Vector3> extraPosL;
    private List<Vector3> extraPosR;

    void Start()
    {
        gm = gameManager.GetComponent<GM>();
        tempPhase = -1;

        blindImage.SetActive(false);
        inGameUI.transform.Find("ReadyImage").gameObject.SetActive(false);

        endTimer = 0.0f;
        readyTimer = 0.0f;

        tempExtraLstL = new List<int>();
        tempExtraLstR = new List<int>();

        extraObjLstL = new List<GameObject>();
        extraObjLstR = new List<GameObject>();

        #region 엑스트라위치설정
        extraPosL = new List<Vector3>();
        extraPosL.Add(new Vector3(-400, -200, 0));
        extraPosL.Add(new Vector3(-300, -200, 0));
        extraPosL.Add(new Vector3(-400, 200, 0));
        extraPosL.Add(new Vector3(-300, 200, 0));
        extraPosL.Add(new Vector3(-200, 200, 0));
        extraPosL.Add(new Vector3(0, 0, 0));

        extraPosR = new List<Vector3>();
        extraPosR.Add(new Vector3(400, -200, 0));
        extraPosR.Add(new Vector3(300, -200, 0));
        extraPosR.Add(new Vector3(400, 200, 0));
        extraPosR.Add(new Vector3(300, 200, 0));
        extraPosR.Add(new Vector3(200, 200, 0));
        extraPosR.Add(new Vector3(0, 0, 0));
        #endregion
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

            SetExtraBoard(0);
        }
        else if (gm.phase == 3) //EndGame
        {
            startUI.SetActive(false);
            inGameUI.SetActive(true);
            endUI.SetActive(true);

            if (gm.scoreL == gm.winScore)
            {
                endUI.transform.GetChild(1).gameObject.SetActive(true);
                endUI.transform.GetChild(2).gameObject.SetActive(false);
            }
            else if (gm.scoreR == gm.winScore)
            {
                endUI.transform.GetChild(1).gameObject.SetActive(false);
                endUI.transform.GetChild(2).gameObject.SetActive(true);
            }

            endUI.transform.GetChild(3).gameObject.SetActive(false);
            endUI.transform.GetChild(4).gameObject.SetActive(false);

            endTimer = 3.0f;

            InGameScreenUI();
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
        SetExtraBoard();
        ReadyText();
    }

    private void EndScreenUI()
    {
        if (endTimer > 0.0f)
        {
            endTimer -= Time.deltaTime;
            if (endTimer <= 0.0f)
            {
                endTimer = 0.0f;
                endUI.transform.GetChild(3).gameObject.SetActive(true);
                endUI.transform.GetChild(4).gameObject.SetActive(true);
            }
        }
    }

    private void SetScoreBoard()
    {
        GameObject scoreBoard = inGameUI.transform.Find("ScoreBoard").gameObject;
        if (gm.scoreL == 0)
        {
            for (int idx = 0; idx < 3; idx++)
            {
                scoreBoard.transform.GetChild(idx).GetComponent<Image>().sprite = scoreOffSprite;
            }
        }
        else if (gm.scoreL == 1)
        {
            scoreBoard.transform.GetChild(0).GetComponent<Image>().sprite = scoreOnSprite;
        }
        else if (gm.scoreL == 2)
        {
            scoreBoard.transform.GetChild(1).GetComponent<Image>().sprite = scoreOnSprite;
        }
        else if (gm.scoreL == 3)
        {
            scoreBoard.transform.GetChild(2).GetComponent<Image>().sprite = scoreOnSprite;
        }
        if (gm.scoreR == 0)
        {
            for (int idx = 3; idx < 6; idx++)
            {
                scoreBoard.transform.GetChild(idx).GetComponent<Image>().sprite = scoreOffSprite;
            }
        }
        else if (gm.scoreR == 1)
        {
            scoreBoard.transform.GetChild(3).GetComponent<Image>().sprite = scoreOnSprite;
        }
        else if (gm.scoreR == 2)
        {
            scoreBoard.transform.GetChild(4).GetComponent<Image>().sprite = scoreOnSprite;
        }
        else if (gm.scoreR == 3)
        {
            scoreBoard.transform.GetChild(5).GetComponent<Image>().sprite = scoreOnSprite;
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

    private void SetExtraBoard(int idx = 1)
    {
        if (idx == 0)
        {
            for (int i = tempExtraLstL.Count - 1; i >= 0; i--)
            {
                Destroy(extraObjLstL[i]);
            }
            for (int i = tempExtraLstR.Count - 1; i >= 0; i--)
            {
                Destroy(extraObjLstR[i]);
            }
            extraObjLstL = new List<GameObject>();
            extraObjLstR = new List<GameObject>();
            tempExtraLstL = new List<int>();
            tempExtraLstR = new List<int>();
        }
        else if (idx == 1)
        {
            if (tempExtraLstL != gm.extraLstL)
            {
                for (int i = tempExtraLstL.Count - 1; i >= 0; i--)
                {
                    if (!gm.extraLstL.Contains(tempExtraLstL[i]))
                    {
                        Destroy(extraObjLstL[i]);
                        tempExtraLstL.RemoveAt(i);
                    }
                }
                foreach (int extra in gm.extraLstL)
                {
                    if (!tempExtraLstL.Contains(extra))
                    {
                        tempExtraLstL.Add(extra);
                        GameObject tempObj = Instantiate(extraPrefabLst[extra - 1]);
                        tempObj.transform.SetParent(inGameUI.transform, false);
                        tempObj.transform.localPosition = extraPosL[extraObjLstL.Count];
                        extraObjLstL.Add(tempObj);
                    }
                }
            }
            if (tempExtraLstR != gm.extraLstR)
            {
                for (int i = tempExtraLstR.Count - 1; i >= 0; i--)
                {
                    if (!gm.extraLstR.Contains(tempExtraLstR[i]))
                    {
                        Destroy(extraObjLstR[i]);
                        tempExtraLstR.RemoveAt(i);
                    }
                }
                foreach (int extra in gm.extraLstR)
                {
                    if (!tempExtraLstR.Contains(extra))
                    {
                        tempExtraLstR.Add(extra);
                        GameObject tempObj = Instantiate(extraPrefabLst[extra - 1]);
                        tempObj.transform.SetParent(inGameUI.transform, false);
                        tempObj.transform.localPosition = extraPosR[extraObjLstR.Count];
                        extraObjLstR.Add(tempObj);
                    }
                }
            }
        }
    }

    private void ReadyText()
    {
        GameObject readyImage = inGameUI.transform.Find("ReadyImage").gameObject;
        if (gm.isReady)
        {
            gm.isReady = false;
            readyTimer = 4.0f;
            readyImage.SetActive(true);
            readyImage.GetComponent<Image>().sprite = readySprite;
        }
        if (readyTimer > 0.0f)
        {
            readyTimer -= Time.deltaTime;
            if (readyTimer <= 0.0f)
            {
                readyTimer = 0.0f;
                readyImage.SetActive(false);
                readyImage.GetComponent<Image>().sprite = readySprite;
            }
            else if (readyTimer <= 1.0f)
            {
                readyImage.GetComponent<Image>().sprite = startSprite;
            }
        }
    }

    public void RestartB()
    {
        gm.SetPhase(-1);

        SoundManager.instance.playButtonSound();
    }

    public void TitleB()
    {
        SoundManager.instance.playButtonSound();

        SceneManager.LoadScene("StartMenu");
    }
}
