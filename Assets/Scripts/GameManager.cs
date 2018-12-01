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
            int impactValue = 0;
            answer.impacts.ForEach(impact =>
            {
                switch(impact.deadlySin)
                {
                    case DeadlySin.LustChastity:
                        impactValue += personality.lustChastity > 50 ? (impact.value - personality.lustChastity) : (personality.lustChastity - impact.value);
                        break;
                    case DeadlySin.GluttonyTemperance:
                        impactValue += personality.gluttonyTemperance > 50 ? (impact.value - personality.gluttonyTemperance) : (personality.gluttonyTemperance - impact.value);
                        break;
                    case DeadlySin.GreedCharity:
                        impactValue += personality.greedCharity > 50 ? (impact.value - personality.greedCharity) : (personality.greedCharity - impact.value);
                        break;
                    case DeadlySin.SlothDiligence:
                        impactValue += personality.slothDiligence > 50 ? (impact.value - personality.slothDiligence) : (personality.slothDiligence - impact.value);
                        break;
                    case DeadlySin.WrathPatience:
                        impactValue += personality.wrathPatience > 50 ? (impact.value - personality.wrathPatience) : (personality.wrathPatience - impact.value);
                        break;
                    case DeadlySin.EnvyKindness:
                        impactValue += personality.envyKindness > 50 ? (impact.value - personality.envyKindness) : (personality.envyKindness - impact.value);
                        break;
                    case DeadlySin.PrideHumility:
                        impactValue += personality.prideHumility > 50 ? (impact.value - personality.prideHumility) : (personality.prideHumility - impact.value);
                        break;
                }
            });
            personality.satisfaction += impactValue;
        });
    }

    private void SelectNextQuestion()
    {
        currentQuestion = settings.PickQuestion();
        // Animate Next question
    }
}
