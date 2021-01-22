using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhythm : Character
{
    protected override void Start()
    {
        base.Start();
        force = 10;
        player = 1; //temp
    }
    
    float startTime = Time.time;

    void Update()   //get pull key input
    {
        float t = Time.time - startTime;
        if (t >= 4)
        {
            startTime = Time.time;
            t = 0;
        }

        if (player == 0)    //Player1
        {
            if ((Input.GetKeyDown("a") || Input.GetKeyDown("d")) && !freeze)
            {
                if (t < 0.25 || (t > 1.75 && t < 2.25) || t > 3.75)         //judge miss
                {
                    force = 0;
                }
                else if ((t >= 0.25 && t < 0.5) || (t > 1.5 && t <= 1.75) || (t >= 2.25 && t < 2.5) || (t > 3.5 && t <= 3.75))       //judge good
                {
                    force = 5;
                }
                else if ((t >= 0.5 && t < 0.75) || (t > 1.25 && t <= 1.5) || (t >= 2.5 && t < 2.75) || (t > 3.25 && t <= 3.5))      //judge great
                {
                    force = 8;
                }
                else        //judge perfect
                {
                    force = 12;
                }
                count++;
                Debug.Log(returnForce());
            }
        }

        if (player == 1)    //Player2
        {
            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) && !freeze)
            {
                if (t < 0.25 || (t > 1.75 && t < 2.25) || t > 3.75)         //judge miss
                {
                    force = 0;
                }
                else if ((t >= 0.25 && t < 0.5) || (t > 1.5 && t <= 1.75) || (t >= 2.25 && t < 2.5) || (t > 3.5 && t <= 3.75))       //judge good
                {
                    force = 5;
                }
                else if ((t >= 0.5 && t < 0.75) || (t > 1.25 && t <= 1.5) || (t >= 2.5 && t < 2.75) || (t > 3.25 && t <= 3.5))      //judge great
                {
                    force = 8;
                }
                else        //judge perfect
                {
                    force = 12;
                }
                count++;
                Debug.Log(returnForce());
            }
        }

    }
}
