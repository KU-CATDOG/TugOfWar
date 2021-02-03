﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public int winScore;
    public int scoreL;
    public int scoreR;
    public int chanceL;
    public int chanceR;

    public float dragForceL;
    public float dragForceR;
    public float ropeSpeed;

    public float timer; //Main Timer
    public float timer2; //Extra Timer for MiniGame
    public float timerSpeed;
    public float tikInterval;
    private float tik;

    private GameObject rope;
    public GameObject ropePrefab;
    private Vector3 ropeInitPos;

    public GameObject mainCamera;

    public int characterCnt;
    public GameObject character1;
    public GameObject character2;
    public GameObject character3;
    private GameObject characterL;
    private GameObject characterR;
    List<GameObject> characterList;

    public GameObject miniControl;

    public bool isTikStop;
    public bool isMovable;
    public bool isTimerStop;

    public int phase; // 0:StartScreen, 1:GameReady, 2:InGame, 3:EndGame

    void Start()// before the first frame update
    {
        characterCnt = 3;

        if (SelectMenu.selectionL == 0 || SelectMenu.selectionL > characterCnt)
        {
            SelectMenu.selectionL = 1;
        }
        if (SelectMenu.selectionR == 0 || SelectMenu.selectionR > characterCnt)
        {
            SelectMenu.selectionR = 1;
        }

        Debug.Log(SelectMenu.selectionL);
        Debug.Log(SelectMenu.selectionR);

        characterList = new List<GameObject>();
        characterList.Add(character1);
        characterList.Add(character2);
        characterList.Add(character3);

        scoreL = 0;
        scoreR = 0;
        winScore = 2;
        chanceL = 4;
        chanceR = 4;
        timer = 30.0f;
        timer2 = 0.0f;
        timerSpeed = 1.0f;
        dragForceL = 0.0f;
        dragForceR = 0.0f;
        ropeSpeed = 0.2f;
        tikInterval = 0.5f;
        ropeInitPos = new Vector3(0, -5, 0);

        SetPhase(0);

        FreezeAll();

        miniControl.SetActive(false);
    }

    void Update()// per frame
    {
        if (phase == 0)
        {
            ResetGame();
        }
        else if (phase == 1)
        {
            rope = Instantiate(ropePrefab, ropeInitPos, Quaternion.identity);
            mainCamera.transform.SetParent(rope.transform, true);

            SetCharacter("L", SelectMenu.selectionL);
            SetCharacter("R", SelectMenu.selectionR);

            UnfreezeAll();
            SetPhase(2);
        }
        else if (phase == 2)
        {
            EventCheck();
        }

        if (!isTimerStop)
        {
            timer -= timerSpeed * Time.deltaTime;
        }

        if (timer2 > 0.0f)
        {
            timer2 -= Time.deltaTime;
            if (timer2 < 0.0f)
            {
                timer2 = 0.0f;
                if (miniControl.activeSelf)
                {
                    miniControl.SetActive(false);
                }
            }
        }

        if (!isTikStop)
        {
            tik += Time.deltaTime;
        }

        if (tik > tikInterval)
        {
            tik = 0.0f;
            Debug.Log("SetDragForce!");
            SetDragForce();
            Debug.Log("1P Force : " + dragForceL.ToString("N2"));
            Debug.Log("2P Force : " + dragForceR.ToString("N2"));
            Debug.Log("1P : " + characterL.GetComponent<Character>().player);
            Debug.Log("2P : " + characterR.GetComponent<Character>().player);
        }

        if (isMovable)
        {
            rope.transform.position += new Vector3(ropeSpeed * (dragForceR - dragForceL) * Time.deltaTime, 0, 0);
        }
    }

    private void ResetGame()
    {
        scoreL = 0;
        scoreR = 0;
        chanceL = 4;
        chanceR = 4;
        timer = 30.0f;
        dragForceL = 0.0f;
        dragForceR = 0.0f;
        mainCamera.transform.parent = null;
        mainCamera.transform.position = new Vector3(0, 0, -10);
        Destroy(rope);

        FreezeAll();
        SetPhase(0);
    }

    private void SetDragForce()
    {
        dragForceL = characterL.GetComponent<Character>().returnForce();
        dragForceR = characterR.GetComponent<Character>().returnForce();
    }

    private void EventCheck() //for events, freeze...
    {
        if (characterL.GetComponent<Character>().freeze || characterR.GetComponent<Character>().freeze)//Freeze 받아오기
        {
            FreezeAll(); //FreezeAllFor()?
        }

        float comPos = rope.transform.position.x * 2f;

        if (timer <= 0.0f) //When Times Up
        {
            if (comPos < 0)
            {
                Debug.Log("1P Takes The Point!");
                scoreL++;
                CalScore();
            }
            else if (comPos > 0)
            {
                Debug.Log("2P Takes The Point!");
                scoreR++;
                CalScore();
            }
            else
            {
                Debug.Log("Draw! No One Takes The Point!");
                CalScore();
            }
        }
        else //According to Distance
        {
            if (comPos <= -10 && chanceL == 4 || comPos <= -20 && chanceL == 3 || comPos <= -30 && chanceL == 2 || comPos <= -40 && chanceL == 1)
            {
                chanceL--;
                miniControl.SetActive(true);
                timer2 = 2.0f; //Timer for Auto-Disable
            }
            else if (comPos <= -50 && chanceL == 0)
            {
                Debug.Log("1P Takes The Point!");
                scoreL++;
                CalScore();
            }
            else if (comPos >= 10 && chanceR == 4 || comPos >= 20 && chanceR == 3 || comPos >= 30 && chanceR == 2 || comPos >= 40 && chanceR == 1)
            {
                chanceR--;
                miniControl.SetActive(true);
                timer2 = 2.0f; //Timer for Auto-Disable
            }
            else if (comPos >= 50 && chanceR == 0)
            {
                Debug.Log("2P Takes The Point!");
                scoreR++;
                CalScore();
            }
        }

        if (miniControl.activeSelf)
        {
            int winnerIdx = miniControl.GetComponent<minigame>().miniWin;
            if (winnerIdx != 0)
            {
                if (winnerIdx == 1)
                {
                    UnityEngine.Debug.Log("미니게임의 승자는 1P!");
                    //Add dragForce
                    miniControl.SetActive(false);
                }
                else if (winnerIdx == 2)
                {
                    UnityEngine.Debug.Log("미니게임의 승자는 2P!");
                    //Add dragForce
                    miniControl.SetActive(false);
                }
            }
        }
    }

    private void CalScore() //Set Score and Manage Progression
    {
        if (scoreL == winScore)
        {
            FreezeAll();
            Debug.Log("1P Win!");
            SetPhase(3);
        }
        else if (scoreR == winScore)
        {
            FreezeAll();
            Debug.Log("2P Win!");
            SetPhase(3);
        }
        else
        {
            FreezeAll();
            timer = 30.0f;
            tik = 0.0f;
            rope.transform.position = ropeInitPos;
            //Effects, texts
            UnfreezeAll();
        }
    }

    /*==============================Can be used in other scripts=====================================*/

    public void SetPhase(int i)
    {
        phase = i;
    }

    public void SetCharacter(string pos, int character) //Can used to set or change character
    {
        //destroy previous character if exists
        //Instantiate with Position, Parent(rope)
        //Can Add FreezeTime
        FreezeAll();

        if (pos == "L" || pos == "Left")
        {
            if (0 < character && character <= characterCnt)
            {
                SelectMenu.selectionL = character;

                if (characterL != null)
                {
                    Destroy(characterL);
                }

                for (int i = 0; i < characterCnt; i++)
                {
                    if (character == (i + 1))
                    {
                        characterL = Instantiate(characterList[i]);
                    }
                }

                characterL.transform.SetParent(rope.transform, false);
                characterL.transform.localPosition = new Vector3(-5, 0.36f, 0);
                characterL.GetComponent<Character>().player = 0;
            }
        }
        else if (pos == "R" || pos == "Right")
        {
            if (0 < character && character <= characterCnt)
            {
                SelectMenu.selectionR = character;

                if (characterR != null)
                {
                    Destroy(characterR);
                }

                for (int i = 0; i < characterCnt; i++)
                {
                    if (character == (i + 1))
                    {
                        characterR = Instantiate(characterList[i]);
                    }
                }

                characterR.transform.SetParent(rope.transform, false);
                characterR.transform.localPosition = new Vector3(5, 0.36f, 0);
                characterR.GetComponent<SpriteRenderer>().flipX = true;
                characterR.GetComponent<Character>().player = 1;
            }
        }

        UnfreezeAll();
    }

    public void FreezeAll()
    {
        isMovable = false;
        isTikStop = true;
        isTimerStop = true;
    }

    public void UnfreezeAll()
    {
        isMovable = true;
        isTikStop = false;
        isTimerStop = false;
    }

    public void FreezeAllFor(float time)
    {
        isMovable = false;
        isTikStop = true;
        isTimerStop = true;
    }
}
