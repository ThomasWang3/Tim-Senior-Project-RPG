using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    enum Turn
    {
        Player,
        Enemy
    };

    // UI Elements
    [Header("UI Elements")]
    [SerializeField] private InitiateBattle enemyList;
    [SerializeField] private List<EntityData> entitiesList;

    // Turn Sequence
    [Header("Turn Sequence")]
    [SerializeField] private Turn battleTurn;
    [SerializeField] private int speedCounter = 20;
    // Start is called before the first frame update
    void Start()
    {
        entitiesList.Add(FindObjectOfType<Player>().getEntityData());
        enemyList = FindObjectOfType<InitiateBattle>();
        if (enemyList != null)
        {
            entitiesList.AddRange(enemyList.getEntities());
        }
        foreach(EntityData el in entitiesList)
        {
            el.turnCounter = speedCounter;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
