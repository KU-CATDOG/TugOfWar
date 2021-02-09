using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra6 : MonoBehaviour
{
    public int count;
    public int player;
    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Character>().player;
        if (player == 0)
        {
            count = GameObject.Find("GMObject").GetComponent<GM>().characterR.GetComponent<Character>().count;
        }
        else
        {
            count = GameObject.Find("GMObject").GetComponent<GM>().characterL.GetComponent<Character>().count;
        }
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime;
        if (t >= 1)
        {
            startTime = Time.time;
            t = 0;
            count -= 1;
        }
    }
}
