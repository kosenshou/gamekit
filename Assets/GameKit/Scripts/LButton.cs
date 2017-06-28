using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using DG.Tweening;

[ExecuteInEditMode]
public class LButton : MonoBehaviour
{
	public SpriteRenderer render;
	public BoxCollider2D col;
    public TextMeshPro text;

    public UnityAction onClick;

    void Update()
    {
        if (Application.isEditor)
        {
            col.size = render.size;
        }

        if (Utils.OnTouchDown(gameObject))
        {
            transform.DOScale(1.1f, 0.05f);
            if (onClick != null)
                onClick();
        }

        if (Utils.OnTouchUp(gameObject))
        {
            transform.DOScale(1f, 0.1f);
        }
    }
}
