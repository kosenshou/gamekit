using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LScrollView : MonoBehaviour
{
	public SpriteRenderer render;
    public Transform scrollRect;
	public float min, max; // scroll bounds
    Vector3 lastPosition; // for panning/scrolling purposes

    void Update()
    {
        Scrolling();
    }

    void Scrolling()
    {
        // Detect if scroll view is clicked
        if (ViewClicked())
        {
            lastPosition = Utils.inputPosition();
        }

        // Detect if scroll view is pressed
        if (ViewHeldDown())
        {
            Vector3 delta = Utils.inputPosition() - lastPosition;

            Vector3 targetPosition = new Vector3(0, delta.y, 0) + scrollRect.localPosition;
            scrollRect.localPosition = Vector3.Lerp(scrollRect.localPosition, targetPosition, Time.deltaTime * 50f);
            float posY = Mathf.Clamp(scrollRect.localPosition.y, min, max);
            scrollRect.localPosition = new Vector3(scrollRect.localPosition.x, posY);
            lastPosition = Utils.inputPosition();
        }
    }

    private bool ViewClicked()
    {
        return Utils.SpriteContains(render, Utils.inputPosition()) && Input.GetButtonDown("Fire1");
    }

    private bool ViewHeldDown()
    {
        return Utils.SpriteContains(render, Utils.inputPosition()) && Input.GetButton("Fire1");
    }
}