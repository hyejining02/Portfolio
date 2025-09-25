using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*
    // 결과화면 카드표시 기능 보류
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
    //    Debug.Log("MatchedPair 추가됨 - 이미지: " + imageSprite.name + ", 단어: " + word);
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
    //            Debug.LogError("이미지를 로드할 수 없습니다 : " + imageName);
    //            continue;
    //        }

    //        MatchedPairs.Add(new MatchedPair { word = word, imageName = imageName });
    //        Debug.Log($"Loading Pair - Word: {word}, Image Name: {imageName}");
    //    }

    //    if (MatchedPairs.Count == 0)
    //    {
    //        Debug.LogError("GameManager의 MatchedPairs 데이터가 없습니다.");
    //    }
    //    else
    //    {
    //        Debug.Log("GameManager의 MatchedPairs 데이터 개수: " + MatchedPairs.Count);
    //    }

    //}
    */

    // 총 코인수를 관리
    public int TotalCoins { get; private set; }

    // 카드 뒤집기 상태를 관리
    public bool IsCardFlipping { get; private set; } = false;

    // 게임이 종료되면 매칭된 카드와 획득한 코인 수를 저장하고 결과하면으로 이동 ( 매칭된 카드는 저장하나, 현재 보류 )
    public void EndGame()
    {
        PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins", 0) + TotalCoins);
        StartCoroutine(DelayedCompleteSceneLoad());
    }

    // 결과 화면 로드 전 0.5초 딜레이 후 씬을 전환
    private IEnumerator DelayedCompleteSceneLoad()
    {
        yield return new WaitForSeconds(0.5f); // 0.5초 딜레이 추가
        SceneManager.Instance.EnableDelay(1.3f, SceneType.Complete);
    }

}