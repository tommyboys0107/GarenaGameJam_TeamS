using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlaySkill : MonoBehaviour
{
    public Character character;
    public PlayerController playerController;
    [Header("技能數值參數")]
    public Vector3 FlashLength;
    public float FlashCoolTime;
    public int healthNumber;
    public int SpeedNumber;
    public int SpeedTime;
    public float SpeedCoolTime;

    public bool canToubleJump;

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
}
