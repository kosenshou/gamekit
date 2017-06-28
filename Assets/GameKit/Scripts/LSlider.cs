using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LSlider : MonoBehaviour
{
    public SpriteRenderer fill;
    public float pad = 0.15f;
    [Range(0f, 1f)]
    public float fillAmount = 1f;
    private float fillX;

    void Start()
    {
        fillX = fill.size.x;
    }

    void Update()
    {
        if (Application.isEditor)
        {
            if (fill != null)
            {
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                fill.size = new Vector2(sr.size.x - pad, sr.size.y - pad);
                fill.transform.localPosition = new Vector3(-sr.size.x / 2f + pad / 2f, fill.transform.localPosition.y, fill.transform.localPosition.z);
                fill.size = new Vector2(fill.size.x * fillAmount, fill.size.y);
            }
        }
        else
        {
            if (fill != null)
            {
                fillAmount = Mathf.Clamp(fillAmount, 0f, 1f);
                fill.size = new Vector2(fillX * fillAmount, fill.size.y);
            }
        }
    }
}