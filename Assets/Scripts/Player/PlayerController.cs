using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    public Rigidbody2D rb;
    public CapsuleCollider2D collider2D;
    public PhysicsCheck physicsCheck;
    public PlaySkill playSkill;

    public Vector2 inputDirection;
    [Header("基本參數")]
    public float Speed;
    public float jumpForce;
    private void Awake()
    {
        inputControl = new PlayerInputControl();
        collider2D = GetComponent<CapsuleCollider2D>();
        physicsCheck = GetComponent<PhysicsCheck>();

        inputControl.GamePlay.Jump.started += Jump;
        //inputControl.GamePlay.ShowText.performed += ShowMessage;
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        //jkl
        inputControl.Disable();
    }

    private void Update()
    {
        this.inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            //IncreaseColliderSize();
            Debug.LogError("1234");
        }


        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            playSkill.Flash();
        }
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            playSkill.Health();
        }
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            if (!playSkill.speedCoolDown)
            {
                StartCoroutine(playSkill.AddSpeed());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogError(collision.name);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        rb.velocity = new Vector2(this.inputDirection.x * Speed * Time.deltaTime, rb.velocity.y);
    }

    private void ShowMessage(InputAction.CallbackContext context)
    {
        Debug.LogError("A1 被按下了！");
        //displayText.text = "1 被按下了！";
    }


    public float sizeChangeAmount = 10.0f;

    void IncreaseColliderSize()
    {
        collider2D.size += new Vector2(sizeChangeAmount, sizeChangeAmount);
    }

    public void Jump(InputAction.CallbackContext obj)
    {
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
