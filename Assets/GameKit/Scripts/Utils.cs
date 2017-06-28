using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

public class Utils
{
    /// <summary>
    /// Get input position of mouse if desktop and touch if mobile.
    /// </summary>
    public static Vector3 inputPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    /// <summary>
    /// Format number of seconds to time format ex. 01:30.
    /// </summary>
    /// <param name="time">Time to be formatted</param>
    public static string FormatTime(int time)
    {
        string minutes = Mathf.Floor(time / 60).ToString("00");
        string seconds = (time % 60).ToString("00");

        return minutes + ":" + seconds;
    }

    /// <summary>
    /// Get the boolean player data/preference.
    /// </summary>
    /// <param name="prefsName">Name of preference to get.</param>
    public static bool GetBool(string prefsName)
    {
        if (PlayerPrefs.GetInt(prefsName) == 1)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Save the boolean player data/preference.
    /// </summary>
    /// <param name="prefsName">Name of preference to be saved.</param>
    /// <param name="boolean">true or false</param>
    public static void SetBool(string prefsName, bool boolean)
    {
        if (boolean)
        {
            PlayerPrefs.SetInt(prefsName, 1);
        }
        else
        {
            PlayerPrefs.SetInt(prefsName, 0);
        }
    }

    /// <summary>
    /// Format number as thousands ex. 31,530.
    /// </summary>
    /// <param name="num">the number to be formatted</param>
    public static string FormatNumber(int num)
    {
        return String.Format("{0:0,0}", num);
    }

    /// <summary>
    /// Title case a string, ex. The Dark Knight.
    /// </summary>
    /// <param name="str">the string</param>
    public static string ToTitleCase(string str)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
    }

    public static bool SpriteContains(SpriteRenderer render, Vector3 pos)
    {
        Rect rect = new Rect(render.transform.position.x - render.size.x / 2f,
            render.transform.position.y - render.size.y / 2f, render.size.x, render.size.y);

        return rect.Contains(pos);
    }

    private static RaycastHit2D[] hits;
    private static LayerMask clickableLayers =
        (1 << LayerMask.NameToLayer("Default"))
        |(1 << LayerMask.NameToLayer("UI"))
        |(1 << LayerMask.NameToLayer("Popup"))
        |(1 << LayerMask.NameToLayer("Loader"));

    public static GameObject SelectedObject()
    {
        Vector3 clickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Store out clicked position
        hits = Physics2D.LinecastAll(new Vector3(clickedPos.x, clickedPos.y, -2), new Vector3(clickedPos.x, clickedPos.y, 5), clickableLayers); //Cast ray at the world space the mouse is at

        if (hits.Length > 0) //Only function if we actually hit something
        {
            int topHit = 0; //Set our top hit to a default of the first index in our "hits" array, in case there are no others
            int preVal = hits[0].transform.GetComponent<SpriteRenderer>().sortingLayerID; //Store the SortingLayerID of the first object in the array, so it doesn't get skipped
            int preSubVal = hits[0].transform.GetComponent<SpriteRenderer>().sortingOrder; //Store the SortingOrder value of the first object in the array, in case it needs to be compared to

            for (int arrayID = 1; arrayID < hits.Length; arrayID++) //Loop for every extra item the raycast hit
            {
                int curVal = hits[arrayID].transform.GetComponent<SpriteRenderer>().sortingLayerID; //Store SortingLayerID of the current item in the array being accessed

                if (curVal < preVal) //If the SortingLayerID of the current array item is lower than the previous lowest
                {
                    preVal = curVal; //Set the "Previous Value" to the current one since it's lower, as it will become the "Previous Lowest" on the next loop
                    topHit = arrayID; //Set our topHit with the Array Index value of the current closest Array item, since it currently has the highest/closest SortingLayerID
                    preSubVal = hits[arrayID].transform.GetComponent<SpriteRenderer>().sortingOrder; //Store SortingOrder value of the current closest object, for comparison next loop if we end up going to the "else if"
                }
                else if ((curVal == preVal) && (hits[arrayID].transform.GetComponent<SpriteRenderer>().sortingOrder > preSubVal)) //If SortingLayerID are the same, then we need to compare SortingOrder. If the SortingOrder is lower than the one stored in the previous loop, then update values
                {
                    topHit = arrayID;
                    preSubVal = hits[arrayID].transform.GetComponent<SpriteRenderer>().sortingOrder;
                }
            }
            return hits[topHit].transform.gameObject;
        }
        return null;
    }

    public static bool OnTouchDown(GameObject gameObject)
    {
        return SelectedObject() == gameObject && Input.GetButtonDown("Fire1");
    }

    public static bool OnTouchUp(GameObject gameObject)
    {
        return SelectedObject() == gameObject && Input.GetButtonUp("Fire1");
    }
}