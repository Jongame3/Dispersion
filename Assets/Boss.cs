using UnityEngine;

public class Boss : MonoBehaviour, IBaseActions
{
    public int Hp = 10000;
    public int maxHp = 10000;
    [SerializeField] private CharKsiusha fox;
    [SerializeField] private CharPoison yad;
    [SerializeField] private CharNastya fireg;
    [SerializeField] private Tsumatsu jirai ;

    
    public void Attack()
    {
        fox.Hp = Hp - 10;
        
    }

}
