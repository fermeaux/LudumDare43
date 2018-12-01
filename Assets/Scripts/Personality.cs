using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Personality", menuName = "Personality")]
public class Personality : ScriptableObject
{
    [HideInInspector]
    public int satisfaction = 50;
    public string nickname;
    public GameObject visual;
    [Range(0, 100)]
    public int lustChastity = 50;
    [Range(0, 100)]
    public int gluttonyTemperance = 50;
    [Range(0, 100)]
    public int greedCharity = 50;
    [Range(0, 100)]
    public int slothDiligence = 50;
    [Range(0, 100)]
    public int wrathPatience = 50;
    [Range(0, 100)]
    public int envyKindness = 50;
    [Range(0, 100)]
    public int prideHumility = 50;
}
