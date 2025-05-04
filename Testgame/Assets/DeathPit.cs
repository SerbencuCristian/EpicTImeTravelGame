using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
public class DeathPit : MonoBehaviour
{
    public static event Action Death;
    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        if (player)
        {
            player.TakeDamage(player.currentHealth);
        }
    }
}
