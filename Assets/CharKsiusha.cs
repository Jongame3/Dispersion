using System.Collections;
using UnityEngine;
using System;

public class CharKsiusha : MonoBehaviour, IBaseActions
{
    public int Hp = 40;
    public int maxHp = 40;
    public int Defense = 30;
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

    public void Attack()
    {
        Boss.TakeDamage(AttackPower, false);
        SkillPoint++;
    }


    public void TakeDamage (int damage, bool ignore)
    {
        if (evade){
            evade = false; 
            return; 
        }

        Hp -= (damage * (1 - Defense/100));
        jirai.DespairPoint++;
        if (Hp <= 0 && revivePoint > 0) { Hp = maxHp/2; }
        else if (Hp < 0) 
        {
            Alive = false;
        }
    }

    public void Def()
    {
        BuffDEF(1, 1.5f);
        SkillPoint++;
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
        }
        else
        {
            // output that not enough SP
        }
    }

    public void StalkingPunch()
    {
        if (SkillPoint >= 2)
        {
            evade = true;

            StartCoroutine(BuffATK(1,2.5f));
            SkillPoint -= 2;
        }
        else
        {
            // output that not enough SP
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
        }
        else
        {
            // output that not enough SP
        }
    }

    public void SlySwing()
    {

        if (SkillPoint >= 4)
        {
            float modifier = 6;
            Boss.TakeDamage((int)(AttackPower * modifier), true);
            SkillPoint -= 4;
        }
        else
        {
            // output that not enough SP
        }

    }
}
