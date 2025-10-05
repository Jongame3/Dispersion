using UnityEngine;

public class Boss : MonoBehaviour, IBaseActions
{
    public int Hp = 10000;
    CharPoison P;
    public void Attack()
    {
        P.Hp = Hp - 10;
    }

}
