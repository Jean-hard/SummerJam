using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FicheScreenMgr : MonoBehaviour
{
    public GameObject fiche;
    public List<Sprite> ficheSpriteList = new List<Sprite>();

    private int ficheIndex = 0;

    void Start()
    {
        if (!fiche || ficheSpriteList.Count == 0) return;

        fiche.GetComponent<Image>().sprite = ficheSpriteList[ficheIndex];
    }

    #region BUTTON FUNCTIONS
    public void NextFiche()
    {
        ficheIndex++;

        if(ficheIndex >= ficheSpriteList.Count)
        {
            ficheIndex = 0;
        }

        fiche.GetComponent<Image>().sprite = ficheSpriteList[ficheIndex]; 
    }

    public void PreviousFiche()
    {
        ficheIndex--;

        if (ficheIndex < 0)
        {
            ficheIndex = ficheSpriteList.Count - 1;
        }

        fiche.GetComponent<Image>().sprite = ficheSpriteList[ficheIndex];
    }
    #endregion


}
