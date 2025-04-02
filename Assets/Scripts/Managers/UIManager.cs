using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    public GameObject gameplayPanel;
    public GameObject pausePanel;
    public GameObject questPanel;
    public TextMeshProUGUI interactionText;
    public TextMeshProUGUI interactionPrompt;
    public Inventory inventory;

    public void EnableMainMenuUI()
    {
        DisableAllMenuUI();
        mainMenuPanel.SetActive(true);
    }

    public void EnableGameplayMenuUI()
    {
        DisableAllMenuUI();
        gameplayPanel.SetActive(true);
    }

    public void EnablePauseMenuUI()
    {
        DisableAllMenuUI();
        pausePanel.SetActive(true);
    }

    public void EnableOptionsUI()
    {
        DisableAllMenuUI();
        optionsPanel.SetActive(true);
    }

    public void EnableQuestUI()
    {
        DisableAllMenuUI();
        questPanel.SetActive(true);
    }

    public void DisableAllMenuUI()
    {
        mainMenuPanel.SetActive(false);
        gameplayPanel.SetActive(false);
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        questPanel.SetActive(false);
    }

    public void SetInteractionText(string text)
    {
        if (text != null)
        {
            // Debug.Log("SetInteractionText called with message: " + text);
            interactionText.text = text;
            StartCoroutine(FadeText(text));
        }
    }

    IEnumerator FadeText(string text)
    {
        interactionText.text = text;
        interactionText.color = new Color(interactionText.color.r, interactionText.color.g, interactionText.color.b, 0);

        // Fade in.
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime * 2;
            interactionText.color = new Color(interactionText.color.r, interactionText.color.g, interactionText.color.b, alpha);
            yield return null;
        }

        // Display for 2 seconds.
        yield return new WaitForSeconds(2);

        // Fade out.
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * 2;
            interactionText.color = new Color(interactionText.color.r, interactionText.color.g, interactionText.color.b, alpha);
            yield return null;
        }
        interactionText.color = new Color(interactionText.color.r, interactionText.color.g, interactionText.color.b, 0);
    }
}