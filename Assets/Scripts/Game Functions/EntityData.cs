using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EntityData : ScriptableObject
{
    [SerializeField] public string charName;
    [SerializeField] public int maxHealth;
    [SerializeField] public Sprite sprite;
    [SerializeField] public Sprite attackSprite;
    [SerializeField] public float spriteScale;
    [SerializeField] public int attack;
}
