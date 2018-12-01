using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameSettings settings;

    private List<Personality> personalities;
    private Question currentQuestion;

    public void LaunchGame()
    {
        settings.Reset();
        SelectPersonalities();
        SelectNextQuestion();
    }

    public void SelectLeftAnswer()
    {
        ApplyAnswer(currentQuestion.left);
        if (CheckLoose())
        {
            Loose();
        } else if (CheckWin())
        {
            Win();
        } else
        {
            SelectNextQuestion();
        }
    }

    public void SelectRightAnswer()
    {
        ApplyAnswer(currentQuestion.right);
        if (CheckLoose())
        {
            Loose();
        }
        else if (CheckWin())
        {
            Win();
        }
        else
        {
            SelectNextQuestion();
        }
    }

    private void SelectPersonalities()
    {
        personalities = new List<Personality>();
        for (int i = 0; i < 4; i++)
        {
            personalities.Add(settings.PickPersonality());
        }
    }

    private bool CheckWin()
    {
        return personalities.TrueForAll(personality => personality.satisfaction >= settings.satisfactionLimit);
    }

    private bool CheckLoose()
    {
        return personalities.Exists(personality => personality.satisfaction <= 0);
    }

    private void Win()
    {
        Debug.Log("You win");
    }

    private void Loose()
    {
        Debug.Log("You loose");
    }

    private void ApplyAnswer(Answer answer)
    {
        personalities.ForEach(personality =>
        {
            float impactValue = 0f;
            answer.impacts.ForEach(impact =>
            {
                switch(impact.deadlySin)
                {
                    case DeadlySin.LustChastity:
                        impactValue += GetImpactValue(personality.lustChastity, impact.value);
                        break;
                    case DeadlySin.GluttonyTemperance:
                        impactValue += GetImpactValue(personality.gluttonyTemperance, impact.value);
                        break;
                    case DeadlySin.GreedCharity:
                        impactValue += GetImpactValue(personality.greedCharity, impact.value);
                        break;
                    case DeadlySin.SlothDiligence:
                        impactValue += GetImpactValue(personality.slothDiligence, impact.value);
                        break;
                    case DeadlySin.WrathPatience:
                        impactValue += GetImpactValue(personality.wrathPatience, impact.value);
                        break;
                    case DeadlySin.EnvyKindness:
                        impactValue += GetImpactValue(personality.envyKindness, impact.value);
                        break;
                    case DeadlySin.PrideHumility:
                        impactValue += GetImpactValue(personality.prideHumility, impact.value);
                        break;
                }
            });
            personality.satisfaction += Mathf.CeilToInt(impactValue);
        });
    }

    private float GetImpactValue(int personalityValue, int impactValue)
    {
        float result = personalityValue > 50 ? (impactValue - personalityValue) : (personalityValue - impactValue);
        if (result > 0)
        {
            result *= settings.satisfactionMultiplier;
        }
        return result;
    }

    private void SelectNextQuestion()
    {
        currentQuestion = settings.PickQuestion();
        // Animate Next question
    }
}
