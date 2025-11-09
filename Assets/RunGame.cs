using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

public class RunGame : MonoBehaviour
{
    public uint RoundCount = 0;
    [SerializeField] private CharKsiusha fox;
    [SerializeField] private CharPoison yad;
    [SerializeField] private CharNastya fireg;
    [SerializeField] private Tsumatsu jirai;
    [SerializeField] private Boss Boss;

    Queue<string> actionqueue = new Queue<string>();

    private IEnumerator AttackFlow()
    {
        while (Boss.alive)
        {
            for (int i = 0; i < 4; i++)
            {
                if (actionqueue.Peek() == "TSUMATSU")
                {
                    jirai.BattleHud.SetActive(true);

                    jirai.isattacking = true;

                    yield return new WaitUntil(() => jirai.isattacking == false);

                    jirai.BattleHud.SetActive(false);
                    actionqueue.Dequeue();

                }

                if (actionqueue.Peek() == "FOX")
                {
                    fox.BattleHud.SetActive(true);

                    fox.isattacking = true;

                    yield return new WaitUntil(() => fox.isattacking == false);

                    fox.BattleHud.SetActive(false);
                    actionqueue.Dequeue();
                }


                if (actionqueue.Peek() == "KANDZIO")
                {
                    fireg.BattleHud.SetActive(true);

                    fireg.isattacking = true;

                    yield return new WaitUntil(() => fireg.isattacking == false);

                    fireg.BattleHud.SetActive(false);
                    actionqueue.Dequeue();
                }




                if (actionqueue.Peek() == "POISON")
                {
                    yad.BattleHud.SetActive(true);

                    yad.isattacking = true;

                    yield return new WaitUntil(() => yad.isattacking == false);

                    yad.BattleHud.SetActive(false);
                    actionqueue.Dequeue();
                }

            }

            
        }
    }

    Queue<string> CreateQueue()
    {

        for (int i = 0; i < 4; i++)
        {
            int maxSpeed = -1;
            string maxName = "k";
            ;

            if (maxSpeed < fox.Speed && !actionqueue.Contains("FOX"))
            {
                maxSpeed = fox.Speed;
                maxName = "FOX";
            }

            if (maxSpeed < fireg.Speed && !actionqueue.Contains("KANDZIO"))
            {
                maxSpeed = fireg.Speed;
                maxName = "KANDZIO";
            }

            if (maxSpeed < jirai.Speed && !actionqueue.Contains("TSUMATSU"))
            {
                maxSpeed = jirai.Speed;
                maxName = "TSUMATSU";
            }

            if (maxSpeed < yad.Speed && !actionqueue.Contains("POISON"))
            {
                maxSpeed = yad.Speed;
                maxName = "POISON";
            }

            actionqueue.Enqueue(maxName);
        }
        return actionqueue;
    }

    private void Start()
    {
        CreateQueue();

        StartCoroutine(AttackFlow());
    }



}
