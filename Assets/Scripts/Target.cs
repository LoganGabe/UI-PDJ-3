using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public RectTransform panel;
    public GameObject canvas;
    public Slider life;
    public int goal;
    public AudioSource audioSource;
    public AudioClip hitSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        life.maxValue = goal;
        life.value = goal;

        TeleportToRandomPosition();
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            audioSource.PlayOneShot(hitSound);

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
