using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlrHealthSystem : MonoBehaviour
{

    public int maxHealth = 5;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDmg(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            //Add respawn
            SceneManager.LoadScene("Level_0");
        }
    }
}
