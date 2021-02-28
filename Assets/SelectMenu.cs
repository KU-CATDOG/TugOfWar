﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class SelectMenu : MonoBehaviour
{
    public Button temp;
    public GameObject player1;
    public GameObject player2;
    public GameObject selectionCheck;
    private int selectCntL;
    private int selectCntR;
    public static int selectionL;
    public static int selectionR;
    private int CountL;
    private int CountR;

    private List<Vector3> framePos;
    public GameObject frameImagePrefab;
    private GameObject frameL;
    private GameObject frameR;

    private float t = 0;

    public GameObject pauseScreen;

    public void Start()
    {
        selectCntL = 1;
        selectCntR = 1;
        selectionL = 0;
        selectionR = 0;
        CountL = 0;
        CountR = 0;
        player1.SetActive(true);
        player2.SetActive(true);
        selectionCheck.SetActive(false);

        framePos = new List<Vector3>();

        MoveFrame(true);

        pauseScreen.SetActive(false);
    }

    public void Update()
    {
        SlowSelectionCheck();
        MoveFrame();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeSelf)
            {
                pauseScreen.SetActive(false);
            }
            else
            {
                pauseScreen.SetActive(true);
            }
        }
    }

    public void button1()
    {
        ActiveL(1);
    }
    public void button2()
    {
        ActiveL(2);
    }
    public void button3()
    {
        ActiveL(3);
    }
    public void button4()
    {
        ActiveL(4);
    }
    public void button5()
    {
        ActiveL(5);
    }
    public void button6()
    {
        UnityEngine.Debug.Log("Coming Soon!");
    }
    public void button21()
    {
        ActiveR(1);
    }
    public void button22()
    {
        ActiveR(2);
    }
    public void button23()
    {
        ActiveR(3);
    }
    public void button24()
    {
        ActiveR(4);
    }
    public void button25()
    {
        ActiveR(5);
    }
    public void button26()
    {
        UnityEngine.Debug.Log("Coming Soon!");
    }

    private void ActiveL(int i)
    {
        if (GameObject.Find("SoundManageObject") != null)
        {
            SoundManager.instance.playButtonSound();
        }

        if (selectCntL > 0)
        {
            Debug.Log("버튼을 누른다. 캐릭터가 선택되었습니다.(1P)");
            selectCntL--;
            selectionL = i;
            if (frameL != null)
            {
                Destroy(frameL);
            }
            frameL = Instantiate(frameImagePrefab, new Vector3 (0, 0, 0), Quaternion.identity);
            frameL.transform.SetParent(player1.transform, false);
            CountL = 1;
        }
        else if (selectionL == i)
        {
            Debug.Log("이미 선택된 캐릭터입니다.(1P)");
        }
        else
        {
            Debug.Log("캐릭터가 변경되었습니다.(1P)");
            selectionL = i;
            CountL = 1;
        }
    }
    private void ActiveR(int i)
    {
        if (GameObject.Find("SoundManageObject") != null)
        {
            SoundManager.instance.playButtonSound();
        }

        if (selectCntR > 0)
        {
            Debug.Log("버튼을 누른다. 캐릭터가 선택되었습니다.(2P)");
            selectCntR--;
            selectionR = i;
            if (frameR != null)
            {
                Destroy(frameR);
            }
            frameR = Instantiate(frameImagePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            frameR.transform.SetParent(player2.transform, false);
            CountR = 1;
        }
        else if (selectionR == i)
        {
            Debug.Log("이미 선택된 캐릭터입니다.(2P)");
        }
        else
        {
            Debug.Log("캐릭터가 변경되었습니다.(2P)");
            selectionR = i;
            CountR = 1;
        }
    }

    private void SlowSelectionCheck(bool isSlow = true)
    {
        if (isSlow)
        {
            if (selectCntL == 0 && selectCntR == 0)
            {
                if (CountL == 1 || CountR == 1)
                {
                    t = 1.0f;
                    CountL = 0;
                    CountR = 0;
                }
            }
            if (t > 0)
            {
                t -= Time.deltaTime;
                if (t <= 0)
                {
                    t = 0;
                    player1.SetActive(false);
                    player2.SetActive(false);
                    selectionCheck.SetActive(true);
                }
            }
        }
        else
        {
            if (selectCntL == 0 && selectCntR == 0)
            {
                player1.SetActive(false);
                player2.SetActive(false);
                selectionCheck.SetActive(true);
            }
        }
    }

    private void MoveFrame(bool isReset = false)
    {
        if (isReset)
        {
            for (int i = 1; i <= 6; i++)
            {
                framePos.Add(player1.transform.GetChild(i).transform.position);
            }
            for (int i = 1; i <= 6; i++)
            {
                framePos.Add(player2.transform.GetChild(i).transform.position);
            }
        }
        else
        {
            if (frameL != null && selectionL != 0)
            {
                frameL.transform.position = Vector3.Lerp(frameL.transform.position, framePos[selectionL - 1], 0.1f);
            }
            if (frameR != null && selectionR != 0)
            {
                frameR.transform.position = Vector3.Lerp(frameR.transform.position, framePos[6 + selectionR - 1], 0.1f);
            }
        }
    }
    
    public void ReSelect()
    {
        if (GameObject.Find("SoundManageObject") != null)
        {
            SoundManager.instance.playButtonSound();
        }

        selectCntL = 1;
        selectCntR = 1;
        selectionL = 0;
        selectionR = 0;
        player1.SetActive(true);
        player2.SetActive(true);
        selectionCheck.SetActive(false);
    }
    public void ExtraSelectB()
    {
        if (GameObject.Find("SoundManageObject") != null)
        {
            SoundManager.instance.playButtonSound();
        }

        SceneManager.LoadScene("SelectExtra");
    }

    public void ClosePauseScreen()
    {
        pauseScreen.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
