using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.Collections;

public class History : MonoBehaviour {

    public GameObject[] animal;
    public GameObject[] animalShadow;
    public Text[] animalScore;
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
            }
            else
            {
                animal[i].SetActive(false);
                animalShadow[i].SetActive(true);
            }
        }
        

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
