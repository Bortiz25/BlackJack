using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour
{
    public Sprite[] cards;
    int[] cardValues = new int[53];
    int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("deckScript called");
        MakeDeck();
    }

    void MakeDeck(){
        int num = 0;
        for(int i = 0; i < cards.Length; i++){
            num = i;
            num %= 13;
            if(num > 10 || num == 0){
                num = 10;
            }
            cardValues[i] = num++;
        }
        currentIndex = 1;
    }

    public void ShuffleDeck(){
        Debug.Log("shuffle is called");
        for(int i = cards.Length - 1; i >0; --i){
        int j = Mathf.FloorToInt(Random.Range(0.0f,1.0f) * cards.Length - 1)+1;
        Sprite face = cards[i];
        cards[i] = cards[j];
        cards[j] = face;

        int val = cardValues[i];
        cardValues[i] = cardValues[j];
        cardValues[j] = val;

        }
    }

    public int DealCard(CardScript cardScript) {
        //cards.
        cardScript.SetSprite(cards[currentIndex]);
        cardScript.SetValue(cardValues[currentIndex++]);
        currentIndex++;
        return cardScript.GetValueOfCard();
    }

    public Sprite GetCardBack(){
        return cards[0];
    }
}
