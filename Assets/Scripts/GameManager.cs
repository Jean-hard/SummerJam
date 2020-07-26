using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("DATABASE")]
    public List<FichePoste> fichesList = new List<FichePoste>();
    //public List<Candidat> candidatsList = new List<Candidat>();
    private List<FichePoste> workFichesList = new List<FichePoste>();
    private List<Candidat> currentCandidatsList = new List<Candidat>();
    [HideInInspector]
    public List<FichePoste> pickedFiches = new List<FichePoste>();
    [HideInInspector]
    public List<Candidat> pickedCandidats = new List<Candidat>();
    public List<Sprite> hoverLogoSpritesList = new List<Sprite>();
    public List<Metier> hoverLogoMetierList = new List<Metier>();


    [Header("SCENE OBJECTS")]
    public GameObject startScreen;
    public GameObject ficheScreen;
    public GameObject candidatsScreen;
    public GameObject gameOverScreen;
    public Text metierTitle;
    

    [Header("SOUNDS")]
    public AudioSource audioVoices;
    public AudioSource audioSfx;
    public AudioSource audioMusic;
    public List<AudioClip> sfxList = new List<AudioClip>();
    public AudioClip music;

    [Header("TIMER")]
    public GameObject timerBox;
    public Text timerText;
    public List<float> timerStartValues = new List<float>();
    public float timerValue = 0;

    [Header("VALUES")]
    public int fichesMin = 2;
    public int fichesMax = 3;
    public int candidatsMin = 2;
    public int candidatsMax = 5;
    private int fichesNb = 0;
    private int candidatsNb = 0;

    [Header("POINTS")]
    public GameObject primePanel;
    public Text primeText;
    [HideInInspector]
    public int prime = 0;
    public int primeBonus = 100;
    public int blameMalus = 50;

    [Header("CURSOR")]
    public Texture2D pointerSprite;

    private bool timerIsRunning = false;
    private bool isStartValueUpdated = true;
    private bool screenChanged = false;

    #region Singleton Pattern
    private static GameManager _instance;
    

    public static GameManager Instance { get { return _instance; } }
    #endregion

    void Awake()
    {
        if (_instance == null)
        {

            _instance = this;
        }
    }

    private void Start()
    {
        Cursor.SetCursor(pointerSprite, UnityEngine.Vector2.zero, CursorMode.ForceSoftware);
        ChooseRandomFicheAndCandidats();

        PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStartValueUpdated)
        {
            if (candidatsScreen.activeInHierarchy)
            {
                timerValue = timerStartValues[1] * pickedCandidats.Count;
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
                PlaySfx(sfxList[3]);

                if (screenChanged) return;

                if (ficheScreen.activeInHierarchy)
                {
                    ficheScreen.SetActive(false);
                    candidatsScreen.SetActive(true);
                    primePanel.SetActive(true);
                    isStartValueUpdated = false;
                    screenChanged = true;
                }
                else if (candidatsScreen.activeInHierarchy)
                {
                    StopVoice();
                    DisplayScoreScreen(false);
                    screenChanged = true;
                }
            }
        }
    }

    //Appelé par le bouton Play du start screen
    public void StartGame()
    {
        PlaySfx(sfxList[1]);

        timerValue = timerStartValues[0] * pickedFiches.Count;

        screenChanged = false;

        startScreen.SetActive(false);
        ficheScreen.SetActive(true);
        timerBox.SetActive(true);
        timerIsRunning = true;
    }

    public void AddPrime()
    {
        prime += primeBonus;
        UpdatePrimeText();

        PlaySfx(sfxList[2]);
    }

    public void AddBlameMalus()
    {
        prime -= blameMalus;
        UpdatePrimeText();
    }

    public void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    //Appelée par FicheScreenMgr au start
    public void ChooseRandomFicheAndCandidats()
    {
        foreach (FichePoste fiche in fichesList)
            workFichesList.Add(fiche);

        fichesNb = UnityEngine.Random.Range(fichesMin, fichesMax + 1);
        candidatsNb = UnityEngine.Random.Range(candidatsMin, candidatsMax + 1);

        Debug.Log("Nb fiches : " + fichesNb);
        Debug.Log("Nb candidats : " + candidatsNb);

        //On choisit un certain nb de fiches
        for (int i = 0; i < fichesNb; i++)
        {
            FichePoste fiche = PickUnusedFiche();
            pickedFiches.Add(fiche);
            //Debug.Log("Fiche picked : " + fiche.name);
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
                //Debug.Log("Candidat picked : " + candidat.name);
            }
        }

        Debug.Log("Nb candidats choisis : " + pickedCandidats.Count);
    }

    public FichePoste PickUnusedFiche()
    {        
        int index = UnityEngine.Random.Range(0, workFichesList.Count);  //On choisit un index random une fois

        FichePoste pickedFiche = workFichesList[index];

        workFichesList.RemoveAt(index);

        return pickedFiche;
    }

    public Candidat PickUnusedCandidat()
    {
        int index = UnityEngine.Random.Range(0, currentCandidatsList.Count);  //On choisit un index random une fois

        Candidat pickedCandidat = currentCandidatsList[index];

        currentCandidatsList.RemoveAt(index);

        return pickedCandidat;
    }

    public void DisplayScoreScreen(bool gameWon)
    {
        candidatsScreen.SetActive(false);
        timerBox.SetActive(false);
        primePanel.SetActive(false);
        gameOverScreen.SetActive(true);
        GameOverScreenMgr.Instance.DisplayMessageAndScore(gameWon);
    }

    public void UpdatePrimeText() 
    {
        primeText.text = prime.ToString("0000");
    }

    public void UpdateBtnName(FichePoste fiche)
    {
        metierTitle.text = fiche.metierName;
    }

    public void PlayVoice(AudioClip clip)
    {
        audioVoices.clip = clip;
        audioVoices.Play();
    }

    public void StopVoice()
    {
        audioVoices.Stop();
    }

    public void PlaySfx(AudioClip clip)
    {
        audioSfx.clip = clip;
        audioSfx.Play();
    }

    public void StopSfx()
    {
        audioSfx.Stop();
    }

    public void PlayMusic()
    {
        audioMusic.clip = music;
        audioMusic.Play();
    }

    public void StopMusic()
    {
        audioMusic.Stop();
    }
}
