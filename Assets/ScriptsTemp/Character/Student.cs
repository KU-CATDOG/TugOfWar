using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : Character
{
    protected override void Start()
    {
        base.Start();
        force = 5;
        //player = 1; //temp
    }

    void Update()   //get pull key input
    {
        if (player == 0)    //Player1
        {
            if ((Input.GetKeyDown("a") || Input.GetKeyDown("a")) && !freeze){
                count++;
                //Debug.Log(returnForce());
            }
        }
        if (player == 1)    //Player2
        {
            if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) && !freeze){
                count++;
                //Debug.Log(returnForce());
            }
        }

    }
}
