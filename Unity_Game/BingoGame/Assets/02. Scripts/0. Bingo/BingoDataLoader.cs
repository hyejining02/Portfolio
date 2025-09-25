using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class ImageWordPair
{
    public string Num;
    public string Eng;
}

[System.Serializable]
public class BingoData
{
    public List<ImageWordPair> imageWordPairs;
}

public static class BingoDataLoader
{
    public static List<ImageWordPair> LoadBingoPairs(string path)
    {
        TextAsset jsonData = Resources.Load<TextAsset>(path);

        if (jsonData == null )
        {
            Debug.LogError($"JSON File not found in Resources : {path}");
            return null;
        }

        BingoData data = JsonUtility.FromJson<BingoData>(jsonData.text);

        if ( data == null || data.imageWordPairs == null || data.imageWordPairs.Count < 16)
        {
            Debug.LogError($"Invalid JSON data. Path: {path}, Count: {data?.imageWordPairs?.Count ?? 0}");
            return null;
        }

        Debug.Log($"Loaded {data.imageWordPairs.Count} pairs from {path}");
        return data.imageWordPairs;
    }
}
