using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField] private uint maxHealth;
    [SerializeField] private uint currHealth;

    private void Start()
    {
        currHealth = maxHealth;
    }

    private void Attack(uint damage, Character enemy)
    {
        enemy.TakeDamage(damage);
    }

    private void TakeDamage(uint damageTaken)
    {
        currHealth -= damageTaken;
        if(currHealth == 0)
        {

        }
    }

    private void Die()
    {
        Debug.Log(this.gameObject.name + " died.");
    }
}
