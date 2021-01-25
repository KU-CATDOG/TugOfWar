using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigame : MonoBehaviour
{
    public int r = 0;
    public int miniWin = 0;     //Left win: 1, Right win: 2

    void Start()
    {
        r = Random.Range(1, 9);
    }

    void Update()
    {
        
        switch (r)
        {
            case 1:
                if (Input.GetKeyDown("b"))
                {
                    miniWin = 1;
                }
                else if (Input.GetKeyDown("[1]"))
                {
                    miniWin = 2;
                }
                break;
            case 2:
                if (Input.GetKeyDown("n"))
                {
                    miniWin = 1;
                }
                else if (Input.GetKeyDown("[2]"))
                {
                    miniWin = 2;
                }
                break;
            case 3:
                if (Input.GetKeyDown("m"))
                {
                    miniWin = 1;
                }
                else if (Input.GetKeyDown("[3]"))
                {
                    miniWin = 2;
                }
                break;
            case 4:
                if (Input.GetKeyDown("g"))
                {
                    miniWin = 1;
                }
                else if (Input.GetKeyDown("[4]"))
                {
                    miniWin = 2;
                }
                break;
            case 5:
                if (Input.GetKeyDown("h"))
                {
                    miniWin = 1;
                }
                else if (Input.GetKeyDown("[5]"))
                {
                    miniWin = 2;
                }
                break;
            case 6:
                if (Input.GetKeyDown("j"))
                {
                    miniWin = 1;
                }
                else if (Input.GetKeyDown("[6]"))
                {
                    miniWin = 2;
                }
                break;
            case 7:
                if (Input.GetKeyDown("t"))
                {
                    miniWin = 1;
                }
                else if (Input.GetKeyDown("[7]"))
                {
                    miniWin = 2;
                }
                break;
            case 8:
                if (Input.GetKeyDown("y"))
                {
                    miniWin = 1;
                }
                else if (Input.GetKeyDown("[8]"))
                {
                    miniWin = 2;
                }
                break;
            case 9:
                if (Input.GetKeyDown("u"))
                {
                    miniWin = 1;
                }
                else if (Input.GetKeyDown("[9]"))
                {
                    miniWin = 2;
                }
                break;
            default:
                break;
        }
    }
}
