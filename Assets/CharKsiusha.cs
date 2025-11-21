using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CharKsiusha : MonoBehaviour, IBaseActions
{
    public int Hp = 40;
    public int maxHp = 40;
    public float Defense = 30;
    public int Speed = 80;
    public int AttackPower = 50;
    public int SkillPoint = 0;
    public int MaxSkillPoint = 10;
    bool evade = false;
    public bool Alive = true;

    public int revivePoint = 0;

    [SerializeField] private Boss Boss;
    [SerializeField] private Tsumatsu jirai;
    [SerializeField] private RunGame GameData;

    [SerializeField] private FoxFrame FoxFrame;

    public GameObject BattleHud;
    public bool isattacking = false;
    public TextMeshProUGUI Text;
    public TextMeshProUGUI HPtext;
    public TextMeshProUGUI SpText;

    public void Attack()
    {
        Boss.TakeDamage(AttackPower, false);
        SkillPoint++;

        isattacking = false;
    }


    public void TakeDamage (int damage, bool ignore)
    {
        if (evade && !ignore){
            evade = false; 
            return; 
        }

        float percentDefence = 1 - (Defense / 100);

        if (ignore) percentDefence = 1;

        int effdamage = (int)(damage * percentDefence);
        FoxFrame.myAnimator.SetTrigger("take_damage");
        Hp -= effdamage;

        if (jirai.DespairPoint < jirai.maxDespairPoint)
        {
            jirai.DespairPoint++;
        }

        if (Hp <= 0 && revivePoint > 0) { 
            Hp = maxHp/2;
            revivePoint--;
            if (!Alive) Alive = true;
        }
        else if (Hp <= 0) 
        {
            Alive = false;
        }

    }

    public void Def()
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

    private IEnumerator BuffATK(uint rounds, float multiplier)
    {
        uint buffStart = GameData.RoundCount;

        AttackPower = (int)(AttackPower * multiplier);

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        AttackPower = 50;
    }

    private IEnumerator BuffDEF(uint rounds, float multiplier)
    {
        uint buffStart = GameData.RoundCount;

        Defense = (int)(Defense * multiplier);

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Defense = 30;
    }

    public void Fangbite()
    {
        if (SkillPoint >= 1)
        {
            Boss.TakeDamage(AttackPower, false) ;
            Hp += 10;
            SkillPoint -= 1;

            isattacking = false;
        }
        else
        {
            Text.text = "Недостаточно Очков Умений";
        }
    }

    public void StalkingPunch()
    {
        if (SkillPoint >= 2)
        {
            evade = true;

            StartCoroutine(BuffATK(1,2.5f));
            SkillPoint -= 2;

            isattacking = false;
        }
        else
        {
            Text.text = "Недостаточно Очков Умений";
        }
    }

    public void FangAndSteel ()
    {
        if (SkillPoint >= 3)
        {
            float modifier = 1 + (1*(Speed/100));

            Boss.TakeDamage((int)(AttackPower * modifier), false);

            modifier = (float) (1.5 + (1.5 * (Speed / 100)));

            Boss.TakeDamage((int)(AttackPower * modifier), false);
            SkillPoint -= 3;

            isattacking = false;
        }
        else
        {
            Text.text = "Недостаточно Очков Умений";
        }
    }

    public void SlySwing()
    {

        if (SkillPoint >= 4)
        {
            float modifier = 6;
            Boss.TakeDamage((int)(AttackPower * modifier), true);
            SkillPoint -= 4;

            isattacking = false;
        }
        else
        {
            Text.text = "Недостаточно Очков Умений";
        }

    }
}
