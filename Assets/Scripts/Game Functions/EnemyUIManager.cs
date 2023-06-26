using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private float width = 600;
    [SerializeField] private float numEnemies;
    [SerializeField] private float offset = -400;

    //[SerializeField] private Camera cam;

    [Header("Curr Enemy")]
    [SerializeField] private Enemy enemy;

    public List<Enemy> getEnemies()
    {
        return enemyList;
    }

    private void Start()
    {
        entityDataList = FindObjectOfType<InitiateBattle>();

        numEnemies = entityDataList.getEntities().Count;
        Debug.Log("transform.position: " + transform.position);
        spawnPosition = GetComponent<RectTransform>().anchoredPosition;
        //transform.position;
        spawnPosition.y = 0;
        offset += width / (numEnemies + 1);
        foreach (EntityData ed in entityDataList.getEntities())
        {
            spawnPosition.x += offset;
            //GameObject 
            enemyUI = Instantiate(enemyUIPrefab, Vector3.zero, Quaternion.identity, transform);
            enemyUI.AddComponent<Enemy>().SetupCharacter(ed);
            
            enemyUI.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
            //enemyUI.GetComponent<RectTransform>().

            enemyList.Add(enemyUI.GetComponent<Enemy>());


        }
        //Debug.Log(rt.rect);
        //width = (rt.anchorMax.x - rt.anchorMin.x) * Screen.width;
    }

    // Update is called once per frame
    public void CreateEnemyUI()
    {
        
    }

    public void UpdateEnemy(Enemy e)
    {
        enemy = e;
    }

    private void Update()
    {
        //enemyUI.GetComponent<RectTransform>().position = new Vector3(offset, 0f, 0f);
        //enemyUI.transform.position = spawnPosition;
        Debug.Log("spawnPosition: " + spawnPosition);
    }
}
