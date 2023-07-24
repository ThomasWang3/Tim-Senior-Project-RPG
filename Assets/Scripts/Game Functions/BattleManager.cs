using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(200)]
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
    [SerializeField] private Queue<Character> turnQueue = new Queue<Character>();
    //[SerializeField] private int speedCounter = 20;


    [Header("Other Scripts")]
    [SerializeField] private EnemyUIManager enemyManager;

    [Header("Other Gameobjects")]
    [SerializeField] private GameObject playerMenu;
    private Button[] uiButtons;
    public uint placeHolder = 10;
    //
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
        
        //initialize global varialbes
        entitiesList.AddRange(enemyManager.getEnemies());
        player = FindObjectOfType<Player>();
        uiButtons = playerMenu.GetComponentsInChildren<Button>();

        Debug.Log("BattleManager Started Here");
        //populate turn queue
        turnQueue.Enqueue(player);
        for (int i = 0; i < entitiesList.Count; i++){
            turnQueue.Enqueue(entitiesList[i]);
            Debug.Log("added " + entitiesList[i].getName() + " to the turn queue");
        }
        
        manageTurn();
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

    //TODO: add functionality for player to choose which enemy to attack. Button should probably expand to a dropdown and allow player to choose. 
    //      for now a placeholder is to just let player attack the next enemy in the queue.
    void manageTurn(){
        //start turn by popping entity from queue
        Character activeChar = turnQueue.Dequeue();

        if(activeChar.IsDead())
        {
            activeChar = turnQueue.Dequeue();
        }
        //end turn by push same entity to end of queue
        Debug.Log("Current Turn: " + activeChar.getName());
        turnQueue.Enqueue(activeChar);

        //TODO: Add a check here to see if the enemy is dead or not. If enemy is dead then remove it from the queue and choose the next character.

        if(activeChar.getName() == player.getName())
        { //player turn
            foreach(Button button in uiButtons)
            {
                button.enabled = true;
            }
        }
        else
        { //enemy turn
            //disable player UI buttons so they can't attack
            foreach(Button button in uiButtons)
            {
                button.enabled = false;
            }
            activeChar.Attack(activeChar.getAttack(), player);
            if(player.IsDead()){
                //Notes: bad programming practice. Should fix maybe
                SceneManager.UnloadSceneAsync("Battle Template");
                Time.timeScale = 1;
            }else{
                manageTurn();
            }
        }

    }

    //Button Onclick UI. These should only be clickable during a Player's turn.
    public void onAttackSelected()
    {
        //placeholder here.For now we will just attack the next enemy in the queue.
        Character nextEnemy = turnQueue.Peek();
        player.Attack(player.getAttack(), nextEnemy);
        manageTurn();
    }

    public void onHealSelected()
    {
        //heal character based on the next one and move on to the next person
        player.Heal(player.getAttack());
        manageTurn();
    }

    public void onRunSelected()
    {
        Debug.Log("Current Scene: " + SceneManager.GetActiveScene().name);
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        //the above code doens't work since it removed the first scene added to scene manager first.
        //needs a way to pass in the name of the added battle scene so we can remove it. 
        SceneManager.UnloadSceneAsync("Battle Template");
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
