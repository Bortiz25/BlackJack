using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class CardSpriteScript : MonoBehaviour
{
    public Button blackBackButton;
    public Button blueBackButton;

    public Sprite blackBack;
    public Sprite blueBack;
    public GameObject deck;
    public GameObject player;
    public GameObject dealer;
    public GameObject hideCard;
    public Canvas styleCanvas;

    public int tracker = 0;
    // Start is called before the first frame update
    void Start()
    {
        blackBackButton.onClick.AddListener(() => handleBlackBack());
        blueBackButton.onClick.AddListener(() => handleBlueBack());
    }

    void handleBlackBack(){
        Debug.Log("change back called");
        deck.GetComponent<DeckScript>().cards[0] = blackBack;
        deck.GetComponent<SpriteRenderer>().sprite = blackBack;
        foreach (GameObject x in player.GetComponent<PlayerScript>().hand){
            x.GetComponent<SpriteRenderer> ().sprite = blackBack;
        }
        foreach (GameObject x in dealer.GetComponent<PlayerScript>().hand){
            x.GetComponent<SpriteRenderer> ().sprite = blackBack;
        }
        hideCard.GetComponent<SpriteRenderer>().sprite = blackBack;
        styleCanvas.gameObject.SetActive(false);
    }

    void handleBlueBack(){
        deck.GetComponent<DeckScript>().cards[0] = blueBack;
        deck.GetComponent<SpriteRenderer>().sprite = blueBack;
        foreach (GameObject x in player.GetComponent<PlayerScript>().hand){
            x.GetComponent<SpriteRenderer> ().sprite = blueBack;
        }
        foreach (GameObject x in dealer.GetComponent<PlayerScript>().hand){
            x.GetComponent<SpriteRenderer> ().sprite = blueBack;
        }
        hideCard.GetComponent<SpriteRenderer>().sprite = blueBack;
        styleCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
