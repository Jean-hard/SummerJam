﻿using System.Collections;
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

    [Header("MAIN WINDOW")]
    public GameObject mainWindow;
    public List<Sprite> mainWindowSprites = new List<Sprite>();

    [Header("BLAME")]
    public GameObject blamePopUp;
    public Text blameCounterText;
    
    public Sprite backButton;

    private int blameCounter = 0;
    private int candidatsCounter = 0;
    private int nbChoices = 0;

    private List<Candidat> remainingCandidatsList = new List<Candidat>();
    private Candidat currentCandidat;

    private List<Metier> choiceList = new List<Metier>();

    private bool isCandidatUpdated = false;

    private void Update()
    {
        if (GameManager.Instance.candidatsScreen.activeInHierarchy && !isCandidatUpdated)
        {
            isCandidatUpdated = true;
            SetupScreen();
        }
    }

    //Set l'UI à l'ouverture du screen candidats
    public void SetupScreen()
    {
        foreach (Candidat candidat in GameManager.Instance.pickedCandidats)
        {
            remainingCandidatsList.Add(candidat);   //On place tous les candidats dans une liste modifiable
        }

        ShuffleCandidatsList(); //On mélange la liste de candidats 

        currentCandidat = remainingCandidatsList[candidatsCounter];

        DisplayCurrentCandidat();
        StartCoroutine(SpeakAnim());

        UpdateCandidatCounterText();

        UpdateChoicesButtons();

        foreach (FichePoste fiche in GameManager.Instance.pickedFiches)
        {
            choiceList.Add(fiche.poste);
        }
    }

    public void UpdateCandidatCounterText()
    {
        candidatsCounterText.text = string.Format("{0} / {1}", candidatsCounter, GameManager.Instance.pickedCandidats.Count);
    }

    //Affiche l'avatar, joue le son du candidat courrant
    public void DisplayCurrentCandidat()
    {
        candidatAvatar.GetComponent<Image>().sprite = currentCandidat.avatar;
        GameManager.Instance.PlayVoice(currentCandidat.voice);
    }

    //Affiche le candidat suivant
    public void LaunchNextCandidat()
    {
        candidatsCounter++;

        if (candidatsCounter < GameManager.Instance.pickedCandidats.Count)
        {
            currentCandidat = remainingCandidatsList[candidatsCounter];
            
            StartCoroutine(SpeakAnim());
            DisplayCurrentCandidat();

            UpdateCandidatCounterText();            
        }
        else
        {
            //Afficher écran de score
            GameManager.Instance.StopVoice();
            GameManager.Instance.DisplayScoreScreen(true);
            Debug.Log("WIN");
        }
    }

    //Met à jour la liste de choix (buttons)
    public void UpdateChoicesButtons()
    {
        nbChoices = GameManager.Instance.pickedFiches.Count;
        int index = 0;

        foreach(GameObject button in choiceButtons)
        {

            if(nbChoices > 0)
            {
                button.SetActive(true);
                button.GetComponent<Image>().sprite = backButton;
                button.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = GameManager.Instance.pickedFiches[index].logo;
                nbChoices--;
                index++;
            }
            else
            {
                button.SetActive(false);
            }
        }
    }

    public IEnumerator LaunchBlame()
    {
        GameManager.Instance.StopVoice();
        GameManager.Instance.PlaySfx(GameManager.Instance.sfxList[0]);
        blamePopUp.SetActive(true);
        blameCounterText.text = string.Format("{0} / 3", blameCounter);        
        yield return new WaitForSeconds(3f);
        blamePopUp.SetActive(false);

        if (blameCounter < 3)
            LaunchNextCandidat();
        else
            GameManager.Instance.DisplayScoreScreen(false);
    }

    //Vérifie l'exactitude du choix du joueur en fonction du candidat présent
    public void CheckAnswer(Metier metierChoosed)
    {
        GameManager.Instance.PlaySfx(GameManager.Instance.sfxList[1]);
        if(currentCandidat.metier == metierChoosed)
        {
            //Ajoute des primes
            GameManager.Instance.AddPrime();
            //Next candidat
            LaunchNextCandidat();
        }
        else
        {
            //Prend un blame
            blameCounter++;
            GameManager.Instance.AddBlameMalus();
            StartCoroutine(LaunchBlame());
        }
    }



    //Clique sur le choix 1
    public void ChooseOne()
    {
        CheckAnswer(choiceList[0]);
    }

    //Clique sur le choix 2
    public void ChooseTwo()
    {
        CheckAnswer(choiceList[1]);
    }
    
    //Clique sur le choix 3
    public void ChooseThree()
    {
        CheckAnswer(choiceList[2]);
    }
    
    //Clique sur le choix 4
    public void ChooseFour()
    {
        CheckAnswer(choiceList[3]);
    }

    //Clique sur le choix 5
    public void ChooseFive()
    {
        CheckAnswer(choiceList[4]);
    }

    public void ShuffleCandidatsList()
    {
        for (int i = 0; i < remainingCandidatsList.Count; i++)
        {
            Candidat temp = remainingCandidatsList[i];
            int randomIndex = Random.Range(i, remainingCandidatsList.Count);
            remainingCandidatsList[i] = remainingCandidatsList[randomIndex];
            remainingCandidatsList[randomIndex] = temp;
        }
    }

    IEnumerator SpeakAnim()
    {
        mainWindow.GetComponent<Image>().sprite = mainWindowSprites[1];
        yield return new WaitForSeconds(1.5f);
        mainWindow.GetComponent<Image>().sprite = mainWindowSprites[0];
        yield return new WaitForSeconds(.5f);
        mainWindow.GetComponent<Image>().sprite = mainWindowSprites[1];
        yield return new WaitForSeconds(1f);
        mainWindow.GetComponent<Image>().sprite = mainWindowSprites[0];
        yield return new WaitForSeconds(.2f);
        mainWindow.GetComponent<Image>().sprite = mainWindowSprites[1];
        yield return new WaitForSeconds(.5f);
        mainWindow.GetComponent<Image>().sprite = mainWindowSprites[0];
        yield return new WaitForSeconds(.2f);
        mainWindow.GetComponent<Image>().sprite = mainWindowSprites[1];
        yield return new WaitForSeconds(2f);
        mainWindow.GetComponent<Image>().sprite = mainWindowSprites[0];
        yield return new WaitForSeconds(.5f);
        mainWindow.GetComponent<Image>().sprite = mainWindowSprites[1];
        yield return new WaitForSeconds(1.5f);
        mainWindow.GetComponent<Image>().sprite = mainWindowSprites[0];
        yield return new WaitForSeconds(.5f);
        mainWindow.GetComponent<Image>().sprite = mainWindowSprites[1];
        yield return new WaitForSeconds(1f);
        mainWindow.GetComponent<Image>().sprite = mainWindowSprites[0];
        yield return new WaitForSeconds(.2f);
        mainWindow.GetComponent<Image>().sprite = mainWindowSprites[1];
        yield return new WaitForSeconds(.5f);
        mainWindow.GetComponent<Image>().sprite = mainWindowSprites[0];
        yield return new WaitForSeconds(.2f);
        mainWindow.GetComponent<Image>().sprite = mainWindowSprites[1];
        yield return new WaitForSeconds(2f);
        mainWindow.GetComponent<Image>().sprite = mainWindowSprites[0];
    }
}
