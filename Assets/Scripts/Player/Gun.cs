using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour, Weapon
{
    #region fields
    [SerializeField] private ObjectPool pool;
    [SerializeField] private float weaponRate = .5f;
    private InputSystem pInput;
    private GameObject currBullet;
    private bool bActive, bFire;
    #endregion

    //Subscription/Unsubcription
    private void OnEnable()
    {
        pInput = new InputSystem();
        pInput.Enable();
        pInput.Player.Attack.performed += FireOn;
        pInput.Player.Attack.canceled += FireOff;

        bActive = true;
        StartCoroutine(nameof(Firing));
    }
    private void OnDisable()
    {
        pInput.Disable();
        pInput.Player.Attack.performed -= FireOn;
        pInput.Player.Attack.canceled -= FireOff;
    }



    private void Attack()
    {
        currBullet.gameObject.layer = gameObject.layer;
        currBullet.tag = gameObject.tag;
        currBullet.gameObject.transform.position = gameObject.transform.position + gameObject.GetComponent<PlayerMovement>().dirHistory;
        currBullet.GetComponent<Projectile>().pool = pool;
        currBullet.GetComponent<Projectile>().direction = gameObject.GetComponent<PlayerMovement>().dirHistory;
        currBullet.gameObject.SetActive(true);
    }

    IEnumerator Firing()
    {
        while (bActive)
        {
            yield return new WaitForEndOfFrame();
            if (bFire)
            {
                currBullet = pool.GetPooledObject();
                if (currBullet)
                {
                    
                    Attack();
                }
                yield return new WaitForSeconds(weaponRate);
            }
        }
    }

    private void FireOn(InputAction.CallbackContext c)
    {
        bFire = true;
    }
    private void FireOff(InputAction.CallbackContext c)
    {
        bFire = false;
    }
}