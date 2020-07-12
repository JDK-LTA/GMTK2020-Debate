using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

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
    private int orSubWhenHit;
    [SerializeField] private Text scoreText;
    private int currentHp;
    [SerializeField] private int punchesHit = 0;
    private int totalGoodActions = 0;
    [SerializeField] private int hitsToChangeInput = 3;
    private bool canChangeInput = false;
    public int CurrentHp { get => currentHp; }
    public int MaxHp { get => maxHp; }
    public bool CanChangeInput { get => canChangeInput; }
    public float Points { get => points; }

    private float points = 0;
    [SerializeField] private float pointsPerSec = 100;
    [SerializeField] private float pointsGainedPerGoodAction = 10;
    private float orPointsGained;
    [SerializeField] private float pointsAddedPerCombo = 5;
    [SerializeField] private GameObject finalScene;

    [SerializeField] private AudioClip changeInput, getHit, woosh, smash;

    private void Start()
    {
        orSubWhenHit = subWhenHit;
        orPointsGained = pointsGainedPerGoodAction;
        InputRequester.InputResult += ActionResults;
    }
    private void Update()
    {
        points += pointsPerSec * Time.deltaTime;
        scoreText.text = ((int)points).ToString();
    }

    private void ActionResults(bool good)
    {
        if (good)
        {
            if (InputRequester.punchTrueDefFalse)
            {
                currentHp += sumWhenPunch;
                AudioSource.PlayClipAtPoint(smash, Vector3.zero);
            }
            else
            {
                AudioSource.PlayClipAtPoint(woosh, Vector3.zero);
            }
            punchesHit++;

            if (punchesHit >= hitsToChangeInput)
            {
                canChangeInput = true;
            }

            totalGoodActions++;
            if (subWhenHit < 30)
            {
                subWhenHit += 2;
            }

            points += pointsGainedPerGoodAction;
            pointsGainedPerGoodAction += pointsAddedPerCombo;
        }
        else
        {
            if (canChangeInput)
            {
                canChangeInput = false;
                punchesHit = 0;
                InputChanger.Instance.ChangeInput();
                AudioSource.PlayClipAtPoint(changeInput, Vector3.zero);
            }
            if (!InputRequester.punchTrueDefFalse)
            {
                if (!canChangeInput)
                {
                    AudioSource.PlayClipAtPoint(getHit, Vector3.zero);
                }
                subWhenHit = orSubWhenHit;
                currentHp -= subWhenHit;
            }

            pointsGainedPerGoodAction = orPointsGained;
        }

        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        if (currentHp <= 0)
        {
            Time.timeScale = 0;
            finalScene.SetActive(true);
        }
    }
}
