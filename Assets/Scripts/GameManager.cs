using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button dealButton;
    public Button hitButton;
    public Button stayButton;
    public Text stayButtonText;

    public PlayerScript playerScript;
    public PlayerScript dealerScript;
    private int standClicks = 0; 

    public TextMeshProUGUI dealerScore; 
    public TextMeshProUGUI playerScore;
    public TextMeshProUGUI mainText;

    public GameObject deck;
    public Animator cardAnimation;

    // Card hiding dealer's 2nd hand
    public GameObject hideCard;

    private bool animate = false;

    public GameObject player;
    public GameObject dealer;

    // Start is called before the first frame update
    void Start()
    {
        //Add a click listender to the buttons
        dealButton.onClick.AddListener(()=>DealClicked());
        hitButton.onClick.AddListener(()=>HitClicked());
        stayButton.onClick.AddListener(()=>StayClicked());
        hitButton.gameObject.SetActive(false);
        stayButton.gameObject.SetActive(false); 
        
    }

    void Update(){
        deck.GetComponent<Animator>().SetBool("cardAnimationBool", animate);
        foreach(GameObject x in player.GetComponent<PlayerScript>().hand){
            x.GetComponent<Animator>().SetBool("cardAnimationBool", animate);
        }

        foreach(GameObject x in dealer.GetComponent<PlayerScript>().hand){
            x.GetComponent<Animator>().SetBool("cardAnimationBool", animate);
        }
    }

    private void DealClicked() {
        // reset everything section
        playerScript.ResetHand();
        dealerScript.ResetHand();   
        // main text isn't visible at the beginning 
        mainText.gameObject.SetActive(false);
        dealerScore.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<DeckScript>().ShuffleDeck();
        playerScript.StartHand();
        dealerScript.StartHand();

        //handling displaying the score on the screen
        playerScore.text = "Player Score: " + playerScript.handValue.ToString();
        dealerScore.text = "Dealer Score: " + dealerScript.handValue.ToString();

        hideCard.GetComponent<Renderer>().enabled = true;
        // make deal button disappear when it is clicked
        dealButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(true);
        stayButton.gameObject.SetActive(true);
        hideCard.SetActive(true);

        // handling the text of the stay button
        stayButtonText.text = "Stay";
        animate = false;
    }

    private void HitClicked() {
        //check there is room to hit one more time
        if(playerScript.cardIndex <= 10){
            playerScript.GetCard();
            playerScore.text = "Player Score: " + playerScript.handValue.ToString();
            if(playerScript.handValue > 20){
                RoundOver();
                //StartCoroutine(waitAndRestart());
            }
        }
    }
    private void StayClicked() {
        standClicks++;
        if(standClicks > 1){
            Debug.Log("Round over is called");
            RoundOver();
            //StartCoroutine(waitAndRestart());
        }
        HitDealer();
        stayButtonText.text = "Call";
    }

    private void HitDealer(){
        while(dealerScript.handValue < 16 && dealerScript.cardIndex < 10){
            dealerScript.GetCard();
            // dealer score to display
            dealerScore.text = "Dealer Score: " + dealerScript.handValue.ToString();
            if(dealerScript.handValue > 20) {
                RoundOver();
                //StartCoroutine(waitAndRestart());
            };
        }
    }

    // declare hand is over
    void RoundOver(){
        hideCard.SetActive(false);
        bool playerBust = playerScript.handValue > 21;
        bool dealerBust = dealerScript.handValue > 21;
        bool player21 = playerScript.handValue == 21;
        bool dealer21 = dealerScript.handValue == 21;
        animate = true;

        // not over 
        if(standClicks < 2 && !playerBust && !dealerBust && !player21 && !dealer21) return;
        bool roundOver = true;
        if(playerBust && dealerBust) {
            mainText.text = "All Bust!";
        }
        // if player busts, dealer didn't 
        else if(playerBust || (!dealerBust && dealerScript.handValue > playerScript.handValue)) {
            mainText.text = "Dealer Wins!";
        }

        else if( dealerBust || !playerBust && playerScript.handValue > dealerScript.handValue) {
            mainText.text = "You Win!!";
        }
        // if tie 
        else if(playerScript.handValue == dealerScript.handValue) {
            mainText.text = "Push!! Tie!!";        
        }
        else{
            // makes sure if something weird happens we don't end preemptively
            roundOver = false;
        }
        // Set ui up for next move / hand / turn 
        if(roundOver){
             hitButton.gameObject.SetActive(false);
             stayButton.gameObject.SetActive(false);
             dealButton.gameObject.SetActive(true);
             mainText.gameObject.SetActive(true);
             dealerScore.gameObject.SetActive(true);
             
             standClicks = 0;
        }
        //cardAnimation.SetBool("cardAnimationBool", false);
    }

    IEnumerator waitAndRestart(){
        Debug.Log("wait is called");
        animate = false;
        yield return new WaitForSeconds(5);
        cardAnimation.SetBool("cardAnimationBool", animate);
    }

    public void resetEverything(){
        // reset everything section
        playerScript.ResetHand();
        dealerScript.ResetHand();   
        // main text isn't visible at the beginning 
        mainText.gameObject.SetActive(false);
        dealerScore.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<DeckScript>().ShuffleDeck();
        playerScript.StartHand();
        dealerScript.StartHand();

        //handling displaying the score on the screen
        playerScore.text = "Player Score: " + playerScript.handValue.ToString();
        dealerScore.text = "Dealer Score: " + dealerScript.handValue.ToString();

        hideCard.GetComponent<Renderer>().enabled = true;

        dealButton.gameObject.SetActive(true);
        hitButton.gameObject.SetActive(true);
        stayButton.gameObject.SetActive(true);
        hideCard.SetActive(true);

        // handling the text of the stay button
        //stayButtonText.text = "Stay";
        animate = false;
    }
}
