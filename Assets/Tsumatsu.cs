using System;
using UnityEngine;
using System.Collections;

public class Tsumatsu : MonoBehaviour, IBaseActions
{
    public int Hp = 70;
    public int maxHp = 70;
    public int Defense = 30;
    public int Speed = 100;
    public int AttackPower = 0;
    public int SkillPoint = 0;
    public int MaxSkillPoint = 10;
    public int DespairPoint = 0;
    public int maxDespairPoint = 5;
    public int revivePoint = 0;

    public bool changedToSweetLie = false;

    [SerializeField] private Boss Boss;
    [SerializeField] private CharKsiusha K;
    [SerializeField] private CharNastya N;
    [SerializeField] private CharPoison P;

    public void Attack()
    {
        
    }

    public void TakeDamage(int damage, bool ignore)
    {

    }

    public void Comedy()
    {
        if(SkillPoint >= 2)
        {
            K.Hp += 50;
            if(K.Hp > K.maxHp) K.Hp = K.maxHp;
            N.Hp += 50;
            if (N.Hp > N.maxHp) N.Hp = N.maxHp;
            P.Hp += 50;
            if (P.Hp > K.maxHp) P.Hp = K.maxHp;
            Hp += 50;
            if (Hp > maxHp) Hp = maxHp;

            SkillPoint -= 2;
        } else
        {
            // output that not enough SP
        }
    }

    public void Delusion()
    {
        if(SkillPoint >= 3)
        {
            // How to make it last only for several rounds....

            SkillPoint -= 3;
        } else
        {
            // output that not enough SP
        }
    }

    public void BackToFriends(MonoBehaviour target)
    {
        if (!changedToSweetLie)
        {
            if (SkillPoint >= 4)
            {
                // I need a general class for everyone..

                SkillPoint -= 4;
            }
            else
            {
                // output that not enough SP
            }
        } else
        {
            if(SkillPoint >= 1)
            {
                // How to make it last only for several rounds....

                SkillPoint = 0;
            }
            else
            {
                // output that not enough SP
            }
        }
    }

    public void TheGloryOfThePast()
    {
        if(DespairPoint >= 1)
        {
            changedToSweetLie = true;
            DespairPoint -= 1;
        }
        else
        {
            // output that not enough DP
        }
    }

    public void Broken()
    {
        if (DespairPoint >= 2)
        {
            // How to make it last only for several rounds....

            DespairPoint -= 2;
        } else
        {
            // output that not enough DP
        }
    }

    public void EndlessLove()
    {
        if(DespairPoint >= 1)
        {
            // At best need to change this to TakeDamage(K, K.Hp) or smth like that 
            // for additional revivePoint check
            int damage = ((K.AttackPower + N.AttackPower + P.AttackPower) * DespairPoint)/(K.Hp + N.Hp + P.Hp); // Dividing is under a queastion....

            // I need to make a fire effect...

            K.Hp = 0;
            N.Hp = 0;
            P.Hp = 0;
            Hp = 0;

            DespairPoint = 0;

        } else
        {
            // output that not enough DP
        }
    }


}
