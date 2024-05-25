using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("Basical Status")]
    public float MaxHP;
    public float CurrentHP;
    
    public float MoveSpeed;
    public float WieldSpeed;

    [Header("Invulnerable Status")]
    public float InvulnerableTime;
    protected float InvulnerableTimer;
    public bool Invulnerable;

    [Header("Events")]
    public UnityEvent<Attack> OnHit;
    public UnityEvent<Attack> OnDie;

    protected void Start()
    {
        CurrentHP = MaxHP;
    }
    public virtual Vector3 GetPosition()
    {
        return Vector3.zero;
    }
    public void SufferAttack(Attack attack)
    {
        if (Invulnerable) return;

        CurrentHP -= attack.Damage;
        if (CurrentHP <= 0)
        {
            CurrentHP = 0;
            OnDie?.Invoke(attack);
        }
        else
        {
            OnHit?.Invoke(attack);
        }
    }
}
