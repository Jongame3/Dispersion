using UnityEngine;
using System.Collections;
using System;

public class CharNastya : MonoBehaviour, IBaseActions
{

    public int Hp;
    public int maxHp;
    public int Defense;
    public int Speed;
    public int AttackPower;
    public int SkillPoint;
    public int MaxSkillPoint;

    public int revivePoint = 0;

    [SerializeField] private RunGame GameData;

    public void Attack()
    {

    }

    public void TakeDamage(int damage, bool ignore)
    {

    }

    private IEnumerator BuffDEF(uint rounds, float multiplier)
    {
        uint buffStart = GameData.RoundCount;

        Defense = (int)(Defense * multiplier);

        yield return new WaitUntil(() => (rounds + buffStart == GameData.RoundCount));

        Defense = 6;  //??
    }
}
