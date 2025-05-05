using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject player;
    public Animator animator;
    public float projectileSpeed = 0.5f;
    public float shootCooldown = 0.5f; // Cooldown duration in seconds
    private float lastShootTime = -0.5f; // Tracks the time of the last shot
    void Awake()
    {
        player = GameObject.Find("Player");
        animator = player.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale == 1 && Time.time >= lastShootTime + shootCooldown) // Check cooldown
        {
            animator.SetTrigger("Shoot");
            StartCoroutine(Shoot());
            lastShootTime = Time.time; // Update the last shoot time
        }
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.25f);
        GetComponent<PlayerHealth>().currentEnergy -= 1; // Decrease energy by 1 after shooting
        // SHOTGUN BLAST
        int numberOfProjectiles = 5;
        float spreadAngle = 30f; // Total spread angle in degrees
        bool isFacingRight = transform.localScale.x < 0; // Check the direction the player is facing
        Vector3 baseDirection = isFacingRight ? Vector3.right : Vector3.left; // Default direction based on facing

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float randomAngle = Random.Range(-spreadAngle / 2, spreadAngle / 2);
            Vector3 shootDirection = Quaternion.Euler(0, 0, randomAngle) * baseDirection;

            GameObject projectile = Instantiate(projectilePrefab, new Vector2(transform.position.x, transform.position.y + 0.1f), Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().linearVelocity = shootDirection * projectileSpeed * 3;

            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(0, 0, angle);

            Destroy(projectile, 0.15f);
        }
        // Apply knockback to the player in the opposite direction of the shot
        Vector2 knockbackDirection = -baseDirection.normalized; // Opposite of the shooting direction
        player.GetComponent<playerMovement>().Knockback(knockbackDirection, 5f); // Call the knockback function
    }
}
