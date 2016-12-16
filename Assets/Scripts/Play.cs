using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;
using System.Collections;
using System.Collections.Generic;


public class Play : MonoBehaviour {

    public GameObject panelScanning, panelScanned, panelQuiz, panelInfo, btnQuiz, btnInfo;
    public Text textAnimalName, textInfo;
    public GameObject imagePrevInfo, imageNextInfo;
    public Text textQuestion, textAnswer, textResult;
    public GameObject panelQuestion, panelAnswer, panelResult;
    public AudioClip sfx;
    public AudioClip backgroundMusic;
    public AudioClip[] animalSounds;
    private SoundManager soundManager;
    private JsonData prettyJson;
    private int animalCount;
    private List<string> info = new List<string>();
    private int infoIndex;
    private List<string> question = new List<string>();
    private List<bool> answerBool = new List<bool>();
    private List<string> answer = new List<string>();
    private int questionIndex;
    private int score;

    // Use this for initialization
    void Start ()
    {
        TextAsset file = Resources.Load<TextAsset>("setting");
        prettyJson = JsonMapper.ToObject(file.text);
        animalCount = prettyJson["animal"].Count;
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager.PlayBackground(backgroundMusic);
        panelScanned.SetActive(false);
        btnInfo.SetActive(false);
        panelScanning.SetActive(true);
        btnQuiz.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Exit()
    {
        soundManager.PlaySoundEffect(sfx);
        SceneManager.LoadScene("Main Menu");
    }

    public void Scanning()
    {
        panelScanned.SetActive(false);
        panelScanning.SetActive(true);
    }

    public void Scanned(string animalName)
    {
        panelScanning.SetActive(false);
        panelScanned.SetActive(true);
        panelInfo.SetActive(true);
        panelQuiz.SetActive(false);
        for(int i=0; i<animalCount; i++)
        {
            if(prettyJson["animal"][i]["name"].ToString() == animalName)
            {
                textAnimalName.text = animalName;
                PlayerPrefs.SetInt(animalName + "Highscore", 0);
                info.Clear();
                for (int j = 0; j < prettyJson["animal"][i]["info"].Count; j++)
                    info.Add(prettyJson["animal"][i]["info"][j].ToString());
                infoIndex = 0;
                checkIndexInfo();
                textInfo.text = info[infoIndex];
                question.Clear();
                answerBool.Clear();
                answer.Clear();
                for (int j = 0; j < prettyJson["animal"][i]["listQuestion"].Count; j++)
                {
                    question.Add(prettyJson["animal"][i]["listQuestion"][j]["question"].ToString());
                    answerBool.Add((bool) prettyJson["animal"][i]["listQuestion"][j]["answerBool"]);
                    answer.Add(prettyJson["animal"][i]["listQuestion"][j]["answer"].ToString());
                }
                questionIndex = 0;
                textQuestion.text = question[questionIndex];
                textAnswer.text = answer[questionIndex];
            }
        }
        score = 0;
    }

    public void nextInfo()
    {
        infoIndex++;
        checkIndexInfo();
        textInfo.text = info[infoIndex];
    }

    public void prevInfo()
    {
        infoIndex--;
        checkIndexInfo();
        textInfo.text = info[infoIndex];
    }

    public void checkIndexInfo()
    {
        if (infoIndex == 0)
            imagePrevInfo.SetActive(false);
        else
            imagePrevInfo.SetActive(true);
        if (infoIndex == info.Count - 1)
            imageNextInfo.SetActive(false);
        else
            imageNextInfo.SetActive(true);
    }

    public void ClearPanelAnswer()
    {
        panelAnswer.SetActive(false);
        panelQuestion.SetActive(true);
        NextQuestion();
    }

    private void NextQuestion()
    {
        questionIndex++;
        if (questionIndex + 1 > question.Count)
        {
            panelQuestion.SetActive(false);
            panelResult.SetActive(true);
            textResult.text = "Kamu mendapat " + score + " Point";
            if (PlayerPrefs.HasKey(textAnimalName.text + "Highscore") && PlayerPrefs.GetInt(textAnimalName.text + "Highscore") < score)
                PlayerPrefs.SetInt(textAnimalName.text + "Highscore", score);
        }
        else
        {
            textQuestion.text = question[questionIndex];
            textAnswer.text = answer[questionIndex];
        }
    }

    public void ClearPanelResult()
    {
        panelResult.SetActive(false);
        Scanning();
    }

    public void Answer(bool ans)
    {
        if (answerBool[questionIndex] != ans)
        {
            panelQuestion.SetActive(false);
            //sound salah
            panelAnswer.SetActive(true);
        }
        else
        {
            //sound benar
            score++;
            NextQuestion();
        }
    }

    public void Quiz()
    {
        panelInfo.SetActive(false);
        btnQuiz.SetActive(false);
        panelQuiz.SetActive(true);
        panelQuestion.SetActive(true);
        panelAnswer.SetActive(false);
        panelResult.SetActive(false);
        btnInfo.SetActive(true);
    }

    public void Info()
    {
        panelQuiz.SetActive(false);
        btnInfo.SetActive(false);
        panelInfo.SetActive(true);
        btnQuiz.SetActive(true);
    }

    public void PlaysSoundAnimal()
    {
        for(int i=0; i<animalCount; i++)
        {
            if(prettyJson["animal"][i]["name"].ToString() == textAnimalName.text)
                soundManager.PlaySoundEffect(animalSounds[i]);
        }
    }
}
