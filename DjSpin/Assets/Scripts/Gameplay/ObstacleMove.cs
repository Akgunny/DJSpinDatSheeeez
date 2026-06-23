using UnityEngine;

// Attach to each obstacle prefab — scrolls left at current game speed then self-destructs
public class ObstacleMove : MonoBehaviour
{
    public float destroyX = -12f;

    void Update()
    {
        float speed = ScrollSpeedController.Instance != null
            ? ScrollSpeedController.Instance.CurrentSpeed
            : 3f;

        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < destroyX)
            Destroy(gameObject);
    }
}
