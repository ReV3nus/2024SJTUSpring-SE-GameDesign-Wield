using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Basical")]
    public float Damage;
    public float Dexterity;

    public Character OwnerCharacter;

    public float RotateSpeed;
    private float RotateAngle = 0f;
    public bool RotateClockwisely;

    private float DistanceFromPlayer = 1.0f;
    protected Vector3 OwnerPosition;

    void Start()
    {
        UpdatePosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        RotateSpeed = OwnerCharacter.WieldSpeed * Dexterity;
        RotateAngle +=  RotateSpeed * Time.deltaTime * (RotateClockwisely ? -1f : 1f);

        UpdatePosition();
    }
    public void UpdatePosition()
    {
        OwnerPosition = OwnerCharacter.GetPosition();
        transform.position = (OwnerPosition + new Vector3(DistanceFromPlayer * Mathf.Cos(RotateAngle), DistanceFromPlayer * Mathf.Sin(RotateAngle), 0));
        transform.rotation = Quaternion.Euler(0f, 0f, RotateAngle / Mathf.PI * 180f - 90f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D[] contacts = collision.contacts;

        Vector2 center = Vector2.zero;
        foreach (ContactPoint2D contact in contacts)
        {
            center += contact.point;
        }
        center /= contacts.Length;
        Vector3 center3 = new Vector3(center.x, center.y, 0f);

        float result = Vector3.Cross((transform.position - OwnerPosition), (center3 - OwnerPosition)).z;
        if((result < 0) == RotateClockwisely)RotateClockwisely ^= true;

        SelfParticleSystem.SpawnHit(center3);
        AudioSystem.PlayMetal();
    }

}
