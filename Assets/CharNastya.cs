using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CharNastya : MonoBehaviour, IBaseActions
{

    public int Hp = 90;
    public int maxHp = 90;
    public float Defense = 40;
    public int Speed = 0;
    public int AttackPower = 70;
    public int SkillPoint = 0;
    public int MaxSkillPoint = 10;
    public bool Alive = true;
    public bool parryBool = false;
    public TextMeshProUGUI Text;
    public int attackCounter = 0;
    public int Vpitano = 0;
    public int agrCounter = 0;

    public int revivePoint = 0;

    [SerializeField] private RunGame GameData;
    [SerializeField] private Tsumatsu jirai;
    [SerializeField] private Boss Boss;

    [SerializeField] private NastyaFrame NastyaFrame;

    public GameObject BattleHud;
    public bool isattacking = false;
    public TextMeshProUGUI HPtext;
    public TextMeshProUGUI SpText;

    public void Attack()
    {
        float Hpmultiplyer = 1 + (maxHp/200);

        Boss.TakeDamage((int)(AttackPower * Hpmultiplyer), false);

        float percentDefence = 1 - (Boss.Defense / 100);
        attackCounter += (int)(AttackPower * Hpmultiplyer * percentDefence);


        if (SkillPoint < MaxSkillPoint)
        {
            SkillPoint++;
        }

        isattacking = false;
    }

    public void TakeDamage(int damage, bool ignore)
    {

        if (!parryBool) { 
        
            float percentDefence = 1 - (Defense / 100);

            if (ignore) percentDefence = 1;

            int effdamage = (int)(damage * percentDefence);
            NastyaFrame.myAnimator.SetTrigger("take_damage");
            Hp -= effdamage;
            Vpitano += effdamage;

            if (jirai.DespairPoint < jirai.maxDespairPoint)
            {
                jirai.DespairPoint++;
            }

            if (Hp <= 0 && revivePoint >= 1)
            {
                Hp = maxHp / 2;
                revivePoint--;
                Alive = true;
            }
            else if (Hp <= 0 && revivePoint == 0)
            {
                Alive = false;
            }

        }
        else
        {
            Boss.TakeDamage(damage, true);
        }
    }

    public void Defence()
    {
        StartCoroutine(BuffDEF(1, 1.5f));
        if (SkillPoint + 2 < MaxSkillPoint)
        {
            SkillPoint += 2;
        }
        else
        {
            SkillPoint = MaxSkillPoint;
        }
        isattacking = false;
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
            agrCounter += 3;

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

            float percentDefence = 1 - (Boss.Defense / 100);
            attackCounter += (int)(Boss.maxHp / 10 * percentDefence);
            
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

    private IEnumerator BuffDEF(uint rounds, float multiplier)
    {
        uint buffStart = GameData.RoundCount;

        Defense = (int)(Defense * multiplier);

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
