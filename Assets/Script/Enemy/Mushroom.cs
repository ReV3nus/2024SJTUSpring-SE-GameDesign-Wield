using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy
{
    [Header("Mushroom's Properties")]
    public Weapon weapon;
    public bool Attacking;
    private bool StateInAtk;
    public float AttackDistance;

    protected void Awake()
    {
        weapon = transform.Find("Mushroom's Weapon").gameObject.GetComponent<Weapon>();
        weapon.gameObject.transform.parent = null;

    }
    protected override void ActionUpdate()
    {
        if (!StateInAtk)
        {
            MoveToPlayer();
            if (DistanceToPlayer <= AttackDistance)
            {
                Attacking = true;
                StateInAtk = true;
                anim.SetBool("Atk", true);
            }
        }
        else
        {
            AttackBehavior();
            if (!Attacking)
            {
                StateInAtk = false;
                anim.SetBool("Atk", false);
            }
        }
    }
    protected void AttackBehavior()
    {
        rb.velocity = Vector3.zero;
    }

    public new void SelfDestroy()
    {
        Destroy(weapon.gameObject);
        Destroy(this.gameObject);
    }
    protected new void onEventHit(Attack attacker)
    {
        base.onEventHit(attacker);
        Attacking = false;

    }
    protected new void onEventDie()
    {
        base.onEventDie();
        Attacking = false;
    }
}
