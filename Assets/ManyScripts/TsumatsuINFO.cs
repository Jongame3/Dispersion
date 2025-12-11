using UnityEngine;
using UnityEngine.EventSystems;

public class TsumatsuINFO : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Textwindow1;
    public TMPro.TextMeshProUGUI Text1;
    public Tsumatsu jirai;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.isActiveAndEnabled)
        {
            Textwindow1.SetActive(true);
            Text1.text = "нг: " + jirai.Hp.ToString() + "/" + jirai.maxHp.ToString() +
                "\r\nнс: " + jirai.SkillPoint.ToString() + "/" + jirai.MaxSkillPoint.ToString() +
                "\r\nнн: " + jirai.DespairPoint.ToString() + "/" + jirai.maxDespairPoint.ToString();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        if (this.isActiveAndEnabled)
        {
            Textwindow1.SetActive(false);
            Text1.text = ":)";
        }
    }
}
