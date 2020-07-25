using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandidatScreenMgr : MonoBehaviour
{
    [Header("SCENE OBJECTS")]
    public GameObject candidatAvatar;
    public List<GameObject> choiceButtons;
    //public List<GameObject> remainingCandidatsAvatar;
    public Text candidatsCounterText;
    public AudioSource audio;

    private int candidatsCounter = 0;
    private List<Candidat> remainingCandidatsList = new List<Candidat>();
    private Candidat currentCandidat;

    private bool isCandidatUpdated = false;

    private void Update()
    {
        if(GameManager.Instance.candidatsScreen.activeInHierarchy && !isCandidatUpdated)
        {
            isCandidatUpdated = true;
            SetupGUIValues();            
        }
    }

    public void SetupGUIValues()
    {
        //Les afficher à l'écran
        //Setup les valeurs numériques

        foreach(Candidat candidat in GameManager.Instance.pickedCandidats)
        {
            remainingCandidatsList.Add(candidat);   //On place tous les candidats dans une liste modifiable
        }

        currentCandidat = remainingCandidatsList[0];

        DisplayCurrentCandidat();

        UpdateCandidatCounterText();
    }

    public void UpdateCandidatCounterText()
    {
        candidatsCounterText.text = string.Format("{0} / {1}", candidatsCounter, GameManager.Instance.pickedCandidats.Count);
    }

    //Affiche l'avatar, joue le son du candidat courrant
    public void DisplayCurrentCandidat()
    {
        candidatAvatar.GetComponent<Image>().sprite = currentCandidat.avatar;
        audio.clip = currentCandidat.voice;
        audio.Play();
    }

    //Met à jour la liste de choix (buttons)
    public void UpdateChoicesButtons()
    {

    }
}
