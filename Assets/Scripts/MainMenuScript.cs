using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Button startGameButton;
    public Button gameInfoButton;
    public Button styleButton;
    public Canvas infoCanvas;
    public Canvas styleCanvas;
    public Canvas mainCanvas;
    public Canvas gameCanvas;
    public GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        infoCanvas.gameObject.SetActive(false);
        styleCanvas.gameObject.SetActive(false);
        //gameCanvas.gameObject.SetActive(false);
        startGameButton.onClick.AddListener(() => StartGame());
        gameInfoButton.onClick.AddListener(() => handleInfo());
        styleButton.onClick.AddListener(() => handleStyleClick());
    }

    public void StartGame(){
        mainCanvas.gameObject.SetActive(false);
        manager.GetComponent<GameManager>().resetEverything();
    }

    void handleInfo(){
        infoCanvas.gameObject.SetActive(true);
    }

    void handleStyleClick(){
        styleCanvas.gameObject.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            infoCanvas.gameObject.SetActive(false);
            //styleCanvas.gameObject.SetActive(false);
        }
    }
}
