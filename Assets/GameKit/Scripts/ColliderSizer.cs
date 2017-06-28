using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColliderSizer : MonoBehaviour
{
    void Update()
    {
        if (Application.isEditor)
        {
            GetComponent<BoxCollider2D>().size = GetComponent<SpriteRenderer>().size;
        }
    }
}
