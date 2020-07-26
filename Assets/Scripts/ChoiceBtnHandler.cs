using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoiceBtnHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int id;
    public Metier metier;
    public Sprite onBackSprite;
    public Sprite outBackSprite;

    private Sprite logoSprite;
    private Sprite hoverLogoSprite;

    private bool isUpdated = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if(GameManager.Instance.candidatsScreen.activeInHierarchy && !isUpdated)
        {
            metier = GameManager.Instance.pickedFiches[id].poste;
            hoverLogoSprite = GameManager.Instance.pickedFiches[id].hoverLogo;
            logoSprite = GameManager.Instance.pickedFiches[id].logo;
            isUpdated = true;
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        this.GetComponent<Image>().sprite = onBackSprite;
        this.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = hoverLogoSprite;
        GameManager.Instance.UpdateBtnName(GameManager.Instance.pickedFiches[id]);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = outBackSprite;
        this.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = logoSprite;
        GameManager.Instance.metierTitle.text = "";
    }
}
