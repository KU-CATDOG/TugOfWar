using System.Collections;
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
    private int checkL;
    private int checkR;

    public void Start()
    {
        selectCntL = 1;
        selectCntR = 1;
        selectionL = 0;
        selectionR = 0;
        checkL = 0;
        checkR = 0;
        player1.SetActive(true);
        player2.SetActive(true);
        selectionCheck.SetActive(false);
    }

    public void Update()
    {
        if (selectCntL == 0 && selectCntR == 0)
        {
            player1.SetActive(false);
            player2.SetActive(false);
            selectionCheck.SetActive(true);
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
        ActiveL(6);
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
        ActiveR(6);
    }

    private void ActiveL(int i)
    {
        SoundManager.instance.playButtonSound();

        if (selectCntL > 0)
        {
            Debug.Log("버튼을 누른다. 캐릭터가 선택되었습니다.(1P)");
            selectCntL--;
            selectionL = i;
        }
        else if (selectionL == i)
        {
            Debug.Log("이미 선택된 캐릭터입니다.(1P)");
        }
        else if (checkL == i)
        {
            Debug.Log("캐릭터가 변경되었습니다.(1P)");
            checkL = 0;
            selectionL = i;
        }
        else
        {
            Debug.Log("이미 고르셨습니다. 다시 한 번 누르면 해당 캐릭터로 변경됩니다.(1P)");
            checkL = i;
        }
    }
    private void ActiveR(int i)
    {
        SoundManager.instance.playButtonSound();

        if (selectCntR > 0)
        {
            Debug.Log("버튼을 누른다. 캐릭터가 선택되었습니다.(2P)");
            selectCntR--;
            selectionR = i;
        }
        else if (selectionR == i)
        {
            Debug.Log("이미 선택된 캐릭터입니다.(2P)");
        }
        else if (checkR == i)
        {
            Debug.Log("캐릭터가 변경되었습니다.(2P)");
            checkR = 0;
            selectionR = i;
        }
        else
        {
            Debug.Log("이미 고르셨습니다. 다시 한 번 누르면 해당 캐릭터로 변경됩니다.(2P)");
            checkR = i;
        }
    }
    
    public void ReSelect()
    {
        SoundManager.instance.playButtonSound();

        selectCntL = 1;
        selectCntR = 1;
        selectionL = 0;
        selectionR = 0;
        checkL = 0;
        checkR = 0;
        player1.SetActive(true);
        player2.SetActive(true);
        selectionCheck.SetActive(false);
    }
    public void ExtraSelectB()
    {
        SoundManager.instance.playButtonSound();

        SceneManager.LoadScene("SelectExtra");
    }
}
