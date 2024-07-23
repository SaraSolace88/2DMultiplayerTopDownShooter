using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour, Weapon
{
    #region fields
    [SerializeField] private GameObject projectile;
    [SerializeField] private float weaponRate = .5f;
    private InputSystem pInput;
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
        GameObject g = Instantiate(projectile, transform.position, Quaternion.identity);
        g.GetComponent<Projectile>().direction = gameObject.GetComponent<PlayerMovement>().dirHistory;
    }

    IEnumerator Firing()
    {
        while (bActive)
        {
            yield return new WaitForEndOfFrame();
            if (bFire)
            {
                Attack();
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