using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] EntityData entityData;
    [SerializeField] Sprite sprite;
    //[SerializeField] private uint maxHealth;
    //[SerializeField] private uint currHealth;
    //[SerializeField] private string charName;

    public EntityData getEntityData()
    {
        return entityData;
    }

    public uint getMaxHealth()
    {
        return entityData.maxHealth;
    }
    public uint getCurrHealth()
    {
        return entityData.currHealth;
    }
    public string getName()
    {
        return entityData.charName;
    }

    public Sprite getSprite()
    {
        return sprite;
    }

    public uint getSpeed()
    {
        return entityData.speed;
    }

    public float getSpriteScale()
    {
        return entityData.spriteScale;
    }

    private void Start()
    {
        entityData.currHealth = entityData.maxHealth;
    }

    private void Attack(uint damage, Character enemy)
    {
        enemy.TakeDamage(damage);
    }

    private void TakeDamage(uint damageTaken)
    {
        entityData.currHealth -= damageTaken;
        if(entityData.currHealth == 0)
        {

        }
    }

    private void Die()
    {
        Debug.Log(this.gameObject.name + " died.");
    }
}
