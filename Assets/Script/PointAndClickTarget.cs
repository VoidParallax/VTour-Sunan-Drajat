using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PointAndClickTarget : MonoBehaviour
{
    [TextArea]
    [Tooltip("The description to show when the player looks at this object.")]
    public string textDescription;

    public List<Sprite> imageList;
}