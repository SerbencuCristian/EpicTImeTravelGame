using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
public class LoadScript : MonoBehaviour
{
    public Camera camera;
    public Animator animatorTimeSight;
    public Animator animatorFade;
    public Image PastImage;
    public Image PresentImage;
    public Image FutureImage;
    public Slider cooldownSlider;
    public float holdDuration = 1.5f;
    public Image FillCircle;
    public GameObject Player;
    public GameObject Holder;
    private float timer = 0;
    public bool isHolding = false;
    public event Action OnHoldComplete;
    public float cooldown = 0;
    public bool ok = true;
    private bool isAnimating = false;

    public Transform disabledCheck;
    public Vector2 disabledCheckSize = new Vector2(0.95f, 0.95f);
    public LayerMask disabledLayer;
    public int lastpress = 2;
    public bool checkok = false;
    public GameObject[] grounds;
    public GameObject[] Pastboxes; //added by hand, again findallbytag could work, but i am scared to use it
    public GameObject[] Futureboxes;
    public GameObject[] Presentboxes;

    public int timeindicator = 2;

    public bool reseter = false;
    public bool HoldingTS = false;
    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        animatorTimeSight = GameObject.Find("TimeSightOverlay").GetComponent<Animator>();
        animatorFade = GameObject.Find("FadeMask").GetComponent<Animator>();
        PastImage = GameObject.Find("PastImage").GetComponent<Image>();
        PresentImage = GameObject.Find("PresentImage").GetComponent<Image>();
        FutureImage = GameObject.Find("FutureImage").GetComponent<Image>();
        cooldownSlider = GameObject.Find("CooldownSlider").GetComponent<Slider>();
        FillCircle = GameObject.Find("LoadCircle").GetComponent<Image>();
        Player = GameObject.Find("Player");
        Holder = GameObject.Find("Holder");
        disabledCheck = GameObject.Find("DisabledCheck").transform;

        grounds[0] = GameObject.FindWithTag("Ground Past");
        grounds[1] = GameObject.FindWithTag("Platform Past");
        grounds[2] = GameObject.FindWithTag("Ground Present");
        grounds[3] = GameObject.FindWithTag("Platform Present");
        grounds[4] = GameObject.FindWithTag("Ground Future");
        grounds[5] = GameObject.FindWithTag("Platform Future");

