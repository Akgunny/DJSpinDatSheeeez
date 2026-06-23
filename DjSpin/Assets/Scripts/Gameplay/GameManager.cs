using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int maxLives = 3;
    public int Lives { get; private set; }
    public bool IsGameOver { get; private set; }

    public UnityEvent onLifeLost;
    public UnityEvent onGameOver;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        Lives = maxLives;
        IsGameOver = false;
    }

    public void LoseLife()
    {
        if (IsGameOver) return;

        Lives--;
        onLifeLost.Invoke();

        if (Lives <= 0)
        {
            IsGameOver = true;
            onGameOver.Invoke();
        }
    }
}
