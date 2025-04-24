using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public RectTransform panel; // Reference to the panel (assumes it's a UI element)
    public GameObject canvas;
    public Slider life;
    public int goal;

    void Start()
    {
        life.maxValue = goal;
        life.value = goal;

        TeleportToRandomPosition();
    }
    void OnCollisionEnter(Collision other)
    {
        // Check if the object that collided with this object has the tag "Bullet"
        if (other.gameObject.CompareTag("Bullet"))
        {
            TeleportToRandomPosition();

            life.value -= 1;

            Destroy(other.gameObject);

            if (life.value <= 0)
            {
                Destroy(canvas);
            }
        }
    }

    void TeleportToRandomPosition()
    {
        if (panel != null)
        {
            // Get the panel's dimensions
            Vector2 panelSize = panel.rect.size;

            // Generate a random position within the panel's bounds
            float randomX = Random.Range(-panelSize.x / 2, panelSize.x / 2);
            float randomY = Random.Range(-panelSize.y / 2, panelSize.y / 2);

            // Set the new position
            transform.localPosition = new Vector3(randomX, randomY, transform.localPosition.z);
        }
    }
}
