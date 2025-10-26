using UnityEngine;
using System.Collections;
using System;

public class Boss : MonoBehaviour, IBaseActions
{
    public int Hp = 10000;
    public int maxHp = 10000;
    public int AttackPower = 10;
    public int Defense = 20;
    public bool HellFire = false;
    public bool agr = false;
    public bool disorientation = false;
    public int timeOfBurningInHell = 1;

    [SerializeField] private CharKsiusha fox;
    [SerializeField] private CharPoison yad;
    [SerializeField] private CharNastya fireg;
    [SerializeField] private Tsumatsu jirai ;

    
    public void Attack()
    {
        if (agr) {
            if (fireg.parryBool)
            {
                Hp -= AttackPower;
                fireg.parryBool = false;
            }
            else
            {
                fireg.Hp -= AttackPower;
            }
        }
        else
        {
            //Random attack
        }
    }

    public void TakeDamage(int damage, bool ignore)
    {
        if (!HellFire)
        {
            if (ignore == false)
            {
                Hp -= (damage * (1 - Defense / 100));
            }
            else
            {
                Hp -= damage;
            }

        }
        Hp -= damage;
        Hp -= 177 * timeOfBurningInHell;
        timeOfBurningInHell++;

            if (Hp < 0)
        {
            //WIN
        }
    }

}
