using CliffLeeCL;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    public Rigidbody2D rb;
    public CapsuleCollider2D collider2D;
    public PhysicsCheck physicsCheck;
    public PlaySkill playSkill;


    public Vector2 inputDirection;
    [Header("�򥻰Ѽ�")]
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
        EventManager.Instance.onChooseEscaperSkill += OpenSkill;
    }

    void OpenSkill(EscaperSkill skill) {
        switch (skill)
        {
            case EscaperSkill.Flash:
                playSkill.canFlash = true;
                Debug.LogError("OpenFlash");
                break;
            case EscaperSkill.Heal:
                playSkill.canHealth = true;
                Debug.LogError("OpenFHeal");
                break;
            case EscaperSkill.SpeedUp:
                playSkill.canSpeed = true;
                Debug.LogError("OpenSpeedUp");
                break;
            case EscaperSkill.Shield:
                playSkill.canShield = true;
                Debug.LogError("OpenFlash");
                break;
            case EscaperSkill.DoubleJump:
                playSkill.canToubleJump = true;
                playSkill.reloadSkill();
                Debug.LogError("OpenFlash");
                break;
        }
    }

    private void OnDisable()
    {
        //jkl
        inputControl.Disable();
    }

    private void Update()
    {
        this.inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            EventManager.Instance.OnChooseEscaperSkill(EscaperSkill.Flash);
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            EventManager.Instance.OnChooseEscaperSkill(EscaperSkill.SpeedUp);
        }
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            EventManager.Instance.OnChooseEscaperSkill(EscaperSkill.DoubleJump);
        }
        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            EventManager.Instance.OnChooseEscaperSkill(EscaperSkill.Shield);
        }


        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            EventManager.Instance.OnUseEscaperSkill(EscaperSkill.Flash, playSkill.FlashCoolTime);
            playSkill.Flash();
        }
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            EventManager.Instance.OnUseEscaperSkill(EscaperSkill.Heal, 0);
            playSkill.Health();
        }
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            EventManager.Instance.OnUseEscaperSkill(EscaperSkill.Shield, playSkill.ShieldCoolTime);
            StartCoroutine(playSkill.Shield());
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

    void aa()
    {
    }

    public void Move()
    {
        rb.velocity = new Vector2(this.inputDirection.x * Speed * Time.deltaTime, rb.velocity.y);
    }

    private void ShowMessage(InputAction.CallbackContext context)
    {
        Debug.LogError("A1 �Q���U�F�I");
        //displayText.text = "1 �Q���U�F�I";
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
