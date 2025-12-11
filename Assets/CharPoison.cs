using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CharPoison : MonoBehaviour, IBaseActions
{
    public Animator myAnimator;

    public int Hp = 30;
    public int maxHp = 30;
    public int phantomHp = 0;
    public int Speed = 70;
    public int AttackPower = 70;
    public uint revivePoint = 0;
    public int SkillPoint = 0;
    public int MaxSkillPoint = 10;
    public float Defense = 40;
    public  bool Alive = true;
    public int attackCounter = 0;

    [SerializeField] private Boss Boss;
    [SerializeField] private Tsumatsu jirai;
    [SerializeField] private PoisonFrame PoisonFrame;

    public GameObject BattleHud;
    public bool isattacking = false;
    public TextMeshProUGUI Text;
    public TextMeshProUGUI HPtext;
    public TextMeshProUGUI SpText;
    [SerializeField] private RunGame GameData;

    public void Attack()
    {
        Boss.TakeDamage(AttackPower, false);
        float percentDefence = 1 - (Boss.Defense / 100);
        attackCounter += (int)(AttackPower * percentDefence);
        

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

    public void TakeDamage(int damage, bool ignore)
    {
        float percentDefence = 1 - (Defense / 100);

        if(ignore) percentDefence = 1;

        if (phantomHp != 0)
        {
            if (damage > phantomHp)
            {
                damage -= phantomHp;
                phantomHp = 0;

                int effdamage = (int)(damage * percentDefence);
                PoisonFrame.myAnimator.SetTrigger("take_damage");
                Hp -= effdamage;
            }
            else
            {
                PoisonFrame.myAnimator.SetTrigger("take_damage");
                phantomHp -= damage;
            }
        }

        else {
            int effdamage = (int)(damage * percentDefence);
            PoisonFrame.myAnimator.SetTrigger("take_damage");
            Hp -= effdamage;
        }

        if (Hp <= 0 && revivePoint > 0)
        {
            Hp = maxHp / 2;
            revivePoint--;
            Alive = true;
        }
        else if (Hp <= 0 && revivePoint == 0)
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
        if (SkillPoint >= 2)
        {
            phantomHp += 30;
            SkillPoint -= 2;

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
            attackCounter += Boss.Hp;
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