using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData", order = 2)]
public class CharacterData : ScriptableObject
{
    public float HealthPoint { get; set; }
    public float DefensePoint { get; set; }
    public int Attack { get; set; }
}
