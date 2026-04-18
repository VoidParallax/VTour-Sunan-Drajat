using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PanelScript : MonoBehaviour
{
    public GameObject ContentReference;
    public GameObject ModalPrefab;

    public void SetModal(List<ModalData> data)
    {
        // Clear existing modals to prevent duplication
        foreach (Transform child in ContentReference.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (ModalData dataItem in data)
        {
            // Create a new instance of the modal prefab and parent it to the content holder
            GameObject modalInstance = Instantiate(ModalPrefab, ContentReference.transform);

            // Find the Image and Text components within the new instance's children
            Image imageComponent = modalInstance.GetComponentInChildren<Image>();
            TMP_Text textComponent = modalInstance.GetComponentInChildren<TMP_Text>();

            // Set the sprite and text from the ModalData
            if (imageComponent != null) imageComponent.sprite = dataItem.sprite;
            if (textComponent != null) textComponent.text = dataItem.description;
        }
    }
}
