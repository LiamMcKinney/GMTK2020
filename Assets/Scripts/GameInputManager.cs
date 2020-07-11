using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class GameInputManager
{
    static Dictionary<string, KeyCode> keyMapping;
    static string[] keyMaps = new string[10]
    {
        "Attack",
        "Block",
        "Up",
        "Down",
        "Left",
        "Right",
        "SoftRepairTool",
        "HardRepairTool",
        "Bow",
        "Bomb"
    };
    static KeyCode[] defaults = new KeyCode[10]
    {
        KeyCode.Q,
        KeyCode.E,
        KeyCode.W,
        KeyCode.S,
        KeyCode.A,
        KeyCode.D,
        KeyCode.None,
        KeyCode.None,
        KeyCode.None,
        KeyCode.None
    };

    static GameInputManager()
    {
        InitializeDictionary();
    }

    private static void InitializeDictionary()
    {
        keyMapping = new Dictionary<string, KeyCode>();
        for (int i = 0; i < keyMaps.Length; ++i)
        {
            keyMapping.Add(keyMaps[i], defaults[i]);
        }
    }

    public static void SetKeyMap(string keyMap, KeyCode key)
    {
        if (!keyMapping.ContainsKey(keyMap))
            throw new ArgumentException("Invalid KeyMap in SetKeyMap: " + keyMap);
        keyMapping[keyMap] = key;
    }

    public static bool GetKeyDown(string keyMap)
    {
        if(keyMapping[keyMap] == KeyCode.None)
        {
            return false;
        }
        return Input.GetKeyDown(keyMapping[keyMap]);
    }

    public static bool GetKey(string keyMap)
    {
        if (keyMapping[keyMap] == KeyCode.None)
        {
            return false;
        }
        return Input.GetKey(keyMapping[keyMap]);
    }
}