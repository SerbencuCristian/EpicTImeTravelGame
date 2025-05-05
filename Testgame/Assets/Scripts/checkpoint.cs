using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;


public class checkpoint : MonoBehaviour
{
    public bool collision = false;
    public GameObject GameController;
    public GameObject LoadCanvas;
    void Awake()
    {
        LoadCanvas = GameObject.Find("LoadCanvas");
        GameController = GameObject.Find("GameController");
    }
    // Update is called once per frame
    void Update()
    {
        if(collision == true) //enable reseter flags if on top of checkpoint
        {
            GameController.GetComponent<GameController>().reseter = true;
            LoadCanvas.GetComponent<LoadScript>().reseter = true;
        }
    }
    void OnTriggerEnter2D(Collider2D col) //whenever player enters remember position for last checkpoint touched
    {
        if (col.gameObject.tag == "Player")
        {
            collision = true;
            GameController.GetComponent<GameController>().lastCheckpoint = transform.position;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {    
        if (col.gameObject.tag == "Player") //disabling reseter flags when player leaves checkpoint
        {
            collision = false;
            GameController.GetComponent<GameController>().reseter = false;
            LoadCanvas.GetComponent<LoadScript>().reseter = false;
        }
    }
}
