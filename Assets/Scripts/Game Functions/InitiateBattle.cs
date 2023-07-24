using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InitiateBattle : MonoBehaviour
{
    [Header("Enemy Information")]
    [SerializeField] private Collider2D enemyCollider;
    [SerializeField] private List<EntityData> entities;
    [SerializeField] private PlayerMovement player;

    [Header("for EnemyUIManager use")]
    public bool battleInitiated;

    [Header("Battle Background Image")]
    [SerializeField] public Sprite backgroundSprite;
    
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }


    public List<EntityData> getEntities()
    {
        return entities;
    }
    //[SerializeField] Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Load battle scene with " + gameObject.name + " vs. " + collision.gameObject.name);
        //player.LockMovement();
        Time.timeScale = 0;
        battleInitiated = true;
        SceneManager.LoadSceneAsync("Battle Template", LoadSceneMode.Additive);
    }
}
