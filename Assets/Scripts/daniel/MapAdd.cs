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
    void Awake()
    {
        // �ͦ��Ĥ@�Ӧa�Ϥ��q
        Transform previousMap = Instantiate(map[Random.Range(0, map.Length)].transform, mapNow.position, mapNow.rotation);
        previousMap.gameObject.SetActive(true);
        generatedMaps.Add(previousMap);

        // �ͦ���l���a�Ϥ��q
        for (int i = 1; i < numberOfMaps; i++)
        {
            // �p��s�a�Ϥ��q����m�A�o�̰��]�a�Ϥ��q�bx��V�W�̦��ƦC
            Vector3 newPosition = previousMap.position + new Vector3(previousMap.localScale.x, 0, 0); // ���]�bx��V�ƦC

            // �H����ܤ@�Ӧa�Ϲw�s��i��ͦ�
            Transform newMap = Instantiate(map[Random.Range(0, map.Length)].transform, newPosition, Quaternion.identity);
            newMap.gameObject.SetActive(true);
            generatedMaps.Add(newMap);

            // ��s previousMap �H�K�U�@���ͦ�
            previousMap = newMap;
        }
    }

    void Update()
    {
        // ���a�Ϥ��q�w�C�V������
        foreach (Transform mapTransform in generatedMaps)
        {
            mapTransform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
    }
}
