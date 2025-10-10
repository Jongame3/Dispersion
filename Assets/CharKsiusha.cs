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
    bool attackBuffed = false;

    public int revivePoint = 0;

    [SerializeField] private Boss Boss;

    public void Attack()
    {
        Boss.Hp = Boss.Hp - AttackPower;
        SkillPoint++;

    }

    public void Fangbite()
    {
        if (SkillPoint >= 2)
        {
            Boss.Hp -= AttackPower ;
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
            attackBuffed = true;
        }
    }
}
