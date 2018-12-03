using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Personality", menuName = "Personality")]
public class Personality : ScriptableObject
{
    [HideInInspector]
    public int satisfaction = 50;
    public string nickname;
    public Sprite visual;
    [Range(0, 10)]
    public int lustChastity = 5;
    [Range(0, 10)]
    public int gluttonyTemperance = 5;
    [Range(0, 10)]
    public int greedCharity = 5;
    [Range(0, 10)]
    public int slothDiligence = 5;
    [Range(0, 10)]
    public int wrathPatience = 5;
    [Range(0, 10)]
    public int envyKindness = 5;
    [Range(0, 10)]
    public int prideHumility = 5;
}
