using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LMask))]
public class LMaskEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LMask myScript = (LMask)target;
        if (GUILayout.Button("Apply Mask"))
        {
            myScript.ApplyMask();
        }
    }
}