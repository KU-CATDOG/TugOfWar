using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public int winScore;
    [HideInInspector]
    public int scoreL;
    [HideInInspector]
    public int scoreR;
    private int chanceL;
    private int chanceR;

    private float dragForceL;
    private float dragForceR;
    public float ropeSpeed;

    [HideInInspector]
    public float timer; //Main Timer
    private float timer2; //Extra Timer
    [HideInInspector]
    public float timerSpeed;
    private float tikInterval;
    private float tik;

    private GameObject rope;
    public GameObject ropePrefab;
    private Vector3 ropeInitPos;

    public GameObject mainCamera;

    [SerializeField] GameObject[] characterLst;
    [HideInInspector]
    public GameObject characterL;
    [HideInInspector]
    public GameObject characterR;

    public GameObject miniControl;

    private bool isTikStop;
    private bool isMovable;
    private bool isTimerStop;

    [HideInInspector]
    public int phase; // -1:ResetGame 0:StartScreen, 1:GameReady, 2:InGame, 3:EndGame

    private bool isPlayerFreeze;
    private bool isGameFreeze;

    [HideInInspector]
    public List<int> extraLstL = new List<int>();
    [HideInInspector]
    public List<int> extraLstR = new List<int>();

    void Awake()// before the first frame update
    {
        ropeInitPos = new Vector3(0, -3.5f, 0);
        isPlayerFreeze = false;
        isGameFreeze = false;

        if (SelectMenu.selectionL == 0 || SelectMenu.selectionL > characterLst.Length)
        {
            SelectMenu.selectionL = 1;
        }
        if (SelectMenu.selectionR == 0 || SelectMenu.selectionR > characterLst.Length)
        {
            SelectMenu.selectionR = 3;
        }

        if (SelectExtra.extraL1 == 0 || SelectExtra.extraL1 > 6)
        {
            SelectExtra.extraL1 = 1;
        }
        if (SelectExtra.extraL2 == 0 || SelectExtra.extraL2 > 6)
        {
            SelectExtra.extraL2 = 2;
        }
        if (SelectExtra.extraR1 == 0 || SelectExtra.extraR1 > 6)
        {
            SelectExtra.extraR1 = 1;
        }
        if (SelectExtra.extraR2 == 0 || SelectExtra.extraR2 > 6)
        {
            SelectExtra.extraR2 = 2;
        }

        SetPhase(-1);

        miniControl.SetActive(false);
    }

    void Update()// per frame
    {
        if (phase == -1)
        {
            ResetGame();
        }
        else if (phase == 1)
        {
            rope = Instantiate(ropePrefab, ropeInitPos, Quaternion.identity);
            mainCamera.transform.SetParent(rope.transform, true);

            SetCharacter("L", SelectMenu.selectionL);
            SetCharacter("R", SelectMenu.selectionR);

            FreezeAllFor(3.0f);
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
            //Debug.Log("1P Force : " + dragForceL.ToString("N2"));
            //Debug.Log("2P Force : " + dragForceR.ToString("N2"));
        }

        if (timer2 > 0.0f)
        {
            timer2 -= Time.deltaTime;
            if (timer2 <= 0.0f)
            {
                timer2 = 0.0f;
                if (phase == 2)
                {
                    UnfreezeAll();
                }
            }
        }

        if (isMovable)
        {
            rope.transform.position += new Vector3(ropeSpeed * (dragForceR - dragForceL) * Time.deltaTime, 0, 0);
        }
    }

    private void ResetGame(int i = 0)
    {
        FreezeAll();

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

        extraLstL = new List<int>();
        extraLstR = new List<int>();
        extraLstL.Add(SelectExtra.extraL1);
        extraLstL.Add(SelectExtra.extraL2);
        extraLstR.Add(SelectExtra.extraR1);
        extraLstR.Add(SelectExtra.extraR2);

        if (i == 0)
        {
            scoreL = 0;
            scoreR = 0;
            SetPhase(0);
        }
        else if (i == 1)
        {
            SetPhase(1);
        }
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
            if (!isPlayerFreeze && !isGameFreeze)
            {
                FreezeAll(); //FreezeAllFor()?
                isPlayerFreeze = true;
            }
        }
        else if (isPlayerFreeze)
        {
            UnfreezeAll();
            isPlayerFreeze = false;
        }

        float comPos = rope.transform.position.x * 2.5f;

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
                    
                    if (chanceL == 0)
                    {
                        for (int i = Random.Range(1, 7); ; i = Random.Range(1, 7))
                        {
                            if (extraLstR.Contains(i))
                            {
                                extraLstR.Remove(i);
                                SubExtra("R", i);
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = Random.Range(1, 7); ; i = Random.Range(1, 7))
                        {
                            if (!extraLstL.Contains(i))
                            {
                                extraLstL.Add(i);
                                SetExtra("L", i);
                                break;
                            }
                        }
                    }

                    miniControl.SetActive(false);
                    UnfreezeAll();
                }
                else if (winnerIdx == 2)
                {
                    UnityEngine.Debug.Log("미니게임의 승자는 2P!");

                    if (chanceL == 0)
                    {
                        for (int i = Random.Range(1, 7); ; i = Random.Range(1, 7))
                        {
                            if (extraLstL.Contains(i))
                            {
                                extraLstL.Remove(i);
                                SubExtra("L", i);
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = Random.Range(1, 7); ; i = Random.Range(1, 7))
                        {
                            if (!extraLstR.Contains(i))
                            {
                                extraLstR.Add(i);
                                SetExtra("R", i);
                                break;
                            }
                        }
                    }

                    miniControl.SetActive(false);
                    UnfreezeAll();
                }
            }
            else if (!isGameFreeze)
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
            ResetGame(1);

            //Effects, texts
            UnityEngine.Debug.Log("준비하시고...");
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
                    case 0:
                        if (extraLstL.Count != 0)
                        {
                            foreach (int i in extraLstL)
                            {
                                SetExtra("L", i);
                            }
                        }
                        break;
                    case 1:
                        if (characterL.GetComponent<Extra1>() == null)
                        {
                            characterL.AddComponent<Extra1>();
                        }
                        break;
                    case 2:
                        if (characterL.GetComponent<Extra2>() == null)
                        {
                            characterL.AddComponent<Extra2>();
                        }
                        break;
                    case 3:
                        if (characterL.GetComponent<Extra3>() == null)
                        {
                            characterL.AddComponent<Extra3>();
                        }
                        break;
                    case 4:
                        if (characterL.GetComponent<Extra4>() == null)
                        {
                            characterL.AddComponent<Extra4>();
                        }
                        break;
                    case 5:
                        if (characterL.GetComponent<Extra5>() == null)
                        {
                            characterL.AddComponent<Extra5>();
                        }
                        break;
                    case 6:
                        if (characterL.GetComponent<Extra6>() == null)
                        {
                            characterL.AddComponent<Extra6>();
                        }
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
                    case 0:
                        if (extraLstR.Count != 0)
                        {
                            foreach (int i in extraLstR)
                            {
                                SetExtra("R", i);
                            }
                        }
                        break;
                    case 1:
                        if (characterR.GetComponent<Extra1>() == null)
                        {
                            characterR.AddComponent<Extra1>();
                        }
                        break;
                    case 2:
                        if (characterR.GetComponent<Extra2>() == null)
                        {
                            characterR.AddComponent<Extra2>();
                        }
                        break;
                    case 3:
                        if (characterR.GetComponent<Extra3>() == null)
                        {
                            characterR.AddComponent<Extra3>();
                        }
                        break;
                    case 4:
                        if (characterR.GetComponent<Extra4>() == null)
                        {
                            characterR.AddComponent<Extra4>();
                        }
                        break;
                    case 5:
                        if (characterR.GetComponent<Extra5>() == null)
                        {
                            characterR.AddComponent<Extra5>();
                        }
                        break;
                    case 6:
                        if (characterR.GetComponent<Extra6>() == null)
                        {
                            characterR.AddComponent<Extra6>();
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void SubExtra(string pos, int extraNum)
    {
        if (pos == "L" || pos == "Left")
        {
            switch (extraNum)
            {
                case 1:
                    if (characterL.GetComponent<Extra1>() != null)
                    {
                        Destroy(characterL.GetComponent<Extra1>());
                    }
                    break;
                case 2:
                    if (characterL.GetComponent<Extra2>() != null)
                    {
                        Destroy(characterL.GetComponent<Extra2>());
                    }
                    break;
                case 3:
                    if (characterL.GetComponent<Extra3>() != null)
                    {
                        Destroy(characterL.GetComponent<Extra3>());
                    }
                    break;
                case 4:
                    if (characterL.GetComponent<Extra4>() != null)
                    {
                        Destroy(characterL.GetComponent<Extra4>());
                    }
                    break;
                case 5:
                    if (characterL.GetComponent<Extra5>() != null)
                    {
                        Destroy(characterL.GetComponent<Extra5>());
                    }
                    break;
                case 6:
                    if (characterL.GetComponent<Extra6>() != null)
                    {
                        Destroy(characterL.GetComponent<Extra6>());
                    }
                    break;
                default:
                    break;
            }
        }
        else if (pos == "R" || pos == "Right")
        {
            switch (extraNum)
            {
                case 1:
                    if (characterR.GetComponent<Extra1>() != null)
                    {
                        Destroy(characterR.GetComponent<Extra1>());
                    }
                    break;
                case 2:
                    if (characterR.GetComponent<Extra2>() != null)
                    {
                        Destroy(characterR.GetComponent<Extra2>());
                    }
                    break;
                case 3:
                    if (characterR.GetComponent<Extra3>() != null)
                    {
                        Destroy(characterR.GetComponent<Extra3>());
                    }
                    break;
                case 4:
                    if (characterR.GetComponent<Extra4>() != null)
                    {
                        Destroy(characterR.GetComponent<Extra4>());
                    }
                    break;
                case 5:
                    if (characterR.GetComponent<Extra5>() != null)
                    {
                        Destroy(characterR.GetComponent<Extra5>());
                    }
                    break;
                case 6:
                    if (characterR.GetComponent<Extra6>() != null)
                    {
                        Destroy(characterR.GetComponent<Extra6>());
                    }
                    break;
                default:
                    break;
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
            if (0 < character && character <= characterLst.Length)
            {
                SelectMenu.selectionL = character;

                if (characterL != null)
                {
                    Destroy(characterL);
                }

                characterL = Instantiate(characterLst[character - 1]);

                SetExtra("L", 0);

                Debug.Log("1P Character: " + SelectMenu.selectionL);

                characterL.transform.SetParent(rope.transform, false);
                characterL.transform.localPosition = new Vector3(-5, 0.62f, 0);
                characterL.transform.localScale = new Vector3(2.2f, 2.2f, 1);
                characterL.GetComponent<Character>().player = 0;
            }
        }
        else if (pos == "R" || pos == "Right")
        {
            if (0 < character && character <= characterLst.Length)
            {
                SelectMenu.selectionR = character;

                if (characterR != null)
                {
                    Destroy(characterR);
                }

                characterR = Instantiate(characterLst[character - 1]);

                SetExtra("R", 0);

                Debug.Log("2P Character: " + SelectMenu.selectionR);

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
        isGameFreeze = true;
        if (characterL != null && characterR != null)
        {
            characterL.GetComponent<Character>().freeze = true;
            characterR.GetComponent<Character>().freeze = true;
        }
    }

    public void UnfreezeAll()
    {
        isMovable = true;
        isTikStop = false;
        isTimerStop = false;
        isGameFreeze = false;
        if (characterL != null && characterR != null)
        {
            characterL.GetComponent<Character>().freeze = false;
            characterR.GetComponent<Character>().freeze = false;
        }
    }

    public void FreezeAllFor(float time)
    {
        FreezeAll();
        timer2 = time;
    }
}
