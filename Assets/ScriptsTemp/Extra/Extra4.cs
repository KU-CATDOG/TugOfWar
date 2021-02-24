﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra4 : MonoBehaviour
{
    float startTime;
    public GameObject blind;
    bool freeze;
    float t;

    // Start is called before the first frame update
    void Start()
    {
        blind = GameObject.Find("InGameUIObject").GetComponent<InGameUIManager>().blindImage;
        startTime = Time.time;
        blind.SetActive(false);
        freeze = GetComponent<Character>().freeze;
    }

    // Update is called once per frame
    void Update()
    {
        if (freeze)
        {
            startTime = Time.time - t;
        }
        else
        {
            t = Time.time - startTime;
            if (t >= 20)
            {
                startTime = Time.time;
                t = 0;
                blind.SetActive(false);
            }

            if (t > 10 && t < 20)
            {
                blind.SetActive(true);
            }
        }
        
        
    }

    void OnDisable()
    {
        blind.SetActive(false);
    }
}
