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
    public Animator escapeAnimator;

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

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, 1), Mathf.Clamp(transform.position.y, 0, 1), 0);
    }

    private void OnEnable()
    {
        playSkill.coolTime = false;

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

        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            EventManager.Instance.OnUseEscaperSkill(EscaperSkill.Flash, playSkill.FlashCoolTime);
            playSkill.Flash();
        }
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            EventManager.Instance.OnUseEscaperSkill(EscaperSkill.Heal, 9999);
            playSkill.Health();
        }
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            if (!playSkill.coolTime)
            {
                EventManager.Instance.OnUseEscaperSkill(EscaperSkill.Shield, playSkill.shieldTime);
                StartCoroutine(playSkill.Shield());
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
        //0~5
        //newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);


        rb.velocity = new Vector2(this.inputDirection.x * Speed * Time.deltaTime, rb.velocity.y);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1, 3), Mathf.Clamp(transform.position.y, 0, 4), 0);
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
            escapeAnimator.SetTrigger("Jump");
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
