using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManger : MonoBehaviour
{
    public CharacterSO healthEvent;
    public PlayerStartBar playerStartBar;

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
        playerStartBar.OnhealthChange(persentage);
    }
}
