using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UIElements;


public class Tsumatsu : MonoBehaviour, IBaseActions
{
    public int Hp = 70;
    public const int maxHp = 70;
    public int Defense = 30;
    public int Speed = 100;
    public int AttackPower = 0;
    public int SkillPoint = 0;
    public int MaxSkillPoint = 10;
    public int DespairPoint = 0;
    public int maxDespairPoint = 5;
    public int revivePoint = 0;
    public bool Alive = true;

    public bool changedToSweetLie = false;

    [SerializeField] private Boss Boss;
    [SerializeField] private CharKsiusha Meanie;
    [SerializeField] private CharNastya Reddie;
    [SerializeField] private CharPoison Snake;
    [SerializeField] private RunGame GameData;
    

    public void Attack()
    {
        
    }

    public void HealMe(int amount)
    {
        if(Hp <= maxHp - 20) Hp = Hp + 20;
    }
    public void HealMeanie(int amount)
    {
        if(Meanie.Hp <= Meanie.maxHp - 20) Meanie.Hp = Meanie.Hp + 20;
    }
    public void HealReddie(int amount)
    {
        if(Reddie.Hp <= Reddie.maxHp - 20) Reddie.Hp = Reddie.Hp + 20;
    }
    public void HealSnake(int amount)
    {
        if(Snake.Hp <= Snake.maxHp - 20) Snake.Hp = Snake.Hp + 20;
    }

    public void TakeDamage(int damage, bool ignore)
    {
        Hp -= (damage * (1 - Defense / 100));
        if (Hp <= 0 && revivePoint > 0) { 
            Hp = maxHp/2;
            revivePoint--;
        }
        else if (Hp < 0)
        {
            Alive = false;
        }
    }

    private IEnumerator BuffDEFMe(uint rounds, int amount)
    {
        uint buffStart = GameData.RoundCount;

        Defense = Defense + amount;

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Defense = 30;
    }
    private IEnumerator BuffDEFMeanie(uint rounds, int amount)
    {
        uint buffStart = GameData.RoundCount;

        Meanie.Defense = Meanie.Defense + amount;

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Meanie.Defense = 30;
    }
    private IEnumerator BuffDEFReddie(uint rounds, int amount)
    {
        uint buffStart = GameData.RoundCount;

        Reddie.Defense = Reddie.Defense + amount;

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Reddie.Defense = 0;
    }
    private IEnumerator BuffDEFSnake(uint rounds, int amount)
    {
        uint buffStart = GameData.RoundCount;

        Snake.Defense = Snake.Defense + amount;

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Snake.Defense = 0;
    }

    void HealKsiusha()
    {
        
    }

    public void Comedy()
    {
        if(SkillPoint >= 2)
        {
            Meanie.Hp += 50;
            if(Meanie.Hp > Meanie.maxHp) Meanie.Hp = Meanie.maxHp;
            Reddie.Hp += 50;
            if (Reddie.Hp > Reddie.maxHp) Reddie.Hp = Reddie.maxHp;
            Snake.Hp += 50;
            if (Snake.Hp > Meanie.maxHp) Snake.Hp = Meanie.maxHp;
            Hp += 50;
            if (Hp > maxHp) Hp = maxHp;

            SkillPoint -= 2;
        } else
        {
            // output that not enough SP
        }
    }

    public void DelusionMeanie()
    {
        if(SkillPoint >= 3)
        {
            

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
            // At best need to change this to TakeDamage(Meanie, Meanie.Hp) or smth like that 
            // for additional revivePoint check
            int damage = ((Meanie.AttackPower + Reddie.AttackPower + Snake.AttackPower) * DespairPoint)/(Meanie.Hp + Reddie.Hp + Snake.Hp); // Dividing is under a queastion....

            // I need to make a fire effect...

            Meanie.Hp = 0;
            Reddie.Hp = 0;
            Snake.Hp = 0;
            Hp = 0;

            DespairPoint = 0;

        } else
        {
            // output that not enough DP
        }
    }


}
