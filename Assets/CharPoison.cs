using System;
using System.Collections;
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
    public int Defense = 40;
    bool Alive = true;

    [SerializeField] private Boss Boss;


    public void Attack()
    {
        Boss.TakeDamage(AttackPower, false);
        SkillPoint++;
    }

    public void TakeDamage(int damage, bool ignore)
    {
        if(phantomHp != 0)
        {
            if(damage > phantomHp)
            {
                damage -= phantomHp;
                phantomHp = 0;
            }
            else
            {
                phantomHp -= damage;
            }
        }

        Hp -= (damage * (1 - Defense / 100));
        if (Hp <= 0 && revivePoint >= 1) { Hp = 20; }
        else if (Hp <= 0)
        {
            Alive = false;
        }
    }
    public void InjectionAlpha()
    {
        if (SkillPoint >= 1)
        {
            Hp -=(int) Hp / 3;
            SkillPoint += 2;
        }
        else
        {
            //Not enough SP
        }
    }
    public void InjectionBeta()
    {
        if (SkillPoint >= 3)
        {
            phantomHp += 30;
            SkillPoint -= 3;
        }
        else
        {
            //Not enough SP
        }
    }
    public void InjectionGamma()
    {
        if (SkillPoint == 10)
        {
            Boss.TakeDamage(Boss.Hp, true);
        }
        else
        {
            //Not enough SP
        }
    }
}