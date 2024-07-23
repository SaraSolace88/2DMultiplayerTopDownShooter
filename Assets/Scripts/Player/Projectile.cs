using System.Collections;
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

    IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(activeTime);
        pool.AddPooledObject(gameObject);
        gameObject.SetActive(false);
    }
}