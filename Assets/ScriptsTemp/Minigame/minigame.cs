using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minigame : MonoBehaviour
{
    public int r = 0;
    public int miniWin = 0;     //Left win: 1, Right win: 2
    private int count = 0;
    public Button pressT;
    public Button pressY;
    public Button pressU;
    public Button pressG;
    public Button pressH;
    public Button pressJ;
    public Button pressB;
    public Button pressN;
    public Button pressM;
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
        r = Random.Range(1, 10);
        pressLeft.gameObject.SetActive(true);
        pressRight.gameObject.SetActive(true);
        
        switch (r)
        {
            case 1:
                pressB.gameObject.GetComponent<Image>().color = Color.red;
                pressOne.gameObject.GetComponent<Image>().color = Color.red;
                break;
            case 2:
                pressN.gameObject.GetComponent<Image>().color = Color.red;
                pressTwo.gameObject.GetComponent<Image>().color = Color.red;
                break;
            case 3:
                pressM.gameObject.GetComponent<Image>().color = Color.red;
                pressThree.gameObject.GetComponent<Image>().color = Color.red;
                break;
            case 4:
                pressG.gameObject.GetComponent<Image>().color = Color.red;
                pressFour.gameObject.GetComponent<Image>().color = Color.red;
                break;
            case 5:
                pressH.gameObject.GetComponent<Image>().color = Color.red;
                pressFive.gameObject.GetComponent<Image>().color = Color.red;
                break;
            case 6:
                pressJ.gameObject.GetComponent<Image>().color = Color.red;
                pressSix.gameObject.GetComponent<Image>().color = Color.red;
                break;
            case 7:
                pressT.gameObject.GetComponent<Image>().color = Color.red;
                pressSeven.gameObject.GetComponent<Image>().color = Color.red;
                break;
            case 8:
                pressY.gameObject.GetComponent<Image>().color = Color.red;
                pressEight.gameObject.GetComponent<Image>().color = Color.red;
                break;
            case 9:
                pressU.gameObject.GetComponent<Image>().color = Color.red;
                pressNine.gameObject.GetComponent<Image>().color = Color.red;
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (count == 0)
        {
            switch (r)
                {
                case 1:
                    if (Input.GetKeyDown("b"))
                    {
                        miniWin = 1;
                        count++;
                    }
                    else if (Input.GetKeyDown("[1]"))
                    {
                        miniWin = 2;
                        count++;
                    }
                    break;
                case 2:
                    if (Input.GetKeyDown("n"))
                    {
                        miniWin = 1;
                        count++;
                    }
                    else if (Input.GetKeyDown("[2]"))
                    {
                        miniWin = 2;
                        count++;
                    }
                    break;
                case 3:
                    if (Input.GetKeyDown("m"))
                    {
                        miniWin = 1;
                        count++;
                    }
                    else if (Input.GetKeyDown("[3]"))
                    {
                        miniWin = 2;
                        count++;
                    }
                    break;
                case 4:
                    if (Input.GetKeyDown("g"))
                    {
                        miniWin = 1;
                        count++;
                    }
                    else if (Input.GetKeyDown("[4]"))
                    {
                        miniWin = 2;
                        count++;
                    }
                    break;
                case 5:
                    if (Input.GetKeyDown("h"))
                    {
                        miniWin = 1;
                        count++;
                    }
                    else if (Input.GetKeyDown("[5]"))
                    {
                        miniWin = 2;
                        count++;
                    }
                    break;
                case 6:
                    if (Input.GetKeyDown("j"))
                    {
                        miniWin = 1;
                        count++;
                    }
                    else if (Input.GetKeyDown("[6]"))
                    {
                        miniWin = 2;
                        count++;
                    }
                    break;
                case 7:
                    if (Input.GetKeyDown("t"))
                    {
                        miniWin = 1;
                        count++;
                    }
                    else if (Input.GetKeyDown("[7]"))
                    {
                        miniWin = 2;
                        count++;
                    }
                    break;
                case 8:
                    if (Input.GetKeyDown("y"))
                    {
                        miniWin = 1;
                        count++;
                    }
                    else if (Input.GetKeyDown("[8]"))
                    {
                        miniWin = 2;
                        count++;
                    }
                    break;
                case 9:
                    if (Input.GetKeyDown("u"))
                    {
                        miniWin = 1;
                        count++;
                    }
                    else if (Input.GetKeyDown("[9]"))
                    {
                        miniWin = 2;
                        count++;
                    }
                    break;
                default:
                    break;
            }
            pressLeft.gameObject.GetComponentInChildren<Image>().color = Color.white;
            pressRight.gameObject.GetComponentInChildren<Image>().color = Color.white;
        }
    }

    private void OnDisable()
    {
        pressLeft.gameObject.SetActive(false);
        pressRight.gameObject.SetActive(false);
    }
}
