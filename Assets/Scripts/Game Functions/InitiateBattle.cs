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

    [Header("Music")]
    [SerializeField] private AudioSource battleMusic;

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
        //Time.timeScale = 0;
        battleInitiated = true;
        SceneManager.LoadSceneAsync("Battle Template", LoadSceneMode.Additive);
        // if there is a battleMusic and it's not active, activate it and play it
        //if (battleMusic && !battleMusic.isActiveAndEnabled)
        //{
        //    battleMusic.enabled = true;
        //}
        StartMusic();
    }

    public void StartMusic()
    {
        if (battleMusic && !battleMusic.isActiveAndEnabled)
        {
            battleMusic.enabled = true;
        }
    }
    public void StopMusic()
    {
        if (battleMusic && battleMusic.isActiveAndEnabled)
        {
            battleMusic.enabled = false;
        }
    }
}
