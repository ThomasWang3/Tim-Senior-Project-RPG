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

    [Header("Entities")]
    [SerializeField] private List<Character> entitiesList;
    [Header("Player")]
    [SerializeField] private Player player;
    [Header("Enemy")]
    [SerializeField] private Enemy enemy;




    [Header("Battle Turn Variables")]
    [SerializeField] private Turn battleTurn;
    [SerializeField] private int speedCounter = 20;


    [Header("Other Scripts")]
    [SerializeField] private EnemyUIManager enemyManager;

    //// Entity Elements
    //[Header("UI Elements")]

    //[SerializeField] private List<Character> entitiesList;
    //[SerializeField] private EnemyUIManager enemyManager;

    //// Enemy Entity Elements
    //[SerializeField] private InitiateBattle enemyList;
    //[SerializeField] private List<EntityData> enemyEntitiesList;
    //[SerializeField] private float numEnemies;
    ////[SerializeField] private RectTransform enemiesUI;
    ////[SerializeField] private Vector3 enemyUISpawningPosition;
    ////[SerializeField] private float enemyUISpawningWidth;

    //// Turn Sequence
    //[Header("Turn Sequence")]
    //[SerializeField] private Turn battleTurn;
    //[SerializeField] private int speedCounter = 20;
    //// Start is called before the first frame update
    void Start()
    {

        ////GameObject enemyUISpawningObject = GameObject.Find("Enemies");
        //enemyUISpawningPosition = enemiesUI.transform.position;
        ////enemyUISpawningWidth = 
        //Debug.Log(enemiesUI.rect.xMax);
        //Debug.Log(enemiesUI.rect.xMin);
        //enemyUISpawningWidth = (enemiesUI.anchorMax.x - enemiesUI.anchorMin.x) * Screen.width;
        entitiesList.AddRange(enemyManager.getEnemies());
        //enemyList = FindObjectOfType<InitiateBattle>();
        //if (enemyList != null)
        //{
        //    enemyEntitiesList.AddRange(enemyList.getEntities());
        //}

        //foreach(EntityData ed in enemyEntitiesList)
        //{
        //    c.turnCounter = speedCounter;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
