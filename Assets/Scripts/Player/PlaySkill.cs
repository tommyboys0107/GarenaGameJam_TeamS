using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaySkill : MonoBehaviour
{
    public Character character;
    public PlayerController playerController;
    [Header("�ޯ�ƭȰѼ�")]
    public Vector3 FlashLength;
    public int FlashCoolTime;
    public int healthNumber;
    public int SpeedNumber;
    public int ShieldCoolTime;

    public float shieldTime;
    [Header("�O�_���}�ޯ�")]
    public bool canFlash;
    public bool canHealth;
    public bool canSpeed;
    public bool canShield;
    public bool canToubleJump;
    public bool speedCoolDown = false;

    public GameObject ShieldGO;
    public void reloadSkill()
    {
        if (canToubleJump)
        {
            //�R�����q���A�[���G�q��
            playerController.inputControl.GamePlay.Jump.started -= playerController.Jump;
            playerController.inputControl.GamePlay.Jump.started += newJump;
        }
    }



    public void Flash()
    {
        if (this.canFlash)
        {
            if (playerController.inputDirection.x > 0)
            {
                this.transform.position += FlashLength;
            }
            if (playerController.inputDirection.x < 0)
            {
                this.transform.position -= FlashLength;
            }
        }
    }

    public void Health()
    {
        if (this.canHealth)
        {
            character.AddHealth(healthNumber);
        }
    }

    public void AddSpeed()
    {
        float orignSpeed = playerController.Speed;
        float newSpeed = playerController.Speed + SpeedNumber;
        playerController.Speed = newSpeed;
    }


    private int extraJump;
    private void newJump(InputAction.CallbackContext obj)
    {
        if (playerController.physicsCheck.isGround)
        {
            extraJump = 2;
        }
        if (extraJump > 0)
        {
            playerController.rb.velocity = Vector2.zero; // �N�u�t�׳]�m���s
            playerController.rb.angularVelocity = 0f;    // �N���t�׳]�m���s
            playerController.rb.Sleep();                 // ������i�J��v���A�A�H����������Ѿl�O���v�T
            playerController.escapeAnimator.SetTrigger("Jump");
            playerController.rb.AddForce(transform.up * playerController.jumpForce, ForceMode2D.Impulse);
            extraJump--;
        }
    }

    public IEnumerator Shield()
    {
        if (canShield)
        {
            //������@���B�ɶ���
            ShieldGO.SetActive(true);
            yield return new WaitForSeconds(shieldTime);
            ShieldGO.SetActive(false);
            Debug.LogError("1234");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            ShieldGO.SetActive(false);
            //Destroy(ShieldGO);
        }
    }




}
