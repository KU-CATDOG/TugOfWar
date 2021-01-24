using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anubis : Character
{
    protected override void Start()
    {
        base.Start();
        force = 0;
        //player = 1; //temp
        changetime = Time.time;
    }

    int mode = 0;
    int energy = 0;
    float changetime;

    // Update is called once per frame
    void FixedUpdate()
    {
        float t = Time.time - changetime;
        if (t > 1 && mode == 1){
            mode = 0;
            force = 0;
            energy = 0;
        }

        if (player == 0)    //Player1
        {
            if ((Input.GetKeyDown("a") || Input.GetKeyDown("d")) && mode == 0 && !freeze){
                energy++;
                if (energy >= 12) {
                    mode = 1;
                    force = 5;
                    count += 2;
                    changetime = Time.time;
                }
                //Debug.Log(returnForce());
            }
            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)) && mode == 1 && !freeze){
                count++;
                //Debug.Log(returnForce());
            }
        }
        if (player == 1)    //Player2
        {
            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) && mode == 0 && !freeze){
                energy++;
                if (energy >= 12) {
                    mode = 1;
                    force = 5;
                    count += 2;
                    changetime = Time.time;
                }
                //Debug.Log(returnForce());
            }
            if ((Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d") || Input.GetKeyDown("w")) && mode == 1 && !freeze){
                count++;
                //Debug.Log(returnForce());
            }
        }

    }
}
