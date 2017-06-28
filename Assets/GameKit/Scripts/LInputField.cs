using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LInputField : MonoBehaviour
{
    public TextMeshPro placeholder, input;
    [HideInInspector] public bool selected;
    private TouchScreenKeyboard keyboard;

    void Update()
    {
        if (input.text == "")
        {
            placeholder.enabled = true;
        }
        else
        {
            placeholder.enabled = false;
        }

        if (Utils.SelectedObject() == gameObject && Input.GetButtonDown("Fire1"))
        {
            selected = true;
            placeholder.color = new Color(0.5f, 0.5f, 0.5f);
            if (Application.isMobilePlatform)
                keyboard = TouchScreenKeyboard.Open(input.text, TouchScreenKeyboardType.Default, false, false, false, true);
        }
        else if (Utils.SelectedObject() != gameObject && Input.GetButtonDown("Fire1"))
        {
            selected = false;
        }

        if (Application.isMobilePlatform && keyboard != null)
        {
            if (!keyboard.active)
                selected = false;
        }

        if (selected)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if (input.text != "")
                    input.text = input.text.Remove(input.text.Length - 1);
            }
            else
            {
                if (Application.isMobilePlatform && keyboard != null)
                    input.text = keyboard.text;
                else if (Input.anyKeyDown)
                    input.text += "" + Input.inputString;
            }
        }
        else
        {
            placeholder.color = new Color(0.7f, 0.7f, 0.7f);
        }
    }
}