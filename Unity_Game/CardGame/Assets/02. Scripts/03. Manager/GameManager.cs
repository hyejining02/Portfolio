using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*
    // ���ȭ�� ī��ǥ�� ��� ����
    //public static GameManager Instance { get; private set; }
    //[System.Serializable]
    //public class MatchedPair
    //{
    //    public string word;
    //    public string imageName;
    //}

    //public List<MatchedPair> MatchedPairs { get; private set; } = new List<MatchedPair>();
    //void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //public void AddMatchedPair(Sprite imageSprite, string word)
    //{
    //    MatchedPairs.Add((imageSprite, word));
    //    Debug.Log("MatchedPair �߰��� - �̹���: " + imageSprite.name + ", �ܾ�: " + word);
    //    TotalCoins += 100;
    //}

    //public void SaveMatchedPairs()
    //{
    //    PlayerPrefs.SetInt("MatchedPairCount", MatchedPairs.Count);
    //    for ( int i = 0; i < MatchedPairs.Count; i++)
    //    {
    //        PlayerPrefs.SetString($"word_{i}", MatchedPairs[i].word);
    //        PlayerPrefs.SetString($"Image_{i}", MatchedPairs[i].imageName);
    //    }
    //    PlayerPrefs.Save();
    //}

    //public void ClearMatchedPairs()
    //{
    //    MatchedPairs.Clear();
    //    int matchedPairCount = PlayerPrefs.GetInt("MatchedPairCount", 0);
    //    PlayerPrefs.DeleteKey("MatchedPairCount");

    //    for (int i = 0; i < matchedPairCount; i++)
    //    {
    //        PlayerPrefs.DeleteKey("MatchedWord_" + i);
    //        PlayerPrefs.DeleteKey("MatchedImage_" + i);
    //    }
    //}

    //public void LoadMatchedPairs()
    //{
    //    MatchedPairs.Clear();
    //    int pairCount = PlayerPrefs.GetInt("MatchedPairCount", 0);
    //    Debug.Log("MatchedPairCount in PlayerPrefs : " + pairCount);

    //    for ( int i = 0; i < pairCount; i++ )
    //    {
    //        string word = PlayerPrefs.GetString($"Word_{i}", "");
    //        string imageName = PlayerPrefs.GetString($"Image_{i}", "");

    //        if ( string.IsNullOrEmpty(word) || string.IsNullOrEmpty(imageName) )
    //        {
    //            Debug.LogError("�̹����� �ε��� �� �����ϴ� : " + imageName);
    //            continue;
    //        }

    //        MatchedPairs.Add(new MatchedPair { word = word, imageName = imageName });
    //        Debug.Log($"Loading Pair - Word: {word}, Image Name: {imageName}");
    //    }

    //    if (MatchedPairs.Count == 0)
    //    {
    //        Debug.LogError("GameManager�� MatchedPairs �����Ͱ� �����ϴ�.");
    //    }
    //    else
    //    {
    //        Debug.Log("GameManager�� MatchedPairs ������ ����: " + MatchedPairs.Count);
    //    }

    //}
    */

    // �� ���μ��� ����
    public int TotalCoins { get; private set; }

    // ī�� ������ ���¸� ����
    public bool IsCardFlipping { get; private set; } = false;

    // ������ ����Ǹ� ��Ī�� ī��� ȹ���� ���� ���� �����ϰ� ����ϸ����� �̵� ( ��Ī�� ī��� �����ϳ�, ���� ���� )
    public void EndGame()
    {
        PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins", 0) + TotalCoins);
        StartCoroutine(DelayedCompleteSceneLoad());
    }

    // ��� ȭ�� �ε� �� 0.5�� ������ �� ���� ��ȯ
    private IEnumerator DelayedCompleteSceneLoad()
    {
        yield return new WaitForSeconds(0.5f); // 0.5�� ������ �߰�
        SceneManager.Instance.EnableDelay(1.3f, SceneType.Complete);
    }

}