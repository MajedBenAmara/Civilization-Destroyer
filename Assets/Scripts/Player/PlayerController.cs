using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float PlayerSpeed = 8f;
    public float PlayerYVelocity = -4;
    public float MinXPosition, MaxXPosition;

    [HideInInspector] public bool CanMove = false;

    private Rigidbody2D _rB;
    private float _horizontalInput;

    private void Start()
    {
        _rB = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        LimitePlayerHorizontalMovement();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Changing _horizontalInput value based on player input  
    public void GetHorizontalInput(InputAction.CallbackContext context)
    {
        if (CanMove)
            _horizontalInput = context.ReadValue<Vector2>().x;
    }

    private void MovePlayer()
    {
        _rB.velocity = new Vector2(_horizontalInput * PlayerSpeed,Mathf.Clamp(PlayerYVelocity, -35f, -2f));
    }

    private void LimitePlayerHorizontalMovement()
    {
        float playerXposition;
        playerXposition = transform.position.x;
        playerXposition = Mathf.Clamp(playerXposition, MinXPosition, MaxXPosition);

        transform.position = new Vector3(playerXposition, transform.position.y, transform.position.z);
    }
}
