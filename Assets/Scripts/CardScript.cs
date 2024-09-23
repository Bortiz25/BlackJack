using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    // value variable to keep track of value
    public int value = 0; 

    void Start(){
    }

     public int GetValueOfCard(){
        return value;
     }
     public void SetValue(int newValue){
        value = newValue;
     }

     public void SetSprite(Sprite newSprite){
         gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
     }

     public string GetSpriteName(){
        return GetComponent<SpriteRenderer>().sprite.name;
     }

     public void ResetCard(){
        Sprite back = GameObject.Find("Deck").GetComponent<DeckScript>().GetCardBack();
        gameObject.GetComponent<SpriteRenderer>().sprite = back;    
        value = 0;
     }
}
