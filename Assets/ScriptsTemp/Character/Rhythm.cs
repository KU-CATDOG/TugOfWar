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
            if (t >= 2)
            {
                startTime = Time.time;
                t = 0;
                keyCount = 0;
                pnt *= -1;
            }

            if (pnt > 0)
            {
                gage.value = t / 2.0f;
            }
            else
            {
                gage.value = (2 - t) / 2.0f;
            }

            if (player == 0)    //Player1
            {
                if (keyCount == 0)
                {
                    if ((Input.GetKeyDown("a") || Input.GetKeyDown("d")) && !freeze)
                    {
                        if (t < 0.25 || t > 1.75)         //judge miss
                        {
                            force = 0;
                        }
                        else if ((t >= 0.25 && t < 0.5) || (t > 1.5 && t <= 1.75))       //judge good
                        {
                            force = 5;
                        }
                        else if ((t >= 0.5 && t < 0.75) || (t > 1.25 && t <= 1.5))       //judge great
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
                        else if ((t >= 0.5 && t < 0.75) || (t > 1.25 && t <= 1.5) || (t >= 2.5 && t < 2.75) || (t > 3.25 && t <= 3.5))       //judge great
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
