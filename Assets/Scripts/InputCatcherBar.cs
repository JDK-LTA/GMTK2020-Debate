using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class InputCatcherBar : MonoBehaviour
{
    [SerializeField] private float leftLimit = 0.05f, rightLimit = 0.95f;
    [SerializeField] private Slider slider;
    [SerializeField] private Image attackZoneImage, defenseZoneImage, handleImage;
    [SerializeField] public Sprite punchSprite, exclamationSprite;
    [SerializeField] private Text debugGoodOrBadText;

    [SerializeField] private float value = 0;
    [SerializeField] private float valueChangePerSec = 0.3f;
    private float originalValueChange;
    [SerializeField] private float valueSumPerHit = 0.15f;
    [SerializeField] private float valueSumPerSec = 0.005f;

    private bool punchTrueDefFalse = false;

    public delegate void ChangeImagesEvent();
    public static event ChangeImagesEvent ChangeImages;

    private void Awake()
    {
        originalValueChange = valueChangePerSec;
    }
    private void Start()
    {
        InputRequester.InputResult += UpdateDebugText;

        //attackZoneImage.rectTransform.anchorMin = new Vector2(0, 0);
        //attackZoneImage.rectTransform.anchorMax = new Vector2(leftLimit, 1);
        //attackZoneImage.rectTransform.sizeDelta = new Vector2(0, 0);
        attackZoneImage.gameObject.SetActive(false);
        defenseZoneImage.rectTransform.anchorMin = new Vector2(rightLimit, -0.2f);
        defenseZoneImage.rectTransform.anchorMax = new Vector2(1, .8f);
        defenseZoneImage.rectTransform.sizeDelta = new Vector2(0, 0);

        handleImage.sprite = exclamationSprite;
    }

    private void Update()
    {
        value += valueChangePerSec * Time.deltaTime;
        value = Mathf.Clamp01(value);

        if (value >= 1f)
        {
            value = 0;
            punchTrueDefFalse = !punchTrueDefFalse;

            handleImage.sprite = punchTrueDefFalse ? punchSprite : exclamationSprite;
            ChangeImages?.Invoke();

            InputRequester.CheckInput();
            InputRequester.RemoveOldestRequestDir();
            InputRequester.GenerateNewRequestDir();
        }

        if (value >= rightLimit)
        {
            slider.transform.Find("Handle Slide Area").Find("Handle").GetComponent<Image>().color = Color.green;
        }
        else
        {
            slider.transform.Find("Handle Slide Area").Find("Handle").GetComponent<Image>().color = Color.white;
        }

        slider.value = value;
     
        
        originalValueChange += valueSumPerSec * Time.deltaTime;
    }

    private void UpdateDebugText(bool good)
    {
        debugGoodOrBadText.text = good ? "Well done" : "Nope";

        valueChangePerSec = good ? valueChangePerSec + valueSumPerHit : originalValueChange;
    }

}
