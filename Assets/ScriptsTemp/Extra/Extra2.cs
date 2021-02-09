using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("GMObject").GetComponent<GM>().timerSpeed *= 2;
        //시간 변수 받아와야되는데 이거 어케하누
    }

    // Update is called once per frame
    void Update()
    {

    }
}
