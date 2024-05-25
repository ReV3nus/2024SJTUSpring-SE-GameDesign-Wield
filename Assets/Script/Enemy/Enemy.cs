using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [Header("Enemy's Properties")]
    protected Character player;
    protected Vector3 PlayerPos;

    public float Weight;
    public float ScoreOnEliminated;
    protected bool died;
    protected float DistanceToPlayer;

    public GameObject DropItem;
    public float DropProbability;

    public float DestroyDistance;

    [Header("Enemy's Components")]
    protected Animator anim;
    protected Rigidbody2D rb;

    [Header("FX Components")]
    public AudioSource FXSource;
    public AudioClip Hit0, Hit1;
    public Canvas FXCanvas;
    public GameObject DamageFX;

    protected new void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

        FXCanvas = GameObject.Find("FX Canvas").GetComponent<Canvas>();
    }
    public override Vector3 GetPosition()
    {
        return transform.position;
    }

    protected void Update()
    {
        if (died) return;
        PlayerPos = player.GetPosition();
        DistanceToPlayer = Vector3.Distance(transform.position, PlayerPos);
        if(DistanceToPlayer >= DestroyDistance)
        {
            SelfDestroy();
        }
        if (Invulnerable)
        {
            InvulnerableTimer -= Time.deltaTime;
            if (InvulnerableTimer <= 0)
            {
                Invulnerable = false;
                anim.SetBool("Hit", false);
            }
        }
        else ActionUpdate();
    }
    protected virtual void ActionUpdate() { }
    protected void MoveToPlayer()
    {
        Vector3 dir = (PlayerPos - transform.position).normalized;
        rb.velocity = dir * MoveSpeed;
        transform.localScale = new Vector3((PlayerPos.x > transform.position.x ? 1f : -1f), 1f, 1f) * transform.localScale.z;
    }

    public void onEventHit(Attack attacker)
    {
        InvulnerableTimer = InvulnerableTime;
        Invulnerable = true;

        anim.SetBool("Hit", true);

        rb.velocity = Vector2.zero;
        Vector2 Dir = (transform.position - attacker.transform.position).normalized;
        rb.AddForce(Dir * attacker.Force / Weight);

    }
    public void PlayingHitFX(Attack attacker)
    {
        FXSource.PlayOneShot(Random.value < 0.5f ? Hit0 : Hit1);
        GameObject FXgo = Instantiate(DamageFX, FXCanvas.transform);
        FXgo.transform.position = transform.position;
        DamageFX dfx = FXgo.GetComponent<DamageFX>();
        dfx.Damage = (int)attacker.Damage;

        Vector2 Dir = (transform.position - attacker.transform.position).normalized;
        dfx.Velocity = Dir * 1.5f;
        dfx.StartFX();
    }

    public void onEventDie()
    {
        InvulnerableTimer = 114514f;
        Invulnerable = true;

        anim.SetBool("Die", true);
        died = true;
        rb.velocity = Vector2.zero;
        GameManager.UpdateScore(ScoreOnEliminated);
    }
    public void DropingItem()
    {
        if(Random.value <= DropProbability)
        {
            GameObject go = Instantiate(DropItem);
            go.transform.position = transform.position;
            go.transform.parent = GameObject.Find("Drops").transform;
        }
    }
    public void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
