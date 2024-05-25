using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelfParticleSystem : MonoBehaviour
{
    [Header("Particle Prefabs")]
    public GameObject HitParticle;

    static SelfParticleSystem instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;
    }

    public static void SpawnHit(Vector3 pos)
    {
        GameObject go = Instantiate(instance.HitParticle, pos, Quaternion.identity);
        go.transform.parent = instance.transform;
    }
}
