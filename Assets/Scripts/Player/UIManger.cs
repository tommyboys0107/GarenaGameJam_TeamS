using CliffLeeCL;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManger : MonoBehaviour
{
    public CharacterSO healthEvent;
    public PlayerStartBar playerStartBar;

    public GameObject GG;
    public GameObject cWin;
    public GameObject eWin;


    public void OnEnable()
    {
        healthEvent.OnEventRaised += OnhealthEvent;
    }

    public void OnDisable()
    {
        healthEvent.OnEventRaised -= OnhealthEvent;
    }


    private void OnhealthEvent(Character character)
    {
        var persentage = character.currentHealth / character.maxHealth;
        if (character.currentHealth <= 0)
        {
            EventManager.Instance.OnGameOver();
            GG.SetActive(true);
            cWin.SetActive(true);
        }

        playerStartBar.OnhealthChange(persentage);
    }

    public void Restart()
    {
        Debug.LogError("重新開始");
        GG.SetActive(false);
        cWin.SetActive(false);
        eWin.SetActive(false);
        EventManager.Instance.OnReStart();
    }
}
