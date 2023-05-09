using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterGrounding))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour, IMove
{
    [SerializeField]
    private float moveSpeed = 2;
    [SerializeField]
    private float jumpForce = 400;
    [SerializeField]
    private float wallJumpForce = 300;

    private Rigidbody2D rigidbody2d;
    private CharacterGrounding characterGrounding;

    public float Speed { get; private set; }

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        characterGrounding = GetComponent<CharacterGrounding>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && characterGrounding.IsGrounded)
        {
            rigidbody2d.AddForce(Vector2.up * jumpForce);
            if(characterGrounding.GroundedDirection != Vector2.down)
            {
                rigidbody2d.AddForce(characterGrounding.GroundedDirection * -1f * wallJumpForce);
            }
        }
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Speed = horizontal;

        Vector3 movement = new Vector3(horizontal, 0);

        transform.position += movement * Time.deltaTime * moveSpeed;
    }

    internal void Bounce()
    {
        rigidbody2d.AddForce(Vector2.up * jumpForce);
    }
}
