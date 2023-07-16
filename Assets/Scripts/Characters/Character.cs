using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //[SerializeField] EntityData entityData;

    [SerializeField] protected string charName;
    [SerializeField] protected uint maxHealth;
    [SerializeField] protected uint currHealth;
    [SerializeField] protected uint attack;
    [SerializeField] protected Sprite sprite;
    [SerializeField] protected float spriteScale;

    public uint getMaxHealth()
    {
        return maxHealth;
    }
    public uint getCurrHealth()
    {
        return currHealth;
    }
    public string getName()
    {
        return charName;
    }
    public Sprite getSprite()
    {
        return sprite;
    }
    public float getSpriteScale()
    {
        return spriteScale;
    }

    public void SetupCharacter(EntityData ed)
    {
        charName = ed.charName;
        maxHealth = ed.maxHealth;
        currHealth = maxHealth;
        sprite = ed.sprite;
        spriteScale = ed.spriteScale;
    }
    private void Start()
    {

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
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(charName + " defeated.");
    }
}
