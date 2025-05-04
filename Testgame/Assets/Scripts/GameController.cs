using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class GameController : MonoBehaviour
{
    public GameObject Pausemenu;
    public Boxes[] boxes;//same like with enemies
    public bool reseter = false;
    public GameObject player;
    public GameObject LoadCanvas;
    public List<GameObject> grounds;

    public List<GameObject> enemies; //added by hand, could be done with findallbytag but it could be expensive performance-wise, i avoided
    private int timeindicator = 2;
    private int nextTime = 2;
    public GameObject healthUI;
    private PlayerHealth playerHealth;
    public Vector2 lastCheckpoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastCheckpoint = player.transform.position;
        playerHealth = player.GetComponent<PlayerHealth>();
        //invokes of actions
        LoadScript.OnHoldComplete += ChangeTime;
        PlayerHealth.Death += ChangeTime;
        DeathPit.Death += ChangeTime;
        LoadCanvas.SetActive(true);
        //basically set ground to present, we start in the present
        grounds[4].layer = LayerMask.NameToLayer("disabled");
        grounds[4].GetComponent<TilemapRenderer>().enabled = false;
        grounds[4].GetComponent<TilemapCollider2D>().enabled = false;
        grounds[5].layer = LayerMask.NameToLayer("disabled");
        grounds[5].GetComponent<TilemapRenderer>().enabled = false;
        grounds[5].GetComponent<TilemapCollider2D>().enabled = false;
        grounds[1].layer = LayerMask.NameToLayer("disabled");
        grounds[1].GetComponent<TilemapRenderer>().enabled = false;
        grounds[1].GetComponent<TilemapCollider2D>().enabled = false;
        grounds[0].layer = LayerMask.NameToLayer("disabled");
        grounds[0].GetComponent<TilemapRenderer>().enabled = false;
        grounds[0].GetComponent<TilemapCollider2D>().enabled = false;
        grounds[2].layer = LayerMask.NameToLayer("ground");
        grounds[2].GetComponent<TilemapRenderer>().enabled = true;
        grounds[2].GetComponent<TilemapCollider2D>().enabled = true;
        grounds[2].GetComponent<TilemapCollider2D>().usedByComposite = true;
        grounds[3].layer = LayerMask.NameToLayer("ground");
        grounds[3].GetComponent<TilemapRenderer>().enabled = true;
        grounds[3].GetComponent<TilemapCollider2D>().enabled = true;
        grounds[3].GetComponent<TilemapCollider2D>().usedByComposite = true;
        
    }
    void ChangeTime()
    {
        if (reseter == false)
        {
            timeindicator = LoadCanvas.GetComponent<LoadScript>().lastpress;
            playerHealth.currentEnergy = playerHealth.currentEnergy - 10;
            LoadCanvas.SetActive(false);
            switch (timeindicator)
            {
                case 1: //change to past, order probably doesnt mater
                    grounds[2].layer = LayerMask.NameToLayer("disabled");
                    grounds[2].GetComponent<TilemapRenderer>().enabled = false;
                    grounds[2].GetComponent<TilemapCollider2D>().enabled = false;
                    grounds[3].layer = LayerMask.NameToLayer("disabled");
                    grounds[3].GetComponent<TilemapRenderer>().enabled = false;
                    grounds[3].GetComponent<TilemapCollider2D>().enabled = false;
                    grounds[4].layer = LayerMask.NameToLayer("disabled");
                    grounds[4].GetComponent<TilemapRenderer>().enabled = false;
                    grounds[4].GetComponent<TilemapCollider2D>().enabled = false;
                    grounds[5].layer = LayerMask.NameToLayer("disabled");
                    grounds[5].GetComponent<TilemapRenderer>().enabled = false;
                    grounds[5].GetComponent<TilemapCollider2D>().enabled = false;
                    grounds[0].layer = LayerMask.NameToLayer("ground");
                    grounds[0].GetComponent<TilemapRenderer>().enabled = true;
                    grounds[0].GetComponent<TilemapCollider2D>().enabled = true;
                    grounds[0].GetComponent<TilemapCollider2D>().usedByComposite = true;
                    grounds[1].layer = LayerMask.NameToLayer("ground");
                    grounds[1].GetComponent<TilemapRenderer>().enabled = true;
                    grounds[1].GetComponent<TilemapCollider2D>().enabled = true;
                    grounds[1].GetComponent<TilemapCollider2D>().usedByComposite = true;
                    break;    
                    case 2: //change to present
                    grounds[4].layer = LayerMask.NameToLayer("disabled");
                    grounds[4].GetComponent<TilemapRenderer>().enabled = false;
                    grounds[4].GetComponent<TilemapCollider2D>().enabled = false;
                    grounds[5].layer = LayerMask.NameToLayer("disabled");
                    grounds[5].GetComponent<TilemapRenderer>().enabled = false;
                    grounds[5].GetComponent<TilemapCollider2D>().enabled = false;
                    grounds[1].layer = LayerMask.NameToLayer("disabled");
                    grounds[1].GetComponent<TilemapRenderer>().enabled = false;
                    grounds[1].GetComponent<TilemapCollider2D>().enabled = false;
                    grounds[0].layer = LayerMask.NameToLayer("disabled");
                    grounds[0].GetComponent<TilemapRenderer>().enabled = false;
                    grounds[0].GetComponent<TilemapCollider2D>().enabled = false;
                    grounds[2].layer = LayerMask.NameToLayer("ground");
                    grounds[2].GetComponent<TilemapRenderer>().enabled = true;
                    grounds[2].GetComponent<TilemapCollider2D>().enabled = true;
                    grounds[2].GetComponent<TilemapCollider2D>().usedByComposite = true;
                    grounds[3].layer = LayerMask.NameToLayer("ground");
                    grounds[3].GetComponent<TilemapRenderer>().enabled = true;
                    grounds[3].GetComponent<TilemapCollider2D>().enabled = true;
                    grounds[3].GetComponent<TilemapCollider2D>().usedByComposite = true;
                    break;
                case 3: //change to future
                    grounds[2].layer = LayerMask.NameToLayer("disabled");
                    grounds[2].GetComponent<TilemapRenderer>().enabled = false;
                    grounds[2].GetComponent<TilemapCollider2D>().enabled = false;
                    grounds[3].layer = LayerMask.NameToLayer("disabled");
                    grounds[3].GetComponent<TilemapRenderer>().enabled = false;
                    grounds[3].GetComponent<TilemapCollider2D>().enabled = false;
                    grounds[0].layer = LayerMask.NameToLayer("disabled");
                    grounds[0].GetComponent<TilemapRenderer>().enabled = false;
                    grounds[0].GetComponent<TilemapCollider2D>().enabled = false;
                    grounds[1].layer = LayerMask.NameToLayer("disabled");
                    grounds[1].GetComponent<TilemapRenderer>().enabled = false;
                    grounds[1].GetComponent<TilemapCollider2D>().enabled = false;
                    grounds[4].layer = LayerMask.NameToLayer("ground");
                    grounds[4].GetComponent<TilemapRenderer>().enabled = true;
                    grounds[4].GetComponent<TilemapCollider2D>().enabled = true;
                    grounds[4].GetComponent<TilemapCollider2D>().usedByComposite = true;
                    grounds[5].layer = LayerMask.NameToLayer("ground");
                    grounds[5].GetComponent<TilemapRenderer>().enabled = true;
                    grounds[5].GetComponent<TilemapCollider2D>().enabled = true;
                    grounds[5].GetComponent<TilemapCollider2D>().usedByComposite = true;
                    break;
                default: //just in case if somethings goes bad, we go back to present
                    timeindicator = 2; 
                    grounds[4].layer = LayerMask.NameToLayer("disabled");
                    grounds[4].GetComponent<TilemapRenderer>().enabled = false;
                    grounds[4].GetComponent<TilemapCollider2D>().enabled = false;
                    grounds[5].layer = LayerMask.NameToLayer("disabled");
                    grounds[5].GetComponent<TilemapRenderer>().enabled = false;
                    grounds[5].GetComponent<TilemapCollider2D>().enabled = false;
                    grounds[1].layer = LayerMask.NameToLayer("disabled");
                    grounds[1].GetComponent<TilemapRenderer>().enabled = false;
                    grounds[1].GetComponent<TilemapCollider2D>().enabled = false;
                    grounds[0].layer = LayerMask.NameToLayer("disabled");
                    grounds[0].GetComponent<TilemapRenderer>().enabled = false;
                    grounds[0].GetComponent<TilemapCollider2D>().enabled = false;
                    grounds[2].layer = LayerMask.NameToLayer("ground");
                    grounds[2].GetComponent<TilemapRenderer>().enabled = true;
                    grounds[2].GetComponent<TilemapCollider2D>().enabled = true;
                    grounds[2].GetComponent<TilemapCollider2D>().usedByComposite = true;
                    grounds[3].layer = LayerMask.NameToLayer("ground");
                    grounds[3].GetComponent<TilemapRenderer>().enabled = true;
                    grounds[3].GetComponent<TilemapCollider2D>().enabled = true;
                    grounds[3].GetComponent<TilemapCollider2D>().usedByComposite = true;
                    break;
            }
            foreach (Boxes box in boxes) //tell each box to change the time
            {
                box.time = timeindicator;
                box.TimeChange();
                box.FreezeBoxes();
            }
            LoadCanvas.SetActive(true);
        }
        else //reseting / death, made them the same, might regret later
        {
            foreach (Boxes box in boxes)  //reseting boxes
            {
                box.GetComponent<Boxes>().ResetBoxes();
            }
            foreach (GameObject enemy in enemies) //reseting enemies
            {
                enemy.GetComponent<Enemies>().MoveToOriginalPosition();
            }
            healthUI.GetComponent<HealthUI>().SetHealth(playerHealth.maxHealth); //set health to max
            playerHealth.currentHealth = playerHealth.maxHealth;
            player.transform.position = lastCheckpoint; //teleport player to last checkpoint
            playerHealth.ResetEnergy();
            reseter = false;
        }
    }
    
}
