using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using static ImageWordLoader;
using UnityEngine.UIElements;

public class KeystoneLV3Controller : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform cardParent;
    public Sprite cardBackSprite;
    public TMP_Text successText;

    public TMP_Text timerText;
    public UnityEngine.UI.Image FillLoding;

    private Dictionary<string, string> imageWordPairs;
    private List<GameObject> spawnedCards = new List<GameObject>();
    private List<Card> cards = new List<Card>();

    private Sprite[] frontImages;
    private GameObject firstCard, secondCard;
    private int totalPairs;
    private int matchedPairs = 0;

    private float timeLimit = 60f;
    private float timeRemaining;

    private bool isFlipping = false;
    private bool gameEnded = false;

    private string selectedDifficulty;

    private List<string> matchedPairsList = new List<string>();

    private void Start()
    {
        timeRemaining = timeLimit;
        FillLoding.fillAmount = 1;

        selectedDifficulty = PlayerPrefs.GetString("SelectedDifficulty", "easy");

        switch (selectedDifficulty)
        {
            case "easy":
                totalPairs = 3;
                break;
            case "normal":
                totalPairs = 4;
                break;
            case "hard":
                totalPairs = 5;
                break;
        }

        LoadImagesAndWords();
        InitializeGame();
        StartCoroutine(StartTimer());

        matchedPairs = 0;
        UpdateSuccessText();

        ArrangeCards();
    }

    void InitializeGame()
    {
        //GameManager.Instance.SetDifficulty(selectedDifficulty);

        int requiredCardCount = totalPairs * 2;

        ClearExistingCards();
        spawnedCards.Clear();

        for (int i = 0; i < requiredCardCount; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, cardParent);
            spawnedCards.Add(newCard);
        }

        ShuffleAndAssignCards();

        Debug.Log($"생성된 카드 수 : {spawnedCards.Count}, 필요한 카드 수 : {requiredCardCount}");

        ArrangeCards();
    }

    // 타이머 코루틴
    IEnumerator StartTimer()
    {
        while (timeRemaining > 0 && !gameEnded)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timeRemaining).ToString();
            FillLoding.fillAmount = timeRemaining / timeLimit;
            yield return null;
        }

        if (timeRemaining <= 0)
        {
            EndGame();
        }
    }

    private void ClearExistingCards()
    {
        foreach (GameObject card in spawnedCards)
        {
            Destroy(card);
        }
        spawnedCards.Clear();
    }

    void ArrangeCards()
    {
        int rows = 2;
        int totalCards = totalPairs * 2;
        int columns = totalCards / rows;

        float spacingX = 200f;
        float spacingY = 250f;

        Vector2 startPos = new Vector2(-((columns - 1) * spacingX) / 2, ((rows - 1) * spacingY) / 2);

        for (int i = 0; i < totalCards; i++)
        {
            int row = i / columns;
            int col = i % columns;
            Vector2 pos = startPos + new Vector2(col * spacingX, -row * spacingY);

            spawnedCards[i].GetComponent<RectTransform>().anchoredPosition = pos;
        }

        Debug.Log($"생성된 카드 수: {spawnedCards.Count}, 필요한 카드 수: {totalCards}");
    }
    void ShuffleAndAssignCards()
    {
        List<KeyValuePair<string, string>> pairs = imageWordPairs.ToList();
        pairs.Shuffle();

        int requiredCardCount = totalPairs * 2;
        if (spawnedCards.Count < requiredCardCount)
        {
            Debug.LogError("Not enough spawned cards to assign pairs");
            return;
        }

        List<int> indices = Enumerable.Range(0, requiredCardCount).ToList();
        indices.Shuffle();

        for (int i = 0; i < totalPairs; i++)
        {
            string imageID = pairs[i].Key;
            string word = pairs[i].Value;

            Sprite imageSprite = frontImages.FirstOrDefault(img => img.name == imageID);

            if (imageSprite == null)
            {
                Debug.LogError($"Image '{imageID}' not found.");
                continue;
            }

            int imageIndex = indices[i];
            int wordIndex = indices[i + totalPairs];

            if (imageIndex >= spawnedCards.Count || wordIndex >= spawnedCards.Count)
            {
                Debug.LogError("Index out of range in spawned cards");
                continue;
            }

            Card imageCard = spawnedCards[imageIndex].GetComponent<Card>();
            imageCard.SetupCard(imageSprite, null);
            imageCard.cardID = imageID;
            imageCard.isTextCard = false;

            Card wordCard = spawnedCards[wordIndex].GetComponent<Card>();
            wordCard.SetupCard(null, word);
            wordCard.cardID = imageID;
            wordCard.isTextCard = true;
        }
    }

    private void UpdateSuccessText()
    {
        successText.text = matchedPairs.ToString() + "/" + totalPairs.ToString();
    }

    void LoadImagesAndWords()
    {
        frontImages = Resources.LoadAll<Sprite>("Level7Images");

        TextAsset jsonFile = Resources.Load<TextAsset>("Level7Names");

        if (jsonFile != null)
        {
            string jsonText = jsonFile.text;
            var data = JsonUtility.FromJson<ImageWordPairs>(jsonText);
            imageWordPairs = new Dictionary<string, string>();

            foreach (var pair in data.imageWordPairs)
            {
                if (!imageWordPairs.ContainsKey(pair.Num))
                {
                    imageWordPairs.Add(pair.Num, pair.Eng);
                }

            }
        }

        else
        {
            Debug.LogError("Json파일을 로드할 수 없습니다. : Level5Names");
        }

    }

    private void OnEnable()
    {
        Card.OnCardClicked += OnCardClicked;

    }

    private void OnDisable()
    {
        Card.OnCardClicked -= OnCardClicked;
    }

    // 카드 클릭시 처리
    private void OnCardClicked(Card selectedCard)
    {
        if (isFlipping || gameEnded || selectedCard.gameObject == firstCard || selectedCard == secondCard)
            return;

        if (firstCard == null)
        {
            firstCard = selectedCard.gameObject;
            Debug.Log("First card selected.");

            StartCoroutine(FlipCard(selectedCard, true));

        }
        else if (secondCard == null)
        {
            secondCard = selectedCard.gameObject;
            Debug.Log("Second card selected.");

            StartCoroutine(FlipCard(selectedCard, true));
            StartCoroutine(CheckForMatch());
        }
    }

    IEnumerator FlipCard(Card card, bool showFront)
    {
        isFlipping = true;
        Animator animator = card.GetComponent<Animator>();

        if (showFront)
        {
            animator.SetTrigger("Flip");
            yield return new WaitForSeconds(0.6f);

            if (card.isTextCard)
            {
                card.cardImage.enabled = false;
                card.textBackground.gameObject.SetActive(true);
                card.wordText.gameObject.SetActive(true);
            }
            else
            {
                card.cardImage.sprite = card.cardFrontSprite;
                card.cardImage.enabled = true;
                card.textBackground.gameObject.SetActive(false);
                card.wordText.gameObject.SetActive(false);
            }
        }
        else
        {
            animator.SetTrigger("FlipBack");
            yield return new WaitForSeconds(0.6f);

            card.cardImage.sprite = cardBackSprite;
            card.cardImage.enabled = true;
            card.textBackground.gameObject.SetActive(false);
            card.wordText.gameObject.SetActive(false);
        }

        isFlipping = false;
    }

    bool IsMatch(Card firstCardComponent, Card secondCardComponent)
    {
        if (firstCardComponent.cardID == secondCardComponent.cardID)
        {
            return (firstCardComponent.isTextCard != secondCardComponent.isTextCard);
        }
        return false;
    }


    // 매칭확인코루틴
    IEnumerator CheckForMatch()
    {
        yield return new WaitForSeconds(0.8f);

        Card firstCardComponent = firstCard.GetComponent<Card>();
        Card secondCardComponent = secondCard.GetComponent<Card>();

        UpdateMatchedPairs(firstCardComponent, secondCardComponent);

        if (matchedPairs == totalPairs)
        {
            EndGame();
        }
        else
        {
            isFlipping = false;
        }

        isFlipping = false;
    }

    private void UpdateMatchedPairs(Card firstCard, Card secondCard)
    {
        if (IsMatch(firstCard, secondCard))
        {
            matchedPairsList.Add(firstCard.cardID);
            matchedPairs++;
            UpdateSuccessText();

            this.firstCard = null;
            this.secondCard = null;
        }
        else
        {
            StartCoroutine(FlipCard(firstCard, false));
            StartCoroutine(FlipCard(secondCard, false));

            this.firstCard = null;
            this.secondCard = null;
        }
    }

    void EndGame()
    {
        gameEnded = true;
        int coinsEarned = matchedPairs * 100;

        PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins", 0) + coinsEarned);
        PlayerPrefs.SetInt("LastGameCoins", coinsEarned);

        SaveMatchedPairs();
        SceneManager.Instance.EnableDelay(1.3f, SceneType.Complete);
    }

    private void SaveMatchedPairs()
    {
        PlayerPrefs.SetInt("MatchedPairCount", matchedPairsList.Count);

        for (int i = 0; i < matchedPairsList.Count; i++)
        {
            PlayerPrefs.SetString($"MatchedPair_{i}", matchedPairsList[i]);
        }

        PlayerPrefs.Save();
        Debug.Log("Matched pairs saved" + matchedPairsList.Count);
    }
}
