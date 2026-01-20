using UnityEngine;
using UnityEngine.EventSystems;

public class BossINFO : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject Textwindow;
    public TMPro.TextMeshProUGUI Text;
    public Boss Boss;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Textwindow.SetActive(true);

        if (this.isActiveAndEnabled)
        {
            Textwindow.SetActive(true);

            if (Boss.agr == true && Boss.disorientation == false)
            {
                Text.text = "Агр : Да \r\nДизориентация: Нет";
            }

            if (Boss.agr == true && Boss.disorientation == true)
            {
                Text.text = "Агр : Да \r\nДизориентация: Да ";
            }

            if (Boss.agr == false && Boss.disorientation == true)
            {
                Text.text = "Агр : Нет \r\nДизориентация: Да";
            }

            if (Boss.agr == false && Boss.disorientation == false)
            {
                Text.text = "Агр : Нет \r\nДизориентация: Нет";
            }
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
