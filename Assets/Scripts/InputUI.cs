using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputUI : MonoBehaviour
{
    [SerializeField] private Text changeText;
    [SerializeField] private ArrowImage up, down, left, right;

    [SerializeField] private List<Text> arrowTexts = new List<Text>();

    [SerializeField] private bool showArrowTexts = true;
    // Start is called before the first frame update
    void Start()
    {
        InputChanger.ChangedInput += UpdateChangeText;
        InputChanger.ChangedInput2 += UpdateChangeText;

        InputManager.Instance.UpArrow += up.ChangeToGreen;
        InputManager.Instance.DownArrow += down.ChangeToGreen;
        InputManager.Instance.LeftArrow += left.ChangeToGreen;
        InputManager.Instance.RightArrow += right.ChangeToGreen;
    }

    private void UpdateChangeText(int iChanged, int iNew)
    {
        string iC, iN;

        iC = UpdateChangeWord(iChanged);
        iN = UpdateChangeWord(iNew);

        changeText.text = iC + " is " + iN;

        for (int i = 0; i < arrowTexts.Count; i++)
        {
            arrowTexts[i].text = InputManager.Instance.Codes[i].ToString();
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
            arrowTexts[i].text = InputManager.Instance.Codes[i].ToString();
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
