using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    [HideInInspector]
    public int winScore;
    public int scoreL;
    public int scoreR;
    public int chanceL;
    public int chanceR;

    public float dragForceL;
    public float dragForceR;
    public float ropeSpeed;

    public float timer;
    public float timerSpeed;
    public float tikInterval;
    private float tik;

    public GameObject startText;
    private GameObject tempText;
    private bool isStartScreen;

    private GameObject rope;
    public GameObject ropePrefab;
    private Vector3 ropeInitPos;
    public GameObject character1;
    public GameObject character2;
    public GameObject character3;
    private GameObject characterL;
    private GameObject characterR;

    public bool isTikStop;
    public bool isMovable;
    public bool isTimerStop;

    void Start()// before the first frame update
    {
        scoreL = 0;
        scoreR = 0;
        chanceL = 4;
        chanceR = 4;
        timer = 30.0f;
        timerSpeed = 1.0f;
        dragForceL = 0.0f;
        dragForceR = 0.0f;
        ropeSpeed = 1.0f;
        tikInterval = 1.0f;
        ropeInitPos = new Vector3(0, -10, 0);
        FreezeAll();
        StartScreen();
    }

    void Update()// per frame
    {
        if (isStartScreen)
        {
            if (Input.anyKeyDown)
            {
                Destroy(tempText);
                isStartScreen = false;
                //Instantiate rope
                //SetCharacter("L", character#);
                //SetCharacter("L", character#);
                UnfreezeAll();
                rope = Instantiate(ropePrefab, ropeInitPos, Quaternion.identity);
            }
        }
        else
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
            tik -= tikInterval;
            SetDragForce();
        }

        if (isMovable)
        {
            rope.transform.position += new Vector3(ropeSpeed * (dragForceR - dragForceL) * Time.deltaTime, 0, 0);
        }
    }

    private void StartScreen()
    {
        //Instantiate StartScreen with "시작하기 위해 아무키나 누르시오"
        tempText = Instantiate(startText) as GameObject;
        tempText.transform.SetParent(GameObject.Find("Canvas").transform, false);
        isStartScreen = true;
    }

    private void SetDragForce()
    {
        dragForceL = 0.0f;
        //dragForceL = characterL.returnFroce();
        dragForceR = 0.0f;
        //dragForceR = characterR.returnFroce();
    }

    private void FreezeAll()
    {
        isMovable = false;
        isTikStop = true;
        isTimerStop = true;
    }

    private void UnfreezeAll()
    {
        isMovable = true;
        isTikStop = false;
        isTimerStop = false;
    }

    private void EventCheck() //for events, freeze...
    {
        /*if (characterL.freeze || characterR.freeze)
        {
            //freeze time, tik, or rope
        }
        else
        {

        }

        if (rope.transform.position.x <= -10 && chanceL == 4) //Can be reduced with "||"
        {
            chanceL--;
            //Instantiate random minigame
        }
        else if (rope.transform.position.x <= -20 && chanceL == 3)
        {
            chanceL--;
            //Instantiate random minigame
        }
        else if (rope.transform.position.x <= -30 && chanceL == 2)
        {
            chanceL--;
            //Instantiate random minigame
        }
        else if (rope.transform.position.x <= -40 && chanceL == 1)
        {
            chanceL--;
            //Instantiate random minigame
        }
        else if (rope.transform.position.x <= -50 && chanceL == 0)
        {
            scoreL++;
            if (scoreL == winScore)
            {
                FreezeAll();
                Debug.Log("1P Win!");
                //화면 전환
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
        else if (rope.transform.position.x >= 10 && chanceR == 4)
        {
            chanceR--;
            //Instantiate random minigame
        }
        else if (rope.transform.position.x >= 20 && chanceR == 3)
        {
            chanceR--;
            //Instantiate random minigame
        }
        else if (rope.transform.position.x >= 30 && chanceR == 2)
        {
            chanceR--;
            //Instantiate random minigame
        }
        else if (rope.transform.position.x >= 40 && chanceR == 1)
        {
            chanceR--;
            //Instantiate random minigame
        }
        else if (rope.transform.position.x <= 50 && chanceR == 0)
        {
            scoreR++;
            if (scoreR == winScore)
            {
                FreezeAll();
                Debug.Log("2P Win!");
                //화면 전환
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
        }*/
    }

    public void SetCharacter(string pos, int character) //Can used to set or change character
    {
        if (pos == "L" || pos == "Left")
        {
            if (character == 1)
            {
                //destroy previous character if exists
                //Instantiate with Position, Parent(rope)
                characterL = character1;
            }
            else if (character == 2)
            {
                //destroy previous character if exists
                //Instantiate with Position, Parent(rope)
                characterL = character2;
            }
            else if (character == 3)
            {
                //destroy previous character if exists
                //Instantiate with Position, Parent(rope)
                characterL = character3;
            }
        }
        else if (pos == "R" || pos == "Right")
        {
            if (character == 1)
            {
                //destroy previous character if exists
                //Instantiate with Position, Parent(rope)
                characterR = character1;
            }
            else if (character == 2)
            {
                //destroy previous character if exists
                //Instantiate with Position, Parent(rope)
                characterR = character2;
            }
            else if (character == 3)
            {
                //destroy previous character if exists
                //Instantiate with Position, Parent(rope)
                characterR = character3;
            }
        }
    }
}
