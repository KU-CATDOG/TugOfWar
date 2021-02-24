using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rhythm : Character
{
    float startTime;
    int keyCount;

    float t;
    int pnt;

    public Slider gage;

    protected override void Start()
    {
        base.Start();
        force = 10;
        //player = 1; //temp
        startTime = Time.time;
        keyCount = 0;

        t = Time.time - startTime;
        pnt = 1;

        if (player == 0)
        {
            gage.transform.localPosition = new Vector3(-250, 100, 0);
        }
        else
        {
            gage.transform.localPosition = new Vector3(250, 100, 0);
        }
    }
    
    

    void Update()   //run timer and get key input
    {
        if (freeze)
        {
            startTime = Time.time - t;
        }
        else
        {
            t = Time.time - startTime;
            if (t >= 1)
            {
                startTime = Time.time;
                t = 0;
                keyCount = 0;
                pnt *= -1;
            }

            if (pnt > 0)
            {
                gage.value = t / 1.0f;
            }
            else
            {
                gage.value = (1 - t) / 1.0f;
            }

            if (player == 0)    //Player1
            {
                if (keyCount == 0)
                {
                    if ((Input.GetKeyDown("a") || Input.GetKeyDown("d")))
                    {
                        if (t < 0.2 || t > 0.8)         //judge miss
                        {
                            force = 0;
                        }
                        else if ((t >= 0.2 && t < 0.35) || (t > 0.65 && t <= 0.8))       //judge good
                        {
                            force = 5;
                        }
                        else if ((t >= 0.35 && t < 0.45) || (t > 0.55 && t <= 0.65))       //judge great
                        {
                            force = 8;
                        }
                        else        //judge perfect
                        {
                            force = 12;
                        }
                        count++;
                        keyCount++;
                        //Debug.Log(returnForce());
                    }
                }
            }

            if (player == 1)    //Player2
            {
                if (keyCount == 0)
                {
                    if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
                    {
                        if (t < 0.2 || t > 0.8)         //judge miss
                        {
                            force = 0;
                        }
                        else if ((t >= 0.2 && t < 0.35) || (t > 0.65 && t <= 0.8))       //judge good
                        {
                            force = 5;
                        }
                        else if ((t >= 0.35 && t < 0.45) || (t > 0.55 && t <= 0.65))       //judge great
                        {
                            force = 8;
                        }
                        else        //judge perfect
                        {
                            force = 12;
                        }
                        count++;
                        keyCount++;
                        //Debug.Log(returnForce());
                    }
                }
            }
        }
    }
}
