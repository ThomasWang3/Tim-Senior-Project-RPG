using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitiateBattle : MonoBehaviour
{
    [SerializeField] private BoxCollider2D enemyCollider;
    [SerializeField] private List<EntityData> entities;
    //[SerializeField] Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Load battle scene with " + gameObject.name + " vs. " + collision.gameObject.name);
        SceneManager.LoadScene("Battle Template");
    }

}
