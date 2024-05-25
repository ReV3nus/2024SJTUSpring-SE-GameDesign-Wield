using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageFX : MonoBehaviour
{
    public TextMeshProUGUI TMP;

    public int Damage;
    private Vector3 MoveForce = new Vector3(0.0f, -9.8f, 0.0f);
    public Vector3 Velocity;

    [Header("FX properties")]
    public float FXTime = 0.8f;
    private float FXTimer;

    private void Start()
    {
        StartFX();
    }
    public void StartFX()
    {
        TMP.text = Damage.ToString();
        StartCoroutine(FXprogress());
    }

    IEnumerator FXprogress()
    {
        FXTimer = FXTime;
        yield return null;
        while (FXTimer >= 0f)
        {
            transform.position += Velocity * Time.deltaTime;
            Velocity += MoveForce * Time.deltaTime;
            FXTimer -= Time.deltaTime;
            TMP.color = new Color(1f, 1f, 1f, FXTimer / FXTime);
            yield return null;
        }
        EndFX();
    }
    private void EndFX()
    {
        Destroy(this.gameObject);
    }
}
