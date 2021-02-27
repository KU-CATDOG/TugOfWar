using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AttackType { up = 0, down = 1, left = 2, right = 3};

public class Command : Character
{
    [Header("Inputs")]
    public KeyCode p1Up = KeyCode.W;
    public KeyCode p1Down = KeyCode.S;
    public KeyCode p1Left = KeyCode.A;
    public KeyCode p1Right = KeyCode.D; 
    public KeyCode p2Up = KeyCode.UpArrow;
    public KeyCode p2Down = KeyCode.DownArrow;
    public KeyCode p2Left = KeyCode.LeftArrow;
    public KeyCode p2Right = KeyCode.RightArrow;

    [Header("Attacks")]
    public Attack up;
    public Attack down;
    public Attack left;
    public Attack right;
    public Attack comboA;
    public Attack comboB;
    public Attack comboC;
    public Attack comboD;

    public GameObject incorrectCombo;
    public GameObject inGame;
    public GameObject gameManager;
    public GameObject rope;

    protected List<Combo> combos = new List<Combo>();
    Combo A = new Combo();
    Combo B = new Combo();
    Combo C = new Combo();
    Combo D = new Combo();
    private float comboLeeway = 0.3f;

    Attack curAttack = null;
    ComboInput lastInput = null;
    List<int> currentCombos = new List<int>();

    float timer = 0;
    float leeway = 0;
    bool skip = false;

    protected override void Start()
    {
        gameManager = GameObject.Find("GMObject");

        rope = gameManager.GetComponent<GM>().rope;

        base.Start();
        force = 1;
        up.strength = 0.1f;
        down.strength = 0.1f;
        left.strength = 0.1f;
        right.strength = 0.1f;

        comboA.strength = 45f;
        comboB.strength = 65f;
        comboC.strength = 100f;
        comboD.strength = 160f;

        A.inputs = new List<ComboInput>() {  
            new ComboInput(AttackType.down),
            new ComboInput(AttackType.down),
            new ComboInput(AttackType.left),
            new ComboInput(AttackType.right)
        };
        A.comboAttack = comboA;
        A.onInputted = new UnityEvent();

        combos.Add(A);

        B.inputs = new List<ComboInput>() {
            new ComboInput(AttackType.right),
            new ComboInput(AttackType.up),
            new ComboInput(AttackType.left),
            new ComboInput(AttackType.left),
            new ComboInput(AttackType.down),
            new ComboInput(AttackType.right)
        };
        B.comboAttack = comboB;
        B.onInputted = new UnityEvent();

        combos.Add(B);

        C.inputs = new List<ComboInput>() {
            new ComboInput(AttackType.left),
            new ComboInput(AttackType.right),
            new ComboInput(AttackType.left),
            new ComboInput(AttackType.right),
            new ComboInput(AttackType.up),
            new ComboInput(AttackType.up),
            new ComboInput(AttackType.down),
            new ComboInput(AttackType.down)
        };
        C.comboAttack = comboC;
        C.onInputted = new UnityEvent();

        combos.Add(C);

        D.inputs = new List<ComboInput>() {
            new ComboInput(AttackType.left),
            new ComboInput(AttackType.up),
            new ComboInput(AttackType.right),
            new ComboInput(AttackType.up),
            new ComboInput(AttackType.left),
            new ComboInput(AttackType.down),
            new ComboInput(AttackType.right),
            new ComboInput(AttackType.down),
            new ComboInput(AttackType.left),
            new ComboInput(AttackType.right)
        };
        D.comboAttack = comboD;
        D.onInputted = new UnityEvent();

        combos.Add(D);


        PrimeCombos();
    }

