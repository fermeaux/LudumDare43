using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DeadlySin
{
    LustChastity = 1,
    GluttonyTemperance,
    GreedCharity,
    SlothDiligence,
    WrathPatience,
    EnvyKindness,
    PrideHumility
}

[System.Serializable]
public class Impact
{
    public DeadlySin deadlySin;
    [Range(0, 10)]
    public int value = 5;
}

[System.Serializable]
public class Answer
{
    [TextArea]
    public string answer;
    public List<Impact> impacts;
}

[CreateAssetMenu(fileName = "New Question", menuName = "Question")]
public class Question : ScriptableObject {
    [TextArea]
    public string question;
    public Answer left;
    public Answer right;
}
