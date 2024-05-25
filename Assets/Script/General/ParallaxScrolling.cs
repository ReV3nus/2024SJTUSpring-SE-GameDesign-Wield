using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    Transform player;

    public float rate = 1.0f;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;

    }

    private void Update()
    {
        transform.position = new Vector3(player.position.x * rate, transform.position.y, 0);
    }
}
