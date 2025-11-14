
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


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
    public int maxDespairPoint = 7;
    public int revivePoint = 0;
    public bool Alive = true;


    [SerializeField] private Boss Boss;
    [SerializeField] private CharKsiusha Meanie;
    [SerializeField] private CharNastya Reddie;
    [SerializeField] private CharPoison Snake;
    [SerializeField] private RunGame GameData;
    public GameObject BattleHud;
    public GameObject DespairHud;
    public GameObject HealUI;
    public GameObject DelusionUI;
    public GameObject BTFUI;
    public bool isattacking = false;
    public bool DespairMode = false;
    public TextMeshProUGUI Text;
    public TextMeshProUGUI SPText;
    public TextMeshProUGUI SPText2;
    public TextMeshProUGUI DPText;
    public TextMeshProUGUI DPText2;
    public Sprite DespairSprite;
    public Sprite BaseSprite;
    public TextMeshProUGUI HPtext;
    public TextMeshProUGUI HPtext2;


    public void Attack()
    {
        HealUI.SetActive(true);
    }

    public void Delusion()
    {
        if (SkillPoint >= 3)
        {
            DelusionUI.SetActive(true);
            SkillPoint -= 3;
        }
        else
        {
            Text.text = "Недостаточно Очков Умений";
        }
    }

    public void BackToFriends()
    {
        if (SkillPoint >= 4)
        {
            BTFUI.SetActive(true);
            SkillPoint -= 4;
        }
        else
        {
            Text.text = "Недостаточно Очков Умений";
        }
    }

    public void Defence()
    {
        StartCoroutine(BuffDEFMe(1, 15));
        if (SkillPoint < MaxSkillPoint)
        {
            SkillPoint += 2;
        }
    }

    public void DespairModeOn()
    {
        if (SkillPoint >= 1)
        {
            DespairMode = true;
            this.gameObject.GetComponent<Image>().sprite = DespairSprite;
            SkillPoint -= 1;

            isattacking = false;
        }
        else
        {
            Text.text = "Недостаточно Очков Умений";
        }
    }

    public void FirstMode()
    {
        DespairMode = false;
        this.gameObject.GetComponent<Image>().sprite = BaseSprite;

        isattacking = false;
    }

    public void HealMe()
    {
        if (Hp <= maxHp - 20) Hp = Hp + 20;
        else Hp = maxHp;
        if (SkillPoint < MaxSkillPoint) {
            SkillPoint++;
        }
        HealUI.SetActive(false);

        isattacking = false;
    }
    public void HealMeanie()
    {
        if (Meanie.Hp <= Meanie.maxHp - 20) Meanie.Hp = Meanie.Hp + 20;
        else Meanie.Hp = Meanie.maxHp;

        if (SkillPoint < MaxSkillPoint)
        {
            SkillPoint++;
        }
        HealUI.SetActive(false);

        isattacking = false;
    }
    public void HealReddie()
    {
        if (Reddie.Hp <= Reddie.maxHp - 20) Reddie.Hp = Reddie.Hp + 20;
        else Reddie.Hp = Reddie.maxHp;

        if (SkillPoint < MaxSkillPoint)
        {
            SkillPoint++;
        }
        HealUI.SetActive(false);

        isattacking = false;
    }
    public void HealSnake()
    {
        if (Snake.Hp <= Snake.maxHp - 20) Snake.Hp = Snake.Hp + 20;
        else Snake.Hp = Snake.maxHp;

        if (SkillPoint < MaxSkillPoint)
        {
            SkillPoint++;
        }
        HealUI.SetActive(false);


        isattacking = false;
    }

    public void TakeDamage(int damage, bool ignore)
    {
        Hp -= (damage * (1 - Defense / 100));

        if (Hp <= 0 && revivePoint > 0)
        {
            Hp = maxHp / 2;
            revivePoint--;
        }
        else if (Hp < 0)
        {
            Alive = false;
        }
        DespairPoint++;
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

        Reddie.AttackPower = 70;
    }
    private IEnumerator BuffATKSnake(uint rounds, int amount)
    {
        uint buffStart = GameData.RoundCount;

        Snake.AttackPower = Snake.AttackPower + amount;

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Snake.AttackPower = 70;
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

        if (amount == 100) Meanie.Defense = amount;
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

        Reddie.Defense = 40;
    }
    private IEnumerator BuffDEFSnake(uint rounds, int amount)
    {
        uint buffStart = GameData.RoundCount;

        if (amount == 100) Snake.Defense = amount;
        else Snake.Defense = Snake.Defense + amount;

        yield return new WaitUntil(() => (rounds + buffStart + 1 == GameData.RoundCount));

        Snake.Defense = 40;
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
        if (SkillPoint >= 2)
        {
            Meanie.Hp += 50;
            if (Meanie.Hp > Meanie.maxHp) Meanie.Hp = Meanie.maxHp;
            Reddie.Hp += 50;
            if (Reddie.Hp > Reddie.maxHp) Reddie.Hp = Reddie.maxHp;
            Snake.Hp += 50;
            if (Snake.Hp > Meanie.maxHp) Snake.Hp = Meanie.maxHp;
            Hp += 50;
            if (Hp > maxHp) Hp = maxHp;

            SkillPoint -= 2;

            isattacking = false;
        }
        else
        {
            Text.text = "Недостаточно Очков Умений";
        }
    }

    public void DelusionMe()
    {

        StartCoroutine(BuffDEFMe(2, 100));
        StartCoroutine(BuffSPDMe(3, 30));

        isattacking = false;

    }
    public void DelusionMeanie()
    {

        StartCoroutine(BuffDEFMeanie(2, 100));
        StartCoroutine(BuffSPDMeanie(3, 30));

        isattacking = false;

    }
    public void DelusionReddie()
    {
        StartCoroutine(BuffDEFReddie(2, 100));
        StartCoroutine(BuffSPDReddie(3, 30));

        isattacking = false;
    }
    public void DelusionSnake()
    {
        StartCoroutine(BuffDEFSnake(2, 100));
        StartCoroutine(BuffSPDSnake(3, 30));

        isattacking = false;
    }

    public void BackToFriendsMe()
    {

        revivePoint++;

        isattacking = false;
    }

    public void BackToFriendsSnake()
    {

        Snake.revivePoint++;

        isattacking = false;
    }

    public void BackToFriendsMeanie()
    {
        Meanie.revivePoint++;

        isattacking = false;
    }

    public void BackToFriendsReddie()
    {
        Reddie.revivePoint++;

        isattacking = false;
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
        }
        else
        {
            Text.text = "Недостаточно Очков Отчаяния";;
        }
    }

    public void EndlessLove()
    {
        if (DespairPoint >= 1)
        {
            int damage = ((Meanie.AttackPower + Reddie.AttackPower + Snake.AttackPower) * DespairPoint); // Dividing is under a queastion....

            TakeDamage(Hp, false);
            Boss.TakeDamage(damage, false);
            StartCoroutine(hellFireToBoss(2));
            Meanie.TakeDamage(Meanie.Hp, false);
            Reddie.TakeDamage(Reddie.Hp, false);
            Snake.TakeDamage(Snake.Hp, false);

            DespairPoint = 0;

            isattacking = false;

        }
        else
        {
            Text.text = "Недостаточно Очков Отчаяния";;
        }
    }
    public void SweetLie()
    {
        if (SkillPoint >= 1)
        {
            StartCoroutine(BuffATKMeanie(2, 25 * SkillPoint));
            StartCoroutine(BuffATKReddie(2, 25 * SkillPoint));
            StartCoroutine(BuffATKSnake(2, 25 * SkillPoint));
            StartCoroutine(BuffDEFMe(2, 25));
            SkillPoint = 0;

            isattacking = false;
        }
        else
        {
            Text.text = "Недостаточно Очков Умений";
        }
    }

};