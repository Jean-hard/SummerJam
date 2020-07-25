using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("DATABASE")]
    public List<FichePoste> fichesList = new List<FichePoste>();
    public List<Candidat> candidatsList = new List<Candidat>();
    private List<FichePoste> workFichesList = new List<FichePoste>();
    private List<Candidat> currentCandidatsList = new List<Candidat>();
    private List<FichePoste> pickedFiches = new List<FichePoste>();
    private List<Candidat> pickedCandidats = new List<Candidat>();

    [Header("SCENE OBJECTS")]
    public GameObject ficheScreen;
    public GameObject candidatsScreen;

    [Header("TIMER")]
    public Text timerText;
    public List<float> timerStartValues = new List<float>();
    private float timerValue = 0;

    [Header("VALUES")]
    public int fichesMin = 2;
    public int fichesMax = 3;
    public int candidatsMin = 2;
    public int candidatsMax = 5;
    private int fichesNb = 0;
    private int candidatsNb = 0;    

    private bool timerIsRunning = false;
    private bool isStartValueUpdated = false;
    private bool screenChanged = false;

    #region Singleton Pattern
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }
    #endregion

    private void Start()
    {
        //ChooseRandomFicheAndCandidats();
        
        foreach (FichePoste fiche in fichesList)
            workFichesList.Add(fiche);            

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

        if(Input.GetKeyDown(KeyCode.Space))
            ChooseRandomFicheAndCandidats();
    }

    public void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ChooseRandomFicheAndCandidats()
    {
        fichesNb = Random.Range(fichesMin, fichesMax + 1);
        candidatsNb = Random.Range(candidatsMin, candidatsMax + 1);

        Debug.Log("Nb fiches : " + fichesNb);
        Debug.Log("Nb candidats : " + candidatsNb);

        //On choisit un certain nb de fiches
        for (int i = 0; i < fichesNb; i++)
        {
            FichePoste fiche = PickUnusedFiche();
            pickedFiches.Add(fiche);
            Debug.Log("Fiche picked : " + fiche.name);
        }

        //Pour chaque fiche, on ajoute X candidats associés à la fiche, à la liste de candidats choisis
        foreach(FichePoste fiche in pickedFiches)
        {
            currentCandidatsList = new List<Candidat>();
            foreach(Candidat candidat in fiche.linkedCandidatsList) //Update la liste de candidats de la liste courante
            {
                currentCandidatsList.Add(candidat);
            }

            for (int i = 0; i < candidatsNb; i++)   //On ajoute X candidats par fiche à la liste de candidats choisis
            {
                Candidat candidat = PickUnusedCandidat();
                pickedCandidats.Add(candidat);
                Debug.Log("Candidat picked : " + candidat.name);
            }
        }

        Debug.Log("Nb candidats choisis : " + pickedCandidats.Count);
    }

    public FichePoste PickUnusedFiche()
    {        
        int index = Random.Range(0, workFichesList.Count);  //On choisit un index random une fois

        FichePoste pickedFiche = workFichesList[index];

        workFichesList.RemoveAt(index);

        return pickedFiche;
    }

    public Candidat PickUnusedCandidat()
    {
        int index = Random.Range(0, currentCandidatsList.Count);  //On choisit un index random une fois

        Candidat pickedCandidat = currentCandidatsList[index];

        currentCandidatsList.RemoveAt(index);

        return pickedCandidat;
    }
}
