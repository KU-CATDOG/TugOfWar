using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectExtra : MonoBehaviour
{
    public static int extraL1;
    public static int extraL2;
    public static int extraR1;
    public static int extraR2;
    private int CountL = 1;
    private int CountR = 1;
    public GameObject p1Buttons;
    public GameObject p2Buttons;

    public GameObject selectionCheck;

    private List<Vector3> framePos;
    public GameObject frameImagePrefab;
    private GameObject frameL1;
    private GameObject frameL2;
    private GameObject frameR1;
    private GameObject frameR2;

    private float t = 0;

    public void button11()
    {
        selectL(1);
    }
    public void button12()
    {
        selectL(2);
    }
    public void button13()
    {
        selectL(3);
    }
    public void button14()
    {
        selectL(4);
    }
    public void button15()
    {
        selectL(5);
    }
    public void button16()
    {
        selectL(6);
    }
    public void button21()
    {
        selectR(1);
    }
    public void button22()
    {
        selectR(2);
    }
    public void button23()
    {
        selectR(3);
    }
    public void button24()
    {
        selectR(4);
    }
    public void button25()
    {
        selectR(5);
    }
    public void button26()
    {
        selectR(6);
    }

    private void Start()
    {
        extraL1 = 0;
        extraL2 = 0;
        extraR1 = 0;
        extraR2 = 0;
        p1Buttons.SetActive(true);
        p2Buttons.SetActive(true);
        selectionCheck.SetActive(false);

        framePos = new List<Vector3>();

        MoveFrame(true);
    }
    private void Update()
    {
        SlowSelectionCheck();
        MoveFrame();
    }
    public void ReSelectB()
    {
        CountL = 1;
        CountR = 1;
        extraL1 = 0;
        extraL2 = 0;
        extraR1 = 0;
        extraR2 = 0;

        p1Buttons.SetActive(true);
        p2Buttons.SetActive(true);
        selectionCheck.SetActive(false);

        if (GameObject.Find("SoundManageObject") != null)
        {
            SoundManager.instance.playButtonSound();
        }
    }
    public void GameStartB()
    {
        SceneManager.LoadScene("InGame");

        if (GameObject.Find("SoundManageObject") != null)
        {
            SoundManager.instance.playButtonSound();
        }
    }

    private void selectL(int i)
    {
        if (CountL == 1)
        {
            extraL1 = i;
            CountL++;
            if (frameL1 != null)
            {
                Destroy(frameL1);
            }
            frameL1 = Instantiate(frameImagePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            frameL1.transform.SetParent(p1Buttons.transform, false);
        }
        else if (CountL == 2)
        {
            if (frameL2 != null)
            {
                Destroy(frameL2);
            }
            frameL2 = Instantiate(frameImagePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            frameL2.transform.SetParent(p1Buttons.transform, false);
            extraL2 = i;
            CountL++;
        }
        else
        {
            extraL2 = i;
            CountL = 3;
        }

        if (GameObject.Find("SoundManageObject") != null)
        {
            SoundManager.instance.playButtonSound();
        }

        Debug.Log("P1 Extra" + i + " Selected");
    }

    private void selectR(int i)
    {
        if (CountR == 1)
        {
            extraR1 = i;
            CountR++;
            if (frameR1 != null)
            {
                Destroy(frameR1);
            }
            frameR1 = Instantiate(frameImagePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            frameR1.transform.SetParent(p2Buttons.transform, false);
        }
        else if (CountR == 2)
        {
            if (frameR2 != null)
            {
                Destroy(frameR2);
            }
            frameR2 = Instantiate(frameImagePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            frameR2.transform.SetParent(p2Buttons.transform, false);
            extraR2 = i;
            CountR++;
        }
        else
        {
            extraR2 = i;
            CountR = 3;
        }

        if (GameObject.Find("SoundManageObject") != null)
        {
            SoundManager.instance.playButtonSound();
        }

        Debug.Log("P2 Extra" + i + " Selected");
    }

    private void SlowSelectionCheck(bool isSlow = true)
    {
        if (isSlow)
        {
            if (extraL2 != 0 && extraR2 != 0)
            {
                if (CountL == 3 || CountR == 3)
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
                    p1Buttons.SetActive(false);
                    p2Buttons.SetActive(false);
                    selectionCheck.SetActive(true);
                }
            }
        }
        else
        {
            if (extraL2 != 0 && extraR2 != 0)
            {
                p1Buttons.SetActive(false);
                p2Buttons.SetActive(false);
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
                framePos.Add(p1Buttons.transform.GetChild(i).transform.position);
            }
            for (int i = 1; i <= 6; i++)
            {
                framePos.Add(p2Buttons.transform.GetChild(i).transform.position);
            }
        }
        else
        {
            if (frameL1 != null && extraL1 != 0)
            {
                frameL1.transform.position = Vector3.Lerp(frameL1.transform.position, framePos[extraL1 - 1], 0.1f);
            }
            if (frameL2 != null && extraL2 != 0)
            {
                frameL2.transform.position = Vector3.Lerp(frameL2.transform.position, framePos[extraL2 - 1], 0.1f);
            }
            if (frameR1 != null && extraR1 != 0)
            {
                frameR1.transform.position = Vector3.Lerp(frameR1.transform.position, framePos[6 + extraR1 - 1], 0.1f);
            }
            if (frameR2 != null && extraR2 != 0)
            {
                frameR2.transform.position = Vector3.Lerp(frameR2.transform.position, framePos[6 + extraR2 - 1], 0.1f);
            }
        }
    }
}
