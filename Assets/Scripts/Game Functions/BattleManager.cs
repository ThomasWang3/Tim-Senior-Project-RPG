using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem;

[DefaultExecutionOrder(200)]
public class BattleManager : MonoBehaviour
{
    //enum Turn
    //{
    //    Player,
    //    Enemy
    //};
    [SerializeField] private PlayerUI pui;
    [Header("Player")]
    [SerializeField] private Player player;
    [Header("Enemies")]
    [SerializeField] private List<Enemy> enemyList;
    [Header("Enemy")]
    [SerializeField] private Enemy currEnemy;


    [Header("Battle Turn Variables")]
    [SerializeField] private Character currEntity;
    [SerializeField] private Queue<Character> turnQueue = new Queue<Character>();
    [SerializeField] private bool entityActed = true;
    //[SerializeField] private bool playerAct = true;
    [SerializeField] private float turnSeconds;


    [Header("Other Scripts")]
    [SerializeField] private EnemyUIManager enemyManager;

    [Header("Other Gameobjects")]
    [SerializeField] private GameObject playerMenu;
    private Button[] uiButtons;
    
    void Start()
    {

        FindObjectOfType<PlayerMovement>().LockMovement();

        //initialize global varialbes
        enemyList.AddRange(enemyManager.getEnemies());
        player = FindObjectOfType<Player>();

        //uiButtons = playerMenu.GetComponentsInChildren<Button>();

        //Debug.Log("BattleManager Started Here");
        //populate turn queue
        turnQueue.Enqueue(player);
        for (int i = 0; i < enemyList.Count; i++){
            turnQueue.Enqueue(enemyList[i]);
            //Debug.Log("added " + enemyList[i].getName() + " to the turn queue");
        }
        
    }

    //Button Onclick UI. These should only be clickable during a Player's turn.

    public void onAttackSelected()
    {
        PlayerTurn();
        //placeholder here.For now we will just attack the next enemy in the queue.
        //playerAct = false;
        player.Attack(player.getAttack(), currEnemy);
        FindObjectOfType<PlayerUI>().SwitchSprite();
        CheckEnemy();
        entityActed = true;
        StartCoroutine(turnTime());
    }

    public void onHealSelected()
    {
        PlayerTurn();
        //heal character based on the next one and move on to the next person
        //playerAct = false;
        player.Heal(player.getAttack());
        entityActed = true;
        StartCoroutine(turnTime());
    }

    public void onRunSelected()
    {
        Debug.Log("Current Scene: " + SceneManager.GetActiveScene().name);
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        //the above code doens't work since it removed the first scene added to scene manager first.
        //needs a way to pass in the name of the added battle scene so we can remove it. 
        SceneManager.UnloadSceneAsync("Battle Template");
        FindObjectOfType<PlayerMovement>().UnlockMovement();
        //Time.timeScale = 1;
    }
    
    IEnumerator turnTime()
    {
        //
        if (!entityActed)
        {
            yield break;
        }
        entityActed = false;
        //print("Start waiting " + delaySeconds + " seconds");

        // check whose turn it is
        if(turnQueue.Peek().getName() == player.getName())
        {
            // if it's the player's turn (enemy just attacked)
            yield return new WaitForSeconds(turnSeconds);
            //print("turning ON buttons");
            playerMenu.GetComponent<CanvasGroup>().interactable = true;
            enemyManager.UnlockBattleUI();
            if (enemyList.Count != 0)
            {
                currEntity.gameObject.GetComponent<EnemyUI>().SwitchSprite();
            }
        } 
        else
        {
            // if it's the enemy's turn (player just attacked)
            //print("turning OFF buttons");

            playerMenu.GetComponent<CanvasGroup>().interactable = false;
            print("lock battle UI");
            enemyManager.LockBattleUI();
            yield return new WaitForSeconds(turnSeconds);

            FindObjectOfType<PlayerUI>().SwitchSprite();
            //player.gameObject.GetComponent<PlayerUI>().SwitchSprite();
            EnemyTurn();
        }
    }

    IEnumerator DefeatedEnemy()
    {
        yield return new WaitForSeconds(turnSeconds);
        // unload battle scene
        SceneManager.UnloadSceneAsync("Battle Template");

        // delete the enemy from the overworld
        enemyManager.DeleteOverworldEnemy();

        // unlock player movement
        FindObjectOfType<PlayerMovement>().UnlockMovement();
    }

    void PlayerTurn()
    {
        currEntity = turnQueue.Dequeue();
        //Debug.Log("Current Turn: " + currEntity.getName());
        turnQueue.Enqueue(currEntity);
    }


    void EnemyTurn()
    {
        entityActed = true;

        if(currEntity.getName() != player.getName())
        {
            // switch back previous enemy's sprite to normal if the previous entity was not the player
            currEntity.gameObject.GetComponent<EnemyUI>().SwitchSprite();
        }

        //start turn by popping entity from queue
        currEntity = turnQueue.Dequeue();

        //end turn by push same entity to end of queue
        Debug.Log("Current Turn: " + currEntity.getName());
        turnQueue.Enqueue(currEntity);


        if (currEntity.getName() == player.getName())
        {
            // check just in case the currEntity is not the player
            return;
        } 
        else
        {
            // if currEntity is not the player, perform an attack by the enemy
            currEntity.Attack(currEntity.getAttack(), player);
            currEntity.gameObject.GetComponent<EnemyUI>().SwitchSprite();

            if (player.IsDead())
            {
                //Notes: bad programming practice. Should fix maybe
                SceneManager.UnloadSceneAsync("Battle Template");

                // unlock player movement
                FindObjectOfType<PlayerMovement>().UnlockMovement();
            } 
            else
            {
                StartCoroutine(turnTime());
            }
        }
    }


    private void PlayerDead()
    {

    }




    private void CheckEnemy()
    {
        // called after every time the player attacks
        if (currEnemy.isDead)
        {
            //Destroy(currEnemy.gameObject);
            //currEntity = turnQueue.Dequeue();
            print("adjust cursor");
            enemyList.Remove(currEnemy);
            enemyManager.RemoveEnemy();

            // check if the enemyList is empty
            if(enemyList.Count == 0)
            {
                // player defeated all enemies
                StartCoroutine(DefeatedEnemy());

            }
            //Destroy(currEnemy.gameObject);
            turnQueue.Clear();
            for (int i = 0; i < enemyList.Count; i++)
            {
                turnQueue.Enqueue(enemyList[i]);
                //Debug.Log("added " + enemyList[i].getName() + " to the turn queue");
            }
            turnQueue.Enqueue(player);

        }
    }

    

    public void UpdateEnemy(Enemy e)
    {
        //print("updating battleManager's currEnemy");
        currEnemy = e;
        //e.SwitchSprite();
    }

}
