using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthBar : MonoBehaviour
{
    #region Fields
    private Image healthBarUI;
    #endregion

    private void OnEnable()
    {
        HealthSystem.UpdateHealthBar += UpdateHealthBar;
    }

    private void OnDisable()
    {
        HealthSystem.UpdateHealthBar -= UpdateHealthBar;
    }
    private void Start()
    {
        healthBarUI = GetComponent<Image>();
    }
    void UpdateHealthBar(float slider)
    {
        healthBarUI.fillAmount = slider;
    }
}