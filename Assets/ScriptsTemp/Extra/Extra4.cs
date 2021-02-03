using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra4 : MonoBehaviour
{
    float startTime;
    public GameObject blind;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        blind.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime;
        if (t >= 20 )
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
