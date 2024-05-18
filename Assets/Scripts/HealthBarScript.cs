using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Stats Player;
    private static float maxHealth;
    private int currentHealth;
    public Image healthBar;

    //public Text healthText;

    void Start()
    {
        currentHealth = Player.Hp;
        maxHealth = Player.Hp;
        //UpdateHealthBar();
    }

    void Update()
    {
        healthBar.fillAmount = (float)Player.Hp / maxHealth;
    }

    /*public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHealthBar();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = (float)currentHealth / Player.Hp;
        //healthText.text = currentHealth + " / " + maxHealth;
    } */
}
