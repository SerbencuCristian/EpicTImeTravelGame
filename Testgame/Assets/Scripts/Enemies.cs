using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class Enemies : MonoBehaviour
{
    public int currentHealth = 5;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider2D;
    private bool isDead = false;
    private int maxHealth = 10;
    private Vector2 originalPosition;
    public Transform player;
    public GameObject LoadCanvas;
    public float Flyspeed = 7.5f;
    public LayerMask disabledLayer;
    private Rigidbody2D rb;
    public bool move = true;
    public bool isHurting = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").transform;
        LoadCanvas = GameObject.Find("LoadCanvas");
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
        rb.gravityScale = 0f; // Disable gravity
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!LoadCanvas.GetComponent<LoadScript>().isHolding && move && !isDead)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= 10f)
            {
                Vector2 directionToPlayer = ((Vector2)player.position - (Vector2)transform.position).normalized; //get direction to player as a vector
                rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, directionToPlayer * Flyspeed, Time.deltaTime * 5f);//move to player
            }
            else if (Vector2.Distance(transform.position, originalPosition) > 0.1f) //move back to og location, stop moving if too close (prevent jittering)
            {
                Vector2 directionToOriginal = (originalPosition - (Vector2)transform.position).normalized;
                rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, directionToOriginal * Flyspeed, Time.deltaTime * 5f);
            }
            else
            {
                rb.linearVelocity = Vector2.zero; // Stop moving
            }
        }
        else if (LoadCanvas.GetComponent<LoadScript>().isHolding && move)
        {
            rb.linearVelocity = Vector2.zero; // Stop moving when holding for time travel
        }
    }
    public void MoveToOriginalPosition() //move back home on death or reset
    {
        transform.position = originalPosition;
        currentHealth = maxHealth;
        if (isDead) //revive if dead
        {
            isDead = false;
            spriteRenderer.enabled = true;
            circleCollider2D.enabled = true;
        } 
    }
    public void Knockback(Vector2 direction, float force)
    {
        StartCoroutine(disablemovement());
        rb.linearVelocity = Vector2.zero;
        rb.linearVelocity = direction.normalized * force;//idk if its right, feels good tho
    }
    private IEnumerator disablemovement() //stop it moving for a second after being hit
    {
        move = false;
        yield return new WaitForSeconds(1f);
        move = true;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (!isHurting)
            StartCoroutine(Iframes()); // Start the invincibility frames coroutine
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private IEnumerator Iframes()
    {
        isHurting = true; // Set the isHurting flag to true
        Color originalColor = spriteRenderer.color; // Store the original color
        spriteRenderer.color = Color.red; // Set color to white
        yield return new WaitForSeconds(0.2f); // Wait for a short duration
        spriteRenderer.color = originalColor; // Revert to the original color
        isHurting = false; // Reset the isHurting flag
    }
    private void Die()
    {
        spriteRenderer.enabled = false;
        circleCollider2D.enabled = false;
        isDead = true;
    }
}
