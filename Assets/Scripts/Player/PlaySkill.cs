using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaySkill : MonoBehaviour
{
    public Character character;
    public PlayerController playerController;
    [Header("�ޯ�ƭȰѼ�")]
    public Vector3 FlashLength;
    public float FlashCoolTime;
    public int healthNumber;
    public int SpeedNumber;
    public int SpeedTime;
    public float SpeedCoolTime;

    public float shieldHeart;
    public float shieldTime;


    public bool canToubleJump;


    public void reloadSkill()
    {
        //�R�����q���A�[���G�q��
        playerController.inputControl.GamePlay.Jump.started -= playerController.Jump;
        playerController.inputControl.GamePlay.Jump.started += newJump;


    }



    public void Flash()
    {
        this.transform.position += FlashLength;
    }

    public void Health()
    {
        character.AddHealth(healthNumber);
    }

    public bool speedCoolDown;
    public IEnumerator AddSpeed()
    {
        float orignSpeed = playerController.Speed;
        float newSpeed = playerController.Speed + SpeedNumber;
        playerController.Speed = newSpeed;
        speedCoolDown = true;
        yield return new WaitForSeconds(SpeedTime);
        speedCoolDown = false;
        playerController.Speed = orignSpeed;
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
            playerController.rb.AddForce(transform.up * playerController.jumpForce, ForceMode2D.Impulse);
            extraJump--;
        }
    }

    public IEnumerator Shield()
    {
        yield return null;
    }

}
