using UnityEngine;
using UnityEngine.InputSystem;
public class playerMovement : MonoBehaviour
{
    public Animator animator;
    public GameObject LoadCanvas;
    public ParticleSystem dust;
    public float platformdistance = 50f;
    bool isFacingRight = false;
    public Rigidbody2D rb;
    public float movespeed = 5f;

    float horizontalMovement;
    public float jumpForce = 10f;

    public Transform groundCheck;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;
    public int maxJumps = 2;
    public int jumpCount = 0;

    public float basicGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallMultiplier = 2f;

    public float cooldown = 0;
    public bool ok = false;
    public float currentEnergy;
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        currentEnergy = GetComponent<PlayerHealth>().currentEnergy;
        if(currentEnergy <= 0) //no energy = no jumping
        {
            maxJumps = 0;
        }
        else
        {
            maxJumps = 2;
        }
        rb.linearVelocity = new Vector2(horizontalMovement * movespeed, rb.linearVelocity.y);
        GroundCheck();
        Gravity();
        Flip();
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetFloat("magnitude", rb.linearVelocity.magnitude);
    }
    private void Gravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = basicGravity * fallMultiplier; // Increase gravity when falling
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed)); // Limit fall speed
        }
        else
        {
            rb.gravityScale = basicGravity;
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x; //moves faster the harder you press the button
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpCount < maxJumps && currentEnergy >= 5 && LoadCanvas.GetComponent<LoadScript>().HoldingTS == false && Time.timeScale == 1)
        {
            if (context.performed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpCount++;
                animator.SetTrigger("jump");
                GetComponent<PlayerHealth>().currentEnergy -= 5; //energy cost of jump
                dust.Play(); //create dust particle effect

            }
            else if (context.canceled)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
                jumpCount++;
                animator.SetTrigger("jump");
                dust.Play(); //create dust particle effect
            }
        }
    }

    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer)) //check collision with ground
        {
            jumpCount = 0;
        }
    }
    private void Flip() //flip sprite when turning around
    {
        if(isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0 && Time.timeScale == 1)
        {
            isFacingRight = !isFacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
    private void OnDrawGizmosSelected() //draw box to visualize ground check
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }
    public void Knockback(Vector2 direction, float force)
    {
        rb.AddForce(direction.normalized * force, ForceMode2D.Impulse); //impulse for now, idk if right
    }
}
