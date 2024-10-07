using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : Singleton<TransitionManager>,iSaveable
{
    [SceneName] public string startScene;
    private bool isFade;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;
    private bool canTransition;
    //public static TransitionManager Instance { set; get; }
    private void OnEnable()
    {
        EventHandler.GameStateChangeEvent += OnGameStateChangeEvent;
    }
    private void OnDisable()
    {
        EventHandler.GameStateChangeEvent -= OnGameStateChangeEvent;
    }
    private void OnGameStateChangeEvent(GameState gameState)
    {
        canTransition = gameState == GameState.GamePLay;
    }
    public void Transition(string from,string to)
    {
        if(!isFade && canTransition)
            StartCoroutine(TransitionToScene(from, to));
    }
    private IEnumerator TransitionToScene(string from,string to)
    {
        yield return Fade(1);
        if (from != string.Empty)
        {
            EventHandler.CallBeforeSceneUnloadEvent();
            yield return SceneManager.UnloadSceneAsync(from);
        }
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);
        EventHandler.CallAfterSceneUnloadEvent();
        yield return Fade(0);
    }
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;
        fadeCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;
        while(!Mathf.Approximately(fadeCanvasGroup.alpha,targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }
        fadeCanvasGroup.blocksRaycasts = false;
        isFade = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(TransitionToScene(string.Empty, startScene));
        iSaveable saveable = this;
        saveable.SaveableRegister();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.currentScene = SceneManager.GetActiveScene().name;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        Transition("MainMenu", saveData.currentScene);
    }
}
