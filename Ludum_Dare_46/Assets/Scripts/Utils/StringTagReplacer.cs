using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringTagReplacer
{
    public static string ReplaceTag(string str, string tag, string value)
    {
        string result = "";
        if (str.Contains(tag))
        {
            result = str.Replace(tag, value);
        }

        return result;
    }
}
