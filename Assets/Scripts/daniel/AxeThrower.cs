using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AxeThrower : MonoBehaviour
{
    [Tooltip("���Y�w�m��")]
    public GameObject axePrefab;
    [Tooltip("���Y�o�g�I")]
    public Transform throwPoint;
    [Tooltip("�o�g�O�q")]
    public float throwForce = 7f;
    [Tooltip("�V�W�O�q")]
    public float upwardForce = 2f;
    [Tooltip("�N�o�ɶ�")]
    public float cooldownTime = 1f;
    private float cooldownTimer;
    [Tooltip("���Y�Ҧ�")]
    public AxeMode currentMode = AxeMode.Parabolic;

    public enum AxeMode
    {
        Parabolic,// ���q�ߪ��u
        Straight, // ���u
        FastParabolic // �ֳt�ߪ��u
    }


    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Mouse.current.leftButton.wasPressedThisFrame && cooldownTimer <= 0f)
        {
            ThrowAxe();
            cooldownTimer = cooldownTime;
        }

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    currentMode = AxeMode.Parabolic;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    currentMode = AxeMode.Straight;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    currentMode = AxeMode.FastParabolic;
        //}
    }

    public void ThrowAxe()
    {
        GameObject axe = Instantiate(axePrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody2D rb = axe.GetComponent<Rigidbody2D>();
        axe.SetActive(true);


        if (rb != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePosition.z = 0; 
            Vector2 direction = (mousePosition - throwPoint.position).normalized;

            switch (currentMode)
            {
                case AxeMode.Parabolic:
                    Vector2 parabolicDirection = new Vector2(direction.x * throwForce, direction.y * throwForce + upwardForce);
                    rb.gravityScale = 1; // ���`���O
                    rb.AddForce(parabolicDirection, ForceMode2D.Impulse);
                    cooldownTime = 1f;
                    break;
                case AxeMode.Straight:
                    rb.gravityScale = 0; // �T�έ��O
                    rb.velocity = direction * throwForce;
                    cooldownTime = 1f;
                    break;
                case AxeMode.FastParabolic:
                    Vector2 fastParabolicDirection = new Vector2(direction.x * throwForce * 2, direction.y * throwForce * 2 + upwardForce);
                    rb.gravityScale = 1; // ��_���O
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
