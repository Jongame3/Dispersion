using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FangBiteHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject Textwindow;
    public string SetText;
    public TMPro.TextMeshProUGUI Text;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.isActiveAndEnabled)
        {
            Textwindow.SetActive(true);
            Text.text = SetText;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.isActiveAndEnabled)
        {
            Textwindow.SetActive(false);
            Text.text = ":)";
        }
        
    }
}
