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
    public bool alive = true;

    [SerializeField] private CharKsiusha fox;
    [SerializeField] private CharPoison yad;
    [SerializeField] private CharNastya fireg;
    [SerializeField] private Tsumatsu jirai;
    public bool isattacking = false;


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

            isattacking = false;
        }
        else
        {
            fox.TakeDamage(10, false);
            yad.TakeDamage(10, false);
            fireg.TakeDamage(10, false);
            jirai.TakeDamage(10, false);
        }
    }

    public void TakeDamage(int damage, bool ignore)
    {
        if (HellFire == false)
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
        else
        {
            Hp -= damage;
            Hp -= 177 * timeOfBurningInHell;
            timeOfBurningInHell++;
        }

        if (Hp < 0)
        {
            alive = false;
        }
    }

}
