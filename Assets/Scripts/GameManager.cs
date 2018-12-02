using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameSettings settings;
    [Header("References")]
    public Text questionText;
    public Image answerLeftImage;
    public Image answerRightImage;
    public Text answerLeftText;
    public Text answerRightText;
    public List<Image> personalitiesSpawn;
    public List<Image> satisfactionIndicatorsSpawn;
    public List<Image> feedbackEmojisSpawn;

    private List<Question> questionsAvailable;
    private List<Personality> personalitiesAvailable;
    private Question currentQuestion;
    private List<Personality> personalities;
    private List<Question> seenQuestions = new List<Question>();
    private List<Personality> seenPersonalities = new List<Personality>();

    public void Awake()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    public void Start()
    {
        personalitiesAvailable = new List<Personality>(settings.personalities);
        questionsAvailable = new List<Question>(settings.questions);
        LaunchGame();
    }

    public void Update()
    {
        Vector3 pos = Input.mousePosition;
        float a = Mathf.Min(Mathf.Abs(Screen.width / 2 - pos.x), Screen.width / 4) / (Screen.width / 4);
        if (pos.x < Screen.width / 2)
        {
            answerLeftImage.color = new Color(1, 1, 1, a);
            answerLeftText.color = new Color(0, 0, 0, a);
            answerLeftImage.gameObject.SetActive(true);
            answerRightImage.gameObject.SetActive(false);
        }
        else
        {
            answerRightImage.color = new Color(1, 1, 1, a);
            answerRightText.color = new Color(0, 0, 0, a);
            answerLeftImage.gameObject.SetActive(false);
            answerRightImage.gameObject.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (pos.x < Screen.width / 4)
            {
                SelectLeftAnswer();
            } else if (pos.x > Screen.width * 3 / 4)
            {
                SelectRightAnswer();
            }
        }
    }

    public void LaunchGame()
    {
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
            Personality tmp = PickPersonality();
            personalities.Add(tmp);
            personalitiesSpawn[i].sprite = tmp.visual;
        }
        UpdateSatisfactionIndicators();
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
            UpdateFeedbackEmojis(personality, impactValue);
        });
        UpdateSatisfactionIndicators();
    }

    private float GetImpactValue(int personalityValue, int impactValue)
    {
        float result = personalityValue > 50 ? (impactValue - personalityValue) : (personalityValue < 50 ? (personalityValue - impactValue) : 0);
        if (result > 0)
        {
            result *= settings.satisfactionMultiplier;
        }
        return result;
    }

    private void SelectNextQuestion()
    {
        currentQuestion = PickQuestion();
        questionText.text = currentQuestion.question;
        answerLeftText.text = currentQuestion.left.answer;
        answerRightText.text = currentQuestion.right.answer;
    }

    private Question PickQuestion()
    {
        if (questionsAvailable.Count == 0)
        {
            questionsAvailable.AddRange(seenQuestions);
        }
        int index = Random.Range(0, questionsAvailable.Count);
        Question tmp = questionsAvailable[index];
        questionsAvailable.RemoveAt(index);
        seenQuestions.Add(tmp);
        return tmp;
    }

    private Personality PickPersonality()
    {
        if (personalitiesAvailable.Count == 0)
        {
            personalitiesAvailable.AddRange(seenPersonalities);
        }
        int index = Random.Range(0, personalitiesAvailable.Count);
        Personality tmp = personalitiesAvailable[index];
        personalitiesAvailable.RemoveAt(index);
        seenPersonalities.Add(tmp);
        return tmp;
    }

    private void Reset()
    {
        questionsAvailable.AddRange(seenQuestions);
        personalitiesAvailable.AddRange(seenPersonalities);
    }

    private void UpdateSatisfactionIndicators()
    {
        for (int i = 0; i < 4; i++)
        {
            int value = Mathf.FloorToInt((float)personalities[i].satisfaction * satisfactionIndicatorsSpawn.Count / settings.satisfactionLimit);
            satisfactionIndicatorsSpawn[i].sprite = settings.satisfactionIndicators[value];
        }
    }

    private void UpdateFeedbackEmojis(Personality personality, float impactValue)
    {
        for (int i = 0; i < settings.feedbackEmojis.Count; i++)
        {
            if (impactValue < settings.feedbackEmojis[i].value || settings.feedbackEmojis.Count - 1 == i)
            {
                Debug.Log(i);
                feedbackEmojisSpawn[personalities.IndexOf(personality)].sprite = settings.feedbackEmojis[i].emoji;
                return;
            }
        }
    }
}
