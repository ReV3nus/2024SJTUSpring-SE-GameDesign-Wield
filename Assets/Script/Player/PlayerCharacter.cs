using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    private PlayerAnimation playerAnimation;
    private PlayerController playerController;
    private Vector3 PlayerPositionOffset = new Vector3(0f, 0.43f, 0f);

    [Header("Player's Properties")]
    public float Strength;
    public Weapon weapon;
    private Attack WeaponAttack;

    public float Weight;
    public float KnockbackResistance;

    public float StunnedTime;
    private float StunnedTimer;
    public bool Stunned;

    public bool Died;

    protected new void Start()
    {
        base.Start();

        playerAnimation = GetComponent<PlayerAnimation>();
        playerController = GetComponent<PlayerController>();

        WeaponAttack = weapon.GetComponent<Attack>();
        CalcAttack();
    }
    public override Vector3 GetPosition()
    {
        return transform.position + PlayerPositionOffset;
    }
    public void CalcAttack()
    {
        WeaponAttack.Damage = Strength * weapon.Damage;
    }

    protected void Update()
    {
        if (Invulnerable)
        {
            InvulnerableTimer -= Time.deltaTime;
            if (InvulnerableTimer <= 0)
            {
                Invulnerable = false;
                playerAnimation.Set_Invulnerable(false);
            }
        }
        if (Stunned)
        {
            StunnedTimer -= Time.deltaTime;
            if(StunnedTimer <= 0)
            {
                Stunned = false;
                playerAnimation.Set_Stunned(false);
            }
        }
        CalcAttack();
    }
    public void OnEventHit(Attack attack)
    {
        InvulnerableTimer = InvulnerableTime;
        Invulnerable = true;
        playerAnimation.Set_Invulnerable(true);
        Stunned = true;
        playerAnimation.Set_Stunned(true);
        StunnedTimer = StunnedTime;

        Vector3 dir = (transform.position - attack.transform.position).normalized;
        float force = attack.Force / Weight * (1 - KnockbackResistance);
        playerController.KnockBack(dir * force);

        GameManager.UpdateHPUI(CurrentHP, MaxHP);
    }
}
