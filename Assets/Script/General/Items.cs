using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public float DistanceToPlayer;
    public float PickDistance;
    private float DestroyTimer = 20f;

    protected Character player;
    protected void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    private void Update()
    {
        DestroyTimer -= Time.deltaTime;
        if (DestroyTimer <= 0) Destroy(this.gameObject);
        DistanceToPlayer = Vector3.Distance(transform.position, player.GetPosition());
        if(DistanceToPlayer <= PickDistance)
        {
            onPick();
        }
    }

    protected virtual void onPick() { }
}
