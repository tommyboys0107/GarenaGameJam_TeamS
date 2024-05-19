using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaySkill : MonoBehaviour
{
    public Character character;
    public PlayerController playerController;
    [Header("技能數值參數")]
    public Vector3 FlashLength;
    public int FlashCoolTime;
    public int healthNumber;
    public int SpeedNumber;
    public int shieldCoolTime;

    public int shieldTime;
    [Header("是否打開技能")]
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
            //刪掉普通跳，加載二段跳
            playerController.inputControl.GamePlay.Jump.started -= playerController.Jump;
            playerController.inputControl.GamePlay.Jump.started += newJump;
        }
    }


    public bool flashCollTime = false;
    public IEnumerator Flash()
    {
        if (this.canFlash)
        {
            flashCollTime = true;
            if (playerController.inputDirection.x > 0)
            {
                this.transform.position += FlashLength;


                transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1, 3), Mathf.Clamp(transform.position.y, 0, 4), 0);

            }
            if (playerController.inputDirection.x < 0)
            {
                this.transform.position -= FlashLength;

                transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1, 3), Mathf.Clamp(transform.position.y, 0, 4), 0);

            }
        }
        yield return new WaitForSeconds(FlashCoolTime);
        flashCollTime = false;
    }

    public void Health()
    {
        if (this.canHealth)
        {
            character.AddHealth(healthNumber);
            canHealth = false;
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
            playerController.rb.velocity = Vector2.zero; // 將線速度設置為零
            playerController.rb.angularVelocity = 0f;    // 將角速度設置為零
            playerController.rb.Sleep();                 // 讓剛體進入休眠狀態，以防止受到任何剩餘力的影響
            playerController.escapeAnimator.SetTrigger("Jump");
            playerController.rb.AddForce(transform.up * playerController.jumpForce, ForceMode2D.Impulse);
            extraJump--;
        }
    }

    public bool coolTime = false;

    public IEnumerator Shield()
    {
        if (canShield)
        {
            //攻擊到一次、時間到
            ShieldGO.SetActive(true);
            coolTime = true;
            yield return new WaitForSeconds(shieldTime);
            ShieldGO.SetActive(false);
            yield return new WaitForSeconds(shieldCoolTime);
            coolTime = false;
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
