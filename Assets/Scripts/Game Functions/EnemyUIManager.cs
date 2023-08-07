using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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




    private float enemyUIOffset = 150;


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
    [SerializeField] private Image cursor;
    [SerializeField] private Vector2 cursorOffset;
    [SerializeField] private bool lockEnemyUI = false;

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
            if (!lockEnemyUI)
            {
                currEnemyIndex--;
                if (currEnemyIndex < 0)
                {
                    currEnemyIndex = enemyList.Count - 1;
                }
                //print(currEnemyIndex);
                currEnemy = enemyList[currEnemyIndex];
                AdjustCursor();
                //cursor.transform.SetParent(currEnemy.transform, false);
                //cursor.transform.position = cursor.transform.parent.position + new Vector3(0f, cursorOffset);

                battleManager.UpdateEnemy(currEnemy);
            }
        };


        right = input.Battle.Right;

        right.started += rightBehavior => {
            if (!lockEnemyUI)
            {
                currEnemyIndex++;
                if (currEnemyIndex >= enemyList.Count)
                {
                    currEnemyIndex = 0;
                }
                //print(currEnemyIndex);
                currEnemy = enemyList[currEnemyIndex];
                AdjustCursor();
                //cursor.transform.SetParent(currEnemy.transform, false);
                //cursor.transform.position = cursor.transform.parent.position + new Vector3(0f, cursorOffset);
                battleManager.UpdateEnemy(currEnemy);
            }
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
        //spawnPosition.x += enemyUIOffset;
        spawnPosition.y = 0;
        foreach (EntityData ed in entityDataList.getEntities())
        {
            //GameObject 
            enemyUI = Instantiate(enemyUIPrefab, Vector3.zero, Quaternion.identity, transform);
            enemyUI.GetComponent<Enemy>().SetupCharacter(ed);

            enemyUI.GetComponent<RectTransform>().anchoredPosition = spawnPosition;

            enemyList.Add(enemyUI.GetComponent<Enemy>());
            //enemyUI.GetComponent<Enemy>().getSprite().rec
            //    += 
            //    new Vector3(enemyUI.GetComponent<Enemy>().getSpawnOffset().x, 
            //                enemyUI.GetComponent<Enemy>().getSpawnOffset().y);
            spawnPosition.x += enemyUIOffset;
        }
        currEnemy = enemyList[currEnemyIndex];
        battleManager.UpdateEnemy(currEnemy);

        AdjustCursor();
        //cursor.transform.SetParent(currEnemy.transform, false);
        //cursor.transform.position = cursor.transform.parent.position + new Vector3(0f, cursorOffset);
        //cursor.transform.localPosition = Vector3.zero;

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
        cursor.transform.SetParent(this.transform);
        Destroy(currEnemy.gameObject);

        //print("updating enemyManager's currEnemy and index");
        if(enemyList.Count > 0)
        {
            currEnemy = enemyList[currEnemyIndex];
            AdjustCursor();
            battleManager.UpdateEnemy(currEnemy);
        }

    }
    public void DeleteOverworldEnemy()
    {
        Destroy(entityDataList.gameObject);

        //SceneManager.UnloadSceneAsync("Battle Template");
        //FindObjectOfType<PlayerMovement>().UnlockMovement();
        //entityDataList.StopMusic();
    }

    public void UnloadBattleScene()
    {
        if(enemyList.Count == 0)
        {
            DeleteOverworldEnemy();
        }
        entityDataList.StopMusic();
        SceneManager.UnloadSceneAsync("Battle Template");
        FindObjectOfType<PlayerMovement>().UnlockMovement();
    }

    public void PlayerDead()
    {
        SceneManager.LoadScene("Game Over");
    }

    public void AdjustCursor()
    {
        //cursor.transform.SetParent()
        cursor.transform.SetParent(currEnemy.transform, true);
        cursor.rectTransform.anchoredPosition = new Vector2(0.5f, 0.0f);
        cursor.rectTransform.localPosition += new Vector3(cursorOffset.x, cursorOffset.y);
        //cursor.transform.position = cursor.transform.parent.position + new Vector3(cursorOffset.x, cursorOffset.y);
        //cursor.transform.position = cursor.transform.parent.position + new Vector3(0f, cursorOffset);
    }

    public void LockBattleUI()
    {
        lockEnemyUI = true;
        cursor.gameObject.SetActive(false);
    }
    public void UnlockBattleUI()
    {
        lockEnemyUI = false;
        cursor.gameObject.SetActive(true);
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
