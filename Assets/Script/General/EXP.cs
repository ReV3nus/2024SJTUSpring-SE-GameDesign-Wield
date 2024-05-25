using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXP : Items
{
    public float Experience;
    protected override void onPick()
    {
        LevelSystem.GainEXP(Experience);
        Destroy(this.gameObject);
    }
}
