using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CharNastya : MonoBehaviour, IBaseActions
{

    public int Hp = 90;
    public int maxHp = 90;
    public int Defense = 40;
    public int Speed = 0;
    public int AttackPower = 70;
    public int SkillPoint = 0;
    public int MaxSkillPoint = 10;
    public bool Alive = true;
    public bool parryBool = false;
    public TextMeshProUGUI Text;

    public int revivePoint = 0;

    [SerializeField] private RunGame GameData;
    [SerializeField] private Tsumatsu jirai;
    [SerializeField] private Boss Boss;
    public GameObject BattleHud;
    public bool isattacking = false;
    public TextMeshProUGUI HPtext;
    public TextMeshProUGUI SpText;

    public void Attack()
    {
        Boss.TakeDamage(AttackPower * (maxHp/2), false);

        isattacking = false;
    }

    public void TakeDamage(int damage, bool ignore)
    {
        Hp -= (damage * (1 - Defense / 100));
        jirai.DespairPoint++;

        if (Hp <= 0 && revivePoint > 0) { Hp = maxHp / 2; }

        else if (Hp < 0)
        {
            Alive = false;
        }

        HPtext.text = "HP:" + Hp.ToString() + "/" + maxHp.ToString();
    }

    public void Defence()
    {

    }

    public void agr()
    {
        if(SkillPoint >= 1)
        {
            if (Boss.disorientation)
            {
                Boss.agr = true;
                StartCoroutine(BuffDEF(3, 20));
            }
            else
            {
                Boss.agr = true;
            }
            SkillPoint--;

            isattacking = false;
        }
        else
        {
            Text.text = "Недостаточно Очков Умений";
        }
    }

    public void parry()
    {
        if (SkillPoint >= 2)
        {
            parryBool = true;
            SkillPoint -= 2;

            isattacking = false;
        }
        else
        {
            Text.text = "Недостаточно Очков Умений";
        }
    }

    public void dismemberment()
    {
        if (SkillPoint >= 3)
        {
            Boss.TakeDamage(Boss.maxHp/10, false);
            StartCoroutine(disorientBuff(4));
            SkillPoint -= 3;

            isattacking = false;

        }
        else
        {
            Text.text = "Недостаточно Очков Умений";
        }
    }

    public void ult()
    {
        if (SkillPoint >= 5)
        {
            maxHp = maxHp * 2;
            Hp = maxHp;
            SkillPoint -= 5;

            isattacking = false;
        }
        else
        {
            Text.text = "Недостаточно Очков Умений";
        }
    }

    private IEnumerator BuffDEF(uint rounds, int amount)
    {
        uint buffStart = GameData.RoundCount;

        Defense += amount;

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Defense = 40;  
    }

    private IEnumerator disorientBuff(uint rounds)
    {
        uint buffStart = GameData.RoundCount;

        Boss.disorientation = true;

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Boss.disorientation = false;
    }
}
