using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointAndClick : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float lookSpeed = 2f;

    public GameObject PanelPrefab;
    // private bool isClicked = false;

    [Header("Raycast Settings")]
    public Transform playerCamera;
    public float interactDistance = 5f;
    public LayerMask interactLayer; // Pick your layer in the Inspector

    private float rotationX = 0f;

    void Start()
    {
        // Lock and hide the cursor for first-person control
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerCamera = Camera.main.transform;
    }

    // void Update()
    // {
    //     CheckObjectInFront();
    // }

    void HandleMovement()
    {
        // Use GetAxisRaw for snappier input without smoothing
        float moveForward = Input.GetAxisRaw("Vertical");
        float moveSide = Input.GetAxisRaw("Horizontal");
        
        // Normalize the movement vector so moving diagonally isn't faster
        Vector3 moveDirection = new Vector3(moveSide, 0, moveForward).normalized;
        transform.Translate(Time.deltaTime * walkSpeed * moveDirection);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        // Rotate the camera up and down (clamped to prevent flipping)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        // Rotate the player body left and right
        transform.Rotate(Vector3.up * mouseX);
    }

    void CheckObjectInFront()
    {
        // if (Input.GetMouseButtonDown(0) && isClicked)
        // {
        //     PanelPrefab.SetActive(false);
        //     isClicked = false;
        // }
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, interactDistance, interactLayer))
        {
            if (hit.collider.TryGetComponent<PointAndClickTarget>(out var target))
            {
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
                {
                    // Pass the target we hit to the interact function
                    TriggerInteraction(target);
                
                }
            }
        }
    }

    public void TriggerInteraction(PointAndClickTarget target)
    {
        PanelPrefab.transform.position = target.transform.position + Vector3.up * 1.5f;
        PanelPrefab.transform.LookAt(playerCamera);
        PanelPrefab.transform.Rotate(0, 180, 0); // Flip so text faces player correctly
        TMP_Text descriptionText = PanelPrefab.GetComponentInChildren<TMP_Text>();
        if (descriptionText != null)
        {
            descriptionText.text = target.textDescription;
        }
        PanelPrefab.SetActive(true);
    }

    void OnGUI()
    {
        // Draws a basic crosshair in the exact center of the screen
        float size = 20f;
        float posX = (Screen.width - size) / 2f;
        float posY = (Screen.height - size) / 2f;
        
        // Simple GUI styling for the crosshair
        GUIStyle style = new();
        style.normal.textColor = Color.white;
        style.fontSize = 20;
        style.alignment = TextAnchor.MiddleCenter;

        GUI.Label(new Rect(posX, posY, size, size), "+", style);
    }
}
