using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    public UnityAction<Character> OnEventRaised;
    public void RaisEvent(Character character)
    {
        OnEventRaised(character);
    }
}
