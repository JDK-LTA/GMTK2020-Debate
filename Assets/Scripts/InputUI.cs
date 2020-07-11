using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputUI : MonoBehaviour
{
    [SerializeField] private Text changeText;
    [SerializeField] private Text requestText;

    [SerializeField] private List<Text> arrowTexts = new List<Text>();
    [SerializeField] public List<ArrowImage> arrowImages = new List<ArrowImage>();

    [SerializeField] private bool showArrowTexts = true;
    void Start()
    {
        InputChanger.ChangedInput += UpdateChangeText;
        InputChanger.ChangedInput2 += UpdateChangeText;

        InputChanger.UpdateGreens += UpdateGreenImages;
    }
    private void Update()
    {
        requestText.text = InputRequester.requestedDirs[0].ToString();
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
        string iC, iN;

        iC = UpdateChangeWord(iChanged);
        iN = UpdateChangeWord(iNew);

        changeText.text = iC + " is " + iN;

        for (int i = 0; i < arrowTexts.Count; i++)
        {
            arrowTexts[i].text = InputManager.Instance.Codes[i].ToString().TrimEnd("Arrow".ToCharArray());
        }
    }
    private void UpdateChangeText(KeyCode iChanged, KeyCode iNew)
    {
        string iC, iN;

        iC = UpdateChangeWord(iChanged);
        iN = UpdateChangeWord(iNew);

        changeText.text = iC + " is " + iN;

        for (int i = 0; i < arrowTexts.Count; i++)
        {
            arrowTexts[i].text = InputManager.Instance.Codes[i].ToString().TrimEnd("Arrow".ToCharArray());
        }
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
}
