using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

public class RunGame : MonoBehaviour
{
    public uint RoundCount = 0;
    [SerializeField] private CharKsiusha fox;
    [SerializeField] private CharPoison yad;
    [SerializeField] private CharNastya fireg;
    [SerializeField] private Tsumatsu jirai;
    [SerializeField] private Boss Boss;

    Queue<string> actionqueue = new Queue<string>();

    private IEnumerator AttackFlow()
    {
        while (Boss.alive)
        {
            for (int i = 0; i < 4; i++)
            {
                if (actionqueue.Peek() == "TSUMATSU")
                {
                    if (jirai.DespairMode == false) {
                        jirai.BattleHud.SetActive(true);

                        jirai.SPText.text = "Sp:" + jirai.SkillPoint.ToString() + "/" + jirai.MaxSkillPoint.ToString();
                        jirai.HPtext.text = "HP:" + jirai.Hp.ToString() + "/" + jirai.maxHp.ToString();
                        jirai.DPText.text = "DP:" + jirai.DespairPoint.ToString() + "/" + jirai.maxDespairPoint.ToString();

                        jirai.isattacking = true;

                        yield return new WaitUntil(() => jirai.isattacking == false);

                        jirai.BattleHud.SetActive(false);
                        actionqueue.Dequeue();
                    }
                    else
                    {
                        jirai.DespairHud.SetActive(true);

                        jirai.isattacking = true;

                        jirai.SPText2.text = "Sp:" + jirai.SkillPoint.ToString() + "/" + jirai.MaxSkillPoint.ToString();
                        jirai.HPtext2.text = "HP:" + jirai.Hp.ToString() + "/" + jirai.maxHp.ToString();
                        jirai.DPText2.text = "DP:" + jirai.DespairPoint.ToString() + "/" + jirai.maxDespairPoint.ToString();


                        yield return new WaitUntil(() => jirai.isattacking == false);

                        jirai.DespairHud.SetActive(false);
                        actionqueue.Dequeue();
                    }
                    continue;
                }

                if (actionqueue.Peek() == "FOX")
                {
                    fox.BattleHud.SetActive(true);

                    fox.isattacking = true;

                    fox.SpText.text = "Sp:" + fox.SkillPoint.ToString() + "/" + fox.MaxSkillPoint.ToString();
                    fox.HPtext.text = "HP:" + fox.Hp.ToString() + "/" + fox.maxHp.ToString();

                    yield return new WaitUntil(() => fox.isattacking == false);

                    fox.BattleHud.SetActive(false);
                    actionqueue.Dequeue();

                    continue;
                }


                if (actionqueue.Peek() == "KANDZIO")
                {
                    fireg.BattleHud.SetActive(true);

                    fireg.isattacking = true;

                    fireg.SpText.text = "Sp:" + fireg.SkillPoint.ToString() + "/" + fireg.MaxSkillPoint.ToString();
                    fireg.HPtext.text = "HP:" + fireg.Hp.ToString() + "/" + fireg.maxHp.ToString();

                    yield return new WaitUntil(() => fireg.isattacking == false);

                    fireg.BattleHud.SetActive(false);
                    actionqueue.Dequeue();

                    continue;
                }


                if (actionqueue.Peek() == "POISON")
                {
                    yad.BattleHud.SetActive(true);

                    yad.isattacking = true;

                    yad.SpText.text = "Sp:" + yad.SkillPoint.ToString() + "/" + yad.MaxSkillPoint.ToString();
                    yad.HPtext.text = "HP:" + yad.Hp.ToString() + "/" + yad.maxHp.ToString();

                    yield return new WaitUntil(() => yad.isattacking == false);

                    yad.BattleHud.SetActive(false);
                    actionqueue.Dequeue();
                    continue;

                }

            }

            Boss.Attack();

            CreateQueue();
        }
    }

    Queue<string> CreateQueue()
    {

        for (int i = 0; i < 4; i++)
        {
            int maxSpeed = -1;
            string maxName = "k";
            ;

            if (maxSpeed < fox.Speed && !actionqueue.Contains("FOX"))
            {
                maxSpeed = fox.Speed;
                maxName = "FOX";
            }

            if (maxSpeed < fireg.Speed && !actionqueue.Contains("KANDZIO"))
            {
                maxSpeed = fireg.Speed;
                maxName = "KANDZIO";
            }

            if (maxSpeed < jirai.Speed && !actionqueue.Contains("TSUMATSU"))
            {
                maxSpeed = jirai.Speed;
                maxName = "TSUMATSU";
            }

            if (maxSpeed < yad.Speed && !actionqueue.Contains("POISON"))
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

        StartCoroutine(AttackFlow());
    }



}
