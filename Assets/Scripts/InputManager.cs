using System.Collections;
using System.Collections.Generic;
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
    public List<KeyCode> Codes { get => codes; set => codes = value; }

    private void Start()
    {
        codes.Add(upCode);
        codes.Add(downCode);
        codes.Add(leftCode);
        codes.Add(rightCode);
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

    private void Update()
    {
        if (!inputPressed)
        {
            if (Input.GetKeyDown(codes[0]))
            {
                UpArrow?.Invoke();
                //inputPressed = true;
            }
            else if (Input.GetKeyUp(codes[0]))
            {
                UpArrowU?.Invoke();
            }
            if (Input.GetKeyDown(codes[1]))
            {
                DownArrow?.Invoke();
                //inputPressed = true;
            }
            else if (Input.GetKeyUp(codes[1]))
            {
                DownArrowU?.Invoke();
            }
            if (Input.GetKeyDown(codes[2]))
            {
                LeftArrow?.Invoke();
                //inputPressed = true;
            }
            else if (Input.GetKeyUp(codes[2]))
            {
                LeftArrowU?.Invoke();
            }
            if (Input.GetKeyDown(codes[3]))
            {
                RightArrow?.Invoke();
                //inputPressed = true;
            }
            else if (Input.GetKeyUp(codes[3]))
            {
                RightArrowU?.Invoke();
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
