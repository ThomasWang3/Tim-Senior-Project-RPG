using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundChanging : MonoBehaviour
{
    [Header("Enemy EntityData List")]
    [SerializeField] private InitiateBattle entityDataList;

    [Header("Background Image")]
    [SerializeField] private Image image;

    void Awake()
    {

        Debug.Log("BackgroundChangingStarted here");
        InitiateBattle[] enemies = FindObjectsOfType<InitiateBattle>();
        foreach (InitiateBattle enemy in enemies)
        {
            if (enemy.battleInitiated)
            {
                entityDataList = enemy;
            }
        }
        //entityDataList.battleInitiated = false;

        if (entityDataList.backgroundSprite)
        {
            image.sprite = entityDataList.backgroundSprite;
        }
        else 
        {
            image.color = Color.black;
            image.sprite = null;
        }
    }

}
