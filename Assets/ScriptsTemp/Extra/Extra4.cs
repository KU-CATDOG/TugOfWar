using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra4 : MonoBehaviour
{
    float startTime;
    public GameObject blind;
    float t;

    // Start is called before the first frame update
    void Start()
    {
        blind = GameObject.Find("InGameUIObject").GetComponent<InGameUIManager>().blindImage;
        startTime = Time.time;
        blind.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Character>().freeze)
        {
            startTime = Time.time - t;
        }
        else
        {
            t = Time.time - startTime;
            if (t >= 8)
            {
                startTime = Time.time;
                t = 0;
                blind.SetActive(false);
            }

            if (t > 5 && t < 8)
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
