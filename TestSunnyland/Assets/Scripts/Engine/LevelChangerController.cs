
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelChangerController : MonoBehaviour {

    private Animator animator;

    public int SceneToLoad;

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
        animator = GetComponent<Animator>();
        
    }


    private void setTrigger()
    {
        animator.SetTrigger("Faded");
    }


    public void LoadScene(int i) {
        setTrigger();
        SceneToLoad = i;
    }
	
    public void LoadLastScene()
    {
        setTrigger();
        SceneToLoad = SceneManager.GetActiveScene().buildIndex - 1;
        
    }

	public void LoadNextScene()
    {
        setTrigger();
        SceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        
    }

    public void LoadTheSameScene()
    {
        setTrigger();
        SceneToLoad = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("kek");
        
    }

    public void LoadMainMenuScene()
    {
        setTrigger();
        SceneToLoad = 0;
    }


    // We invoke this function in the animator when the animation is complete
    public void onFadeComplete()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}
