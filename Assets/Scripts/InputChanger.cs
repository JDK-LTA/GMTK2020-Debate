using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputChanger : MonoBehaviour
{
    public static InputChanger Instance;
    private void Awake()
    {
        Instance = this;
    }

    public delegate void ChangerEvents(int i, int j);
    public static event ChangerEvents ChangedInput;
    public delegate void ChangerEvents2(KeyCode i, KeyCode j);
    public static event ChangerEvents2 ChangedInput2;
    public delegate void ArrowImageChangeEvents(DirectionEnum dChange, DirectionEnum dNew);
    public static event ArrowImageChangeEvents UpdateGreens;

    private int lastIChanged = -1;
    private int lastNewChanged = -1;
    private KeyCode lastICodeChanged = KeyCode.Asterisk;
    private KeyCode lastNewCodeChanged = KeyCode.Asterisk;

    [SerializeField] bool true2_false1 = false;
    [SerializeField] bool canChangeTheSameTwice = false;

    public void ChangeInput()
    {
        if (true2_false1)
        {
            RNGenerateChange();
        }
        else
        {
            RNGenerateChange2();
        }
        Debug.Log("Changed Input");
    }

    private void RNGenerateChange()
    {
        List<KeyCode> auxCodes = InputManager.Instance.Codes;
        List<DirectionEnum> auxDirs = InputManager.Instance.Directions;

        int iToChange;
        int iNew;

        if (!canChangeTheSameTwice)
        {
            do
            {
                GetIndexes(auxCodes, out iToChange, out iNew);
            } while (iToChange == lastIChanged && iNew == lastNewChanged);
        }
        else
        {
            GetIndexes(auxCodes, out iToChange, out iNew);
        }

        lastIChanged = iToChange;
        lastNewChanged = iNew;

        KeyCode auxKC = auxCodes[iToChange];

        auxCodes[iToChange] = auxCodes[iNew];
        auxCodes[iNew] = auxKC;

        DirectionEnum auxDC = auxDirs[iToChange];
        DirectionEnum auxDN = auxDirs[iNew];

        UpdateGreens(auxDC, auxDN);

        for (int i = 0; i < InputRequester.pressedDirs.Count; i++)
        {
            if (InputRequester.pressedDirs[i] == auxDirs[iToChange])
            {
                InputRequester.pressedDirs[i] = auxDN;
            }
            else if (InputRequester.pressedDirs[i] == auxDirs[iNew])
            {
                InputRequester.pressedDirs[i] = auxDC;
            }
        }

        ChangedInput?.Invoke(iToChange, iNew);
    }

    private void GetIndexes(List<KeyCode> auxCodes, out int iToChange, out int iNew)
    {
        iToChange = Random.Range(0, auxCodes.Count);
        do
        {
            iNew = Random.Range(0, auxCodes.Count);
        } while (iNew == iToChange);
    }

    private void RNGenerateChange2()
    {
        List<KeyCode> auxCodes = InputManager.Instance.Codes;
        List<DirectionEnum> auxDirs = InputManager.Instance.Directions;

        int iToChange;
        int iNew;

        if (!canChangeTheSameTwice)
        {
            do
            {
                GetIndexes2(auxCodes, out iToChange, out iNew);
            } while (auxCodes[iToChange] == lastICodeChanged && auxCodes[iNew] == lastNewCodeChanged);
        }
        else
        {
            GetIndexes2(auxCodes, out iToChange, out iNew);
        }

        lastICodeChanged = auxCodes[iToChange];
        lastNewCodeChanged = auxCodes[iNew];

        KeyCode auxKC = auxCodes[iToChange];

        auxCodes[iToChange] = auxCodes[iNew];
        auxCodes[iNew] = auxKC;

        DirectionEnum auxDC = auxDirs[iToChange];
        DirectionEnum auxDN = auxDirs[iNew];

        UpdateGreens(auxDC, auxDN);

        for (int i = 0; i < InputRequester.pressedDirs.Count; i++)
        {
            if (InputRequester.pressedDirs[i] == auxDirs[iToChange])
            {
                InputRequester.pressedDirs[i] = auxDN;
            }
            else if (InputRequester.pressedDirs[i] == auxDirs[iNew])
            {
                InputRequester.pressedDirs[i] = auxDC;
            }
        }

        ChangedInput2?.Invoke(auxCodes[iNew], auxCodes[iToChange]);
    }

    private static void GetIndexes2(List<KeyCode> auxCodes, out int iToChange, out int iNew)
    {
        iToChange = Random.Range(0, auxCodes.Count);
        do
        {
            iNew = Random.Range(0, auxCodes.Count);
        } while (auxCodes[iNew] == auxCodes[iToChange]);
    }
}
