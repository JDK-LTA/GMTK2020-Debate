using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputUI : MonoBehaviour
{
    [SerializeField] private Image controlToChangeImage, newControlImage, isImage;
    [SerializeField] private Sprite upS, downS, leftS, rightS;

    [SerializeField] private List<Text> arrowTexts = new List<Text>();
    [SerializeField] public List<ArrowImage> arrowImages = new List<ArrowImage>();
    [SerializeField] public List<Image> exclamationImages = new List<Image>();
    public static List<Image> exImages = new List<Image>();
    [SerializeField] public Sprite punchSprite, exclamationSprite;

    [SerializeField] private Image hpBar;

    //DEPRECATED \ DEBUG
    [SerializeField] private Text requestText;
    [SerializeField] private bool showArrowTexts = true;

    private bool exTruePunchFalse = true;

    private void Awake()
    {
        exImages = exclamationImages;
    }
    void Start()
    {
        InputChanger.ChangedInput += UpdateChangeText;
        InputChanger.ChangedInput2 += UpdateChangeText;

        InputChanger.UpdateGreens += UpdateGreenImages;

        InputCatcherBar.ChangeImages += ChangeExImages;
    }
    private void Update()
    {
        hpBar.fillAmount = (float)Player.Instance.CurrentHp / (float)Player.Instance.MaxHp;
        
        requestText.text = InputRequester.requestedDirs[0].ToString();
    }

    private void ChangeExImages()
    {
        exTruePunchFalse = !exTruePunchFalse;
        for (int i = 0; i < exImages.Count; i++)
        {
            exImages[i].sprite = exTruePunchFalse ? exclamationSprite : punchSprite;
        }
    }

    private void UpdateGreenImages(DirectionEnum dChange, DirectionEnum dNew)
    {
        int i = 0, j = 0;

        for (int m = 0; m < arrowImages.Count; m++)
        {
            if (arrowImages[m].dir == dChange)
            {
                i = m;
            }
            else if (arrowImages[m].dir == dNew)
            {
                j = m;
            }
        }

        if (arrowImages[i].green)
        {
            if (!arrowImages[j].green)
            {
                arrowImages[i].ChangeToRed();
                arrowImages[j].ChangeToGreen();
            }
        }
        else
        {
            if (arrowImages[j].green)
            {
                arrowImages[i].ChangeToGreen();
                arrowImages[j].ChangeToRed();
            }
        }
    }

    private void UpdateChangeText(int iChanged, int iNew)
    {
        //string iC, iN;

        //iC = UpdateChangeWord(iChanged);
        //iN = UpdateChangeWord(iNew);

        //changeText.text = iC + " is " + iN;

        //for (int i = 0; i < arrowTexts.Count; i++)
        //{
        //    arrowTexts[i].text = InputManager.Instance.Codes[i].ToString().TrimEnd("Arrow".ToCharArray());
        //}

        controlToChangeImage.sprite = UpdateWords(iChanged);
        newControlImage.sprite = UpdateWords(iNew);

        controlToChangeImage.gameObject.SetActive(true);
        newControlImage.gameObject.SetActive(true);
        isImage.gameObject.SetActive(true);

        StartCoroutine(DeactivateUI(controlToChangeImage, newControlImage, isImage));
    }
    private void UpdateChangeText(KeyCode iChanged, KeyCode iNew)
    {
        //string iC, iN;

        //iC = UpdateChangeWord(iChanged);
        //iN = UpdateChangeWord(iNew);

        //changeText.text = iC + " is " + iN;

        //for (int i = 0; i < arrowTexts.Count; i++)
        //{
        //    arrowTexts[i].text = InputManager.Instance.Codes[i].ToString().TrimEnd("Arrow".ToCharArray());
        //}

        controlToChangeImage.sprite = UpdateWords(iChanged);
        newControlImage.sprite = UpdateWords(iNew);

        controlToChangeImage.gameObject.SetActive(true);
        newControlImage.gameObject.SetActive(true);
        isImage.gameObject.SetActive(true);

        StartCoroutine(DeactivateUI(controlToChangeImage, newControlImage, isImage));
    }

    private Sprite UpdateWords(int i)
    {
        Sprite aux;
        switch (i)
        {
            case 0:
                aux = upS;
                break;
            case 1:
                aux = downS;
                break;
            case 2:
                aux = leftS;
                break;
            case 3:
                aux = rightS;
                break;

            default:
                aux = upS;
                break;
        }

        return aux;
    }
    private Sprite UpdateWords(KeyCode i)
    {
        Sprite aux;
        switch (i)
        {
            case KeyCode.UpArrow:
                aux = upS;
                break;
            case KeyCode.DownArrow:
                aux = downS;
                break;
            case KeyCode.LeftArrow:
                aux = leftS;
                break;
            case KeyCode.RightArrow:
                aux = rightS;
                break;

            default:
                aux = upS;
                break;
        }

        return aux;
    }
    private static string UpdateChangeWord(int i)
    {
        string aux;
        switch (i)
        {
            case 0:
                aux = "Up";
                break;
            case 1:
                aux = "Down";
                break;
            case 2:
                aux = "Left";
                break;
            case 3:
                aux = "Right";
                break;

            default:
                aux = "Error";
                break;
        }

        return aux;
    }
    private static string UpdateChangeWord(KeyCode i)
    {
        string aux;
        switch (i)
        {
            case KeyCode.UpArrow:
                aux = "Up";
                break;
            case KeyCode.DownArrow:
                aux = "Down";
                break;
            case KeyCode.LeftArrow:
                aux = "Left";
                break;
            case KeyCode.RightArrow:
                aux = "Right";
                break;

            default:
                aux = "Error";
                break;
        }

        return aux;
    }
    IEnumerator DeactivateUI(Image image, Image image2, Image image3)
    {
        yield return new WaitForSeconds(4f);

        image.gameObject.SetActive(false);
        image2.gameObject.SetActive(false);
        image3.gameObject.SetActive(false);
    }
}
