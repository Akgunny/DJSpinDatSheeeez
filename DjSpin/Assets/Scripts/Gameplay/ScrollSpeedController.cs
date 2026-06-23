using UnityEngine;

public class ScrollSpeedController : MonoBehaviour
{
    public static ScrollSpeedController Instance { get; private set; }

    [Header("Speed Settings")]
    public float baseSpeed = 3f;
    public float maxSpeed = 10f;
    public float minSpeed = 0.5f;
    public float acceleration = 4f;

    public float CurrentSpeed { get; private set; }

    private float spinInput;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        CurrentSpeed = baseSpeed;
    }

    public void SetSpinInput(float input)
    {
        spinInput = input;
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;

        float target = spinInput > 0f ? maxSpeed : spinInput < 0f ? minSpeed : baseSpeed;
        CurrentSpeed = Mathf.MoveTowards(CurrentSpeed, target, acceleration * Time.deltaTime);
    }
}
