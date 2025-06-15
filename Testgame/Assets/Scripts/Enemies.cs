using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class Enemies : MonoBehaviour
{
    public int currentHealth = 5;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider2D;
    private bool isDead = false;
    private int maxHealth = 3;
    private Vector2 originalPosition;
    public Transform player;
    public GameObject LoadCanvas;
    public float Flyspeed = 7.5f;
    public LayerMask disabledLayer;
    private Rigidbody2D rb;
    public bool move = true;
    public bool isHurting = false;
    public Animator animator;
    public bool isFacingRight = true; // Track the facing direction
    public bool isAttacking = false; // Track if the enemy is attacking
                                     // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float attackcooldown = 0f;
    public CircleCollider2D attackCollider; // Collider for the attack area
    void Start()
    {
        player = GameObject.Find("Player").transform;
        LoadCanvas = GameObject.Find("LoadCanvas");
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
        rb.gravityScale = 0f; // Disable gravity
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        attackCollider = transform.GetChild(0).gameObject.GetComponent<CircleCollider2D>();
        attackCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead && attackcooldown > 0)
        {
            attackcooldown -= Time.deltaTime; // Decrease cooldown timer
        }
        if (move && !isDead && !isAttacking)
            Flip();
        if (move && !isDead && !isAttacking)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= 4f && attackcooldown <= 0f)
            {
                attackcooldown = 5f;
                AttackPlayer();
            }
            else if (distanceToPlayer <= 10f)
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
    private void Flip() //flip sprite when turning around
    {
        if (rb.linearVelocity.x > 0 && !isFacingRight)
        {
            isFacingRight = true;
            spriteRenderer.flipX = false; // Flip the sprite to face right
        }
        else if (rb.linearVelocity.x < 0 && isFacingRight)
        {
            isFacingRight = false;
            spriteRenderer.flipX = true; // Flip the sprite to face left
        }
    }
    public void MoveToOriginalPosition() //move back home on death or reset
    {
        transform.position = originalPosition;
        currentHealth = maxHealth;
        if (isDead)
        {
            isDead = false;
            animator.SetBool("Dead", isDead);
            spriteRenderer.enabled = true;
            circleCollider2D.enabled = true;
            isAttacking = false;
            isHurting = false;
            isFacingRight = true;

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
        rb.linearVelocity = Vector2.zero;
        move = true;
    }
    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            if (!isHurting)
                StartCoroutine(Iframes()); // Start the invincibility frames coroutine
            if (currentHealth <= 0)
            {
                StartCoroutine(Die());
            }
        }
    }
    public void OnAttackHit()
    {
        attackCollider.enabled = true;
        Vector2 directionToPlayer = ((Vector2)player.position - (Vector2)transform.position).normalized;
        rb.linearVelocity = directionToPlayer * 10f;
        Flip();
    }
    public void OnAttackExit()
    {
        attackCollider.enabled = false; // Disable the attack collider after the attack
        rb.linearVelocity = Vector2.zero; // Stop moving after the attack
        isAttacking = false; // Reset the attacking state
        move = true;
    }
    private void AttackPlayer()
    {
        isAttacking = true;
        move = false;
        rb.linearVelocity = Vector2.zero;
        animator.SetTrigger("Attack");

        SoundManager.Instance.PlayEnemyScream(); // Play enemy scream sound


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
    private IEnumerator Die()
    {
        if(!isDead){
        isDead = true;
        animator.SetBool("Dead",isDead);
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(1f); 
        spriteRenderer.enabled = false;
        circleCollider2D.enabled = false;
    }
    }
}
