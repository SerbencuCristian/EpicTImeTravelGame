using UnityEngine;

public class Laser : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Enemies enemy = other.GetComponent<Enemies>();
        if (enemy)
        {
            Vector2 collisionPoint = other.ClosestPoint(transform.position);
            Vector2 knockbackDirection = (enemy.transform.position - (Vector3)collisionPoint).normalized;
            enemy.Knockback(knockbackDirection, 5f);
            enemy.TakeDamage(1);
        }
    }
    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
