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

    //// Health variables
    //[Header("Health variables")]
    //[SerializeField] protected uint maxHealth;
    //[SerializeField] protected uint currHealth;

    // Slider variables
    [Header("Slider variables")]
    [SerializeField] protected Slider healthSlider;
    [SerializeField] protected Image fillImage;
    protected Color fullHealthColor = Color.green;
    protected Color zeroHealthColor = Color.red;

    // UI Image
    [Header("UI Image")]
    [SerializeField] protected Image image;
    [SerializeField] protected Sprite normalImage;
    [SerializeField] protected Sprite attackImage;


    // Entity variables
    [Header("Entity variables")]
    [SerializeField] protected Character character;

    

    protected void SetHealthUI()
    {


        normalImage = character.getSprite();
        attackImage = character.getAttackSprite();

        image.sprite = normalImage;
        //image.scaleMode = ScaleMode.ScaleAndCrop;
        image.preserveAspect = true;


        //image.minHeight = character.minHeight;
        //image.minWidth= character.minWidth;
        //= true;
        image.rectTransform.localScale = new Vector3(character.getSpriteScale(), character.getSpriteScale());



        //image.sprite.bounds. *= character.getSpriteScale();
        //image.sprite.bounds.size.y *= character.getSpriteScale();

        // setting up name variable
        charName.text = character.getName();

        //// setting up health
        //maxHealth = character.getMaxHealth();
        //currHealth = character.getCurrHealth();

        // setting up slider
        healthSlider.maxValue = character.getMaxHealth();
        healthSlider.value = character.getCurrHealth();
        healthSlider.minValue = 0;

        // setting up hp text
        charHP.text = character.getCurrHealth() + "/" + character.getMaxHealth();
        float scale = (float)character.getCurrHealth() / (float)character.getMaxHealth();
        if (scale <= 0.3f)
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
    }

    public void SwitchSprite()
    {
        print("switching " + name + "'s sprite");
        if(image.sprite == normalImage)
        {
            //print("normal sprite --> attack sprite");
            image.sprite = attackImage;
        } 
        else
        {
            //print("attack sprite --> normal sprite");
            image.sprite = normalImage;
        }
    }

    private void Update()
    {
        if (healthSlider.value != character.getCurrHealth())
        {
            SetHealthUI();
        }
    }
}
