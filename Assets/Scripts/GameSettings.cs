using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    public List<Question> questions;
    public List<Personality> personalities;
    public List<Sprite> satisfactionIndicators;
    public int satisfactionLimit = 100;
    public float satisfactionMultiplier = 1.5f;

    private List<Question> seenQuestions = new List<Question>();
    private List<Personality> seenPersonalities = new List<Personality>();

    public Question PickQuestion()
    {
        if (questions.Count == 0)
        {
            questions.AddRange(seenQuestions);
        }
        int index = Random.Range(0, questions.Count);
        Question tmp = questions[index];
        questions.RemoveAt(index);
        seenQuestions.Add(tmp);
        Debug.Log(questions);
        return tmp;
    }

    public Personality PickPersonality()
    {
        if (personalities.Count == 0)
        {
            personalities.AddRange(seenPersonalities);
        }
        int index = Random.Range(0, personalities.Count);
        Personality tmp = personalities[index];
        personalities.RemoveAt(index);
        seenPersonalities.Add(tmp);
        return tmp;
    }

    public void Reset()
    {
        if (seenQuestions.Count > 0)
        {
            questions.AddRange(seenQuestions);
        }
        if (seenPersonalities.Count > 0)
        {
            personalities.AddRange(seenPersonalities);
        }
    }
}
