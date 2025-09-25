using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform cardParent;
    public ImageWordLoader imageWordLoader;

    private List<Card> cards = new List<Card>();

    private int totalPairs;
    private int totalCards;

    private void OnEnable()
    {
        Card.OnCardClicked += HandleCardClicked;
    }

    private void OnDisable()
    {
        Card.OnCardClicked -= HandleCardClicked;
    }

    void Start()
    {
        SetDifficulty("normal");
        SetupCards();
    }

    public void SetDifficulty(string difficulty)
    {
        switch (difficulty.ToLower())
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

        totalCards = totalPairs * 2;
    }

    void SetupCards()
    {
        cards.Clear();
        Debug.Log("Total Pairs : " + totalPairs);

        List<ImageWordLoader.ImageWordPair> selectedCards = LoadRandomCardData(totalPairs);

        for (int i = 0; i < totalPairs; i++)
        {
            // 이미지 카드 생성
            GameObject imageCardObj = Instantiate(cardPrefab, cardParent);
            Card imageCard = imageCardObj.GetComponent<Card>();
            Sprite imageSprite = imageWordLoader.GetImageByNum(selectedCards[i].Num);
            imageCard.SetupCard(imageSprite, null);
            cards.Add(imageCard);
            Debug.Log("Image Card Added : " + i);

            // 텍스트카드(단어카드)생성
            GameObject textCardObj = Instantiate(cardPrefab, cardParent);
            Card textCard = textCardObj.GetComponent<Card>();
            textCard.SetupCard(null, selectedCards[i].Eng);
            cards.Add(textCard);
            Debug.Log("Text Card Added : " + i);
        }

        ShuffleAndArrangeCards();

        Debug.Log("Total Card Generated : " + cards.Count);
    }

    void ShuffleAndArrangeCards()
    {
        cards = ShuffleList(cards);
        int rows = 2;
        int cols = totalCards / rows;

        for (int i = 0; i < cards.Count; i++)
        {
            int row = i / cols;
            int col = i % cols;
            Vector2 position = new Vector2(col * 100, row * -100);
            cards[i].transform.localPosition = position;
        }
    }

    List<ImageWordLoader.ImageWordPair> LoadRandomCardData(int pairs)
    {
        if (imageWordLoader == null )
        {
            Debug.LogError("ImageWordLoader 설정되지 않음");
            return null;
        }

        List<ImageWordLoader.ImageWordPair> selectedCards = new List<ImageWordLoader.ImageWordPair>();
        HashSet<string> selectedNums = new HashSet<string>();
        imageWordLoader.LoadImageWordPairs();

        while (selectedCards.Count < pairs)
        {
            var randomCard = imageWordLoader.imageWordPairs.imageWordPairs[Random.Range(0, imageWordLoader.imageWordPairs.imageWordPairs.Count)];

            if ( !selectedNums.Contains(randomCard.Num) )
            {
                selectedCards.Add(randomCard);
                selectedNums.Add(randomCard.Num);
            }
        }

        return selectedCards;
    }

    List<Card> ShuffleList(List<Card> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Card temp = list[i];
            int randomIndex = Random.Range(0, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }

    void HandleCardClicked(Card selectedCard)
    {
        Debug.Log("Card clicked: " + selectedCard.GetCardValue());
    }
}
