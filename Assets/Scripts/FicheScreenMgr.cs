using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FicheScreenMgr : MonoBehaviour
{
    public GameObject ficheObject;
    private List<Sprite> _ficheSpriteList = new List<Sprite>();

    private int ficheIndex = 0;

    void Start()
    {
        GameManager.Instance.ChooseRandomFicheAndCandidats();

        foreach(FichePoste fiche in GameManager.Instance.pickedFiches)
        {
            _ficheSpriteList.Add(fiche.visual);
        }

        if (!ficheObject || _ficheSpriteList.Count == 0) return;

        ficheObject.GetComponent<Image>().sprite = _ficheSpriteList[ficheIndex];
    }

    #region BUTTON FUNCTIONS
    public void NextFiche()
    {
        ficheIndex++;

        if(ficheIndex >= _ficheSpriteList.Count)
        {
            ficheIndex = 0;
        }

        ficheObject.GetComponent<Image>().sprite = _ficheSpriteList[ficheIndex]; 
    }

    public void PreviousFiche()
    {
        ficheIndex--;

        if (ficheIndex < 0)
        {
            ficheIndex = _ficheSpriteList.Count - 1;
        }

        ficheObject.GetComponent<Image>().sprite = _ficheSpriteList[ficheIndex];
    }
    #endregion

}
