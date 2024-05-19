using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAdd : MonoBehaviour
{
    public MapPrefab[] map; // �s��a�Ϲw�s�󪺰}�C
    public Transform mapNow; // �Ĥ@�Ӧa�Ϥ��q����m
    public int numberOfMaps = 5; // �ͦ����a�Ϥ��q�ƶq
    public float moveSpeed = 2f; // �a�ϲ��ʳt��
    private List<Transform> generatedMaps = new List<Transform>();


    public Material material; // Reference to the material you want to control
    public Material material2; // Reference to the material you want to control
    public Material material3;
    public Material material_back;
    public Vector2 offsetSpeed = new Vector2(2f, 2f); // Speed of the UV offset change
    private Vector2 currentOffset = Vector2.zero;
    private Vector2 currentOffset2 = Vector2.zero;
    private Vector2 currentOffset3 = Vector2.zero;
    private Vector2 currentOffset_back = Vector2.zero;
    public float currentOffsetSpeed1 = 15f;
    public float currentOffsetSpeed2 = 15f;
    public float currentOffsetSpeed3 = 25f;
    public float currentOffsetSpeed_back = 15f;


    void Awake()
    {
        // // �ͦ��Ĥ@�Ӧa�Ϥ��q
        // Transform previousMap = Instantiate(map[Random.Range(0, map.Length)].transform, mapNow.position, mapNow.rotation);
        // previousMap.gameObject.SetActive(true);
        // generatedMaps.Add(previousMap);
        //
        // // �ͦ���l���a�Ϥ��q
        // for (int i = 1; i < numberOfMaps; i++)
        // {
        //     // �p��s�a�Ϥ��q����m�A�o�̰��]�a�Ϥ��q�bx��V�W�̦��ƦC
        //     Vector3 newPosition = previousMap.position + new Vector3(previousMap.localScale.x, 0, 0); // ���]�bx��V�ƦC
        //
        //     // �H����ܤ@�Ӧa�Ϲw�s��i��ͦ�
        //     Transform newMap = Instantiate(map[Random.Range(0, map.Length)].transform, newPosition, Quaternion.identity);
        //     newMap.gameObject.SetActive(true);
        //     generatedMaps.Add(newMap);
        //
        //     // ��s previousMap �H�K�U�@���ͦ�
        //     previousMap = newMap;
        // }
    }

    void Update()
    {
        // ���a�Ϥ��q�w�C�V������
        foreach (Transform mapTransform in generatedMaps)
        {
            mapTransform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }

        // Update the UV offset
        currentOffset += offsetSpeed * Time.deltaTime* currentOffsetSpeed1;
        currentOffset2 += offsetSpeed * Time.deltaTime* currentOffsetSpeed2;
        currentOffset3 += offsetSpeed * Time.deltaTime* currentOffsetSpeed3;
        currentOffset_back += offsetSpeed * Time.deltaTime* currentOffsetSpeed_back;
        // Apply the UV offset to the material
        material.mainTextureOffset = new Vector2(material.mainTextureOffset.x, currentOffset.y);

        material2.mainTextureOffset = new Vector2(currentOffset2.x, material2.mainTextureOffset.y);

        material3.mainTextureOffset = new Vector2(currentOffset3.x, material3.mainTextureOffset.y);

        material_back.mainTextureOffset = new Vector2(currentOffset_back.x, material_back.mainTextureOffset.y);

        if(currentOffset.x > 1)
        {
            currentOffset = new Vector2(0, currentOffset.y);
        }
        if(currentOffset.y > 1)
        {
            currentOffset = new Vector2(currentOffset.x, 0);
        }
        if(currentOffset2.y > 1)
        {
            currentOffset2 = new Vector2(currentOffset2.x, 0);
        }
        if(currentOffset2.x > 1)
        {
            currentOffset2 = new Vector2(0, currentOffset2.y);
        }
        if(currentOffset3.y > 1)
        {
            currentOffset3 = new Vector2(currentOffset3.x, 0);
        }
        if(currentOffset3.x > 1)
        {
            currentOffset3 = new Vector2(0, currentOffset3.y);
        }
        if(currentOffset_back.y > 1)
        {
            currentOffset_back = new Vector2(currentOffset_back.x, 0);
        }
    }
}
