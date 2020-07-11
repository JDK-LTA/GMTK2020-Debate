using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.UI;

public enum DirectionEnum { UP, DOWN, LEFT, RIGHT }
public class ArrowImage : MonoBehaviour
{
    
    private Image image;
    public DirectionEnum dir;

    public bool green = false;

    private void Start()
    {
        image = GetComponent<Image>();
        //InputManager.Instance.DebugChangeColor += ChangeToRed;

        switch (dir)
        {
            case DirectionEnum.UP:
                InputManager.Instance.UpArrow += ChangeToGreen;
                InputManager.Instance.UpArrowU += ChangeToRed;
                break;
            case DirectionEnum.DOWN:
                InputManager.Instance.DownArrow += ChangeToGreen;
                InputManager.Instance.DownArrowU += ChangeToRed; 
                break;
            case DirectionEnum.LEFT:
                InputManager.Instance.LeftArrow += ChangeToGreen;
                InputManager.Instance.LeftArrowU += ChangeToRed; 
                break;
            case DirectionEnum.RIGHT:
                InputManager.Instance.RightArrow += ChangeToGreen;
                InputManager.Instance.RightArrowU += ChangeToRed; 
                break;
            default:
                InputManager.Instance.UpArrow += ChangeToGreen;
                InputManager.Instance.UpArrowU += ChangeToRed; 
                break;
        }

        //InputChanger.ChangeBasic += ChangeToRed;
    }

    public void ChangeToGreen()
    {
        image.color = Color.green;
        green = true;
    }

    public void ChangeToRed()
    {
        if (green)
        {
            green = false;
            image.color = Color.red;
        }
    }

}
