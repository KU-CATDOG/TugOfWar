using System.Collections;
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
    public float timer2; //Extra Timer
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

    private bool isPlayerFreeze;

    void Awake()// before the first frame update
    {
        ropeInitPos = new Vector3(0, -3.5f, 0);
        characterCnt = 3;
        isPlayerFreeze = false;

        if (SelectMenu.selectionL == 0 || SelectMenu.selectionL > characterCnt)
        {
            SelectMenu.selectionL = 1;
        }
        if (SelectMenu.selectionR == 0 || SelectMenu.selectionR > characterCnt)
        {
            SelectMenu.selectionR = 3;
        }

        Debug.Log(SelectMenu.selectionL);
        Debug.Log(SelectMenu.selectionR);

        characterList = new List<GameObject>();
        characterList.Add(character1); //Student
        characterList.Add(character2); //Rhythm
        characterList.Add(character3); //Anubis

        ResetGame();

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

            SetExtra("L", 1);
            SetExtra("R", 2);

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
        mainCamera.transform.parent = null;
        mainCamera.transform.position = new Vector3(0, 0, -10);
        if (rope != null)
        {
            Destroy(rope);
        }

        isPlayerFreeze = false;
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
            isPlayerFreeze = true;
        }
        else if (isPlayerFreeze)
        {
            UnfreezeAll();
            isPlayerFreeze = false;
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
                    UnfreezeAll();
                }
                else if (winnerIdx == 2)
                {
                    UnityEngine.Debug.Log("미니게임의 승자는 2P!");
                    //Add dragForce
                    miniControl.SetActive(false);
                    UnfreezeAll();
                }
            }
            else
            {
                FreezeAll();
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
            chanceL = 4;
            chanceR = 4;
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

    public void SetExtra(string pos, int extraNum)
    {
        if (pos == "L" || pos == "Left")
        {
            if (characterL != null)
            {
                switch (extraNum)
                {
                    case 1:
                        characterL.AddComponent<Extra1>();
                        break;
                    case 2:
                        characterL.AddComponent<Extra2>();
                        break;
                    case 3:
                        characterL.AddComponent<Extra3>();
                        break;
                    case 4:
                        characterL.AddComponent<Extra4>();
                        break;
                    case 5:
                        characterL.AddComponent<Extra5>();
                        break;
                    case 6:
                        characterL.AddComponent<Extra6>();
                        break;
                    default:
                        break;
                }
            }
        }
        else if (pos == "R" || pos == "Right")
        {
            if (characterR != null)
            {
                switch (extraNum)
                {
                    case 1:
                        characterR.AddComponent<Extra1>();
                        break;
                    case 2:
                        characterR.AddComponent<Extra2>();
                        break;
                    case 3:
                        characterR.AddComponent<Extra3>();
                        break;
                    case 4:
                        characterR.AddComponent<Extra4>();
                        break;
                    case 5:
                        characterR.AddComponent<Extra5>();
                        break;
                    case 6:
                        characterR.AddComponent<Extra6>();
                        break;
                    default:
                        break;
                }
            }
        }
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
                characterL.transform.localPosition = new Vector3(-5, 0.62f, 0);
                characterL.transform.localScale = new Vector3(2.2f, 2.2f, 1);
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
                characterR.transform.localPosition = new Vector3(5, 0.62f, 0);
                characterR.transform.localScale = new Vector3(2.2f, 2.2f, 1);
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
