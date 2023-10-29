using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public ContainerBehavior containerBehavior;

    public void OnRandomClick(){
        if (containerBehavior.iceAdded)
        {
        dialogueManager.StartDialogue(2);
        }
    }
}