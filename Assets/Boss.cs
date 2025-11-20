using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Boss : MonoBehaviour, IBaseActions
{
    public Animator myAnimator;
    public const string DAMAGE_ANIM = "take_damage";

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
    public bool isattacking = false;
    public TextMeshProUGUI HPtext;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void Attack()
    {
        if (agr) {
            if (fireg.parryBool)
            {
                fireg.myAnimator.SetTrigger(DAMAGE_ANIM);
                Hp -= AttackPower;
                fireg.parryBool = false;
            }
            else
            {
                fireg.myAnimator.SetTrigger(DAMAGE_ANIM);
                fireg.Hp -= AttackPower;
            }

            isattacking = false;
        }
        else
        {
            fox.myAnimator.SetTrigger(DAMAGE_ANIM);
            fox.TakeDamage(10, false);
            yad.myAnimator.SetTrigger(DAMAGE_ANIM);
            yad.TakeDamage(10, false);
            fireg.myAnimator.SetTrigger(DAMAGE_ANIM);
            fireg.TakeDamage(10, false);
            jirai.myAnimator.SetTrigger(DAMAGE_ANIM);
            jirai.TakeDamage(10, false);
        }
    }

    public void TakeDamage(int damage, bool ignore)
    {
        if (HellFire == false)
        {
            if (ignore == false)
            {
                myAnimator.SetTrigger(DAMAGE_ANIM);
                Hp -= (damage * (1 - Defense / 100));
            }
            else
            {
                myAnimator.SetTrigger(DAMAGE_ANIM);
                Hp -= damage;
            }
        }
        else
        {
            myAnimator.SetTrigger(DAMAGE_ANIM);
            Hp -= damage;
            Hp -= 177 * timeOfBurningInHell;
            timeOfBurningInHell++;
        }

        if (Hp < 0)
        {
            alive = false;
        }

        HPtext.text = "HP:" + Hp.ToString() + "/" + maxHp.ToString();

    }

}
