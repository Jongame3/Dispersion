using UnityEngine;
using System.Collections;
using System;

public class CharPoison : MonoBehaviour, IBaseActions
{
    public int Hp = 100;
    public int maxHp = 100;
    public int Speed = 60;
    public const int MaxHp = 100;
    public int AttackPower;
    public int revivePoint = 0;

    public void Attack()
    {

    }

    public void TakeDamage(int damage, bool ignore)
    {

    }
}
