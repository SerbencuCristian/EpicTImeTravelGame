using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
public class PlayerShoot : MonoBehaviour
{
    public GameObject beamPrefab;
    public GameObject beamDotPrefab;
    public GameObject player;
    public Animator animator;
    public float projectileSpeed = 0.5f;
    public float shootCooldown = 0.5f; // Cooldown duration in seconds
    private float lastShootTime = -0.5f; // Tracks the time of the last shot
    private PlayerControls controls;
    void Awake()
    {
        controls = KeybindManager.Instance.controls;
        player = GameObject.Find("Player");
        animator = player.GetComponent<Animator>();
    }
    void OnEnable()
    {
        controls.Player.Shoot.performed += OnShoot; // Subscribe to the Shoot action
        controls.Enable();
    }
    void OnDisable()
    {
        controls.Player.Shoot.performed -= OnShoot; // Unsubscribe from the Shoot action
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed && Time.timeScale == 1 && Time.time >= lastShootTime + shootCooldown) // Check cooldown
        {
            animator.SetTrigger("Shoot");
            Shoot();
            lastShootTime = Time.time;
        }
    }
    public void Shoot()
    {
        GetComponent<PlayerHealth>().currentEnergy -= 1;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = player.transform.position;
        Vector2 direction = (mousePosition - playerPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject beamDot = Instantiate(beamDotPrefab, playerPosition, Quaternion.AngleAxis(angle, Vector3.forward));
        shootCooldown = 0.75f;
        SoundManager.Instance.PlayLazerShoot();
    }
    public void ShootBeam(GameObject beamDot)
    {
        GameObject beam = Instantiate(beamPrefab, beamDot.transform.position, beamDot.transform.rotation);
        beam.transform.localScale = new Vector3(0f, beam.transform.localScale.y, beam.transform.localScale.z);

    }
}
