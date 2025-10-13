using UnityEngine;
using System.Collections;
using System;

public class Boss : MonoBehaviour, IBaseActions
{
    public int Hp = 10000;
    public int maxHp = 10000;
    public int AttackPower = 10;
    public int Defense = 20;
    [SerializeField] private CharKsiusha fox;
    [SerializeField] private CharPoison yad;
    [SerializeField] private CharNastya fireg;
    [SerializeField] private Tsumatsu jirai ;

    
    public void Attack()
    {
        fox.Hp = Hp - AttackPower;
        
    }

    public void TakeDamage(int damage, bool ignore)
    {
        
        if (ignore == false)
        {
            Hp -= (damage * (1 - Defense / 100));
        }
        else {
            Hp -= damage;
        }

        if (Hp < 0)
        {
            //WIN
        }
    }

}
