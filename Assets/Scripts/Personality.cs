using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Personality", menuName = "Personality")]
public class Personality : ScriptableObject
{
    private int satisfaction = 50;
    public string name;
    public GameObject visual;
    [Range(0, 100)]
    public int pride = 50;
    [Range(0, 100)]
    public int greed = 50;
    [Range(0, 100)]
    public int lust = 50;
    [Range(0, 100)]
    public int envy = 50;
    [Range(0, 100)]
    public int glouttony = 50;
    [Range(0, 100)]
    public int wrath = 50;
    [Range(0, 100)]
    public int sloth = 50;
}
