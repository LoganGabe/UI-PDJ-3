using UnityEngine;

public class CameraAim : MonoBehaviour
{
    public Transform spawnPoint; // The point where the bullet will spawn
    public GameObject bulletPrefab; // The bullet prefab to instantiate
    public float bulletSpeed = 20f; // Speed of the bullet

    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate the bullet at the spawnPoint's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);

        // Add velocity to the bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = spawnPoint.forward * bulletSpeed;
            Destroy(bullet, 6f); // Destroy the bullet after 2 seconds to avoid cluttering the scene
        }
    }
}