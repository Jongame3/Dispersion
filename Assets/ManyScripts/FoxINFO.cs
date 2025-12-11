using UnityEngine;
using UnityEngine.EventSystems;

public class FoxINFO : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Textwindow;
    public TMPro.TextMeshProUGUI Text;
    public CharKsiusha fox;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.isActiveAndEnabled)
        {
            Textwindow.SetActive(true);
            Text.text = "нг: " + fox.Hp.ToString() + "/" + fox.maxHp.ToString() + "\r\nнс: " + fox.SkillPoint.ToString() + "/" + fox.MaxSkillPoint.ToString();
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
