using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;  // Geschwindigkeit der Kamera-Bewegung
    public float baseDragSpeed = 5f;  // Grundgeschwindigkeit des Mausziehens
    public float zoomSpeed = 10f;  // Geschwindigkeit des Zooms
    public float minZoom = 2f;    // Minimale Zoom-Ebene
    public float maxZoom = 10f;   // Maximale Zoom-Ebene

    private float currentDragSpeed; // Aktuelle Drag-Geschwindigkeit

    private void Start()
    {
        currentDragSpeed = baseDragSpeed;
    }

    private void Update()
    {
        HandleKeyboardInput();
        HandleMouseInput();
        HandleZoomInput();
    }

    void HandleKeyboardInput()
    {
        // Tastatureingabe für die Kamerabewegung
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, vertical, 0f).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    void HandleMouseInput()
    {
        // Mausziehen für die Kamerabewegung
        if (Input.GetMouseButton(0)) // 0 entspricht der linken Maustaste
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Vector3 dragDirection = new Vector3(-mouseX, -mouseY, 0f).normalized;
            transform.Translate(dragDirection * currentDragSpeed * Time.deltaTime);
        }
    }

    void HandleZoomInput()
    {
        // Mausrad-Zoom
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        float newZoom = Camera.main.orthographicSize - zoomInput * zoomSpeed * Time.deltaTime;
        Camera.main.orthographicSize = Mathf.Clamp(newZoom, minZoom, maxZoom);

        // Pass die Drag-Geschwindigkeit basierend auf der aktuellen Zoom-Stufe an
        currentDragSpeed = baseDragSpeed * (Camera.main.orthographicSize / minZoom);
    }
}
