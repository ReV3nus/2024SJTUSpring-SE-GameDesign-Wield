using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Attack Properties")]
    public float Damage;
    public float Force;
    public Character Attacker;

    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Character>()?.SufferAttack(this);
    }
}
