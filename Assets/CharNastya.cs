using System;
using System.Collections;
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

    public int revivePoint = 0;

    [SerializeField] private RunGame GameData;
    [SerializeField] private Tsumatsu jirai;
    [SerializeField] private Boss Boss;

    public void Attack()
    {
        Boss.TakeDamage(AttackPower, false);
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
    }

    public void laserFromEyes()
    {
        if(SkillPoint >= 1)
        {
            Boss.agr = true;
            SkillPoint--;
        }
        else
        {
            // output that not enough SP
        }
    }

    public void bladeStrike()
    {
        if (SkillPoint >= 2)
        {
            Boss.TakeDamage(AttackPower * 2, false);
            SkillPoint -= 2;
        }
        else
        {
            // output that not enough SP
        }
    }

    private IEnumerator BuffDEF(uint rounds, float multiplier)
    {
        uint buffStart = GameData.RoundCount;

        Defense = (int)(Defense * multiplier);

        yield return new WaitUntil(() => (rounds + buffStart == GameData.RoundCount));

        Defense = 6;  //??
    }
}
