using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    #region Fields
    [SerializeField] private bool bPlayer;
    [SerializeField] private int score;
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private Image healthBar;
    private bool bAlive = true;

    public int currentHealth;
    public bool isAlive => bAlive;
    public static Action<float> UpdateHealthBar = delegate { };
    public static Action<int> UpdateScore = delegate { };
    #endregion
    private void OnEnable()
    {
        currentHealth = maxHealth;
        if (bPlayer)
        {
            healthBar = GameObject.FindWithTag("HealthBar").GetComponent<Image>();
        }
    }

    public void UpdateHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        //talk to healthbar
        if (bPlayer)
        {
            UpdateHealthBar(currentHealth / (maxHealth * 1f)); //Player
        }
        else
        {
            healthBar.fillAmount = currentHealth / (maxHealth * 1f); //Enemies
        }

        if (currentHealth == 0 && bAlive)
        {
            bAlive = false;
            Death();
        }
    }

    private void Death()
    {
        if (!bPlayer)
            UpdateScore(score);
    }
}