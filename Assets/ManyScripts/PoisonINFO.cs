using UnityEngine;
using UnityEngine.EventSystems;

public class PoisonINfO : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Textwindow;
    public TMPro.TextMeshProUGUI Text;
    public CharPoison yad;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.isActiveAndEnabled)
        {
            Textwindow.SetActive(true);
            Text.text = "нг: " + (yad.Hp + yad.phantomHp).ToString() + "/" + yad.maxHp.ToString() + "\r\nнс: " + yad.SkillPoint.ToString() + "/" + yad.MaxSkillPoint.ToString(); 
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
