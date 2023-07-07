using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : BattleUI
{
    protected void Start()
    {
        character = GetComponent<Enemy>();
        this.name = character.getName();
        SetHealthUI();
    }

}
