using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriter : MonoBehaviour
{
    public float letterPause = 0.05f;

    public string message;
    Text text;

	public bool isTyping;

    void Start()
    {
        text = GetComponent<Text>();
        message = text.text;
        text.text = "";
    }

	public void StartTyping()
	{
		message = text.text;
		text.text = "";
		StartCoroutine(TypeText());
	}

    IEnumerator TypeText()
    {
		isTyping = true;
        foreach (char letter in message.ToCharArray())
        {
            text.text += letter;
			yield return new WaitForSeconds(letterPause);
        }
		isTyping = false;
    }

    public void SkipTyping()
    {
		isTyping = false;
        StopAllCoroutines();
        text.text = message;
    }
}