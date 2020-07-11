using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class InputCatcherBar : MonoBehaviour
{
    [SerializeField] private float leftLimit = 0.05f, rightLimit = 0.95f;
    [SerializeField] private Slider slider;
    [SerializeField] private Image attackZoneImage, defenseZoneImage;
    [SerializeField] private Text debugGoodOrBadText;

    [SerializeField] private float value = 0;
    [SerializeField] private float valueChangePerSec = 0.1f;

    private bool leftTrueRightFalse = false;

    private bool recieveInput = false;

    public bool RecieveInput { get => recieveInput; }

    private void Start()
    {
        InputRequester.InputResult += UpdateDebugText;

        attackZoneImage.rectTransform.anchorMin = new Vector2(0, 0);
        attackZoneImage.rectTransform.anchorMax = new Vector2(leftLimit, 1);
        attackZoneImage.rectTransform.sizeDelta = new Vector2(0, 0);
        defenseZoneImage.rectTransform.anchorMin = new Vector2(rightLimit, 0);
        defenseZoneImage.rectTransform.anchorMax = new Vector2(1, 1);
        defenseZoneImage.rectTransform.sizeDelta = new Vector2(0, 0);
    }

    private void Update()
    {
        if (!leftTrueRightFalse)
        {
            value += valueChangePerSec * Time.deltaTime;
            value = Mathf.Clamp01(value);

            if (value >= 1f)
            {
                leftTrueRightFalse = true;
                InputRequester.CheckInput();
                InputRequester.RemoveOldestRequestDir();
                InputRequester.GenerateNewRequestDir();
            }
        }
        else
        {
            value -= valueChangePerSec * Time.deltaTime;
            value = Mathf.Clamp01(value);

            if (value <= 0f)
            {
                leftTrueRightFalse = false;
                InputRequester.CheckInput();
                InputRequester.RemoveOldestRequestDir();
                InputRequester.GenerateNewRequestDir();
            }
        }

        if (value <= leftLimit || value >= rightLimit)
        {
            slider.transform.Find("Handle Slide Area").Find("Handle").GetComponent<Image>().color = Color.green;
            recieveInput = true;
        }
        else
        {
            slider.transform.Find("Handle Slide Area").Find("Handle").GetComponent<Image>().color = Color.red;
            recieveInput = false;
        }

        slider.value = value;
    }

    private void UpdateDebugText(bool good)
    {
        debugGoodOrBadText.text = good ? "Well done" : "Nope";
    }

}
