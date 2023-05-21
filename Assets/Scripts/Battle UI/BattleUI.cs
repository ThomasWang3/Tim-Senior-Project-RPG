using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUI : MonoBehaviour
{
    // Text variables
    [Header("Text variables")]
    [SerializeField] protected TextMeshProUGUI charName;
    [SerializeField] protected TextMeshProUGUI charHP;

    // Health variables
    [Header("Health variables")]
    [SerializeField] protected uint maxHealth;
    [SerializeField] protected uint currHealth;

    // Slider variables
    [Header("Slider variables")]
    [SerializeField] protected Slider healthSlider;
    [SerializeField] protected Image fillImage;
    protected Color fullHealthColor = Color.green;
    protected Color zeroHealthColor = Color.red;

    // Entity variables
    [Header("Entity variables")]
    [SerializeField] protected Character character;
    [SerializeField] protected Image image;

    public void SetHealthUI()
    {
        image.sprite = character.getSprite(); 

        // setting up name variable
        charName.text = character.getName();

        // setting up health
        maxHealth = character.getMaxHealth();
        currHealth = character.getCurrHealth();

        // setting up slider
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currHealth;
        healthSlider.minValue = 0;

        // setting up hp text
        charHP.text = currHealth + "/" + maxHealth;
        float scale = (float) currHealth / (float) maxHealth;
        if(scale <= 0.3f)
        {
            fillImage.color = Color.red;
        } 
        else if (scale <= 0.7f)
        {
            fillImage.color = Color.yellow;
        } 
        else
        {
            fillImage.color = Color.green;
        }
        if(currHealth == 0)
        {
            fillImage.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        SetHealthUI();
    }
    private void Update()
    {
        if (currHealth != character.getCurrHealth())
        {
            SetHealthUI();
        }
    }
}
