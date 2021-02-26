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

    private List<Vector3> framePos;
    public GameObject frameImagePrefab;
    private GameObject frameL;
    private GameObject frameR;

    private float t = 0;
    private float t2 = 0;

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

        framePos = new List<Vector3>();

        MoveFrame(true);
        PopUp(true);
    }

    public void Update()
    {
        SlowSelectionCheck();
        PopUp();
        MoveFrame();
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

    private void SlowSelectionCheck(bool isSlow = true)
    {
        if (isSlow)
        {
            if (selectCntL == 0 && selectCntR == 0)
            {
                t = 1.0f;
                selectCntL = -1;
                selectCntR = -1;
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

    private void PopUp(bool isReset = false)
    {
        if (isReset)
        {
            for (int i = 1; i <= 6; i++)
            {
                player1.transform.GetChild(i).transform.localScale = 0.1f * new Vector3(1, 1, 1);
                player1.transform.GetChild(i).gameObject.SetActive(false);

                player2.transform.GetChild(i).transform.localScale = 0.1f * new Vector3(1, 1, 1);
                player2.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else
        {
            t2 += Time.deltaTime;

            for (int i = 1; i < 7; i++)
            {
                if (t2 > i * 0.5f)
                {
                    if (!player1.transform.GetChild(i).gameObject.activeSelf) player1.transform.GetChild(i).gameObject.SetActive(true);
                    if (!player2.transform.GetChild(i).gameObject.activeSelf) player2.transform.GetChild(i).gameObject.SetActive(true);
                    if (player1.transform.GetChild(i).transform.localScale.x < 1)
                    {
                        player1.transform.GetChild(i).transform.localScale += 1.0f * new Vector3(1, 1, 1) * Time.deltaTime;
                        player2.transform.GetChild(i).transform.localScale += 1.0f * new Vector3(1, 1, 1) * Time.deltaTime;
                    }
                }
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
        checkL = 0;
        checkR = 0;
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
}
