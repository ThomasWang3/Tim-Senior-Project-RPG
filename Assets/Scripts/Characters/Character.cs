using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //[SerializeField] EntityData entityData;

    [SerializeField] public string charName;
    [SerializeField] public uint maxHealth;
    [SerializeField] public uint currHealth;
    [SerializeField] public int turnCounter;
    [SerializeField] public uint speed;
    [SerializeField] public Sprite sprite;
    [SerializeField] public float spriteScale;

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
    public uint getSpeed()
    {
        return speed;
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
        turnCounter = ed.turnCounter;
        speed = ed.speed;
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
