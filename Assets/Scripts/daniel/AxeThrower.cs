using CliffLeeCL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class AxeThrower : MonoBehaviour
{
    // 角色動畫
    public Animator animator;
    // 斧頭預置物
    public GameObject axePrefab;
    // 假武器預置物
    public GameObject FeintsAxe;
    // 拋射點
    public Transform throwPoint;
    // 拋射力道
    public float throwForce = 8f;
    // 上升力道
    public float upwardForce = 2f;
    // 冷卻時間
    public float cooldownTime = 2f;
    // 飛行速度
    public float speed = 0.8f;
    public float offset = 0.5f;
    // 冷卻計時器
    private float cooldownTimer;
    // 重力
    private float gravityScale = 1f;
    // 傷害
    private int damage = 10;

    // 是否開啟回力鏢
    public bool isBoomerang = false;
    // 可以使用軌跡武器
    public bool canUseTrajectoryWeapon = false;
    // 是否可以攻擊
    public bool canAtt = true;

    public AxeMode currentMode = AxeMode.Parabolic;
    private TrajectoryRenderer trajectoryRenderer;

    public enum AxeMode
    {
        Parabolic,
        Straight, 
    }

    private void OnEnable()
    {
        //監聽中控傳來技能升級
        EventManager.Instance.onChooseChaserSkill += ChooseAxeMode;
        EventManager.Instance.onGameOver += GameOver;
        EventManager.Instance.onGameStart += Remake;
        trajectoryRenderer = FindObjectOfType<TrajectoryRenderer>();
    }

    private void OnDisable()
    {
        // 取消監聽
        EventManager.Instance.onChooseChaserSkill -= ChooseAxeMode;
        EventManager.Instance.onGameOver -= GameOver;
        EventManager.Instance.onGameStart -= Remake;
    }

    void GameOver()
    {
        canAtt = false;
    }

    void Remake()
    {
        canAtt = true;
        throwForce = 9f;
        upwardForce = 2f;
        cooldownTime = 1f;
        speed = 0.8f;
        gravityScale = 1f;
        damage = 10;
        isBoomerang = false;
        canUseTrajectoryWeapon = false;
        currentMode = AxeMode.Parabolic;
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
              Debug.Log("直線");
              currentMode = AxeMode.Straight;
              break;
          case ChaserSkill.AxeFlyFaster:
                Debug.Log("快速飛行");
               //快速拋射
               speed *=1.7f;
               throwForce*=0.7f;
              break;
          case ChaserSkill.AxeThrowFaster:
                Debug.Log("CD--");
                //CD--
                cooldownTime /= 2;
                break;
          case ChaserSkill.AxeDamageUp:
                Debug.Log("傷害+");
               //傷害+
               damage *= 2;
              break;
          case ChaserSkill.AxeBoomerang:
                Debug.Log("回旋飛鏢");
               //回旋飛鏢
               isBoomerang = true;
              break;
          case ChaserSkill.FakeGhostWeapon:
               Debug.Log("假武器");
              //假武器
              canUseTrajectoryWeapon = true;
              break;
      }
    }

    void CanNotAtt()
    {
        canAtt = false;
        StartCoroutine(waitCanAtt());
    }

    IEnumerator waitCanAtt()
    {
        yield return new WaitForSeconds(1.0f);
        canAtt = true;
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        // 按下滑鼠左鍵並且冷卻時間小於等於0
        if (Mouse.current.leftButton.wasPressedThisFrame && cooldownTimer <= 0f && canAtt && !EventSystem.current.IsPointerOverGameObject())
        {
            StartCoroutine(ThrowAxe());
            cooldownTimer = cooldownTime;
        }
        // 按下滑鼠右鍵
        if (Mouse.current.rightButton.wasPressedThisFrame && !EventSystem.current.IsPointerOverGameObject())
        {
            animator.SetTrigger("Att");
            //可以使用軌跡武器
            if (canUseTrajectoryWeapon)
                Feints();
        }

    }

    // 拋出斧頭
    public IEnumerator ThrowAxe()
    {
        animator.SetTrigger("Att");
        yield return new WaitForSeconds(0.3f);
        GameObject axe = Instantiate(axePrefab, throwPoint.position, throwPoint.rotation);
        axe.transform.position = axe.transform.position + axe.transform.up * offset;
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
                    rb.gravityScale = 1;
                    rb.AddForce(parabolicDirection, ForceMode2D.Impulse);
                    axeScript.damage = damage;
                    break;
                case AxeMode.Straight:
                    rb.gravityScale = 0;
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
            axe.transform.Rotate(Vector3.forward * -5);
            yield return null;
        }
    }

    /// <summary>
    /// 迴力鏢
    /// </summary>
    /// <param name="axe"></param>
    /// <param name="returnPosition"></param>
    /// <returns></returns>
    IEnumerator BoomerangDart(GameObject axe, Vector3 returnPosition)
    {
        yield return new WaitForSeconds(1.1f);

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

    /// <summary>
    /// 假武器
    /// </summary>
    void Feints()
    {
        Debug.Log("Feints");
        GameObject axe = Instantiate(FeintsAxe, throwPoint.position, throwPoint.rotation);
        axe.transform.position = axe.transform.position + axe.transform.up * offset;
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

    /// <summary>
    /// 淡出斧頭
    /// </summary>
    /// <param name="axe"></param>
    /// <returns></returns>
    IEnumerator fadeOutAxe(GameObject axe)
    {
        Material material = axe.GetComponentInChildren<MeshRenderer>().materials[0];
        Debug.Log("Material successfully retrieved: " + material.name);
        Color color = material.color;
        color.a = 1.0f;
        material.color = color;

        while (material.color.a > 0)
        {
            color.a -= 0.03f;
            material.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(axe);
    }

}
