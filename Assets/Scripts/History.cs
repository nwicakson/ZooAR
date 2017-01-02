using UnityEngine;
using UnityEngine.UI;
using LitJson;
using UnityEngine.SceneManagement;
using System.Collections;

public class History : MonoBehaviour {

    public GameObject[] animal, animalShadow;
    public Text[] animalName, animalScore;
    private JsonData prettyJson;
    private int animalCount;

    // Use this for initialization
    void Start () {
        TextAsset file = Resources.Load<TextAsset>("setting");
        prettyJson = JsonMapper.ToObject(file.text);
        animalCount = prettyJson["animal"].Count;
        for (int i=0; i<animalCount; i++)
        {
            if (PlayerPrefs.HasKey(prettyJson["animal"][i]["name"] + "Highscore"))
            {
                animalShadow[i].SetActive(false);
                animal[i].SetActive(true);
                animalName[i].text = prettyJson["animal"][i]["name"].ToString();
                animalScore[i].text = PlayerPrefs.GetInt(prettyJson["animal"][i]["name"] + "Highscore").ToString();
            }
            else
            {
                animal[i].SetActive(false);
                animalShadow[i].SetActive(true);
                animalName[i].text = "???";
                animalScore[i].text = 0.ToString();
            }
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Back();
    }
    

    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }
	
	
}
