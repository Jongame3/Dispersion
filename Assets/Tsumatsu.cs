using System;
using UnityEngine;

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

    [SerializeField] private Boss Boss;
    [SerializeField] private CharKsiusha K;
    [SerializeField] private CharNastya N;
    [SerializeField] private CharPoison P;

    public void Attack()
    {
        
    }

    void Start()
    {

    }

    
    void Update()
    {
        
    }
}
