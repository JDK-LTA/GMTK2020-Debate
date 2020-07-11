using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class InputRequester : MonoBehaviour
{
    public static List<DirectionEnum> requestedDirs = new List<DirectionEnum>();
    public static List<DirectionEnum> pressedDirs = new List<DirectionEnum>();

    public List<DirectionEnum> debugDirReq;
    public List<DirectionEnum> debugDirPressed;

    public delegate void RequesterEvents(bool good);
    public static event RequesterEvents InputResult;

    private void Start()
    {
        GenerateNewRequestDir();

        debugDirReq = requestedDirs;
        debugDirPressed = pressedDirs;
    }

    public static void GenerateNewRequestDir()
    {
        DirectionEnum dirE = (DirectionEnum)Random.Range(0, 4);
        requestedDirs.Add(dirE);

        InputUI.exImages[(int)dirE].gameObject.SetActive(true);
    }
    public static void RemoveOldestRequestDir()
    {
        InputUI.exImages[(int)requestedDirs[0]].gameObject.SetActive(false);
        requestedDirs.RemoveAt(0);
    }

    public static void CheckInput()
    {
        bool canGoodInput = true;
        bool shouldCheck = requestedDirs.Count == pressedDirs.Count;

        if (shouldCheck)
        {
            for (int i = 0; i < requestedDirs.Count; i++)
            {
                bool inputFound = false;
                for (int j = 0; j < pressedDirs.Count; j++)
                {
                    if (requestedDirs[i] == pressedDirs[j])
                    {
                        inputFound = true;
                        break;
                    }
                }
                if (!inputFound)
                {
                    canGoodInput = false;
                    InputChanger.Instance.ChangeInput();
                    InputResult?.Invoke(false);
                    break;
                }
            }
            if (canGoodInput)
            {
                InputResult?.Invoke(true);
            }
        }
        else
        {
            InputChanger.Instance.ChangeInput();
            InputResult?.Invoke(false);
        }
    }
}
