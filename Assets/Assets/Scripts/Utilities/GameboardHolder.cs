using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    private const string gameboardPath = "Gameboards/";
    private const string defaultGameboardName = "Standard";
    private static string selectedGameboardName;
        
    public static Board ReadSelectedGameboard()
    {
        string path = (selectedGameboardName == null ? gameboardPath + defaultGameboardName : gameboardPath + selectedGameboardName);
        object obj = Resources.Load(path);
        Board retrievedGameboard = (Board)obj;
        return retrievedGameboard;
    }

    public static void SelectGameboard(string boardName)
    {
        selectedGameboardName = boardName;
    }
}
