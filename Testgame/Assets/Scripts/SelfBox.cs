using UnityEngine;

public class SelfBox : MonoBehaviour
{
    public Vector2 originalPosition;
    public GameObject player;
    public Animator animator;
    public Vector2 currentPosition;
    public LayerMask groundLayer;
    private bool isChecking = false;
    private bool isPushed = false;
    private Rigidbody2D rb;
    void Awake()
    {
        originalPosition = transform.position;
    }
    void Start()
    {
        player = GameObject.Find("Player");
        animator = player.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Rigidbody2D>().linearVelocity.x != 0 && GetComponent<Rigidbody2D>().isKinematic == false)
        {
            isPushed = true;
        }
        currentPosition = transform.position;
        if (GetComponent<Collider2D>().IsTouching(player.GetComponent<Collider2D>()) && isPushed && GetComponent<Rigidbody2D>().linearVelocity.x != 0)
        {
            animator.SetBool("isPushing", true);
        }
        else if(isPushed)
        {
            isPushed = false;
            animator.SetBool("isPushing", false);
        }
        
    }
    public void MoveToOriginalPosition()
    {
        transform.position = originalPosition;
    }
    public void OnTriggerEnter2D(Collider2D other) //checker for ground collision, and move left or right to find a suitable spot
    {
        if ((other.CompareTag("Ground Present") || other.CompareTag("Platform Present") || other.CompareTag("Ground Past")|| other.CompareTag("Platform Past")|| other.CompareTag("Ground Future")|| other.CompareTag("Platform Future") ) && isChecking == false)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            isChecking = true;
            if (Physics2D.OverlapBox(transform.position, GetComponent<BoxCollider2D>().bounds.size, 0, groundLayer))
            {
                int LR = 0;
                Vector2 newPositionL = transform.position;
                Vector2 newPositionR = transform.position;
                while (Physics2D.OverlapBox(newPositionL, GetComponent<BoxCollider2D>().bounds.size, 0, groundLayer) && Physics2D.OverlapBox(newPositionR, GetComponent<BoxCollider2D>().bounds.size, 0, groundLayer))
                {
                    newPositionL.x -= 0.1f; // Move left incrementally
                    if (!Physics2D.OverlapBox(newPositionL, GetComponent<BoxCollider2D>().bounds.size, 0, groundLayer) || transform.position.x - newPositionL.x > 1.5f)
                    {   
                        LR = 1; // Move left
                        break;
                    }
                    newPositionR.x += 0.1f; // Move right incrementally
                    if (!Physics2D.OverlapBox(newPositionR, GetComponent<BoxCollider2D>().bounds.size, 0, groundLayer)||  newPositionR.x -  transform.position.x > 1.5f)
                    {   
                        LR = 2; // Move right
                        break;
                    }
                }
                if (LR == 1)
                {
                    transform.position = newPositionL;
                }
                else if (LR == 2)
                {
                    transform.position = newPositionR;
                }
            }
            GetComponent<SpriteRenderer>().enabled = true;
            isChecking = false;
        }
    }
}
