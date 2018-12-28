
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuController : MonoBehaviour {

    public GameObject levelChanger;
    private LevelChangerController levelChangerController;

    private void Start()
    {
        levelChangerController = levelChanger.GetComponent<LevelChangerController>();
    }


    public void PlayGame()
    {
        levelChangerController.LoadNextScene();
    }
    


    public void QuitGame()
    {
        Application.Quit();
    }
}
