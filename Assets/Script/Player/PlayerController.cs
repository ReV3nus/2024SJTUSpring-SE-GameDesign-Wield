using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Weapon weapon;

    private PlayerCharacter playerCharacter;
    private PlayerAnimation playerAnimation;

    private Rigidbody2D rb;

    public bool isRunning;
    private bool isDead;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerCharacter = GetComponent<PlayerCharacter>();
    }


    void Update()
    {
        if (isDead) return;
        if (!playerCharacter.Stunned) Move();

        if (Input.GetKeyDown(KeyCode.K))
        {
            OnDebugFunction();
        }
    }

    private void OnDebugFunction()
    {
        LevelSystem.GainEXP(5f);
        if(transform.position.y <= 10f)transform.position = new Vector3(0f, 40f, 0f);
    }

    private void FixedUpdate()
    {

    }

    void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(xInput * playerCharacter.MoveSpeed, yInput * playerCharacter.MoveSpeed);

        isRunning = (xInput != 0 || yInput != 0);
        if (xInput != 0)
            transform.localScale = new Vector3(xInput, 1, 1);
    }

    public void KnockBack(Vector3 force)
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(force);
    }

    public void onEventDie()
    {
        rb.velocity = Vector3.zero;
        isDead = true;
        weapon.gameObject.SetActive(false);
    }

    public void EndDyingAnimation()
    {
        GameManager.GameOver();
    }
}
