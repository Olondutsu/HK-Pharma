using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public GameObject dialogueBox;
    public GameObject nextButton;
    public List<string> sentences;
    private int currentIndex = 0;

    void Start()
    {
        currentIndex = 0;
        dialogueBox.SetActive(false);
    }

    public void StartDialogue(int startIndex = 0)
    {
        currentIndex = Mathf.Clamp(startIndex, 0, sentences.Count - 1); // Assurez-vous que l'index de départ est valide.
        dialogueBox.SetActive(true);
        nextButton.SetActive(true);
    }
    
    public void OnNextButtonClick(){
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (currentIndex < sentences.Count)
        {
            string sentence = sentences[currentIndex];
            dialogueText.text = sentence;
            currentIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
    }
}