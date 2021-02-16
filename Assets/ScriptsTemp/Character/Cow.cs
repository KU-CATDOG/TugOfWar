using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : Character
{
    float startTime;
    float t;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        force = 0;
        //player = 1; //temp
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player == 0)    //Player1
        {
            if ((Input.GetKeyDown("a") || Input.GetKeyDown("d")) && !freeze)
            {
                startTime = Time.time;
            }

            if ((Input.GetKey("a") || Input.GetKey("d")) && !freeze)
            {
                t = Time.time - startTime;

                    
            }

            if ((Input.GetKeyUp("a") || Input.GetKeyUp("d")) && !freeze)
            {
                if (t < 0.25)
                {
                    force = 1;
                }
                else if (t < 0.5)
                {
                    force = 3;
                }
                else if (t < 0.75)
                {
                    force = 5;
                }
                else if (t < 1.0)
                {
                    force = 7;
                }
                else if (t >= 1.0)
                {
                    force = 0;
                }
                count++;
                Debug.Log(returnForce());

            }

        }

        if (player == 1)    //Player2
        {

            if ((Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.RightArrow))&& !freeze)
            {
                startTime = Time.time;
            }

            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && !freeze)
            {
                t = Time.time - startTime;

                    
            }

            if ((Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) && !freeze)
            {
                if (t < 0.25)
                {
                    force = 1;
                }
                else if (t < 0.5)
                {
                    force = 3;
                }
                else if (t < 0.75)
                {
                    force = 5;
                }
                else if (t < 1.0)
                {
                    force = 7;
                }
                else if (t >= 1.0)
                {
                    force = 0;
                }
                count++;
                Debug.Log(returnForce());
            }

        }
    }
}
