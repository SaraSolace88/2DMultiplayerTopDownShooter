using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region fields
    private InputSystem pInput;
    [SerializeField] private Vector3 constraints;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed = 10, Bspeed = 20;
    [HideInInspector] public Vector3 dirHistory { get; private set; } = new Vector3(1, 0, 0);
    private bool BspeedBoost;
    #endregion

    //Subscription/Unsubcription
    private void OnEnable()
    {
        pInput = new InputSystem();
        pInput.Enable();
    }
    private void OnDisable()
    {
        pInput.Disable();
    }
    
    //Update position per frame.
    private void Update()
    {
        direction.x = pInput.Player.Movement.ReadValue<Vector2>().x;
        direction.y = pInput.Player.Movement.ReadValue<Vector2>().y;
        direction.z = 0;

        if (BspeedBoost)
        {
            transform.localPosition += direction * Bspeed * Time.deltaTime;
        }
        else
        {
            transform.localPosition += direction * speed * Time.deltaTime;
        }

        if(direction != Vector3.zero)
        {
            dirHistory = direction;
        }

        ConstrainMovement();
    }

    //Constrains Movement to camera's view
    private void ConstrainMovement()
    {
        Vector3 currentPosition = transform.localPosition;

        //Upper constraint
        if (currentPosition.y >= constraints.y)
            currentPosition.y = constraints.y;
        //Lower constraint
        else if (currentPosition.y <= -constraints.y)
            currentPosition.y = -constraints.y;
        //Right constraint
        if (currentPosition.x >= constraints.x)
            currentPosition.x = constraints.x;
        //Left constraint
        else if (currentPosition.x <= -constraints.x)
            currentPosition.x = -constraints.x;

        currentPosition.z = constraints.z;

        transform.localPosition = currentPosition;
    }
}