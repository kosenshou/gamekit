using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LUI : MonoBehaviour
{
    [MenuItem("GameObject/GameKit/Image", false, 0)]
    static void AddImgButton()
    {
        LoadPrefab("Image");
    }
    
    [MenuItem("GameObject/GameKit/Image Button", false, 0)]
    static void AddImage()
    {
        LoadPrefab("ImageButton");
    }

    [MenuItem("GameObject/GameKit/Text", false, 0)]
    static void AddText()
    {
        LoadPrefab("Text");
    }

    [MenuItem("GameObject/GameKit/Text Button", false, 0)]
    static void AddButton()
    {
        LoadPrefab("TextButton");
    }

    [MenuItem("GameObject/GameKit/Scroll View", false, 0)]
    static void AddScrollView()
    {
        LoadPrefab("ScrollView");
    }

    [MenuItem("GameObject/GameKit/Mask", false, 0)]
    static void AddMask()
    {
        LoadPrefab("Mask");
    }

    [MenuItem("GameObject/GameKit/Slider", false, 0)]
    static void AddSlider()
    {
        LoadPrefab("Slider");
    }

    [MenuItem("GameObject/GameKit/Input Field", false, 0)]
    static void AddInputField()
    {
        LoadPrefab("InputField");
    }

    static void LoadPrefab(string name)
    {
        Selection.activeTransform = Load.GameObject("Prefabs/" + name, Selection.activeTransform).transform;
        Selection.activeGameObject.name = name;
    }
}