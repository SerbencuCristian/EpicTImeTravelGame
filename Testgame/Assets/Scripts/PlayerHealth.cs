using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // Maximum health of the player
    public float currentEnergy;
    public float maxEnergy = 100f;
    public int currentHealth; // Current health of the player
    public HealthUI healthUI; // Reference to the HealthUI script
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool isInvincible = false; // Flag to check if the player is invincible
    private playerMovement playerMovement; // Reference to the playerMovement script
    public event Action Death;
    public Image EnergyBar;
    public GameObject GameController; // Reference to the GameController GameObject
    void Awake()
    {
        GameController = GameObject.Find("GameController"); // Find the GameController GameObject in the scene
    }
    void Start()
    {
        EnergyBar = GameObject.Find("EnergyBar").GetComponent<Image>(); // Find the EnergyBar GameObject in the scene
        currentEnergy = maxEnergy;
        EnergyBar.GetComponent<EnergyBarUI>().currentEnergy = currentEnergy;
        currentHealth = maxHealth; // Initialize current health to max health
        healthUI.SetMaxHealth(maxHealth); // Set the max health in the UI
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        playerMovement = GetComponent<playerMovement>(); // Get the playerMovement component
    }
    void Update()
    {
        EnergyBar.GetComponent<EnergyBarUI>().currentEnergy = currentEnergy; // Update the current energy in the EnergyBar UI
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemies enemy = collision.GetComponent<Enemies>();
        if (enemy && !isInvincible)
        {
             TakeDamage(1); // Take damage when colliding with an enemy
             enemy.Knockback(enemy.transform.position - transform.position, 5f); // Apply knockback to the enemy
             playerMovement.Knockback(transform.position - enemy.transform.position, 5f); // Apply knockback to the player
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Decrease current health by damage amount
        healthUI.SetHealth(currentHealth); // Update the health UI
        StartCoroutine(Iframes());
        if (currentHealth <= 0)
        {
            // Handle player death (e.g., restart level, show game over screen, etc.)
            GameController.GetComponent<GameController>().reseter = true; // Set the reseter flag in the GameController
            Death.Invoke(); // Invoke the Death event
        }
    }
    private IEnumerator Iframes()
    {
        spriteRenderer.color = Color.red; // Change color to red to indicate damage
        isInvincible = true; // Set invincibility flag to true
        yield return new WaitForSeconds(0.2f); // Wait for half a second
        isInvincible = false; // Set invincibility flag to false
        spriteRenderer.color = Color.white; // Change color back to white
    }
    public void ResetEnergy()
    {
        currentEnergy = maxEnergy;
    }
}
