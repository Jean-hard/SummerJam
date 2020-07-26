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
    
    public Sprite backButton;

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

    public void SetupScreen()
    {
        //Les afficher à l'écran
        //Setup les valeurs numériques



        foreach (Candidat candidat in GameManager.Instance.pickedCandidats)
        {
            remainingCandidatsList.Add(candidat);   //On place tous les candidats dans une liste modifiable
        }

        ShuffleCandidatsList(); //On mélange la liste de candidats 

        currentCandidat = remainingCandidatsList[candidatsCounter];

        DisplayCurrentCandidat();

        UpdateCandidatCounterText();

        UpdateChoicesButtons();

        foreach (FichePoste fiche in GameManager.Instance.pickedFiches)
        {
            choiceList.Add(fiche.poste);
        }
    }

    public void UpdateCandidatCounterText()
    {
        candidatsCounterText.text = string.Format("{0}    {1}", candidatsCounter, GameManager.Instance.pickedCandidats.Count);
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

            DisplayCurrentCandidat();

            UpdateCandidatCounterText();
        }
        else
        {
            //Afficher écran de score
            GameManager.Instance.StopVoice();
            GameManager.Instance.DisplayScoreScreen();
            Debug.Log("FINITO");
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

    //Vérifie l'exactitude du choix du joueur en fonction du candidat présent
    public void CheckAnswer(Metier metierChoosed)
    {
        if(currentCandidat.metier == metierChoosed)
        {
            //Ajoute des primes
            GameManager.Instance.AddPrime();
        }
        else
        {
            //Prend un blame
            Debug.Log("Prend un blame");
        }

        //Next candidat
        LaunchNextCandidat();
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
}
