using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("GMObject").GetComponent<GM>().timerSpeed /= 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
