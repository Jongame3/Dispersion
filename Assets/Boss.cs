using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

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
    public Slider Hpslide;

    [SerializeField] private BossFrame BossFrame;

    public bool isattacking = false;

    public void Attack()
    {
        if (agr) {
            if (fireg.parryBool)
            {
                TakeDamage(AttackPower * 4, true);
                fireg.attackCounter += AttackPower * 4;
                fireg.parryBool = false;
            }
            else
            {
                fireg.TakeDamage(AttackPower * 4, false);
            }

            isattacking = false;
        }
        else
        {
            if (fireg.parryBool)
            {
                TakeDamage(AttackPower * 4, true);
                fireg.attackCounter += AttackPower * 4;
                fireg.parryBool = false;

            } else { 
                fox.TakeDamage(10, false);
                yad.TakeDamage(10, false);
                fireg.TakeDamage(10, false);
                jirai.TakeDamage(10, false);
            }
        }
    }

    public void TakeDamage(int damage, bool ignore)
    {
        if (HellFire == false)
        {
            if (ignore == false)
            {
                BossFrame.myAnimator.SetTrigger("take_damage");
                Hp -= (damage * (1 - Defense / 100));
            }
            else
            {
                BossFrame.myAnimator.SetTrigger("take_damage");
                Hp -= damage;
            }
        }
        else
        {
            BossFrame.myAnimator.SetTrigger("take_damage");
            Hp -= damage;
            Hp -= 177 * timeOfBurningInHell;
            jirai.attackCounter += 177 * timeOfBurningInHell;
            timeOfBurningInHell++;
        }

        if (Hp <= 0)
        {
            alive = false;
        }

        Hpslide.value = Hp;
    }

}
