using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectExtra : MonoBehaviour
{
    public static int extraL1;
    public static int extraL2;
    public static int extraR1;
    public static int extraR2;
    private int CountL = 1;
    private int CountR = 1;

    public void button11()
    {
        if (CountL == 1)
        {
            extraL1 = 1;
            CountL++;
        }
        else extraL2 = 1;

        Debug.Log("P1 Extra1 Selected");
    }
    public void button12()
    {
        if (CountL == 1)
        {
            extraL1 = 2;
            CountL++;
        }
        else extraL2 = 2;

        Debug.Log("P1 Extra2 Selected");
    }
    public void button13()
    {
        if (CountL == 1)
        {
            extraL1 = 3;
            CountL++;
        }
        else extraL2 = 3;

        Debug.Log("P1 Extra3 Selected");
    }
    public void button14()
    {
        if (CountL == 1)
        {
            extraL1 = 4;
            CountL++;
        }
        else extraL2 = 4;

        Debug.Log("P1 Extra4 Selected");
    }
    public void button21()
    {
        if (CountR == 1)
        {
            extraR1 = 1;
            CountR++;
        }
        else extraR2 = 1;

        Debug.Log("P2 Extra1 Selected");
    }
    public void button22()
    {
        if (CountR == 1)
        {
            extraR1 = 2;
            CountR++;
        }
        else extraR2 = 2;

        Debug.Log("P2 Extra2 Selected");
    }
    public void button23()
    {
        if (CountR == 1)
        {
            extraR1 = 3;
            CountR++;
        }
        else extraR2 = 3;

        Debug.Log("P2 Extra3 Selected");
    }
    public void button24()
    {
        if (CountR == 1)
        {
            extraR1 = 4;
            CountR++;
        }
        else extraR2 = 4;

        Debug.Log("P2 Extra4 Selected");
    }

}
