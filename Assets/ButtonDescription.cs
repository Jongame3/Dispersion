using UnityEngine;
using UnityEngine.EventSystems;

public class FangBiteHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject Textwindow;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Textwindow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Textwindow.SetActive(false);
    }
}
