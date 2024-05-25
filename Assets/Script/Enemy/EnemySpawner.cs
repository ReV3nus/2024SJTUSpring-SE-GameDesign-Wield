using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform Enemys;
    public List<GameObject> EnemyPrefabs;
    public List<float> EnemyWeights;
    public List<float> EnemyProbability;
    private int EnemyCnt;

    public Transform player;
    public float Distance2Player;
    public float EnemySpawnUpgradeTime;


    private void Start()
    {
        player = GameObject.Find("Player").transform;
        EnemyCnt = EnemyPrefabs.Count;
        if (EnemyWeights.Count != EnemyCnt || EnemyProbability.Count != EnemyCnt)
            Debug.LogError("ERROR ENEMY SPAWNER");
        StartCoroutine(GenerateEnemy());
    }

    IEnumerator GenerateEnemy()
    {
        while (true)
        {
            float total = Time.time / EnemySpawnUpgradeTime + 1.0f;

            for(int i = EnemyCnt - 1; i >= 0; i--)
            {
                if (total < EnemyWeights[i]) continue;
                if (Random.value > EnemyProbability[i]) continue;

                total -= EnemyWeights[i];

                float Angle = Random.Range(0f, 360f);
                Vector3 Pos = new Vector3(player.position.x + Distance2Player * Mathf.Cos(Angle), player.position.y + Distance2Player * Mathf.Sin(Angle), 0f);
                if (Mathf.Abs(Pos.x) >= 45.0f || Mathf.Abs(Pos.y) >= 48f) continue;
                GameObject go = Instantiate(EnemyPrefabs[i], Pos, Quaternion.identity);
                go.transform.parent = Enemys;
            }

            while (total >= EnemyWeights[0])
            {
                total -= EnemyWeights[0];
                if (Random.value > EnemyProbability[0]) continue;

                float Angle = Random.Range(0f, 360f);
                Vector3 Pos = new Vector3(player.position.x + Distance2Player * Mathf.Cos(Angle), player.position.y + Distance2Player * Mathf.Sin(Angle), 0f);
                if (Mathf.Abs(Pos.x) >= 45.0f || Mathf.Abs(Pos.y) >= 48f) continue;
                GameObject go = Instantiate(EnemyPrefabs[0], Pos, Quaternion.identity);
                go.transform.parent = Enemys;
            }
            yield return new WaitForSeconds(3f);
        }
    }
}
