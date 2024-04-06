using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManger : MonoBehaviour
{
    PlayerControls playerControls;
    AnimationManager animatorManager;

    public Vector2 movementInput;
    private float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    private void Awake()
    {
        animatorManager = GetComponent<AnimationManager>();
    }


    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            // Registering event handlers for movement input
            playerControls.PlayerMovement.Movement.performed += i => OnMovementInput(i.ReadValue<Vector2>());
            playerControls.PlayerMovement.Movement.canceled += i => OnMovementInput(Vector2.zero);
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void OnMovementInput(Vector2 input)
    {
        movementInput = input;
        HandleMovementInput();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) * Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount);
    }
}
