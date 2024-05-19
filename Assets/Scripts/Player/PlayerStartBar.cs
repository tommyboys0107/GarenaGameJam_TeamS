using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStartBar : MonoBehaviour
{
    public Image HealthImage;
    public Image HealthDailyImage;

    public void OnhealthChange(float persentage)
    {
        HealthImage.fillAmount = persentage;
    }
}
