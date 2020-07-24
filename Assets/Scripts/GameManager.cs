using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ficheScreen;
    public GameObject candidatsScreen;

    public Text timerText;
    public List<float> timerStartValues = new List<float>();
    private float timerValue = 0;

    private bool timerIsRunning = false;
    private bool isStartValueUpdated = false;
    private bool screenChanged = false;

    private void Start()
    {
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStartValueUpdated)
        {
            if (ficheScreen.activeInHierarchy)
            {
                timerValue = timerStartValues[0];
            }
            else if (candidatsScreen.activeInHierarchy)
            {
                timerValue = timerStartValues[1];
            }
            else Debug.Log("Probleme avec le active in hierarchy");

            isStartValueUpdated = true;
            screenChanged = false;
        }

        if(timerIsRunning)
        {
            if (timerValue > 0)
            {
                timerValue -= Time.deltaTime;
                DisplayTime(timerValue);
            }
            else
            {
                Debug.Log("Fin du timer !");

                if (screenChanged) return;

                if (ficheScreen.activeInHierarchy)
                {
                    ficheScreen.SetActive(false);
                    candidatsScreen.SetActive(true);
                    isStartValueUpdated = false;
                    screenChanged = true;
                }
                else if (candidatsScreen.activeInHierarchy)
                {
                    candidatsScreen.SetActive(false);
                    Debug.Log("Afficher score final");
                    screenChanged = true;
                }
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
