using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("�˴��Ѽ�")]
    public Vector2 buttomOffset;
    public float checkRaduis;
    public LayerMask groundLayer;
    [Header("���A")]
    public bool isGround;


    public void Update()
    {
        check();
    }


    public void check()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + buttomOffset, checkRaduis, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + buttomOffset, checkRaduis);
    }
}
