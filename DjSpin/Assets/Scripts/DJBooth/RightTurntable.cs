using UnityEngine;

// Click and drag rotates the right turntable and moves the wave player vertically
public class RightTurntable : MonoBehaviour
{
    [Header("Drag Settings")]
    public float dragSensitivity = 0.5f;

    private bool isDragging;
    private Vector2 lastMousePos;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsMouseOverTurntable())
            BeginDrag();

        if (Input.GetMouseButtonUp(0))
            isDragging = false;

        if (isDragging)
            HandleDrag();
    }

    void BeginDrag()
    {
        isDragging = true;
        lastMousePos = Input.mousePosition;
    }

    void HandleDrag()
    {
        Vector2 currentMouse = Input.mousePosition;
        Vector2 delta = currentMouse - lastMousePos;
        lastMousePos = currentMouse;

        // Rotate the turntable visual based on horizontal drag
        float rotationAmount = delta.x * dragSensitivity;
        transform.Rotate(0f, 0f, -rotationAmount);

        // Drive vertical player movement from vertical drag
        WavePlayer.Instance?.ApplyDragInput(delta.y * dragSensitivity);
    }

    bool IsMouseOverTurntable()
    {
        Vector2 worldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(worldPos);
        return hit != null && hit.transform == transform;
    }
}
