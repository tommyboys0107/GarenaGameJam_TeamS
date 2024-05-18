using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrower : MonoBehaviour
{
    [Tooltip("斧頭預置物")]
    public GameObject axePrefab;
    [Tooltip("斧頭發射點")]
    public Transform throwPoint;
    [Tooltip("發射力量")]
    public float throwForce = 7f;
    [Tooltip("向上力量")]
    public float upwardForce = 2f;
    [Tooltip("冷卻時間")]
    public float cooldownTime = 1f;
    private float cooldownTimer;
    [Tooltip("斧頭模式")]
    public AxeMode currentMode = AxeMode.Parabolic;

    public enum AxeMode
    {
        Parabolic,// 普通拋物線
        Straight, // 直線
        FastParabolic // 快速拋物線
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && cooldownTimer <= 0f) 
        {
            ThrowAxe();
            cooldownTimer = cooldownTime;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentMode = AxeMode.Parabolic;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentMode = AxeMode.Straight;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentMode = AxeMode.FastParabolic;
        }
    }

    void ThrowAxe()
    {
        GameObject axe = Instantiate(axePrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody2D rb = axe.GetComponent<Rigidbody2D>();
        axe.SetActive(true);


        if (rb != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; 
            Vector2 direction = (mousePosition - throwPoint.position).normalized;

            switch (currentMode)
            {
                case AxeMode.Parabolic:
                    Vector2 parabolicDirection = new Vector2(direction.x * throwForce, direction.y * throwForce + upwardForce);
                    rb.gravityScale = 1; // 恢复重力
                    rb.AddForce(parabolicDirection, ForceMode2D.Impulse);
                    cooldownTime = 1f;
                    break;
                case AxeMode.Straight:
                    rb.gravityScale = 0; // 禁用重力
                    rb.velocity = direction * throwForce;
                    cooldownTime = 1f;
                    break;
                case AxeMode.FastParabolic:
                    Vector2 fastParabolicDirection = new Vector2(direction.x * throwForce * 2, direction.y * throwForce * 2 + upwardForce);
                    rb.gravityScale = 1; // 恢復重力
                    rb.AddForce(fastParabolicDirection, ForceMode2D.Impulse);
                    cooldownTime = 0.5f;
                    break;
            }
        }
        else
        {
            Debug.LogError("Rigidbody2D not found on the axe prefab.");
        }
    }
}
