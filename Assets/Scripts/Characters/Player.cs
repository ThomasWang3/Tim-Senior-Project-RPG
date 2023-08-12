using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] EntityData playerData;

    private void Start()
    {
        SetupCharacter(playerData);
    }

    public EntityData GetPlayerData()
    {
        return playerData;
    }

    public void Heal()
    {
        this.currHealth += 20;
        //prevent overhealing
        if(this.currHealth > maxHealth)
        {
            this.currHealth = maxHealth;
        }
    }
}
