using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class GameController : MonoBehaviour
{
    private Boxes[] boxes;//same like with enemies
    public bool reseter = false;
    public GameObject player;
    public GameObject LoadCanvas;
    public GameObject[] grounds;
    private Enemies[] enemies; //added by hand, could be done with findallbytag but it could be expensive performance-wise, i avoided
    public int timeindicator;
    public HealthUI healthUI;
    private PlayerHealth playerHealth;
    public Vector2 lastCheckpoint;
    public GameObject SaveData;
    public List<bool> triggeredScenes = new List<bool>(); //list of scenes that were triggered, used for saving and loading
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        healthUI = FindObjectsOfType<HealthUI>()[0];
        player = GameObject.FindWithTag("Player");
        boxes = FindObjectsOfType<Boxes>();
        enemies = FindObjectsOfType<Enemies>();
        grounds[0] = GameObject.FindWithTag("Ground Past");
        grounds[1] = GameObject.FindWithTag("Platform Past");
        grounds[2] = GameObject.FindWithTag("Ground Present");
        grounds[3] = GameObject.FindWithTag("Platform Present");
        grounds[4] = GameObject.FindWithTag("Ground Future");
        grounds[5] = GameObject.FindWithTag("Platform Future");
        SaveData = GameObject.Find("SaveData");
        if (SaveData.GetComponent<SaveData>().data.TriggeredScenes != null)
        {
            triggeredScenes = SaveData.GetComponent<SaveData>().data.TriggeredScenes; //load triggered scenes from save data
        }
        if (triggeredScenes.Count == 0)
        {
            List<CutsceneTrigger> triggers = new List<CutsceneTrigger>(FindObjectsOfType<CutsceneTrigger>());
            foreach (CutsceneTrigger trigger in triggers)
            {
                triggeredScenes.Add(false);
            }
        }
    }
    void Start()
    {
        LoadCanvas = GameObject.Find("LoadCanvas");
        lastCheckpoint = player.transform.position;
        playerHealth = player.GetComponent<PlayerHealth>();
        //invokes of actions
        LoadCanvas.GetComponent<LoadScript>().OnHoldComplete += ChangeTime;
        player.GetComponent<PlayerHealth>().Death += ChangeTime;
        //basically set ground to present, we start in the present
        if (SaveData.GetComponent<SaveData>().data.timeindicator != 0)
        {
            timeindicator = SaveData.GetComponent<SaveData>().data.timeindicator;
            LoadCanvas.GetComponent<LoadScript>().lastpress = SaveData.GetComponent<SaveData>().data.timeindicator;
            LoadCanvas.GetComponent<LoadScript>().timeindicator = SaveData.GetComponent<SaveData>().data.timeindicator;
        }
        else
        {
            timeindicator = 2;
        }
        ChangeTime();
        if (SaveData.GetComponent<SaveData>().data.lastCheckpoint != new Vector2(player.GetComponent<Rigidbody2D>().transform.position.x, player.GetComponent<Rigidbody2D>().transform.position.y) && SaveData.GetComponent<SaveData>().data.lastCheckpoint != Vector2.zero)
        {
            lastCheckpoint = SaveData.GetComponent<SaveData>().data.lastCheckpoint;
            reseter = true;
            ChangeTime();
        }
        List<CutsceneTrigger> triggers = new List<CutsceneTrigger>(FindObjectsOfType<CutsceneTrigger>());
        foreach (CutsceneTrigger trigger in triggers)
        {
            if(triggeredScenes[trigger.CutsceneID] == true)
            {
                trigger.gameObject.SetActive(false); //disable cutscene triggers that were already triggered
            }
            else
            {
                trigger.gameObject.SetActive(true); //enable cutscene triggers that were not triggered
            }
        }
        
    }
    void ChangeTime()
    {
        if (reseter == false)
        {
            timeindicator = LoadCanvas.GetComponent<LoadScript>().lastpress;
            playerHealth.currentEnergy = playerHealth.currentEnergy - 10;
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
        }
        else //reseting / death, made them the same, might regret later
        {
            foreach (Boxes box in boxes)  //reseting boxes
            {
                box.GetComponent<Boxes>().ResetBoxes();
            }
            foreach (Enemies enemy in enemies) //reseting enemies
            {
                enemy.GetComponent<Enemies>().MoveToOriginalPosition();
            }
            healthUI.GetComponent<HealthUI>().SetHealth(playerHealth.maxHealth); //set health to max
            playerHealth.currentHealth = playerHealth.maxHealth;
            player.transform.position = new Vector2(lastCheckpoint.x,lastCheckpoint.y+1f); //teleport player to last checkpoint
            playerHealth.ResetEnergy();
            reseter = false;
        }
    }
    
}
