using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra1 : MonoBehaviour
{
    public bool freeze;
    public int player;

    // Start is called before the first frame update
    void Start()
    {
        freeze = GetComponent<Character>().freeze;
        player = GetComponent<Character>().player;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == 0)
        {
            if ((Input.GetKeyDown("a") || Input.GetKeyDown("d")) && !freeze)
            {
                GameObject.Find("GMObject").GetComponent<GM>().dragForceL += 3;
                //count++;
            }
        }
        if (player == 1)
        {
            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) && !freeze)
            {
                GameObject.Find("GMObject").GetComponent<GM>().dragForceR += 3;
                //count++;
            }
        }
    }
}
