using UnityEngine;

public class PlayerInteractor : MonoBehaviour // Накинуть на камеру
{
    private Iinteractable currentInteractable;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ЛКМ нажата
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Iinteractable interactable = hit.collider.GetComponent<Iinteractable>();
                if (interactable != null)
                {
                    currentInteractable = interactable;
                    interactable.OnInteract();
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && currentInteractable != null) // ЛКМ отпущена
        {
            currentInteractable.OnRelease();
            currentInteractable = null;
        }
    }
}