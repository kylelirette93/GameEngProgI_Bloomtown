using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Delegate to see if dialogue has ended.
// Prevents interruption of dialogue until it finishes.
public delegate void DialogueEnded();

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public string[] DialogueLines;
    public TextMeshProUGUI dialogueText;
    public Queue<string> dialogue;

    public static event DialogueEnded OnDialogueEnded;

    private Coroutine displayCoroutine; 
    private string currentSentence; 

    private void Start()
    {
        // Initialize empty queue.
        dialogue = new Queue<string>();
    }

    public void StartDialogue(string[] sentences)
    {
        // Clear the queue and add the new sentences.
        dialogue.Clear();
        dialoguePanel.SetActive(true);
        GameManager.Instance.playerController.SetCanMove(false);
        GameManager.Instance.GameStateManager.EnableCursor();
        foreach (string currentString in sentences)
        {
            // Add each sentence to the queue.
            dialogue.Enqueue(currentString);
        }
        // Display the first sentence.
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // If a coroutine is running, stop it and display the full sentence.
        if (displayCoroutine != null) 
        {
            StopCoroutine(displayCoroutine);
            // Display the full sentence.
            dialogueText.text = currentSentence; 
            // Clear the reference so a new coroutine can be started.
            displayCoroutine = null; 
            return;
        }

        if (dialogue.Count == 0)
        {
            EndSentence();
            return;
        }

        // Clear the dialogue text at the start of each sentence.
        dialogueText.text = "";
        // Store the current sentence, so it can be displayed in full.
        currentSentence = dialogue.Dequeue(); 
        // Store the coroutine so we can stop it abruptly.
        displayCoroutine = StartCoroutine(DisplayDialogueCoroutine(currentSentence));
    }

    public void EndSentence()
    {
        // When dialogue finishes, clear it, disable panel and reactivate player movement and cursor.
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
        GameManager.Instance.playerController.SetCanMove(true);
        GameManager.Instance.GameStateManager.DisableCursor();

        if (OnDialogueEnded != null)
        {
            OnDialogueEnded();
        }
    }

    private IEnumerator DisplayDialogueCoroutine(string currentSentence)
    {
        for (int i = 0; i < currentSentence.Length; i++)
        {
            if (GameManager.Instance != null && GameManager.Instance.SoundManager != null)
            {
                GameManager.Instance.SoundManager.PlaySoundByName("typewriter");
            }
            // While coroutine is running, display the sentence letter by letter.
            dialogueText.text = currentSentence.Substring(0, i + 1); 
            yield return new WaitForSecondsRealtime(0.05f);
        }
        yield return new WaitForSecondsRealtime(1f);
        displayCoroutine = null; 
    }
}