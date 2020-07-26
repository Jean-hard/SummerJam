using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreenMgr : MonoBehaviour
{
    public GameObject messageBox;
    public List<Sprite> messageBoxList = new List<Sprite>();
    public Text primeFinale;

    #region Singleton Pattern
    private static GameOverScreenMgr _instance;

    public static GameOverScreenMgr Instance { get { return _instance; } }
    #endregion

    void Awake()
    {
        if (_instance == null)
        {

            _instance = this;
        }
    }


    public void DisplayMessageAndScore(bool gameWon)
    {
        if(gameWon)
        {
            messageBox.GetComponent<Image>().sprite = messageBoxList[0];
        }
        else
        {
            messageBox.GetComponent<Image>().sprite = messageBoxList[1];
        }


        int finalPrime;
        if(GameManager.Instance.timerValue >= 10 && gameWon)
            finalPrime = GameManager.Instance.prime * ((int)GameManager.Instance.timerValue / 10);
        else
            finalPrime = GameManager.Instance.prime;

        primeFinale.text = "Prime : " + finalPrime.ToString("00000");
    }
}
