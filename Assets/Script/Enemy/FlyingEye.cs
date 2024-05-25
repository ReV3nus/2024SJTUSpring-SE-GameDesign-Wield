using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FlyingEye : Enemy
{
    public enum FlyingEye_States
    {
        fly, bite, dash
    };

    [Header("FlyingEye's Properties")]
    public FlyingEye_States currentState;


    [Header("Bite Attack")]
    public bool isBite;
    public float BiteDistance;

    [Header("Dash Attack")]
    public bool isDash;
    public float DashDistance;
    [Range(0, 1)]
    public float DashChance;
    public Vector3 DashDir;
    public float DashForce;

    //public bool PreparedToDash;
    //private bool Dashed;

    private float ConstDashCooldown = 3f;
    public float DashCooldown;

    protected override void ActionUpdate()
    {
        if (currentState == FlyingEye_States.fly)
        {
            MoveToPlayer();
            if (DistanceToPlayer <= BiteDistance)
            {
                currentState = FlyingEye_States.bite;
                isBite = true;
                anim.SetInteger("State", (int)FlyingEye_States.bite);
            }
            else if (DistanceToPlayer <= DashDistance)
            {
                if (DashCooldown <= 0f)
                {
                    if (Random.value <= DashChance)
                    {
                        DashDir = (PlayerPos - transform.position).normalized;
                        currentState = FlyingEye_States.dash;
                        isDash = true;
                        rb.velocity = Vector3.zero;
                        anim.SetInteger("State", (int)FlyingEye_States.dash);
                    }
                }
                DashCooldown = ConstDashCooldown;
            }
            else if(DashCooldown > 0f)DashCooldown -= Time.deltaTime;
        }
        else if (currentState == FlyingEye_States.bite)
        {
            BiteBehavior();
            if (!isBite)
            {
                currentState = FlyingEye_States.fly;
                anim.SetInteger("State", (int)FlyingEye_States.fly);
                DashCooldown = ConstDashCooldown;
            }
        }
        else if (currentState == FlyingEye_States.dash)
        {
            DashBehavior();
            if (!isDash)
            {
                currentState = FlyingEye_States.fly;
                anim.SetInteger("State", (int)FlyingEye_States.fly);
            }
        }
    }

    private void BiteBehavior()
    {
        rb.velocity = Vector3.zero;
    }

    private void DashBehavior()
    {
    }
    public void AddDashForce()
    {
        rb.AddForce(DashDir * DashForce);
    }

    protected new void onEventHit(Attack attacker)
    {
        base.onEventHit(attacker);
        isBite = isDash = false;

    }
    protected new void onEventDie()
    {
        base.onEventDie();
        isBite = isDash = false;

    }
    public void EndDash()
    {
        isDash = false;
    }
}
