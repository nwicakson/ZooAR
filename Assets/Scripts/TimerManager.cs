using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerManager : MonoBehaviour {

    private float time;
    public GameObject panelTimeUp;
    public Play playScript;
    public Text textTimer;
    private bool stopTimer;

    // Use this for initialization
    public void Start () {
        time = 10f;
        textTimer.text = time.ToString();
        stopTimer = true;
        panelTimeUp.SetActive(false);
	}

    public int GetTime()
    {
        return Mathf.RoundToInt(time);
    }

    public void StartTimer(bool start)
    {
        stopTimer = !start;
    }
	
	// Update is called once per frame
	public void Update () {
        if(!stopTimer)
        {
            float timeLeft = time - Time.deltaTime;
            if (timeLeft <= 0)
            {
                timeLeft = 0;
                stopTimer = true;
                panelTimeUp.SetActive(true);
                playScript.TimeUp();
            }
            time -= Time.deltaTime;
            textTimer.text = timeLeft.ToString();
        }
    }
}
