using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEditor.Analytics;
using TMPro;
using Unity.VisualScripting;

public class RunGame : MonoBehaviour
{
    public uint RoundCount = 0;
    [SerializeField] private CharKsiusha fox;
    [SerializeField] private CharPoison yad;
    [SerializeField] private CharNastya fireg;
    [SerializeField] private Tsumatsu jirai;
    [SerializeField] private Boss Boss;
    [SerializeField] private TextMeshProUGUI RoundText;

    [SerializeField] private GameObject EndScreen;

    [SerializeField] private TextMeshProUGUI FoxEND;
    [SerializeField] private TextMeshProUGUI JiraiEND;
    [SerializeField] private TextMeshProUGUI KandzioEND;
    [SerializeField] private TextMeshProUGUI PoisonEND;

    void EndGameDefeat()
    {
        Debug.Log("Someone died! Game Over.");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    Queue<string> actionqueue = new Queue<string>();

    private IEnumerator AttackFlow()
    {
        while (Boss.alive && RoundCount <= 30)
        {
            if (!jirai.Alive || !yad.Alive || !fireg.Alive || !fox.Alive)
            {
                EndGameDefeat();
                yield break; 
                
            }

            RoundText.text = "Round Count: " + RoundCount.ToString() + " / 30";

            for (int i = 0; i < 4; i++)
            {
                if (actionqueue.Peek() == "TSUMATSU" && jirai.Alive && Boss.alive)
                {
                    if (jirai.DespairMode == false) {
                        jirai.BattleHud.SetActive(true);

                        jirai.SPText.text = "ОУ:" + jirai.SkillPoint.ToString() + "/" + jirai.MaxSkillPoint.ToString();
                        jirai.HPtext.text = "ОЗ:" + jirai.Hp.ToString() + "/" + jirai.maxHp.ToString();
                        jirai.DPText.text = "ОО:" + jirai.DespairPoint.ToString() + "/" + jirai.maxDespairPoint.ToString();

                        jirai.isattacking = true;

                        yield return new WaitUntil(() => jirai.isattacking == false);

                        jirai.BattleHud.SetActive(false);
                        jirai.HealUI.SetActive(false);
                        jirai.BTFUI.SetActive(false); 
                        jirai.DelusionUI.SetActive(false);
                        actionqueue.Dequeue();
                    }
                    else
                    {
                        jirai.DespairHud.SetActive(true);

                        jirai.isattacking = true;

                        jirai.SPText2.text = "ОУ:" + jirai.SkillPoint.ToString() + "/" + jirai.MaxSkillPoint.ToString();
                        jirai.HPtext2.text = "ОЗ:" + jirai.Hp.ToString() + "/" + jirai.maxHp.ToString();
                        jirai.DPText2.text = "ОО:" + jirai.DespairPoint.ToString() + "/" + jirai.maxDespairPoint.ToString();


                        yield return new WaitUntil(() => jirai.isattacking == false);

                        jirai.DespairHud.SetActive(false);
                        actionqueue.Dequeue();
                    }
                    continue;
                } 

                if (actionqueue.Peek() == "FOX" && fox.Alive && Boss.alive)
                {
                    fox.BattleHud.SetActive(true);

                    fox.isattacking = true;

                    fox.SpText.text = "ОУ:" + fox.SkillPoint.ToString() + "/" + fox.MaxSkillPoint.ToString();
                    fox.HPtext.text = "ОЗ:" + fox.Hp.ToString() + "/" + fox.maxHp.ToString();

                    yield return new WaitUntil(() => fox.isattacking == false);

                    fox.BattleHud.SetActive(false);
                    actionqueue.Dequeue();

                    continue;
                }
                


                if (actionqueue.Peek() == "KANDZIO" && fireg.Alive && Boss.alive)
                {
                    fireg.BattleHud.SetActive(true);

                    fireg.isattacking = true;

                    fireg.SpText.text = "ОУ:" + fireg.SkillPoint.ToString() + "/" + fireg.MaxSkillPoint.ToString();
                    fireg.HPtext.text = "ОЗ:" + fireg.Hp.ToString() + "/" + fireg.maxHp.ToString();

                    yield return new WaitUntil(() => fireg.isattacking == false);

                    fireg.BattleHud.SetActive(false);
                    actionqueue.Dequeue();

                    continue;
                }
                


                if (actionqueue.Peek() == "POISON" && yad.Alive && Boss.alive)
                {
                    yad.BattleHud.SetActive(true);

                    yad.isattacking = true;

                    yad.SpText.text = "ОУ:" + yad.SkillPoint.ToString() + "/" + yad.MaxSkillPoint.ToString();
                    yad.HPtext.text = "ОЗ:" + (yad.Hp + yad.phantomHp).ToString() + "/" + yad.maxHp.ToString();

                    yield return new WaitUntil(() => yad.isattacking == false);

                    yad.BattleHud.SetActive(false);
                    actionqueue.Dequeue();
                    continue;

                }
                

            }

            Boss.Attack();
            RoundCount++;

            actionqueue.Clear();
            CreateQueue();
        }

        EndScreen.SetActive(true);
        FoxEND.text = "Нанесено урона: " + fox.attackCounter.ToString()+ "\r\nУвернулась от " + fox.evadecount.ToString() + " атак";
        JiraiEND.text = "Нанесено урона: " + jirai.attackCounter.ToString()+ "\r\nИсцелено здоровья: " + jirai.healCounter.ToString() ;
        KandzioEND.text = "Нанесено урона: " + fireg.attackCounter.ToString()+ "\r\nПолучено урона: " + fireg.Vpitano.ToString();
        PoisonEND.text = "Нанесено урона: " + yad.attackCounter.ToString();
    }

    Queue<string> CreateQueue()
    {

        for (int i = 0; i < 4; i++)
        {
            int maxSpeed = -1;
            string maxName = "k";
            ;

            if (fox.Alive && maxSpeed < fox.Speed && !actionqueue.Contains("FOX"))
            {
                maxSpeed = fox.Speed;
                maxName = "FOX";
            }

            if (fireg.Alive && maxSpeed < fireg.Speed && !actionqueue.Contains("KANDZIO"))
            {
                maxSpeed = fireg.Speed;
                maxName = "KANDZIO";
            }

            if (jirai.Alive && maxSpeed < jirai.Speed && !actionqueue.Contains("TSUMATSU"))
            {
                maxSpeed = jirai.Speed;
                maxName = "TSUMATSU";
            }

            if (yad.Alive && maxSpeed < yad.Speed && !actionqueue.Contains("POISON"))
            {
                maxSpeed = yad.Speed;
                maxName = "POISON";
            }

            actionqueue.Enqueue(maxName);
        }
        return actionqueue;
    }

    private void Start()
    {
        CreateQueue();
        Boss.Hpslide.maxValue = Boss.maxHp;
        Boss.Hpslide.value = Boss.Hp;
        StartCoroutine(AttackFlow());
    }



}
