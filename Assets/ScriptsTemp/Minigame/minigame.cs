using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minigame : MonoBehaviour
{
    public int r = 0;
    public int miniWin;     //Left win: 1, Right win: 2
    private int count;
    public Button pressB;
    public Button pressN;
    public Button pressM;
    public Button pressG;
    public Button pressH;
    public Button pressJ;
    public Button pressT;
    public Button pressY;
    public Button pressU;
    public Button pressOne;
    public Button pressTwo;
    public Button pressThree;
    public Button pressFour;
    public Button pressFive;
    public Button pressSix;
    public Button pressSeven;
    public Button pressEight;
    public Button pressNine;
    public GameObject pressLeft;
    public GameObject pressRight;

    void OnEnable()
    {
        count = 0;
        miniWin = 0;
        r = Random.Range(1, 10);
        pressLeft.gameObject.SetActive(true);
        pressRight.gameObject.SetActive(true);

        ChangeChildColor(Color.red, r);
    }

    void Update()
    {
        if (count == 0)
        {
            switch (r)
                {
                case 1:
                    CheckInput("b", "[1]");
                    break;
                case 2:
                    CheckInput("n", "[2]");
                    break;
                case 3:
                    CheckInput("m", "[3]");
                    break;
                case 4:
                    CheckInput("g", "[4]");
                    break;
                case 5:
                    CheckInput("h", "[5]");
                    break;
                case 6:
                    CheckInput("j", "[6]");
                    break;
                case 7:
                    CheckInput("t", "[7]");
                    break;
                case 8:
                    CheckInput("y", "[8]");
                    break;
                case 9:
                    CheckInput("u", "[9]");
                    break;
                default:
                    break;
            }
        }
    }

    private void CheckInput(string leftKey, string rightKey)
    {
        if (Input.GetKeyDown(leftKey))
        {
            ChangeChildColor(Color.white);
            count++;
            miniWin = 1;
        }
        else if (Input.GetKeyDown(rightKey))
        {
            ChangeChildColor(Color.white);
            count++;
            miniWin = 2;
        }
    }

    private void ChangeChildColor(UnityEngine.Color col, int i = 0)
    {
        List<Image> imgLst = new List<Image>();
        imgLst.AddRange(pressLeft.transform.GetComponentsInChildren<Image>());
        imgLst.AddRange(pressRight.transform.GetComponentsInChildren<Image>());

        if (i == 0)
        {
            foreach (Image img in imgLst)
            {
                img.color = col;
            }
        }
        else
        {
            imgLst[i - 1].color = col;
            imgLst[imgLst.Count / 2 + i - 1].color = col;
        }
    }

    private void OnDisable()
    {
        pressLeft.gameObject.SetActive(false);
        pressRight.gameObject.SetActive(false);
    }
}
