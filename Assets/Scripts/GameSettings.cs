using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FeedbackEmoji
{
    public Sprite emoji;
    public int value;
}

[CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    public List<Question> questions;
    public List<Personality> personalities;
    public List<Sprite> satisfactionIndicators;
    public List<FeedbackEmoji> feedbackEmojis;
    public int satisfactionLimit = 1000;
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
