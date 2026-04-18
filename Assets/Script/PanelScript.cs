using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{
    public GameObject ImageSide;

    public void SetImage(List<Sprite> imageList)
    {
        //create a new image for each sprite in the list and set it as the ImageSide children
        // Clear any existing images first
        foreach (Transform child in ImageSide.transform)
        {
            Destroy(child.gameObject);
        }

        // Create a new image for each sprite in the list and set it as the ImageSide children
        foreach (Sprite sprite in imageList)
        {
            GameObject imgObject = new("Image_Item");
            imgObject.transform.SetParent(ImageSide.transform, false);
            Image img = imgObject.AddComponent<Image>();
            img.sprite = sprite;
            img.preserveAspect = true;
        }
    }

    public void SetText(string text)
    {
        GetComponentInChildren<TMP_Text>().text = text;
    }
}
