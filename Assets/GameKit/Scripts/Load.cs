using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Load
{
    /// <summary> Load raw sprite file. </summary>
    public static Sprite Sprite(string fileName)
    {
        return (Sprite)Resources.Load(fileName, typeof(Sprite));
    }

    /// <summary> Load an animator controller file. </summary>
    public static RuntimeAnimatorController Animator(string fileName)
    {
        return (RuntimeAnimatorController)Resources.Load(fileName, typeof(RuntimeAnimatorController));
    }

    /// <summary> Load a prefab gameObject. </summary>
    public static GameObject GameObject(string fileName, Transform reference)
    {
        return (GameObject)MonoBehaviour.Instantiate(Resources.Load(fileName), reference);
    }

    /// <summary> Convert hex to Color class. </summary>
    public static Color Color(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }

    /// <summary> Load an xml, json or any text file and convert to TextAsset class. </summary>
    public static TextAsset TextAsset(string fileName)
    {
        return (TextAsset)Resources.Load(fileName, typeof(TextAsset));
    }

    public static void Notification(string message, Transform reference)
    {
        Text notification = null;
        GameObject go = GameObject("prefabs/gameobjects/notification", reference);
        notification = go.transform.Find("Text").GetComponent<Text>();
        notification.text = message;
        MonoBehaviour.Destroy(go, 1.5f);
    }

    /// <summary> Load a material file. </summary>
    public static Material Material(string fileName)
    {
        return (Material)Resources.Load(fileName, typeof(Material));
    }

    /// <summary> Load a text prefab. </summary>
    public static Text Text(string fileName, Transform reference)
    {
        Text text = (Text)MonoBehaviour.Instantiate((Text)Resources.Load(fileName, typeof(Text)), reference);
        text.transform.localScale = new Vector3(1, 1, 1);
        return text;
    }

    /// <summary> Load a scene asynchronously so you can show a loading bar/screen. </summary>
    public static IEnumerator Scene(string scene)
    {
        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(0.5f);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
        }
    }

}