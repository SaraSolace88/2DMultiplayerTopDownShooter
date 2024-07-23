using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region fields
    [HideInInspector] public Vector3 direction;
    [SerializeField] private float speed = 5;
    #endregion

    private void Start()
    {
        StartCoroutine(nameof(WaitToDie));
    }

    void Update()
    {
        transform.localPosition += direction * speed * Time.deltaTime;
    }

    IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}