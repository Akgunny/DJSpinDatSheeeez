using UnityEngine;

public class WavePlayer : MonoBehaviour
{
    public static WavePlayer Instance { get; private set; }

    [Header("Movement")]
    public float verticalSpeed = 5f;
    public float yMin = -4f;
    public float yMax = 4f;

    private float pendingVerticalInput;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;

        float move = pendingVerticalInput * verticalSpeed * Time.deltaTime;
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y + move, yMin, yMax);
        transform.position = pos;

        pendingVerticalInput = 0f;
    }

    public void ApplyDragInput(float amount)
    {
        pendingVerticalInput += amount;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
            GameManager.Instance?.LoseLife();
    }
}
