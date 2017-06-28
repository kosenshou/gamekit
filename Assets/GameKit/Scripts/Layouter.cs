using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Layouter : MonoBehaviour
{
    public enum Location { LEFT, RIGHT }
    public Location location;
    public float pad;

    void Start()
    {
        UpdateLayout();
    }

    void Update()
    {
        if (Application.isEditor)
        {
            UpdateLayout();
        }
    }

    void UpdateLayout()
    {
        Vector3 pos = transform.localPosition;
        float width = (Camera.main.orthographicSize * 2f) * Camera.main.aspect;
        float posX = 0;

        if (location == Location.LEFT)
        {
            posX = -(width / 2f) + pad;
        }
        else if (location == Location.RIGHT)
        {
            posX = (width / 2f) - pad;
        }

        transform.localPosition = new Vector3(posX, pos.y, pos.z);
    }
}