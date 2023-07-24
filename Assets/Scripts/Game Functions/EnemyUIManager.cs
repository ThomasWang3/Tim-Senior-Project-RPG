using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(100)]
public class EnemyUIManager : MonoBehaviour
{
    [Header("Enemy EntityData List")]
    [SerializeField] private InitiateBattle entityDataList;
    [SerializeField] private List<Enemy> enemyList;

    [Header("UI Variables")]
    [SerializeField] private GameObject enemyUIPrefab;
    [SerializeField] private GameObject enemyUI;
    [SerializeField] Vector3 spawnPosition;
    //[SerializeField] private RectTransform rt;
    //[SerializeField] private float width = 600;
    [SerializeField] private float numEnemies;
    private float offset = 200;

    //[SerializeField] private Camera cam;

    [Header("Curr Enemy")]
    [SerializeField] private Enemy enemy;

    public List<Enemy> getEnemies()
    {
        return enemyList;
    }

    void Awake()
    {
        Debug.Log("EnemyUI Manager Started here");
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
        //Debug.Log("transform.position: " + transform.position);
        //spawnPosition = GetComponent<RectTransform>().anchoredPosition;

        //transform.position;
        spawnPosition.x = (4 - numEnemies) * 100;
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
        //Debug.Log(rt.rect);
        //width = (rt.anchorMax.x - rt.anchorMin.x) * Screen.width;
    }

    private void Start()
    {
    }

    // Update is called once per frame
    //public void CreateEnemyUI()
    //{
        
    //}

    public void UpdateEnemy(Enemy e)
    {
        enemy = e;
    }

    private void Update()
    {
        //enemyUI.GetComponent<RectTransform>().position = new Vector3(offset, 0f, 0f);
        //enemyUI.transform.position = spawnPosition;
        //Debug.Log("spawnPosition: " + spawnPosition);
    }
}
