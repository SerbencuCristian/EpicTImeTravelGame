using UnityEngine;

public class Boxes : MonoBehaviour
{
    public GameObject box1;
    public GameObject box2;
    public GameObject box3;
    public LayerMask groundLayer;
    public LayerMask disabledLayer;
    public int time = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   //Disable collision between the boxes, needed at one point, dont know if needed anymore, too scared to delete
        Physics2D.IgnoreCollision(box1.GetComponent<BoxCollider2D>(), box2.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(box1.GetComponent<BoxCollider2D>(), box3.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(box2.GetComponent<BoxCollider2D>(), box1.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(box2.GetComponent<BoxCollider2D>(), box3.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(box3.GetComponent<BoxCollider2D>(), box2.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(box3.GetComponent<BoxCollider2D>(), box1.GetComponent<BoxCollider2D>(), true);
                    
        TimeChange();
    }
    public void TimeChange()
    {
        if(time == 1) //change to past
        {   
            //order matters i think, enable circle collider for the box in the time first to do the check for ground
            box1.GetComponent<CircleCollider2D>().enabled = true;
            //move to disabled the other boxes and all the colliders and sprites turned off
            box2.GetComponent<BoxCollider2D>().enabled = false;
            box2.GetComponent<CircleCollider2D>().enabled = false;
            box2.GetComponent<Rigidbody2D>().isKinematic = true;
            box2.layer = LayerMask.NameToLayer("disabled");
            box2.GetComponent<SpriteRenderer>().enabled = false;
            box3.GetComponent<Rigidbody2D>().isKinematic = true;
            box3.layer = LayerMask.NameToLayer("disabled");
            box3.GetComponent<BoxCollider2D>().enabled = false;
            box3.GetComponent<CircleCollider2D>().enabled = false;
            box3.GetComponent<SpriteRenderer>().enabled = false;
            //enable box1 collider and set layer to ground
            box1.GetComponent<BoxCollider2D>().enabled = true;
            box1.layer = LayerMask.NameToLayer("ground");
            box1.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if(time == 2)//move to present
        {   //same thing
            box2.GetComponent<CircleCollider2D>().enabled = true;
            box3.GetComponent<BoxCollider2D>().enabled = false;
            box3.GetComponent<CircleCollider2D>().enabled = false;
            box2.GetComponent<Rigidbody2D>().isKinematic = false;
            box1.layer = LayerMask.NameToLayer("disabled");
            box1.GetComponent<BoxCollider2D>().enabled = false;
            box1.GetComponent<CircleCollider2D>().enabled = false;
            box1.GetComponent<SpriteRenderer>().enabled = false;
            box3.GetComponent<Rigidbody2D>().isKinematic = true;
            box3.layer = LayerMask.NameToLayer("disabled");
            box3.GetComponent<SpriteRenderer>().enabled = false;
            box2.GetComponent<BoxCollider2D>().enabled = true;
            box2.layer = LayerMask.NameToLayer("ground");
            box2.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if(time == 3)//move to future
        {   //nothing changed
            box3.GetComponent<CircleCollider2D>().enabled = true;
            box1.GetComponent<BoxCollider2D>().enabled = false;
            box1.GetComponent<CircleCollider2D>().enabled = false;
            box1.layer = LayerMask.NameToLayer("disabled");
            box1.GetComponent<SpriteRenderer>().enabled = false;
            box3.GetComponent<Rigidbody2D>().isKinematic = false;   
            box2.layer = LayerMask.NameToLayer("disabled");
            box2.GetComponent<BoxCollider2D>().enabled = false;
            box2.GetComponent<CircleCollider2D>().enabled = false;
            box2.GetComponent<SpriteRenderer>().enabled = false;
            box3.GetComponent<BoxCollider2D>().enabled = true;
            box3.layer = LayerMask.NameToLayer("ground");
            box3.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    public void FreezeBoxes()
    {
        if (time == 1) //change to past
        {
            box1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            box2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            box3.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        else if (time == 2)//move to present
        {
            box1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            box2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            box3.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        else if (time == 3)//move to future
        {
            box1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            box2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            box3.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
 }

    public void ResetBoxes()//on death/reset at checkpoint move all the boxes back
    {
        box1.GetComponent<SelfBox>().MoveToOriginalPosition();
        box2.GetComponent<SelfBox>().MoveToOriginalPosition();
        box3.GetComponent<SelfBox>().MoveToOriginalPosition();
    }
}
