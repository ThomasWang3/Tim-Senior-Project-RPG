using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : BattleUI
{
    protected void Start()
    {
        character = FindObjectOfType<Player>();
        this.name = character.getName();
        character.SetupCharacter(FindObjectOfType<Player>().GetPlayerData());
        SetHealthUI();
    }

}
