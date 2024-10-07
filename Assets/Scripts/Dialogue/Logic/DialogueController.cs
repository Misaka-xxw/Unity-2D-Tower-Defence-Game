using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public DialogueData_SO dialogueEmpty;
    public DialogueData_SO dialogueFinish;
    private bool isTalking;
    private Stack<string> dialogueEmptyStack;
    private Stack<string> dialogueFinishStack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        FillDialogueStack();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            teleport.isEnter = true;
            Debug.Log(teleport.isEnter);
        }
    }
    private void FillDialogueStack()
    {
        dialogueEmptyStack = new Stack<string>();
        dialogueFinishStack = new Stack<string>();
        for(int i=dialogueEmpty.dialogueList.Count - 1; i > -1; i--)
        {
            dialogueEmptyStack.Push(dialogueEmpty.dialogueList[i]);
        }
        for(int i=dialogueFinish.dialogueList.Count-1;i > -1;i--)
        {
            dialogueFinishStack.Push(dialogueFinish.dialogueList[i]);
        }
    }
    public void ShowDialogueEmpty()
    {
        if(!isTalking)
        {
            StartCoroutine(DialogueRoutine(dialogueEmptyStack));
        }
    }
    public void ShowDialogueFinish() 
    {
        if(!isTalking)
        {
            StartCoroutine(DialogueRoutine(dialogueFinishStack));
        }
    }
    private IEnumerator DialogueRoutine(Stack<string> data)
    {
        isTalking = true;
        Time.timeScale = 0;
        if(data.TryPop(out string result))
        {
            EventHandler.CallShowDialogueEvent(result);
            yield return null;
            isTalking = false;
            Time.timeScale = 1f;
            EventHandler.CallGameStateChangeEvent(GameState.Pause);
        }
        else
        {
            EventHandler.CallShowDialogueEvent(string.Empty);
            FillDialogueStack();
            isTalking = false;
            Time.timeScale = 1f;
            EventHandler.CallGameStateChangeEvent(GameState.GamePLay);
        }
    }
}
