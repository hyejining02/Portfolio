using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageWordLoader : MonoBehaviour
{
    public List<Sprite> cardImages;
    public List<string> cardWords;
    public string jsonFilePath;

    // json 데이터 저장클래스
    [System.Serializable]
    public class ImageWordPair
    {
        public string Num;
        public string Eng;
    }

    [System.Serializable]
    public class ImageWordPairs
    {
        public List<ImageWordPair> imageWordPairs;
    }

    public ImageWordPairs imageWordPairs;

    void Start()
    {
        LoadImageWordPairs();
    }

    public void LoadImageWordPairs()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFilePath.Replace(".json", ""));

        if ( jsonFile != null)
        {
            string json = jsonFile.text;
            imageWordPairs = JsonUtility.FromJson<ImageWordPairs>(json);

            LoadImages();
            LoadWords();
        }
        else
        {
            Debug.LogError("Cannot find json file in Resources: " + jsonFilePath);
        }
    }

    private void LoadImages()
    {
        cardImages = new List<Sprite>(Resources.LoadAll<Sprite>("Level1Images"));
    }

    private void LoadWords()
    {
        cardWords = new List<string>();
        
        foreach ( var pair in imageWordPairs.imageWordPairs)
        {
            cardWords.Add(pair.Eng);
        }
    }

    public Sprite GetImageByNum(string num)
    {
        foreach ( var sprite in cardImages)
        {
            if ( sprite.name == num )
            {
                return sprite;
            }
        }

        Debug.LogWarning("Image not found for Num : " + num);
        return null;
    }

}