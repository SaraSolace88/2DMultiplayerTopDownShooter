using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FlexibleHP : MonoBehaviour
{
    #region Fields
    [SerializeField] private int flexhealth = -1;
    [SerializeField] private bool continual;
    [SerializeField] private float contInternal;
    private bool intervalActive;
    #endregion
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthSystem healthSystem) && !other.CompareTag(gameObject.tag))
        {
            healthSystem.UpdateHealth(flexhealth);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (continual && !intervalActive)
        {
            if (other.TryGetComponent(out HealthSystem healthSystem) && !other.CompareTag(gameObject.tag))
            {
                healthSystem.UpdateHealth(flexhealth);
                StartCoroutine(nameof(ContinualFlexibleHP));
            }
        }
    }

    IEnumerator ContinualFlexibleHP()
    {
        intervalActive = true;
        yield return new WaitForSeconds(contInternal);
        intervalActive = false;
    }
}