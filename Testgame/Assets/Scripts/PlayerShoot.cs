using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 30f;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale == 1) // Check if the fire button is pressed
        {
            Shoot();
        }
    }
    void Shoot()
    {
        //all the weapons, could add more, not right now
        if (GetComponent<PlayerHealth>().currentEnergy < 1) return; // Check if the player has enough energy to shoot
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //get mouse position
        Vector3 shootDirection = (mousePosition - transform.position).normalized;
        // LASER
        GameObject projectile = Instantiate(projectilePrefab,new Vector2 (transform.position.x,transform.position.y+0.55f), Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(shootDirection.x, shootDirection.y) * projectileSpeed;
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
        Destroy(projectile, 2f);
        GetComponent<PlayerHealth>().currentEnergy -= 1; // Decrease energy by 1 after shooting
        //LIGHTNING BOLT
    }
}
