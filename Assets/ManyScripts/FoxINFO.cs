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
            Text.text =
                "ОЗ: " + fox.Hp + "/" + fox.maxHp + "\r\n" +
                "ОУ: " + fox.SkillPoint + "/" + fox.MaxSkillPoint;
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
