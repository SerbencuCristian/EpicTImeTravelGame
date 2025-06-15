using UnityEngine;

public class Explosion : MonoBehaviour
{
    void Start()
    {
    transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));    
    GetComponent<Animator>().SetTrigger("Explode");   
    }
    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
