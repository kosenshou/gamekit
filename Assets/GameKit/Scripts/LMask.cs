using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

[ExecuteInEditMode]
public class LMask : MonoBehaviour
{
    private TextMeshPro[] texts;

    void Update()
    {
        if (Application.isEditor)
        {
            GetComponent<BoxCollider2D>().size = GetComponent<SpriteRenderer>().size / 1.1f;
        }

        if (transform.parent != null)
            texts = transform.parent.GetComponentsInChildren<TextMeshPro>(true);

        // optimize this code to run only once when inside and outside
        if (texts != null)
        foreach (TextMeshPro t in texts)
        {
            if (GetComponent<BoxCollider2D>().bounds.Contains(t.transform.position))
            {
                //Debug.Log("Inside!");
                t.enabled = true;
            }
            else
            {
                //Debug.Log("Outside!");
                t.enabled = false;
            }
        }
    }

    void OnDisable()
    {
        if (texts != null)
        foreach (TextMeshPro t in texts)
        {
            t.enabled = true;
        }
    }

    public void ApplyMask()
    {
        if (transform.parent != null)
        {
            SpriteRenderer[] renders = transform.parent.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (SpriteRenderer r in renders)
            {
                if (r.transform != transform && r.transform != transform.parent)
                {
                    r.material = Load.Material("Shaders/Mask/SMasked");
                }
            }
        }
    }
}