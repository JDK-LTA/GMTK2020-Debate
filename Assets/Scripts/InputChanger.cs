using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputChanger : MonoBehaviour
{
    public delegate void ChangerEvents(int i, int j);
    public static event ChangerEvents ChangedInput;
    public delegate void ChangerEvents2(KeyCode i, KeyCode j);
    public static event ChangerEvents2 ChangedInput2;

    private float tChange = 0;
    public float timerToChangeInput = 5;

    private int lastIChanged = -1;
    private int lastNewChanged = -1;
    private KeyCode lastICodeChanged = KeyCode.Asterisk;
    private KeyCode lastNewCodeChanged = KeyCode.Asterisk;

    [SerializeField] bool true2_false1 = false;
    [SerializeField] bool canChangeTheSameTwice = false;

    private void Update()
    {
        tChange += Time.deltaTime;
        if (tChange >= timerToChangeInput)
        {
            tChange = 0;
            if (true2_false1)
            {
                RNGenerateChange();
            }
            else
            {
                RNGenerateChange2();
            }
        }
    }

    private void RNGenerateChange()
    {
        List<KeyCode> auxCodes = InputManager.Instance.Codes;

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
