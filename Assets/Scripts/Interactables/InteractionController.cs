using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public IInteractable interactable = null;
    public GameObject interactPrompt;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InteractObj"))
        {
            interactable = other.GetComponent<IInteractable>();

            // Place the interact prompt above the interactable object.
            interactPrompt.SetActive(true);
            interactPrompt.transform.position = other.transform.position + new Vector3(0, 0.5f, 0);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && interactable != null)
        {
            interactPrompt.SetActive(false);
            interactable.Interact();                 
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InteractObj"))
        {
            interactPrompt.SetActive(false);
            interactable = null;
        }
    }
}
