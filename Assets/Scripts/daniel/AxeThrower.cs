using CliffLeeCL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AxeThrower : MonoBehaviour
{
    // 斧頭預置物
    public GameObject axePrefab;
    // 假武器預置物
    public GameObject FeintsAxe;
    // 拋射點
    public Transform throwPoint;
    // 拋射力道
    public float throwForce = 7f;
    // 上升力道
    public float upwardForce = 2f;
    // 冷卻時間
    public float cooldownTime = 1f;
    // 冷卻計時器
    private float cooldownTimer;
    // 重力
    private float gravityScale = 1f;
    // 傷害
    private int damage = 10;
    // 飛行速度
    private float speed = 1f;
    // 是否開啟回力鏢
    public bool isBoomerang = false;
    // 可以使用軌跡武器
    public bool canUseTrajectoryWeapon = false;

    public AxeMode currentMode = AxeMode.Parabolic;

    public enum AxeMode
    {
        Parabolic,
        Straight, 
        FastParabolic ,
        DecreaseCD ,
        DamageADD, 
        BoomerangDart 
    }

    private void OnEnable()
    {
        //監聽中控傳來技能升級
        EventManager.Instance.onChooseChaserSkill += ChooseAxeMode;
    }

    private void OnDisable()
    {
        // 取消監聽
        EventManager.Instance.onChooseChaserSkill -= ChooseAxeMode;
    }

    /// <summary>
    /// 技能升級
    /// </summary>
    /// <param name="skill"></param>
    void ChooseAxeMode(ChaserSkill skill)
    {
      switch (skill)
        {
          case ChaserSkill.AxeStraightThrow:
              //直線
              currentMode = AxeMode.Straight;
              break;
          case ChaserSkill.AxeFlyFaster:
               //快速拋射
               speed = 2f;
              break;
          case ChaserSkill.AxeThrowFaster:
               //CD--
               cooldownTime = 0.5f;
               break;
          case ChaserSkill.AxeDamageUp:
               //傷害+
               damage = 20;
              break;
          case ChaserSkill.AxeBoomerang:
               //回旋飛鏢
               isBoomerang = true;
              break;
          case ChaserSkill.FakeGhostWeapon:
              //假武器
              canUseTrajectoryWeapon = true;
              break;
      }
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        // 按下滑鼠左鍵並且冷卻時間小於等於0
        if (Mouse.current.leftButton.wasPressedThisFrame && cooldownTimer <= 0f)
        {
            ThrowAxe();
            cooldownTimer = cooldownTime;
        }
        // 按下滑鼠右鍵
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            //可以使用軌跡武器
            if (canUseTrajectoryWeapon)
                Feints();
        }
    }

    // 拋出斧頭
    public void ThrowAxe()
    {
        GameObject axe = Instantiate(axePrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody2D rb = axe.GetComponent<Rigidbody2D>();
        axe.transform.rotation = Quaternion.Euler(0, 0, 0);
        axe.SetActive(true);
        StartCoroutine(AxeRevolve(axe));
        Axe axeScript = axe.GetComponent<Axe>();

        if (rb != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePosition.z = 0;
            Vector2 direction = (mousePosition - throwPoint.position).normalized;

            switch (currentMode)
            {
                case AxeMode.Parabolic:
                    Vector2 parabolicDirection = new Vector2(direction.x * throwForce * speed, direction.y * throwForce* speed+ upwardForce);
                    rb.gravityScale = gravityScale;
                    rb.AddForce(parabolicDirection, ForceMode2D.Impulse);
                    axeScript.damage = damage;
                    break;
                case AxeMode.Straight:
                    rb.gravityScale = gravityScale;
                    rb.velocity = direction * throwForce * speed;
                    axeScript.damage = damage;
                    break;
            }

            if (isBoomerang)
            {
                StartCoroutine(BoomerangDart(axe, throwPoint.position));
            }
        }
        else
        {
            Debug.LogError("Rigidbody2D not found on the axe prefab.");
        }
    }

    /// <summary>
    /// 旋轉斧頭
    /// </summary>
    /// <param name="axe"></param>
    /// <returns></returns>
    IEnumerator AxeRevolve(GameObject axe)
    {
        while (axe != null)
        {
            axe.transform.Rotate(Vector3.forward * 10);
            yield return null;
        }
    }

    // 回力鏢
    IEnumerator BoomerangDart(GameObject axe, Vector3 returnPosition)
    {
        yield return new WaitForSeconds(1.0f);

        if (axe == null)
        {
            yield break;
        }

        Rigidbody2D rb = axe.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0;
            Vector2 directionBack = (returnPosition - axe.transform.position).normalized;
            rb.velocity = directionBack * throwForce * 4;

            while (axe != null && (returnPosition - axe.transform.position).magnitude > 0.1f)
            {
                yield return null;
            }

            if (axe != null)
            {
                Destroy(axe);
            }
        }
    }

    // 假武器
    void Feints()
    {
        Debug.Log("Feints");
        GameObject axe = Instantiate(FeintsAxe, throwPoint.position, throwPoint.rotation);
        Rigidbody2D rb = axe.GetComponent<Rigidbody2D>();
        axe.transform.rotation = Quaternion.Euler(0, 0, 0);
        axe.SetActive(true);
        StartCoroutine(AxeRevolve(axe));
        StartCoroutine(fadeOutAxe(axe));

        if (rb != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePosition.z = 0;
            Vector2 direction = (mousePosition - throwPoint.position).normalized;

            switch (currentMode)
            {
                case AxeMode.Parabolic:
                    Vector2 parabolicDirection = new Vector2(direction.x * throwForce * speed, direction.y * throwForce * speed + upwardForce);
                    rb.gravityScale = gravityScale;
                    rb.AddForce(parabolicDirection, ForceMode2D.Impulse);
                    break;
                case AxeMode.Straight:
                    rb.gravityScale = gravityScale;
                    rb.velocity = direction * throwForce * speed;
                    break;
            }

            if (isBoomerang)
            {
                StartCoroutine(BoomerangDart(axe, throwPoint.position));
            }
        }
        else
        {
            Debug.LogError("Rigidbody2D not found on the axe prefab.");
        }
    }

    // 淡出斧頭
    IEnumerator fadeOutAxe(GameObject axe)
    {
        Material material = axe.GetComponentInChildren<MeshRenderer>().materials[0];
        Debug.Log("Material successfully retrieved: " + material.name);
        Color color = material.color;
        color.a = 1.0f;
        material.color = color;

        while (material.color.a > 0)
        {
            color.a -= 0.01f;
            material.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(axe);
    }

}
