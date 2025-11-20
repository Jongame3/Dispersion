using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CharNastya : MonoBehaviour, IBaseActions
{

    public Animator myAnimator;
    public const string DAMAGE_ANIM = "take_damage";
    public const string HEAL_ANIM = "heals";

    public int Hp = 90;
    public int maxHp = 90;
    public float Defense = 40;
    public int Speed = 0;
    public int AttackPower = 70;
    public int SkillPoint = 0;
    public int MaxSkillPoint = 10;
    public bool Alive = true;
    public bool parryBool = false;
    public TextMeshProUGUI Text;

    public int revivePoint = 0;

    [SerializeField] private RunGame GameData;
    [SerializeField] private Tsumatsu jirai;
    [SerializeField] private Boss Boss;
    public GameObject BattleHud;
    public bool isattacking = false;
    public TextMeshProUGUI HPtext;
    public TextMeshProUGUI SpText;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void Attack()
    {
        Boss.myAnimator.SetTrigger(DAMAGE_ANIM);
        Boss.TakeDamage((int)(AttackPower * (1+(maxHp/200))), false);

        if (SkillPoint < MaxSkillPoint)
        {
            SkillPoint++;
        }

        isattacking = false;
    }

    public void TakeDamage(int damage, bool ignore)
    {

        if (!parryBool) { 
        
            float percentDefence = 1 - (Defense / 100);

            int effdamage = (int)(damage * percentDefence);
            myAnimator.SetTrigger(DAMAGE_ANIM);
            Hp -= effdamage;

            if (jirai.DespairPoint < jirai.maxDespairPoint)
            {
                jirai.DespairPoint++;
            }

            if (Hp <= 0 && revivePoint > 0) {
                myAnimator.SetTrigger(HEAL_ANIM);
                Hp = maxHp / 2; 
            }

            else if (Hp < 0)
            {
                Alive = false;
            }

        }
        else
        {
            Boss.myAnimator.SetTrigger(DAMAGE_ANIM);
            Boss.TakeDamage(damage, true);
        }
    }

    public void Defence()
    {
        StartCoroutine(BuffDEF(1, 1.5f));
        if (SkillPoint < MaxSkillPoint)
        {
            SkillPoint += 2;
        }
        isattacking = false;
    }

    public void agr()
    {
        if(SkillPoint >= 1)
        {
            if (Boss.disorientation)
            {
                Boss.agr = true;
                StartCoroutine(BuffDEF(3, 20));
            }
            else
            {
                Boss.agr = true;
            }
            SkillPoint--;

            isattacking = false;
        }
        else
        {
            Text.text = "Недостаточно Очков Умений";
        }
    }

    public void parry()
    {
        if (SkillPoint >= 2)
        {
            parryBool = true;
            SkillPoint -= 2;

            isattacking = false;
        }
        else
        {
            Text.text = "Недостаточно Очков Умений";
        }
    }

    public void dismemberment()
    {
        if (SkillPoint >= 3)
        {
            Boss.myAnimator.SetTrigger(DAMAGE_ANIM);
            Boss.TakeDamage(Boss.maxHp/10, false);
            StartCoroutine(disorientBuff(4));
            SkillPoint -= 3;

            isattacking = false;

        }
        else
        {
            Text.text = "Недостаточно Очков Умений";
        }
    }

    public void ult()
    {
        if (SkillPoint >= 5)
        {
            maxHp = maxHp * 2;
            Hp = maxHp;
            SkillPoint -= 5;

            isattacking = false;
        }
        else
        {
            Text.text = "Недостаточно Очков Умений";
        }
    }

    private IEnumerator BuffDEF(uint rounds, float multiplier)
    {
        uint buffStart = GameData.RoundCount;

        Defense = (int)(Defense * multiplier);

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Defense = 40;
    }

    private IEnumerator disorientBuff(uint rounds)
    {
        uint buffStart = GameData.RoundCount;

        Boss.disorientation = true;

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Boss.disorientation = false;
    }
}
