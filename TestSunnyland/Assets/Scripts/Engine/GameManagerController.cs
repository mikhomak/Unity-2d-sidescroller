
using UnityEngine;

public class GameManagerController : MonoBehaviour {


    //Public variables
    [HideInInspector]
    public static bool IsInputEnable = true;

    //Referencies
    public GameObject player;
    private PlayerController playerController;
    
    public GameObject DeathScreen;
    public GameObject PauseScreen;

    //Private variables

	// Use this for initialization
	void Start () {
        playerController = player.GetComponent<PlayerController>();
        resumeTheGame();
        
	}
	
	// Update is called once per frame
	void Update () {
        gameOver();
        pauseTheGame();
    }


    // Pausing method
    // It's public because we invoke it from our PauseScreenCotroller
    public void resumeTheGame()
    {
        // Resuming the game
        Time.timeScale = 1;
        IsInputEnable = true;
        playerController.pauseGame = false;
        playerController.gameOver = false;
        PauseScreen.SetActive(false);
        DeathScreen.SetActive(false);
    }


    public void loadFromTheCheckpoint()
    {
        resumeTheGame();
        playerController.restartFromTheLastCheckpoint();
    }



    // Pausing the game
    private void pauseTheGame()
    {
        if (playerController.pauseGame)
        {
            // Pausing the game and activating the pausing screen
            Time.timeScale = 0;
            IsInputEnable = false;
            PauseScreen.SetActive(true);
        }
    }

    private void gameOver()
    {
        if (playerController.gameOver)
        {
            // Pausing the game and activating the death screen
            Time.timeScale = 0;
            IsInputEnable = false;
            DeathScreen.SetActive(true);
        }
    }
}
