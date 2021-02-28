using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra5 : MonoBehaviour
{
    public int player;
    float startTime;
    public bool freeze;
    float t;


    // Start is called before the first frame update
    void Start()
    {

        player = GetComponent<Character>().player;
        startTime = Time.time;
        freeze = GetComponent<Character>().freeze;
        t = Time.time - startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!freeze)
        {
            t = Time.time - startTime;
            if (player == 0)
            {
                if (t >= 1)
                {
                    startTime = Time.time;
                    t = 0;
                    GameObject.Find("GMObject").GetComponent<GM>().dragForceL += 15;
                    //count += 3;
                }
            }
            if (player == 1)
            {
                if (t >= 1)
                {
                    startTime = Time.time;
                    t = 0;
                    GameObject.Find("GMObject").GetComponent<GM>().dragForceR += 15;
                    //count += 3;
                }
            }

        }
    }
}
