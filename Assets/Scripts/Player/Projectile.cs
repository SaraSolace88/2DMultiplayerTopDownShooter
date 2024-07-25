using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region fields
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public ObjectPool pool;
    [SerializeField] private float speed = 5;
    [SerializeField] private float activeTime = 5f;
    #endregion

    private void OnEnable()
    {
        StartCoroutine(nameof(WaitToDie));
    }

    void Update()
    {
        transform.localPosition += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StopCoroutine(nameof(WaitToDie));
        DestroyProjectile();
    }

    IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(activeTime);
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        pool.AddPooledObject(gameObject);
        gameObject.SetActive(false);
    }
}