using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(100)]
public class EnemyUIManager : MonoBehaviour
{
    [Header("Enemy EntityData List")]
    [SerializeField] public InitiateBattle entityDataList;
    [SerializeField] private List<Enemy> enemyList;

    [Header("UI Variables")]
    [SerializeField] private GameObject enemyUIPrefab;
    [SerializeField] private GameObject enemyUI;
    [SerializeField] Vector3 spawnPosition;
    //[SerializeField] private RectTransform rt;
    //[SerializeField] private float width = 600;
    [SerializeField] private int numEnemies;
    private float offset = 150;


    // Input
    private Input input;
    private InputAction left;
    private InputAction right;
    //private float lastPressTime;
    //[SerializeField] private float pressDelay;
    //[SerializeField] private Camera cam;

    [Header("Other Scripts")]
    [SerializeField] private BattleManager battleManager;

    [Header("Curr Enemy")]
    [SerializeField] private Enemy currEnemy;
    [SerializeField] private int currEnemyIndex = 0;

    public List<Enemy> getEnemies()
    {
        return enemyList;
    }

    void Awake()
    {
        //Debug.Log("EnemyUI Manager Started here");
        input = new Input();
        left = input.Battle.Left;

        left.started += leftBehavior => {
            currEnemyIndex--;
            if(currEnemyIndex < 0)
            {
                currEnemyIndex = enemyList.Count - 1;
            }
            print(currEnemyIndex);
            currEnemy = enemyList[currEnemyIndex];
            battleManager.UpdateEnemy(currEnemy);
        };


        right = input.Battle.Right;

        right.started += rightBehavior => {
            currEnemyIndex++;
            if (currEnemyIndex >= enemyList.Count)
            {
                currEnemyIndex = 0;
            }
            print(currEnemyIndex);
            currEnemy = enemyList[currEnemyIndex];
            battleManager.UpdateEnemy(currEnemy);
        };



        InitiateBattle[] enemies = FindObjectsOfType<InitiateBattle>();
        foreach(InitiateBattle enemy in enemies)
        {
            if (enemy.battleInitiated)
            {
                entityDataList = enemy;
            }
        }
        entityDataList.battleInitiated = false;

        numEnemies = entityDataList.getEntities().Count;

        spawnPosition.x = (4 - (numEnemies + 1)) * 100;
        //spawnPosition.x += offset;
        spawnPosition.y = 0;
        foreach (EntityData ed in entityDataList.getEntities())
        {
            //GameObject 
            enemyUI = Instantiate(enemyUIPrefab, Vector3.zero, Quaternion.identity, transform);
            enemyUI.GetComponent<Enemy>().SetupCharacter(ed);

            enemyUI.GetComponent<RectTransform>().anchoredPosition = spawnPosition;

            enemyList.Add(enemyUI.GetComponent<Enemy>());
            spawnPosition.x += offset;
        }
        currEnemy = enemyList[currEnemyIndex];
        battleManager.UpdateEnemy(currEnemy);
    }

    public void RemoveEnemy()
    {

        //print("removing from enemyManager's enemyList");
        enemyList.Remove(currEnemy);
        if (currEnemyIndex >= enemyList.Count)
        {
            currEnemyIndex = enemyList.Count - 1;
        }

        //print("destroying currEnemy");
        Destroy(currEnemy.gameObject);

        //print("updating enemyManager's currEnemy and index");
        if(enemyList.Count > 0)
        {
            currEnemy = enemyList[currEnemyIndex];
            battleManager.UpdateEnemy(currEnemy);
        }

    }
    public void DeleteOverworldEnemy()
    {
        Destroy(entityDataList.gameObject);
    }

    public void OnEnable()
    {
        left.Enable();
        right.Enable();
    }
    public void OnDisable()
    {
        left.Disable();
        right.Disable();
    }
}
