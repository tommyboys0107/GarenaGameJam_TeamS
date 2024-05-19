using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAdd : MonoBehaviour
{
    public MapPrefab[] map; // 存放地圖預製件的陣列
    public Transform mapNow; // 第一個地圖片段的位置
    public int numberOfMaps = 5; // 生成的地圖片段數量
    public float moveSpeed = 2f; // 地圖移動速度
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
        // 生成第一個地圖片段
        Transform previousMap = Instantiate(map[Random.Range(0, map.Length)].transform, mapNow.position, mapNow.rotation);
        previousMap.gameObject.SetActive(true);
        generatedMaps.Add(previousMap);

        // 生成其餘的地圖片段
        for (int i = 1; i < numberOfMaps; i++)
        {
            // 計算新地圖片段的位置，這裡假設地圖片段在x方向上依次排列
            Vector3 newPosition = previousMap.position + new Vector3(previousMap.localScale.x, 0, 0); // 假設在x方向排列

            // 隨機選擇一個地圖預製件進行生成
            Transform newMap = Instantiate(map[Random.Range(0, map.Length)].transform, newPosition, Quaternion.identity);
            newMap.gameObject.SetActive(true);
            generatedMaps.Add(newMap);

            // 更新 previousMap 以便下一次生成
            previousMap = newMap;
        }
    }

    void Update()
    {
        // 讓地圖片段緩慢向左移動
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
