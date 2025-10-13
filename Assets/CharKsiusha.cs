using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CharKsiusha : MonoBehaviour, IBaseActions
{
    public int Hp = 40;
    public int maxHp = 40;
    public uint Defense = 30;
    public uint Speed = 80;
    public int AttackPower = 50;
    public int SkillPoint = 0;
    public int MaxSkillPoint = 10;
    bool evade = false;
    public bool Alive = true;

    public int revivePoint = 0;

    [SerializeField] private Boss Boss;
    [SerializeField] private RunGame GameData;

    public void Attack()
    {
        Boss.TakeDamage(AttackPower);
        SkillPoint++;
    }

    public void TakeDamage (int damage)
    {
        if (evade) { return; }

        Hp -= damage;
        if (Hp < 0 && revivePoint < 1) { Hp = 20; }
        else if (Hp < 0) 
        {
            Alive = false;
        }
    }

    private IEnumerator BuffATK(uint rounds)
    {
        uint buffStart = GameData.RoundCount;

        AttackPower = (int)(AttackPower * 1.5);

        yield return new WaitUntil(() => (rounds + buffStart == GameData.RoundCount));

        AttackPower = 40;
    }

    public void Fangbite()
    {
        if (SkillPoint >= 2)
        {
            Boss.TakeDamage(AttackPower) ;
            Hp += 10;
            SkillPoint -= 2;
        }
        else
        {
            // output that not enough SP
        }
    }

    public void StalkingPunch()
    {
        if (SkillPoint >= 3)
        {
            evade = true;

            StartCoroutine(BuffATK(1));
        }
        else
        {
            // output that not enough SP
        }
    }
}
