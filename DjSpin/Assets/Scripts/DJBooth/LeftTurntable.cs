using UnityEngine;

// A/D keys rotate the left turntable and set scroll speed via ScrollSpeedController
public class LeftTurntable : MonoBehaviour
{
    [Header("Input")]
    public KeyCode spinLeft = KeyCode.A;
    public KeyCode spinRight = KeyCode.D;

    [Header("Rotation")]
    public float rotationSpeed = 180f;

    void Update()
    {
        float input = 0f;
        if (Input.GetKey(spinLeft)) input = -1f;
        if (Input.GetKey(spinRight)) input = 1f;

        if (input != 0f)
            transform.Rotate(0f, 0f, -input * rotationSpeed * Time.deltaTime);

        ScrollSpeedController.Instance?.SetSpinInput(input);
    }
}
