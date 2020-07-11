using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowImage : MonoBehaviour
{
    private Image image;

    bool changed = false;

    private void Start()
    {
        image = GetComponent<Image>();
        InputManager.Instance.DebugChangeColor += ChangeToRed;
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
