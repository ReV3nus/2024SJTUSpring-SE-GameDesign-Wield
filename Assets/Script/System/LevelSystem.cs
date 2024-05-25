using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public int Level;
    public float EXP;
    public float nxtEXP;

    public GameObject UpgradePanel;
    private bool isUpgrading;

    public PlayerCharacter player;

    private float CalcForNxtExp1 = 1f, CalcForNxtExp2 = 2f;

    static LevelSystem instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;
    }
    private void Start()
    {
        instance.Level = 1;
        instance.EXP = 0f;
        instance.nxtEXP = 10f;
        instance.CalcForNxtExp1 = instance.CalcForNxtExp2 = 1f;

        instance.isUpgrading = false;
        instance.UpgradePanel.SetActive(false);
    }

    public static void GainEXP(float gained)
    {
        instance.EXP += gained;
        while(instance.EXP >= instance.nxtEXP)
        {
            instance.EXP -= instance.nxtEXP;
            instance.LevelUP();
        }
        GameManager.UpdateEXPUI(instance.EXP / instance.nxtEXP);
    }
    private void LevelUP()
    {
        Level++;
        CalcForNxtExp2 += CalcForNxtExp1;
        CalcForNxtExp1 = CalcForNxtExp2 - CalcForNxtExp1;
        nxtEXP = CalcForNxtExp2 * 5f;

        isUpgrading = true;
        UpgradePanel.SetActive(true);
        Time.timeScale = 0f;
        AudioSystem.PlayLevelUp();
    }

    public void UpgradeAtk()
    {
        if (!isUpgrading) return;
        player.Strength *= 1.3f;

        isUpgrading = false;
        UpgradePanel.SetActive(false);
        Time.timeScale = 1.0f;
        AudioSystem.PlaySelect();
    }
    public void UpgradeHP()
    {
        if (!isUpgrading) return;
        player.MaxHP += 25;
        player.CurrentHP += 25;
        GameManager.UpdateHPUI(player.CurrentHP, player.MaxHP);

        isUpgrading = false;
        UpgradePanel.SetActive(false);
        Time.timeScale = 1.0f;
        AudioSystem.PlaySelect();
    }
    public void UpgradeWield()
    {
        if (!isUpgrading) return;
        player.WieldSpeed *= 1.2f;

        isUpgrading = false;
        UpgradePanel.SetActive(false);
        Time.timeScale = 1.0f;
        AudioSystem.PlaySelect();
    }
    public void UpgradeSpd()
    {
        if (!isUpgrading) return;
        player.MoveSpeed *= 1.15f;

        isUpgrading = false;
        UpgradePanel.SetActive(false);
        Time.timeScale = 1.0f;
        AudioSystem.PlaySelect();
    }
}
