
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resume(){
        PlayerController.paused = false;
        pauseMenu.SetActive(false);
    }

    public void endlessMode(){
        SceneManager.LoadScene(1);
    }

    public void mainMenu(){
        SceneManager.LoadScene(0);
    }
}
