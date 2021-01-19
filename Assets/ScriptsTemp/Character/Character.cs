using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float force { get; set; }
    public int player { get; set; }  //1P: 0, 2P: 1
    public int count { get; protected set; }

    public bool freeze = false;

    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    public float returnForce()
    {
        float temp = count * force;
        count = 0;
        return temp;
    }

}
