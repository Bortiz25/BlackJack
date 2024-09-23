using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // used for both the player and the dealer
    // Start is called before the first frame update

    public CardScript cardScript;
    public DeckScript deckScript;
    public int handValue = 0;

    public GameObject[] hand;
    public int cardIndex = 0;
    List<CardScript> aceList = new List<CardScript>();
    public void StartHand()
    {
        GetCard();
        GetCard();
    }

    // add card to player hand
    public int GetCard() {
        int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
        handValue += cardValue;
        if(cardValue == 1) {
            aceList.Add(hand[cardIndex].GetComponent<CardScript>());
        }
        // create function to update the value of the ace card
        AceCheck();
        ++cardIndex;
        return handValue;
    }


    public void AceCheck(){
        foreach(CardScript ace in aceList){
            if(handValue + 10 < 22 && ace.GetValueOfCard() == 1){
                ace.SetValue(11);
                handValue += 10;
            }else if(handValue > 21 && ace.GetValueOfCard() == 11){
                ace.SetValue(1);
                handValue -= 10;
            }

        }
    }

    public void ResetHand(){
        for(int i = 0; i < hand.Length; i++){
            hand[i].GetComponent<CardScript>().ResetCard();
            hand[i].GetComponent<Renderer>().enabled = false;
        }
        cardIndex = 0;
        handValue = 0;
        aceList = new List<CardScript>();
    }
}
