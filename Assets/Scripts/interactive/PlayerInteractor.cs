using UnityEngine;

public class PlayerInteractor : MonoBehaviour // Накинуть на камеру
{
    private Iinteractable currentInteractable;
    private MeshRenderer currentRenderer;
    private Material originalMaterial;
    public Material highlightMaterial; // Материал для подсветки (с outline)

    void Update()
    {
        // Проверяем, наведен ли курсор на объект
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Iinteractable interactable = hit.collider.GetComponent<Iinteractable>();
            if (interactable != null)
            {
                if (currentInteractable != interactable)
                {
                    // Убираем подсветку с предыдущего объекта
                    if (currentRenderer != null)
                    {
                        // Убираем материал подсветки
                        RemoveHighlightMaterial();
                    }

                    // Подсвечиваем новый объект
                    currentInteractable = interactable;
                    currentRenderer = hit.collider.GetComponent<MeshRenderer>();
                    originalMaterial = currentRenderer.material;
                    AddHighlightMaterial(); // Добавляем материал подсветки
                }

                // Обработка клика
                if (Input.GetMouseButtonDown(0)) // ЛКМ нажата
                {
                    interactable.OnInteract();
                }
            }
            else if (currentRenderer != null)
            {
                // Убираем материал подсветки
                RemoveHighlightMaterial();
            }

        }
        else
        {
            // Убираем подсветку, если курсор не на объекте
            if (currentRenderer != null)
            {
                RemoveHighlightMaterial(); // Убираем материал подсветки
                currentInteractable = null;
            }
        }

        // Когда ЛКМ отпущена
        if (Input.GetMouseButtonUp(0) && currentInteractable != null)
        {
            currentInteractable.OnRelease();
            currentInteractable = null;
        }
    }

    // Функция для добавления материала подсветки
    void AddHighlightMaterial()
    {
        // Создаем новый массив материалов, который включает подсветку
        Material[] materials = new Material[currentRenderer.materials.Length + 1];
        for (int i = 0; i < currentRenderer.materials.Length; i++)
        {
            materials[i] = currentRenderer.materials[i];
        }
        materials[materials.Length - 1] = highlightMaterial; // Добавляем материал подсветки
        currentRenderer.materials = materials; // Применяем новый массив материалов
    }

    // Функция для удаления материала подсветки
    void RemoveHighlightMaterial()
    {
        // Получаем текущие материалы
        Material[] materials = currentRenderer.materials;

        // Если материалов больше одного, то удаляем последний (материал подсветки)
        if (materials.Length > 1)
        {
            var newMaterials = new Material[materials.Length - 1];
            for (int i = 0; i < materials.Length - 1; i++) // Копируем все кроме последнего
            {
                newMaterials[i] = materials[i];
            }
            currentRenderer.materials = newMaterials;
        }
    }

}
