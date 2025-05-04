using UnityEngine;

public class Laser : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Enemies enemy = other.GetComponent<Enemies>();
        if (enemy)
        {
            enemy.Knockback((enemy.transform.position - transform.position).normalized, 3f);
            enemy.TakeDamage(1);        
            Destroy(gameObject);
        }
    }
}
