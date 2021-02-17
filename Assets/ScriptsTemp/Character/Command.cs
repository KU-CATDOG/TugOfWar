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
    public Attack comboUDLR;

    protected List<Combo> combos = new List<Combo>();
    Combo A = new Combo();
    public float comboLeeway = 0.2f;

    Attack curAttack = null;
    ComboInput lastInput = null;
    List<int> currentCombos = new List<int>();

    float timer = 0;
    float leeway = 0;
    bool skip = false;

    protected override void Start()
    {
        base.Start();
        player = 0;
        force = 14;
        up.strength = 0.1f;
        down.strength = 0.1f;
        left.strength = 0.1f;
        right.strength = 0.1f;
        comboUDLR.strength = 1f;

        A.inputs = new List<ComboInput>() {  
            new ComboInput(AttackType.up),
            new ComboInput(AttackType.down),
            new ComboInput(AttackType.left),
            new ComboInput(AttackType.right)
        };
        A.comboAttack = comboUDLR;
        A.onInputted = new UnityEvent();

        combos.Add(A);


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

        if(player == 0)
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
        else
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
        

        if (input == null) return;
        lastInput = input;

        List<int> remove = new List<int>();
        for(int i=0; i<currentCombos.Count; i++)
        {
            Combo c = combos[currentCombos[i]];
            if (c.continueCombo(input)) 
                leeway = 0;
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
                currentCombos.Add(i);
                leeway = 0;
            }
        }

        foreach (int i in remove)
            currentCombos.RemoveAt(i);

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
        Debug.Log(curAttack.strength * force);
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
