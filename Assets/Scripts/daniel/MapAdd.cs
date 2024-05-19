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
    }
}
