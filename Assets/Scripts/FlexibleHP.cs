using UnityEngine;

public class FlexibleHP : MonoBehaviour
{
    #region Fields
    [SerializeField] private int flexhealth = -1;
    #endregion
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.UpdateHealth(flexhealth);
        }
    }
}