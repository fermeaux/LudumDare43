﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    public List<Question> questions;
    public List<Personality> personalities;

    private List<Question> seenQuestions;
    private List<Personality> seenPersonalities;

    public Question PickQuestion()
    {
        int index = Random.Range(0, questions.Count);
        Question tmp = questions[index];
        questions.RemoveAt(index);
        seenQuestions.Add(tmp);
        return tmp;
    }

    public Personality PickPersonality()
    {
        int index = Random.Range(0, personalities.Count);
        Personality tmp = personalities[index];
        personalities.RemoveAt(index);
        seenPersonalities.Add(tmp);
        return tmp;
    }

    public void Reset()
    {
        questions.AddRange(seenQuestions);
        personalities.AddRange(seenPersonalities);
    }
}