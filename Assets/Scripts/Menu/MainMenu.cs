using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {

    }

    public void ContinueGame()
    {
        SaveLoadManager.Instance.Load();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void GoBackTomenu()
    {
        var currentScene = SceneManager.GetActiveScene().name;
        TransitionManager.Instance.Transition(currentScene, "MainMenu");
        SaveLoadManager.Instance.Save();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
