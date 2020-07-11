using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private KeyCode upCode = KeyCode.UpArrow;
    private KeyCode downCode = KeyCode.DownArrow;
    private KeyCode leftCode = KeyCode.LeftArrow;
    private KeyCode rightCode = KeyCode.RightArrow;

    [SerializeField] private List<KeyCode> codes = new List<KeyCode>();
    [SerializeField] private List<DirectionEnum> directions = new List<DirectionEnum>();
    public List<KeyCode> Codes { get => codes; set => codes = value; }
    public List<DirectionEnum> Directions { get => directions; set => directions = value; }

    private void Start()
    {
        codes.Add(upCode);
        codes.Add(downCode);
        codes.Add(leftCode);
        codes.Add(rightCode);

        directions.Add(DirectionEnum.UP);
        directions.Add(DirectionEnum.DOWN);
        directions.Add(DirectionEnum.LEFT);
        directions.Add(DirectionEnum.RIGHT);
    }

    public delegate void InputEvents();
    public event InputEvents UpArrow;
    public event InputEvents UpArrowU;
    public event InputEvents DownArrow;
    public event InputEvents DownArrowU;
    public event InputEvents LeftArrow;
    public event InputEvents LeftArrowU;
    public event InputEvents RightArrow;
    public event InputEvents RightArrowU;
    public event InputEvents Extra1;
    public event InputEvents Extra2;

    public event InputEvents DebugChangeColor;

    private bool inputPressed;
    public bool InputPressed { get => inputPressed; set => inputPressed = value; }

    [SerializeField] private bool debugCode = true;
    float debT = 0;
    public float debugTimeToResetInput = 0.5f;

    int[] indexes = new int[4];
    private void Update()
    {
        if (!inputPressed)
        {
            if (Input.GetKeyDown(codes[0]))
            {
                UpArrow?.Invoke();
                InputRequester.pressedDirs.Add(directions[0]);
            }
            else if (Input.GetKeyUp(codes[0]))
            {
                UpArrowU?.Invoke();
                InputRequester.pressedDirs.Remove(directions[0]);
            }
            if (Input.GetKeyDown(codes[1]))
            {
                DownArrow?.Invoke();
                InputRequester.pressedDirs.Add(directions[1]);
            }
            else if (Input.GetKeyUp(codes[1]))
            {
                DownArrowU?.Invoke();
                InputRequester.pressedDirs.Remove(directions[1]);
            }
            if (Input.GetKeyDown(codes[2]))
            {
                LeftArrow?.Invoke();
                InputRequester.pressedDirs.Add(directions[2]);
            }
            else if (Input.GetKeyUp(codes[2]))
            {
                LeftArrowU?.Invoke();
                InputRequester.pressedDirs.Remove(directions[2]);
            }
            if (Input.GetKeyDown(codes[3]))
            {
                RightArrow?.Invoke();
                InputRequester.pressedDirs.Add(directions[3]);
            }
            else if (Input.GetKeyUp(codes[3]))
            {
                RightArrowU?.Invoke();
                InputRequester.pressedDirs.Remove(directions[3]);
            }
        }
        else
        {
            if (debugCode)
            {
                debT += Time.deltaTime;
                if (debT >= debugTimeToResetInput)
                {
                    debT = 0;
                    DebugChangeColor?.Invoke();
                    inputPressed = false;
                }
            }
        }
    }
}
