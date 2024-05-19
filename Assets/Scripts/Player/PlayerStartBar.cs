using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStartBar : MonoBehaviour
{
    public Image HealthImage;
    public Image HealthDailyImage;

    private void Update()
    {
        if (HealthDailyImage.fillAmount > HealthImage.fillAmount)
        {
            HealthDailyImage.fillAmount -= Time.deltaTime;
        }
    }

    public void OnhealthChange(float persentage)
    {
        HealthImage.fillAmount = persentage;
    }
}
