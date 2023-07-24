using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //[SerializeField] EntityData entityData;

    [SerializeField] public string charName;
    [SerializeField] public int maxHealth;
    [SerializeField] public int currHealth;
    //[SerializeField] public int turnCounter;
    //[SerializeField] public uint speed;
    [SerializeField] public Sprite sprite;
    [SerializeField] public float spriteScale;
    [SerializeField] public int attack;
    [SerializeField] public bool isDead = false;

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
    public float getSpriteScale()
    {
        return spriteScale;
    }
    public int getAttack(){
        return attack;
    }
    public bool IsDead(){
        return isDead;
    }

    public void SetupCharacter(EntityData ed)
    {
        charName = ed.charName;
        maxHealth = ed.maxHealth;
        currHealth = maxHealth;
        sprite = ed.sprite;
        spriteScale = ed.spriteScale;
        attack = ed.attack;
    }
    private void Start()
    {

    }

    public void Attack(int damage, Character enemy)
    {
        enemy.TakeDamage(damage);
    }

    public void TakeDamage(int damageTaken)
    {
        currHealth -= damageTaken;
        Debug.Log("CURRENT HEALTH IS: " + this.currHealth);
        if(currHealth <= 0)
        {
            Debug.Log(this.getName() + " has died");
            currHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(charName + " defeated.");
        isDead = true;
    }
}
