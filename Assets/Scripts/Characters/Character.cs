using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //[SerializeField] EntityData entityData;

    [SerializeField] protected string charName;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int currHealth;
    //[SerializeFieldprotected public int turnCounter;
    //[SerializeFieldprotected public uint speed;
    [SerializeField] protected Sprite sprite;
    [SerializeField] protected Sprite attackSprite;
    [SerializeField] protected float spriteScale;
    [SerializeField] protected int attack;
    [SerializeField] protected Vector2 spawnOffset;
    [SerializeField] protected bool isDead = false;

    public int getMaxHealth()
    {
        return maxHealth;
    }
    public int getCurrHealth()
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
    public Sprite getAttackSprite()
    {
        return attackSprite;
    }
    public float getSpriteScale()
    {
        return spriteScale;
    }
    public int getAttack(){
        return attack;
    }
    public Vector2 getSpawnOffset()
    {
        return spawnOffset;
    }

    public bool getIsDead(){
        return isDead;
    }

    public void SetupCharacter(EntityData ed)
    {
        charName = ed.charName;
        maxHealth = ed.maxHealth;
        currHealth = maxHealth;
        sprite = ed.sprite;
        attackSprite = ed.attackSprite;
        spriteScale = ed.spriteScale;
        attack = ed.attack;
        spawnOffset = ed.spawnOffset;
    }
    //private void Start()
    //{
    //}

    public void Attack(int damage, Character enemy)
    {
        //Debug.Log(name + " attacks " + enemy.getName() + " for " + damage + " damage.");
        enemy.TakeDamage(damage);
    }

    public void TakeDamage(int damageTaken)
    {
        currHealth -= damageTaken;
        //Debug.Log(name + " HEALTH IS: " + this.currHealth);
        if(currHealth <= 0)
        {
            //Debug.Log(this.getName() + " has died");
            currHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(charName + " died.");
        isDead = true;
        //Destroy(this.gameObject);
        //this.gameObject.SetActive(false);
    }
}
