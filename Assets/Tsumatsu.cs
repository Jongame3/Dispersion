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
    public int maxDespairPoint = 7;
    public int revivePoint = 0;
    public bool Alive = true;

    public bool changedToSweetLie = false;

    [SerializeField] private Boss Boss;
    [SerializeField] private CharKsiusha Meanie;
    [SerializeField] private CharNastya Reddie;
    [SerializeField] private CharPoison Snake;
    [SerializeField] private RunGame GameData;
    public GameObject BattleHud;
    public bool isattacking = false;
    

    public void Attack()
    {
        
    }

    public void HealMe(int amount)
    {
        if(Hp <= maxHp - amount) Hp = Hp + amount;
        else Hp = maxHp;

        isattacking = false;
    }
    public void HealMeanie(int amount)
    {
        if(Meanie.Hp <= Meanie.maxHp - amount) Meanie.Hp = Meanie.Hp + amount;
        else Meanie.Hp = Meanie.maxHp;

        isattacking = false;
    }
    public void HealReddie(int amount)
    {
        if(Reddie.Hp <= Reddie.maxHp - amount) Reddie.Hp = Reddie.Hp + amount;
        else Reddie.Hp = Reddie.maxHp;

        isattacking = false;
    }
    public void HealSnake(int amount)
    {
        if(Snake.Hp <= Snake.maxHp - amount) Snake.Hp = Snake.Hp + amount;
        else Snake.Hp = Snake.maxHp;

        isattacking = false;
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

    private IEnumerator hellFireToBoss(uint rounds)
    {
        uint buffStart = GameData.RoundCount;

        Boss.HellFire = true;

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Boss.HellFire = false;
    }

    private IEnumerator BuffATKReddie(uint rounds, int amount)
    {
        uint buffStart = GameData.RoundCount;

        Reddie.AttackPower = Reddie.AttackPower + amount;

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Reddie.AttackPower = 50;
    }
    private IEnumerator BuffATKSnake(uint rounds, int amount)
    {
        uint buffStart = GameData.RoundCount;

        Snake.AttackPower = Snake.AttackPower + amount;

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Snake.AttackPower = 50;
    }
    private IEnumerator BuffATKMeanie(uint rounds, int amount)
    {
        uint buffStart = GameData.RoundCount;

        Meanie.AttackPower = Meanie.AttackPower + amount;

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Meanie.AttackPower = 50;
    }

    private IEnumerator BuffDEFMe(uint rounds, int amount)
    {
        uint buffStart = GameData.RoundCount;

        if (amount == 100) Defense = amount;
        else Defense = Defense + amount;

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Defense = 30;
    }
    private IEnumerator BuffDEFMeanie(uint rounds, int amount)
    {
        uint buffStart = GameData.RoundCount;

        if(amount == 100) Meanie.Defense = amount;
        else Meanie.Defense = Meanie.Defense + amount;

            yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Meanie.Defense = 30;
    }
    private IEnumerator BuffDEFReddie(uint rounds, int amount)
    {
        uint buffStart = GameData.RoundCount;

        if (amount == 100) Reddie.Defense = amount;
        else Reddie.Defense = Reddie.Defense + amount;

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Reddie.Defense = 0;
    }
    private IEnumerator BuffDEFSnake(uint rounds, int amount)
    {
        uint buffStart = GameData.RoundCount;

        if (amount == 100) Snake.Defense = amount;
        else Snake.Defense = Snake.Defense + amount;

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Snake.Defense = 0;
    }
    private IEnumerator BuffSPDMe(uint rounds, int amount)
    {
        uint buffStart = GameData.RoundCount;

        Speed = Speed + amount;

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Speed = 30;
    }
    private IEnumerator BuffSPDMeanie(uint rounds, int amount)
    {
        uint buffStart = GameData.RoundCount;

        Meanie.Speed = Meanie.Speed + amount;

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Meanie.Speed = 30;
    }
    private IEnumerator BuffSPDReddie(uint rounds, int amount)
    {
        uint buffStart = GameData.RoundCount;

        Reddie.Speed = Reddie.Speed + amount;

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Reddie.Speed = 0;
    }
    private IEnumerator BuffSPDSnake(uint rounds, int amount)
    {
        uint buffStart = GameData.RoundCount;

        Snake.Speed = Snake.Speed + amount;

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Snake.Speed = 0;
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

            isattacking = false;
        } else
        {
            // output that not enough SP
        }
    }

    public void DelusionMe()
    {
        if(SkillPoint >= 3)
        {
            StartCoroutine(BuffDEFMe(2, 100));
            StartCoroutine(BuffSPDMe(3, 30));
            SkillPoint -= 3;

            isattacking = false;
        } else
        {
            // output that not enough SP
        }
    }
    public void DelusionMeanie()
    {
        if(SkillPoint >= 3)
        {
            StartCoroutine(BuffDEFMeanie(2, 100));
            StartCoroutine(BuffSPDMeanie(3, 30));
            SkillPoint -= 3;

            isattacking = false;
        } else
        {
            // output that not enough SP
        }
    }
    public void DelusionReddie()
    {
        if(SkillPoint >= 3)
        {
            StartCoroutine(BuffDEFReddie(2, 100));
            StartCoroutine(BuffSPDReddie(3, 30));
            SkillPoint -= 3;

            isattacking = false;
        } else
        {
            // output that not enough SP
        }
    }
    public void DelusionSnake()
    {
        if(SkillPoint >= 3)
        {
            StartCoroutine(BuffDEFSnake(2, 100));
            StartCoroutine(BuffSPDSnake(3, 30));
            SkillPoint -= 3;

            isattacking = false;
        } else
        {
            // output that not enough SP
        }
    }

    public void BackToFriendsMe()
    {
        if (!changedToSweetLie)
        {
            if (SkillPoint >= 4)
            {
                revivePoint++;
                SkillPoint -= 4;

                isattacking = false;
            }
            else
            {
                // output that not enough SP
            }
        } else
        {
            if(SkillPoint >= 1)
            {
                StartCoroutine(BuffDEFMe(2, 25));

                SkillPoint = 0;

                isattacking = false;
            }
            else
            {
                // output that not enough SP
            }
        }
    }
    public void BackToFriendsSnake()
    {
        if (!changedToSweetLie)
        {
            if (SkillPoint >= 4)
            {
                Snake.revivePoint++;
                SkillPoint -= 4;

                isattacking = false;
            }
            else
            {
                // output that not enough SP
            }
        } else
        {
            if(SkillPoint >= 1)
            {
                StartCoroutine(BuffATKSnake(3, 25 * SkillPoint));

                SkillPoint = 0;

                isattacking = false;
            }
            else
            {
                // output that not enough SP
            }
        }
    }
    public void BackToFriendsReddie()
    {
        if (!changedToSweetLie)
        {
            if (SkillPoint >= 4)
            {
                Reddie.revivePoint++;
                SkillPoint -= 4;

                isattacking = false;
            }
            else
            {
                // output that not enough SP
            }
        } else
        {
            if(SkillPoint >= 1)
            {
                StartCoroutine(BuffATKReddie(3, 25 * SkillPoint));

                SkillPoint = 0;

                isattacking = false;
            }
            else
            {
                // output that not enough SP
            }
        }
    }
    public void BackToFriendsMeanie()
    {
        if (!changedToSweetLie)
        {
            if (SkillPoint >= 4)
            {
                Meanie.revivePoint++;
                SkillPoint -= 4;

                isattacking = false;
            }
            else
            {
                // output that not enough SP
            }
        } else
        {
            if(SkillPoint >= 1)
            {
                StartCoroutine(BuffATKMeanie(3, 25 * SkillPoint));

                SkillPoint = 0;

                isattacking = false;
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

            isattacking = false;
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
            Meanie.TakeDamage(Meanie.Hp, false);
            StartCoroutine(BuffATKMeanie(2, 30));
            StartCoroutine(BuffDEFMeanie(2, 30));
            StartCoroutine(BuffSPDMeanie(2, 30));
            Reddie.TakeDamage(Reddie.Hp, false);
            StartCoroutine(BuffATKReddie(2, 30));
            StartCoroutine(BuffDEFReddie(2, 30));
            StartCoroutine(BuffSPDReddie(2, 30));
            Snake.TakeDamage(Snake.Hp, false);
            StartCoroutine(BuffATKSnake(2, 30));
            StartCoroutine(BuffDEFSnake(2, 30));
            StartCoroutine(BuffSPDSnake(2, 30));

            DespairPoint -= 2;
            SkillPoint += 4;

            isattacking = false;
        } else
        {
            // output that not enough DP
        }
    }

    public void EndlessLove()
    {
        if(DespairPoint >= 1)
        {
            int damage = ((Meanie.AttackPower + Reddie.AttackPower + Snake.AttackPower) * DespairPoint); // Dividing is under a queastion....
            
            TakeDamage(Hp, false);
            Boss.TakeDamage(damage, false);
            hellFireToBoss(2);
            Meanie.TakeDamage(Meanie.Hp, false);
            Reddie.TakeDamage(Reddie.Hp, false);
            Snake.TakeDamage(Snake.Hp, false);

            DespairPoint = 0;

            isattacking = false;

        } else
        {
            // output that not enough DP
        }
    }


}
