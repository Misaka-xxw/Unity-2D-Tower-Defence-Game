using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueController))]
public class Character1 : Interactive
{
    private DialogueController dialogueController;
    private void Awake()
    {
        dialogueController = GetComponent<DialogueController>();
    }
    public override void EmptyClicked()
    {
        if(isDone)
        {
            dialogueController.ShowDialogueEmpty();
        }
        else
            dialogueController.ShowDialogueEmpty();
    }
    protected override void OnCLickedAction()
    {
        dialogueController.ShowDialogueFinish();
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
