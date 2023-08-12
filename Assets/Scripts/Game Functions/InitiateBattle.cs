using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class InitiateBattle : MonoBehaviour
{
    [Header("Enemy Information")]
    [SerializeField] private Collider2D enemyCollider;
    [SerializeField] private List<EntityData> entities;
    [SerializeField] private PlayerMovement player;

    [Header("for EnemyUIManager use")]
    public bool battleInitiated = false;
    public bool enemyDefeated = false;

    [Header("Battle Background Image")]
    [SerializeField] public Sprite backgroundSprite;

    [Header("Music")]
    [SerializeField] private AudioSource battleMusic;
    [SerializeField] private AudioSource overworldMusic;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        battleMusic = GetComponent<AudioSource>();
    }


    public List<EntityData> getEntities()
    {
        return entities;
    }
    //[SerializeField] Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enemyDefeated)
        {
            //Debug.Log("Load battle scene with " + gameObject.name + " vs. " + collision.gameObject.name);
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
        
    }

    public void StartMusic()
    {
        //print("Start Battle Music");
        if (!battleMusic.isActiveAndEnabled)
        {
            battleMusic.enabled = true;
            battleMusic.volume = 1;
        }
        if (overworldMusic.isPlaying)
        {
            overworldMusic.volume = 0f;
            overworldMusic.Pause();
        }
    }
    public void StopMusic()
    {
        //print("Stop Battle Music");

        //if (battleMusic.isActiveAndEnabled && !overworldMusic.isPlaying)
        //{
            StartCoroutine(MusicFades());
        //}
    }


    IEnumerator MusicFades()
    {
        if (enemyDefeated)
        {
            print(this.gameObject);
            GetComponent<SpriteRenderer>().enabled = false;

            //enemyCollider.
            //this..SetActive(false);
        }
        //print("start music fade");
        //print("battle music lowering volume");

        for (float i = 1; i >= 0; i -= (Time.deltaTime / 1f))
        {
            battleMusic.volume = i;
            yield return null;
        }
        battleMusic.volume = 0;
        battleMusic.enabled = false;

        //if (!overworldMusic)
        //{
        //print("battle music muted");
        overworldMusic.Play();
        //print("overworld music lowering volume");
        for (float i = 0; i < 1; i += (Time.deltaTime / 1.5f))
        {
            overworldMusic.volume = i;
            yield return null;
        }
        battleMusic.volume = 1;
        //print("overworld music muted");
        //}
        if(enemyDefeated)
        {
            Destroy(this.gameObject);
        }
        yield return null;
    }


}
