
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenController : MonoBehaviour {

    //Referencies
    public GameManagerController gameManager;
    public GameObject levelChanger;
    private LevelChangerController levelChangerController;

    private void Start()
    {
        levelChangerController = levelChanger.GetComponent<LevelChangerController>();
    }


    public void LoadFromTheCheckpoint()
    {
        gameManager.loadFromTheCheckpoint();
    }



    public void ResumeTheGame()
    {
        gameManager.resumeTheGame();
    }


    public void goToTheMenu()
    {
        levelChangerController.LoadMainMenuScene();
    }
}
