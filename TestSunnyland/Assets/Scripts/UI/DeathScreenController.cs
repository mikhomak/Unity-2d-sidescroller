
using UnityEngine;


public class DeathScreenController : MonoBehaviour {

    public GameObject levelChanger;
    private LevelChangerController levelChangerController;
    public GameManagerController gameManager;

    private void Start()
    {
        levelChangerController = levelChanger.GetComponent<LevelChangerController>();
    }


    public void RestartTheGame()
    {
        levelChangerController.LoadTheSameScene();
    }
        
    public void LoadFromTheCheckpoint()
    {
        gameManager.loadFromTheCheckpoint();
    }


    public void goToTheMenu()
    {
        levelChangerController.LoadMainMenuScene(); // 0 -> main menu
    }
}
