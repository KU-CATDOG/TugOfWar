using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Cow : Character
{
    float startTime;
    float t;

    public SpriteAtlas spriteA;
    public GameObject timerBarUI;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        force = 0;
        //player = 1; //temp
        startTime = Time.time;

        if (player == 0)
        {
            timerBarUI.transform.localPosition = new Vector3(-250, 100, 0);
        }
        else
        {
            timerBarUI.transform.localPosition = new Vector3(250, 100, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float sub_t = t;
        if (sub_t > 1.0f)
        {
            sub_t = 0.0f;
        }
        int num = (int)(12 * sub_t);
        if (0 <= num && num <= 12)
        {
            timerBarUI.GetComponent<Image>().sprite = spriteA.GetSprite("anubisUIsheet_" + num);
        }

        if (player == 0)    //Player1
        {
            if ((Input.GetKeyDown("a") || Input.GetKeyDown("d")) && !freeze)
            {
                startTime = Time.time;
            }

            if ((Input.GetKey("a") || Input.GetKey("d")) && !freeze)
            {
                t = Time.time - startTime;

                    
            }

            if ((Input.GetKeyUp("a") || Input.GetKeyUp("d")) && !freeze)
            {
                if (t < 0.25)
                {
                    force = 4;
                }
                else if (t < 0.5)
                {
                    force = 15;
                }
                else if (t < 0.75)
                {
                    force = 30;
                }
                else if (t < 1.0)
                {
                    force = 70;

                    if (GameObject.Find("SoundManageObject") != null)
                    {
                        SoundManager.instance.PlaySoundDic("swipe-whoosh");
                    }
                }
                else if (t >= 1.0)
                {
                    force = 0;

                    if (GameObject.Find("SoundManageObject") != null)
                    {
                        SoundManager.instance.PlaySoundDic("Tick");
                    }
                }
                count++;
                //Debug.Log(returnForce());

            }

        }

        if (player == 1)    //Player2
        {

            if ((Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.RightArrow))&& !freeze)
            {
                startTime = Time.time;
            }

            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && !freeze)
            {
                t = Time.time - startTime;

                    
            }

            if ((Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) && !freeze)
            {
                if (t < 0.25)
                {
                    force = 4;
                }
                else if (t < 0.5)
                {
                    force = 15;
                }
                else if (t < 0.75)
                {
                    force = 30;
                }
                else if (t < 1.0)
                {
                    force = 70;

                    if (GameObject.Find("SoundManageObject") != null)
                    {
                        SoundManager.instance.PlaySoundDic("swipe-whoosh");
                    }
                }
                else if (t >= 1.0)
                {
                    force = 0;

                    if (GameObject.Find("SoundManageObject") != null)
                    {
                        SoundManager.instance.PlaySoundDic("Tick");
                    }
                }
                count++;
                //Debug.Log(returnForce());
            }

        }
    }
}
