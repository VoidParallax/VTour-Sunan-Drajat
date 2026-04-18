using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ModalData
{
    public string description;
    public Sprite sprite;
}
public class PointAndClickTarget : MonoBehaviour
{
    public List<ModalData> Data;
}