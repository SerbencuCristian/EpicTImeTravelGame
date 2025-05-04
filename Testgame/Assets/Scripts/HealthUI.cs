using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class HealthUI : MonoBehaviour
{
    public Image HearthPrefab; // Prefab for the heart icon
    public Sprite FullHeart;
    public Sprite EmptyHeart;

    public List<Image> Hearts = new List<Image>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetMaxHealth(int maxHealth)
    {
        // Clear existing hearts
        foreach (var heart in Hearts)
        {
            Destroy(heart.gameObject);
        }
        Hearts.Clear();

        // Create new hearts based on max health
        for (int i = 0; i < maxHealth; i++)
        {
            Image heart = Instantiate(HearthPrefab, transform);
            heart.sprite = FullHeart;
            heart.color = Color.red;
            Hearts.Add(heart);
        }
    }
    public void SetHealth(int health)
    {
        for (int i = 0; i < Hearts.Count; i++)
        {
            if (i < health)
            {
                Hearts[i].sprite = FullHeart;
                Hearts[i].color = Color.red;
            }
            else
            {
                Hearts[i].sprite = EmptyHeart;
                Hearts[i].color = Color.white;
            }
        }
    }
}
