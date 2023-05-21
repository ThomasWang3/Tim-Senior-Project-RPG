using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EntityData : ScriptableObject
{
    [SerializeField] public string charName;
    [SerializeField] public uint maxHealth;
    [SerializeField] public uint currHealth;
}
