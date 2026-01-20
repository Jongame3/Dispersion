using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

using TMPro;


public class RunGame : MonoBehaviour
{
    public uint RoundCount = 0;
    [SerializeField] private CharKsiusha fox;
    [SerializeField] private CharPoison yad;
    [SerializeField] private CharNastya fireg;
    [SerializeField] private Tsumatsu jirai;
    [SerializeField] private Boss Boss;
    [SerializeField] private TextMeshProUGUI RoundText;

    [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject LoseScreen;
    [SerializeField] private TextMeshProUGUI FoxEND;
    [SerializeField] private TextMeshProUGUI JiraiEND;
    [SerializeField] private TextMeshProUGUI KandzioEND;
    [SerializeField] private TextMeshProUGUI PoisonEND;
    [SerializeField] private TextMeshProUGUI RoundsEND;

    void EndGameDefeat()
    {
        LoseScreen.SetActive(true);
    }

    Queue<string> actionqueue = new Queue<string>();

    private IEnumerator AttackFlow()
    {
        while (Boss.alive && RoundCount <= 30)
        {
            if ((!jirai.Alive && jirai.revivePoint == 0) ||
                (!yad.Alive && yad.revivePoint == 0) ||
                (!fireg.Alive && fireg.revivePoint == 0) ||
                (!fox.Alive && fox.revivePoint == 0))
            {
                EndGameDefeat();
                yield break;
            }


            RoundText.text = "Раунд: " + RoundCount.ToString() + " / 30";

            for (int i = 0; i < 4; i++)
            {

                if (actionqueue.Count == 0) CreateQueue();

                if (actionqueue.Peek() == "TSUMATSU" && jirai.Alive && Boss.alive)
                {
                    if (!jirai.DespairMode)
                    {
                        jirai.BattleHud.SetActive(true);

                        jirai.SPText.text = "ОУ: " + jirai.SkillPoint + "/" + jirai.MaxSkillPoint;
                        jirai.HPtext.text = "ОЗ: " + jirai.Hp + "/" + jirai.maxHp;
                        jirai.DPText.text = "ОО: " + jirai.DespairPoint + "/" + jirai.maxDespairPoint;

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

                        jirai.SPText2.text = "ОУ: " + jirai.SkillPoint + "/" + jirai.MaxSkillPoint;
                        jirai.HPtext2.text = "ОЗ: " + jirai.Hp + "/" + jirai.maxHp;
                        jirai.DPText2.text = "ОО: " + jirai.DespairPoint + "/" + jirai.maxDespairPoint;

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

                    fox.SpText.text = "ОУ: " + fox.SkillPoint + "/" + fox.MaxSkillPoint;
                    fox.HPtext.text = "ОЗ: " + fox.Hp + "/" + fox.maxHp;

                    yield return new WaitUntil(() => fox.isattacking == false);

                    fox.BattleHud.SetActive(false);
                    actionqueue.Dequeue();

                    continue;
                }

                if (actionqueue.Peek() == "KANDZIO" && fireg.Alive && Boss.alive)
                {
                    fireg.BattleHud.SetActive(true);

                    fireg.isattacking = true;

                    fireg.SpText.text = "ОУ: " + fireg.SkillPoint + "/" + fireg.MaxSkillPoint;
                    fireg.HPtext.text = "ОЗ: " + fireg.Hp + "/" + fireg.maxHp;

                    yield return new WaitUntil(() => fireg.isattacking == false);

                    fireg.BattleHud.SetActive(false);
                    actionqueue.Dequeue();
                    continue;
                }

                if (actionqueue.Peek() == "POISON" && yad.Alive && Boss.alive)
                {
                    yad.BattleHud.SetActive(true);

                    yad.isattacking = true;

                    yad.SpText.text = "ОУ: " + yad.SkillPoint + "/" + yad.MaxSkillPoint;
                    yad.HPtext.text = "ОЗ: " + (yad.Hp + yad.phantomHp) + "/" + yad.maxHp;

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

        WinScreen.SetActive(true);
        FoxEND.text = "Нанесено урона: " + fox.attackCounter + "\r\nУвернулась от " + fox.evadecount + " атак";
        JiraiEND.text = "Нанесено урона: " + jirai.attackCounter + "\r\nИсцелено здоровья: " + jirai.healCounter;
        KandzioEND.text = "Нанесено урона: " + fireg.attackCounter + "\r\nПолучено урона: " + fireg.Vpitano;
        PoisonEND.text = "Нанесено урона: " + yad.attackCounter;
        RoundsEND.text = "Раундов прошло: " + RoundCount.ToString() + " / 30";

    }

    Queue<string> CreateQueue()
    {
        List<(string name, int speed)> candidates = new List<(string, int)>();

        if (fox.Alive) candidates.Add(("FOX", fox.Speed));
        if (fireg.Alive) candidates.Add(("KANDZIO", fireg.Speed));
        if (jirai.Alive) candidates.Add(("TSUMATSU", jirai.Speed));
        if (yad.Alive) candidates.Add(("POISON", yad.Speed));

        candidates.Sort((a, b) => b.speed.CompareTo(a.speed));

        actionqueue.Clear();
        foreach (var c in candidates)
        {
            actionqueue.Enqueue(c.name);
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