    void PrimeCombos()
    {
        for(int i=0; i<combos.Count; i++)
        {
            Combo c = combos[i];
            c.onInputted.AddListener(() =>
            {
                skip = true;
                Attack(c.comboAttack);
                ResetCombos();
            });
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(curAttack != null)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else curAttack = null;

            return;
        }

        if(currentCombos.Count > 0)
        {
            leeway += Time.deltaTime;
            if(leeway >= comboLeeway)
            {
                if(lastInput != null)
                {
                    Attack(getAttackFromType(lastInput.type));
                    lastInput = null;
                }
                ResetCombos();
            }
        } else
            leeway = 0;

        ComboInput input = null;

        if(player == 0 && !freeze)
        {
            if (Input.GetKeyDown(p1Up))
                input = new ComboInput(AttackType.up);
            if (Input.GetKeyDown(p1Down))
                input = new ComboInput(AttackType.down);
            if (Input.GetKeyDown(p1Left))
                input = new ComboInput(AttackType.left);
            if (Input.GetKeyDown(p1Right))
                input = new ComboInput(AttackType.right);
        }
        else if(player == 1 && !freeze)
        {
            if (Input.GetKeyDown(p2Up))
                input = new ComboInput(AttackType.up);
            if (Input.GetKeyDown(p2Down))
                input = new ComboInput(AttackType.down);
            if (Input.GetKeyDown(p2Left))
                input = new ComboInput(AttackType.left);
            if (Input.GetKeyDown(p2Right))
                input = new ComboInput(AttackType.right);
        }

        //if(count == 0) input = new ComboInput(AttackType.down);
        //if(count == 1) input = new ComboInput(AttackType.down);
        //if(count == 2) input = new ComboInput(AttackType.left);
        //if(count == 3) input = new ComboInput(AttackType.down);


        if (input == null) return;
        lastInput = input;

        List<int> remove = new List<int>();
        for(int i=0; i<currentCombos.Count; i++)
        {
            Combo c = combos[currentCombos[i]];
            if (c.continueCombo(input))
            {
                if (GameObject.Find("SoundManageObject") != null)
                {
                    SoundManager.instance.PlayRandomSoundDic(new string[] { "Click On", "Button", "Button Click On" });
                }
                leeway = 0;
            }
                
            else
                remove.Add(i);
        }

        if (skip)
        {
            skip = false;
            return;
        }

        for(int i=0; i<combos.Count; i++)
        {
            if (currentCombos.Contains(i)) continue;
            if (combos[i].continueCombo(input))
            {
                if (GameObject.Find("SoundManageObject") != null)
                {
                    SoundManager.instance.PlayRandomSoundDic(new string[] { "Click On", "Button", "Button Click On" });
                }
                currentCombos.Add(i);
                leeway = 0;
            }
        }
        
        //foreach (int i in remove)
        //{
        //    currentCombos.RemoveAt(i);
        //    //Debug.Log(currentCombos[i]);
        //}

        for(int i=remove.Count - 1; i>0; i--)
        {
            currentCombos.RemoveAt(remove[i]);
        }

        if (currentCombos.Count <= 0)
            Attack(getAttackFromType(input.type));

    }

    void ResetCombos()
    {
        leeway = 0;
        for(int i=0; i<currentCombos.Count; i++)
        {
            Combo c = combos[currentCombos[i]];
            c.ResetCombo();
        }
        currentCombos.Clear();
    }

    void Attack(Attack att)
    {
        curAttack = att;
        timer = att.length;
        if (curAttack.strength > 1f)
        {
            count++;
            force = curAttack.strength;
            Debug.Log("count: " + count);
            Debug.Log("force: " + force);
        }
        else
        {
            GameObject temp = null;

            Vector3 p1 = new Vector3(-3.7f + rope.transform.position.x, 2f, 0);
            Vector3 p2 = new Vector3(3.7f + rope.transform.position.x, 2f, 0);

            if (player == 0) { temp = Instantiate(incorrectCombo, p1, Quaternion.identity, rope.transform); }
            if (player == 1) { temp = Instantiate(incorrectCombo, p2, Quaternion.identity, rope.transform); }

            Destroy(temp, 0.5f);
        }
        //Debug.Log("Attack: " + curAttack.strength * force);
    }

    Attack getAttackFromType(AttackType t)
    {
        if (t == AttackType.up)
            return up;
        if (t == AttackType.down)
            return down;
        if (t == AttackType.left)
            return left;
        if (t == AttackType.right)
            return right;
        return null;
    }
}

[System.Serializable]
public class Attack 
{
    public float length;
    public float strength;

}

[System.Serializable]
public class ComboInput {
    public AttackType type;

    public ComboInput(AttackType t)
    {
        type = t;
    }

    public bool isSameAs(ComboInput test)
    {
        return (type == test.type);
    }
}

[System.Serializable]
public class Combo
{
    public List<ComboInput> inputs;
    public Attack comboAttack;
    public UnityEvent onInputted;
    int curInput = 0;

    public bool continueCombo(ComboInput i)
    {
        if(inputs[curInput].isSameAs(i))
        {
            curInput++;
            if(curInput >= inputs.Count)
            {
                onInputted.Invoke();
                curInput = 0;
            }
            return true;
        }
        else
        {
            curInput = 0;
            return false;
        }
    }

    public ComboInput currentComboInput()
    {
        if (curInput >= inputs.Count) return null;
        return inputs[curInput];
    }

    public void ResetCombo()
    {
        curInput = 0;
    }
}
