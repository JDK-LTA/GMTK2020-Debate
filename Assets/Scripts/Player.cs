using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private void Awake()
    {
        Instance = this;

        currentHp = maxHp;
    }

    [SerializeField] private int maxHp = 100;
    [SerializeField] private int sumWhenPunch = 5, subWhenHit = 10;
    private int currentHp;

    public int CurrentHp { get => currentHp; }
    public int MaxHp { get => maxHp; }

    private void Start()
    {
        InputRequester.InputResult += ChangeHp;
    }

    private void ChangeHp(bool good)
    {
        currentHp = good ? currentHp += sumWhenPunch : currentHp -= subWhenHit;

        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
    }
}
