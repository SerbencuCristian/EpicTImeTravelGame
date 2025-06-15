using UnityEngine;

public class BeamDot : MonoBehaviour
{
    void CallShootBeam()
    {
        PlayerShoot playerShoot = FindObjectOfType<PlayerShoot>();
        if (playerShoot != null)
        {
            playerShoot.ShootBeam(gameObject);
            GetComponent<Animator>().speed = 0f;
        }
        Destroy(gameObject, 1f);
    }
}
