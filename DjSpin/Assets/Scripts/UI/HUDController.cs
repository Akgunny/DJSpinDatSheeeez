using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    [Header("Lives")]
    public GameObject[] lifeIcons;

    [Header("Boo VFX")]
    public GameObject booTextPrefab;
    public Transform booSpawnPoint;

    void OnEnable()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.onLifeLost.AddListener(OnLifeLost);
        GameManager.Instance.onGameOver.AddListener(OnGameOver);
    }

    void OnDisable()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.onLifeLost.RemoveListener(OnLifeLost);
        GameManager.Instance.onGameOver.RemoveListener(OnGameOver);
    }

    void OnLifeLost()
    {
        int lives = GameManager.Instance.Lives;
        for (int i = 0; i < lifeIcons.Length; i++)
            lifeIcons[i].SetActive(i < lives);

        if (booTextPrefab != null && booSpawnPoint != null)
        {
            GameObject boo = Instantiate(booTextPrefab, booSpawnPoint.position, Quaternion.identity, booSpawnPoint);
            Destroy(boo, 2f);
        }
    }

    void OnGameOver()
    {
        Debug.Log("Game Over!");
        // Hook up your game over screen here
    }
}
