using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.UI;

public class ArrowImage : MonoBehaviour
{
    public enum Direction { UP, DOWN, LEFT, RIGHT }
    
    private Image image;
    [SerializeField] Direction dir;

    bool changed = false;

    private void Start()
    {
        image = GetComponent<Image>();
        InputManager.Instance.DebugChangeColor += ChangeToRed;

        switch (dir)
        {
            case Direction.UP:
                InputManager.Instance.UpArrow += ChangeToGreen;
                InputManager.Instance.UpArrowU += ChangeToRed;
                break;
            case Direction.DOWN:
                InputManager.Instance.DownArrow += ChangeToGreen;
                InputManager.Instance.DownArrowU += ChangeToRed; 
                break;
            case Direction.LEFT:
                InputManager.Instance.LeftArrow += ChangeToGreen;
                InputManager.Instance.LeftArrowU += ChangeToRed; 
                break;
            case Direction.RIGHT:
                InputManager.Instance.RightArrow += ChangeToGreen;
                InputManager.Instance.RightArrowU += ChangeToRed; 
                break;
            default:
                InputManager.Instance.UpArrow += ChangeToGreen;
                InputManager.Instance.UpArrowU += ChangeToRed; 
                break;
        }

        InputChanger.ChangeBasic += ChangeToRed;
    }

    public void ChangeToGreen()
    {
        image.color = Color.green;
        changed = true;
    }

    public void ChangeToRed()
    {
        if (changed)
        {
            changed = false;
            image.color = Color.red;
        }
    }

}
