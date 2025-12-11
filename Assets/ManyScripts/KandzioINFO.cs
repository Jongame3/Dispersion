using UnityEngine;
using UnityEngine.EventSystems;

public class KandzioINFO : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Textwindow;
    public TMPro.TextMeshProUGUI Text;
    public CharNastya fireg;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.isActiveAndEnabled)
        {
            Textwindow.SetActive(true);
            Text.text = "нг: " + fireg.Hp.ToString() + "/" + fireg.maxHp.ToString() + "\r\nнс: " + fireg.SkillPoint.ToString() + "/" + fireg.MaxSkillPoint.ToString();
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
