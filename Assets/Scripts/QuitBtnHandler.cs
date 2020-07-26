using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuitBtnHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite onQuitSprite;
    public Sprite outQuitSprite;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        this.GetComponent<Image>().sprite = onQuitSprite;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = outQuitSprite;
    }
}
