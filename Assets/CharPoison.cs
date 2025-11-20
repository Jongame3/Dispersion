using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CharPoison : MonoBehaviour, IBaseActions
{
    public int Hp = 30;
    public int maxHp = 30;
    public int phantomHp = 0;
    public int Speed = 70;
    public int AttackPower = 70;
    public int revivePoint = 0;
    public int SkillPoint = 0;
    public int MaxSkillPoint = 10;
    public float Defense = 40;
    [SerializeField]  bool Alive = true;

    [SerializeField] private Boss Boss;
    [SerializeField] private Tsumatsu jirai;
    public GameObject BattleHud;
    public bool isattacking = false;
    public TextMeshProUGUI Text;
    public TextMeshProUGUI HPtext;
    public TextMeshProUGUI SpText;
    [SerializeField] private RunGame GameData;

    public void Attack()
    {
        Boss.TakeDamage(AttackPower, false);

        if (SkillPoint < MaxSkillPoint)
        {
            SkillPoint++;
        }
        phantomHp += 10;
        isattacking = false;
    }
    public void Defence()
    {
        StartCoroutine(BuffDEF(1, 1.5f));
        if (SkillPoint < MaxSkillPoint)
        {
            SkillPoint += 2;
        }
        isattacking = false;
    }

    public void TakeDamage(int damage, bool ignore)
    {
        float percentDefence = 1 - (Defense / 100);

        if (phantomHp != 0)
        {
            if (damage > phantomHp)
            {
                damage -= phantomHp;
                phantomHp = 0;

                int effdamage = (int)(damage * percentDefence);
                Hp -= effdamage;
            }
            else
            {
                phantomHp -= damage;
            }
        }

        else {
            int effdamage = (int)(damage * percentDefence);
            Hp -= effdamage;
        }

        if (Hp <= 0 && revivePoint >= 1) { Hp = 20; revivePoint--; }

        else if (Hp <= 0)
        {
            Alive = false;
        }

        if (jirai.DespairPoint < jirai.maxDespairPoint)
        {
            jirai.DespairPoint++;
        }

        HPtext.text = "HP:" + (Hp + phantomHp).ToString() + "/" + maxHp.ToString();
    }
    public void InjectionAlpha()
    {
        if (SkillPoint >= 1)
        {
            Hp -=(int) Hp / 3;
            HPtext.text = "HP:" + (Hp + phantomHp).ToString() + "/" + maxHp.ToString();

            SkillPoint += 2;

            isattacking = false;
        }
        else
        {
            Text.text = "Недостаточно Очков Умений";
        }
    }
    public void InjectionBeta()
    {
        if (SkillPoint >= 3)
        {
            phantomHp += 30;
            SkillPoint -= 3;

            isattacking = false;
        }
        else
        {
            Text.text = "Недостаточно Очков Умений";
        }
    }
    public void InjectionGamma()
    {
        if (SkillPoint == 10)
        {
            Boss.TakeDamage(Boss.Hp, true);

            isattacking = false;
        }
        else
        {
            Text.text = "Недостаточно Очков Умений";
        }
    }

    private IEnumerator BuffDEF(uint rounds, float multiplier)
    {
        uint buffStart = GameData.RoundCount;

        Defense = (int)(Defense * multiplier);

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Defense = 30;
    }
}