        Pastboxes = GameObject.FindGameObjectsWithTag("PastBox");
        Presentboxes = GameObject.FindGameObjectsWithTag("PresentBox");
        Futureboxes = GameObject.FindGameObjectsWithTag("FutureBox");
        cooldownSlider.value = 0;
        cooldownSlider.maxValue = 5f;
        cooldownSlider.minValue = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0) //lower cooldown
        {
            cooldown -= Time.deltaTime;
        }
        else if (ok == false) //enable back if cooldown done
        {
            FillCircle.color = Color.blue;
            ok = true;
        }
        cooldownSlider.value = cooldown;
        if (lastpress == 1) //set colors for whichever button was pressed
        {
            PastImage.color = Color.blue;
            PresentImage.color = Color.white;
            FutureImage.color = Color.white;
        }
        else if (lastpress == 2)
        {
            PastImage.color = Color.white;
            PresentImage.color = Color.blue;
            FutureImage.color = Color.white;
        }
        else if (lastpress == 3)
        {
            PastImage.color = Color.white;
            PresentImage.color = Color.white;
            FutureImage.color = Color.blue;
        }
        if(timeindicator == 1) //set outline for last pressed button
        {
            PastImage.GetComponent<Outline>().effectColor = Color.green;
            PastImage.GetComponent<Outline>().effectDistance = new Vector2(10, 10);
            PresentImage.GetComponent<Outline>().effectColor = Color.clear;
            PresentImage.GetComponent<Outline>().effectDistance = Vector2.zero;
            FutureImage.GetComponent<Outline>().effectColor = Color.clear;
            FutureImage.GetComponent<Outline>().effectDistance = Vector2.zero;
        }
        else if(timeindicator == 2)
        {
            PresentImage.GetComponent<Outline>().effectColor = Color.green;
            PresentImage.GetComponent<Outline>().effectDistance = new Vector2(10, 10);
            PastImage.GetComponent<Outline>().effectColor = Color.clear;
            PastImage.GetComponent<Outline>().effectDistance = Vector2.zero;
            FutureImage.GetComponent<Outline>().effectColor = Color.clear;
            FutureImage.GetComponent<Outline>().effectDistance = Vector2.zero;
        }
        else if(timeindicator == 3)
        {
            FutureImage.GetComponent<Outline>().effectColor = Color.green;
            FutureImage.GetComponent<Outline>().effectDistance = new Vector2(10, 10);
            PastImage.GetComponent<Outline>().effectColor = Color.clear;
            PastImage.GetComponent<Outline>().effectDistance = Vector2.zero;
            PresentImage.GetComponent<Outline>().effectColor = Color.clear;
            PresentImage.GetComponent<Outline>().effectDistance = Vector2.zero;
        }
        if (isHolding && ok == true && Time.timeScale ==1) //here start timetravel checks
        {
            FillCircle.color = Color.blue;
            timer += Time.deltaTime; //increase timer
            FillCircle.fillAmount = timer / (holdDuration-0.5f);//fill circle, has an extra 0.5s after its filled for animation shenanigans
            if (timer >= holdDuration && checkok && lastpress != timeindicator && reseter == false)
            {
                //Time travel section
                timeindicator = lastpress;
                OnHoldComplete.Invoke();
                cooldown = 2.5f;
                ok = false;
                ResetHold();
            }
            else if(timer >= holdDuration && !checkok && reseter == false) //didnt pass the check for ground on other times
            {
                FailHold();
            }
            else if (timer >= holdDuration-0.5f && !checkok && reseter == false)//setting the red color for bad travel
            {
                FillCircle.color = Color.red;
            }
            else if (timer >= holdDuration && reseter == true)//reseting to checkpoint
            {
                OnHoldComplete.Invoke();
                FailHold();
            }
            else if(timer >= holdDuration && lastpress == timeindicator) //cant travel to same time
            {
                FailHold();
            }
            else if(timer >= holdDuration) //this just so you cant hold the button down forever
            {
                FailHold();
            }
        } 
    }

    public void OnHold(InputAction.CallbackContext context)
    {
        if (ok == true && cooldown <= 0 && !HoldingTS && Time.timeScale ==1)
        {
            if (context.started) //staring the hold
            {
                animatorFade.SetTrigger("Fade");
                isHolding = true;
                timer = 0;
                checkok = DisabledCheck();
                //freeze player in place
                Player.GetComponent<playerMovement>().enabled = false;
                Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                Player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            }
            else if (context.canceled)//button lifted
            {
                cooldown = Mathf.Max(cooldown, 0.5f);
                ResetHold();
            }
            
        }
    }
    private void ResetHold() //reset after holding down the button
    {
        Player.GetComponent<playerMovement>().enabled = true;
        isHolding = false;
        timer = 0;
        ok = false;
        FillCircle.fillAmount = 0;
        checkok = false;
        Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        Player.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(Player.GetComponent<Rigidbody2D>().linearVelocity.x, -0.1f);
        
    }
    private void FailHold() //didnt wanna repeat code
    {
        cooldown = 1f; 
        ResetHold();
    }
    private bool DisabledCheck() //the check for collision with the disabled layers, aka ground in other times
    {
        if(Player.GetComponent<PlayerHealth>().currentEnergy < 10)
        {
            return false;
        }
        if (lastpress != timeindicator) //cant check same time as is in
        {
            if(timeindicator == 1)
            {
                foreach(GameObject box in Pastboxes)
                {
                    box.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                }
            }
            else if(timeindicator == 2)
            {
                foreach(GameObject box in Presentboxes)
                {
                    box.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                }
            }
            else if(timeindicator == 3)
            {
                foreach(GameObject box in Futureboxes)
                {
                    box.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                }
            }


        if(lastpress == 1) //enable past colliders
        {
            grounds[0].layer = LayerMask.NameToLayer("disabled");
            grounds[0].GetComponent<TilemapCollider2D>().enabled = true;
            grounds[0].GetComponent<TilemapCollider2D>().usedByComposite = false;
            grounds[1].layer = LayerMask.NameToLayer("disabled");
            grounds[1].GetComponent<TilemapCollider2D>().enabled = true;
            grounds[1].GetComponent<TilemapCollider2D>().usedByComposite = false; //need to do this cause composite collider makes it one single hitbox and it wont check in the center of blocks
            foreach (GameObject box in Pastboxes)
            {
                box.GetComponent<BoxCollider2D>().enabled = true;
            }
            
        }
        else if (lastpress == 2)//same
        {
            grounds[2].layer = LayerMask.NameToLayer("disabled");
            grounds[2].GetComponent<TilemapCollider2D>().enabled = true;
            grounds[2].GetComponent<TilemapCollider2D>().usedByComposite = false;
            grounds[3].layer = LayerMask.NameToLayer("disabled");
            grounds[3].GetComponent<TilemapCollider2D>().enabled = true;
            grounds[3].GetComponent<TilemapCollider2D>().usedByComposite = false;
            foreach (GameObject box in Presentboxes)
            {
                box.GetComponent<BoxCollider2D>().enabled = true;
            }
            
        }
        else if (lastpress == 3)//same
        {
            grounds[4].layer = LayerMask.NameToLayer("disabled");
            grounds[4].GetComponent<TilemapCollider2D>().enabled = true;
            grounds[4].GetComponent<TilemapCollider2D>().usedByComposite = false;
            grounds[5].layer = LayerMask.NameToLayer("disabled");
            grounds[5].GetComponent<TilemapCollider2D>().enabled = true;
            grounds[5].GetComponent<TilemapCollider2D>().usedByComposite = false;
            foreach (GameObject box in Futureboxes)
            {
                box.GetComponent<BoxCollider2D>().enabled = true;
            }
            
        }
        if (Physics2D.OverlapBox(disabledCheck.position, disabledCheckSize, 0f, disabledLayer)) //actual check
        {
            if(lastpress == 1) //disable checked time
            {
                grounds[0].GetComponent<TilemapCollider2D>().enabled = false;
                grounds[0].GetComponent<TilemapCollider2D>().usedByComposite = true;
                grounds[1].GetComponent<TilemapCollider2D>().enabled = false;
                grounds[1].GetComponent<TilemapCollider2D>().usedByComposite = true;
                foreach (GameObject box in Pastboxes)
                {
                    box.GetComponent<BoxCollider2D>().enabled = false;
                }
                
            }
            else if (lastpress == 2)
            {
                grounds[2].GetComponent<TilemapCollider2D>().enabled = false;
                grounds[2].GetComponent<TilemapCollider2D>().usedByComposite = true;
                grounds[3].GetComponent<TilemapCollider2D>().enabled = false;
                grounds[3].GetComponent<TilemapCollider2D>().usedByComposite = true;
                foreach (GameObject box in Presentboxes)
                {
                    box.GetComponent<BoxCollider2D>().enabled = false;
                }
                
            }
            else if (lastpress == 3)
            {
                grounds[4].GetComponent<TilemapCollider2D>().enabled = false;
                grounds[4].GetComponent<TilemapCollider2D>().usedByComposite = true;
                grounds[5].GetComponent<TilemapCollider2D>().enabled = false;
                grounds[5].GetComponent<TilemapCollider2D>().usedByComposite = true;
                foreach (GameObject box in Futureboxes)
                {
                    box.GetComponent<BoxCollider2D>().enabled = false;
                }
                
            }
            if(timeindicator == 1)
            {
                foreach(GameObject box in Pastboxes)
                {
                    box.GetComponent<Rigidbody2D>().constraints =RigidbodyConstraints2D.FreezeRotation;
                }
            }
            else if(timeindicator == 2)
            {
                foreach(GameObject box in Presentboxes)
                {
                    box.GetComponent<Rigidbody2D>().constraints =RigidbodyConstraints2D.FreezeRotation;
                }
            }
            else if(timeindicator == 3)
            {
                foreach(GameObject box in Futureboxes)
                {
                    box.GetComponent<Rigidbody2D>().constraints =RigidbodyConstraints2D.FreezeRotation;
                }
            }
            return false;
        }
        if(lastpress == 1) //disable checked times
            {
                grounds[0].GetComponent<TilemapCollider2D>().enabled = false;
                grounds[0].GetComponent<TilemapCollider2D>().usedByComposite = true;
                grounds[1].GetComponent<TilemapCollider2D>().enabled = false;
                grounds[1].GetComponent<TilemapCollider2D>().usedByComposite = true;
                foreach (GameObject box in Pastboxes)
                {
                    box.GetComponent<BoxCollider2D>().enabled = false;
                }
                
            }
            else if (lastpress == 2)
            {
                grounds[2].GetComponent<TilemapCollider2D>().enabled = false;
                grounds[2].GetComponent<TilemapCollider2D>().usedByComposite = true;
                grounds[3].GetComponent<TilemapCollider2D>().enabled = false;
                grounds[3].GetComponent<TilemapCollider2D>().usedByComposite = true;
                foreach (GameObject box in Presentboxes)
                {
                    box.GetComponent<BoxCollider2D>().enabled = false;
                }
                
            }
            else if (lastpress == 3)
            {
                grounds[4].GetComponent<TilemapCollider2D>().enabled = false;
                grounds[4].GetComponent<TilemapCollider2D>().usedByComposite = true;
                grounds[5].GetComponent<TilemapCollider2D>().enabled = false;
                grounds[5].GetComponent<TilemapCollider2D>().usedByComposite = true;
                foreach (GameObject box in Futureboxes)
                {
                    box.GetComponent<BoxCollider2D>().enabled = false;
                }
                
            }
            if(timeindicator == 1)
            {
                foreach(GameObject box in Pastboxes)
                {
                    box.GetComponent<Rigidbody2D>().constraints =RigidbodyConstraints2D.FreezeRotation;
                }
            }
            else if(timeindicator == 2)
            {
                foreach(GameObject box in Presentboxes)
                {
                    box.GetComponent<Rigidbody2D>().constraints =RigidbodyConstraints2D.FreezeRotation;
                }
            }
            else if(timeindicator == 3)
            {
                foreach(GameObject box in Futureboxes)
                {
                    box.GetComponent<Rigidbody2D>().constraints =RigidbodyConstraints2D.FreezeRotation;
                }
            }
        return true;
        }
        return false;
    }
    private void OnDrawGizmosSelected() //i dont even know if this is needed, but i think it was needed at one point, probably worthless
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(disabledCheck.position, disabledCheckSize);
    }
    public void Pressed1(InputAction.CallbackContext context)
    {
        if (context.performed && !isHolding && !HoldingTS && Time.timeScale ==1)
        {
            lastpress = 1;
        }
    }
    public void Pressed2(InputAction.CallbackContext context)
    {
        if (context.performed && !isHolding && !HoldingTS && Time.timeScale ==1)
        {
            lastpress = 2;
        }
    }
    public void Pressed3(InputAction.CallbackContext context)
    {
        if (context.performed && !isHolding && !HoldingTS && Time.timeScale ==1)
        {
            lastpress = 3;
        }
    }
    public void TimeSight(InputAction.CallbackContext context) //looking into the other times
    {
        if(context.started && lastpress != timeindicator && !HoldingTS && Time.timeScale ==1)
        {
            animatorTimeSight.SetTrigger("TimeSightOn");
        }
        else if (context.canceled && HoldingTS && Time.timeScale ==1 && GameObject.Find("TimeSightOverlay").GetComponent<TimeSightOverlay>().isHolding == true)
        {
            animatorTimeSight.SetTrigger("TimeSightOff");
            Holder.SetActive(true);
            HoldingTS = false; 
        }
        
    }
    public void TimeSightOff()
    {
        Player.GetComponent<playerMovement>().enabled = true;
        grounds[2].GetComponent<TilemapRenderer>().enabled = false;
        grounds[1].GetComponent<TilemapRenderer>().enabled = false;
        grounds[0].GetComponent<TilemapRenderer>().enabled = false;
        grounds[3].GetComponent<TilemapRenderer>().enabled = false;
        grounds[4].GetComponent<TilemapRenderer>().enabled = false;
        grounds[5].GetComponent<TilemapRenderer>().enabled = false;
        foreach (GameObject box in Pastboxes)
        {
            box.GetComponent<SpriteRenderer>().enabled = false;
        }
        foreach (GameObject box in Presentboxes)
        {
            box.GetComponent<SpriteRenderer>().enabled = false;
        }
        foreach (GameObject box in Futureboxes)
        {
            box.GetComponent<SpriteRenderer>().enabled = false;
        }
        if(timeindicator == 1)
        {
            foreach (GameObject box in Pastboxes)
            {
                box.GetComponent<SpriteRenderer>().enabled = true;
            }
            grounds[0].GetComponent<TilemapRenderer>().enabled = true;
            grounds[1].GetComponent<TilemapRenderer>().enabled = true;
        }
        else if(timeindicator == 2)
        {
            foreach (GameObject box in Presentboxes)
            {
                box.GetComponent<SpriteRenderer>().enabled = true;
            }
            grounds[2].GetComponent<TilemapRenderer>().enabled = true;
            grounds[3].GetComponent<TilemapRenderer>().enabled = true;
        }
        else if(timeindicator == 3)
        {
            foreach (GameObject box in Futureboxes)
            {
                box.GetComponent<SpriteRenderer>().enabled = true;
            }
            grounds[4].GetComponent<TilemapRenderer>().enabled = true;
            grounds[5].GetComponent<TilemapRenderer>().enabled = true;
        }
    }
    public void TimeSightAnim()
    {
        HoldingTS = true;
        Holder.SetActive(false);
        if (lastpress == 1 && lastpress != timeindicator)
        {
            Player.GetComponent<playerMovement>().enabled = false;
            Player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            foreach (GameObject box in Pastboxes)
            {
                box.GetComponent<SpriteRenderer>().enabled = true;
            }
            foreach (GameObject box in Presentboxes)
            {
                box.GetComponent<SpriteRenderer>().enabled = false;
            }
            foreach (GameObject box in Futureboxes)
            {
                box.GetComponent<SpriteRenderer>().enabled = false;
            }
            grounds[5].GetComponent<TilemapRenderer>().enabled = false;
            grounds[4].GetComponent<TilemapRenderer>().enabled = false;
            grounds[3].GetComponent<TilemapRenderer>().enabled = false;
            grounds[2].GetComponent<TilemapRenderer>().enabled = false;
            grounds[0].GetComponent<TilemapRenderer>().enabled = true;
            grounds[1].GetComponent<TilemapRenderer>().enabled = true;

        }
        else if (lastpress == 2 && lastpress != timeindicator)
        {
            Player.GetComponent<playerMovement>().enabled = false;
            Player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            foreach (GameObject box in Presentboxes)
            {
                box.GetComponent<SpriteRenderer>().enabled = true;
            }
            foreach (GameObject box in Pastboxes)
            {
                box.GetComponent<SpriteRenderer>().enabled = false;
            }
            foreach (GameObject box in Futureboxes)
            {
                box.GetComponent<SpriteRenderer>().enabled = false;
            }
            grounds[5].GetComponent<TilemapRenderer>().enabled = false;
            grounds[4].GetComponent<TilemapRenderer>().enabled = false;
            grounds[0].GetComponent<TilemapRenderer>().enabled = false;
            grounds[1].GetComponent<TilemapRenderer>().enabled = false;
            grounds[2].GetComponent<TilemapRenderer>().enabled = true;
            grounds[3].GetComponent<TilemapRenderer>().enabled = true;
        }
        else if (lastpress == 3 && lastpress != timeindicator)
        {
            Player.GetComponent<playerMovement>().enabled = false;
            Player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            foreach (GameObject box in Futureboxes)
            {
                box.GetComponent<SpriteRenderer>().enabled = true;
            }
            foreach (GameObject box in Presentboxes)
            {
                box.GetComponent<SpriteRenderer>().enabled = false;
            }
            foreach (GameObject box in Pastboxes)
            {
                box.GetComponent<SpriteRenderer>().enabled = false;
            }
            Player.GetComponent<playerMovement>().enabled = false;
            grounds[0].GetComponent<TilemapRenderer>().enabled = false;
            grounds[1].GetComponent<TilemapRenderer>().enabled = false;
            grounds[2].GetComponent<TilemapRenderer>().enabled = false;
            grounds[3].GetComponent<TilemapRenderer>().enabled = false;
            grounds[4].GetComponent<TilemapRenderer>().enabled = true;
            grounds[5].GetComponent<TilemapRenderer>().enabled = true;

        }
    }
}
