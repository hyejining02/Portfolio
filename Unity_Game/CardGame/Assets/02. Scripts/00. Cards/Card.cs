using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Card : MonoBehaviour
{
    public Image cardImage;
    public Image textBackground;    // �ؽ�Ʈī�� ����̹���

    public TMP_Text wordText;

    public Sprite cardBackSprite;
    public Sprite cardFrontSprite;

    public string cardID;   // ī���� ����ID

    private Animator animator;
    private bool isFlipped = false;
    public bool isTextCard = false;

    public delegate void CardClickedDelegate(Card selectedCard);  // Card �Ű������� ����
    public static event CardClickedDelegate OnCardClicked;  // �̺�Ʈ ����

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetupCard(Sprite frontImage, string word)
    {
        if (frontImage != null)   // �̹���ī��
        {
            isTextCard = false;
            cardFrontSprite = frontImage;
            wordText.gameObject.SetActive(false);
            textBackground.gameObject.SetActive(false);
            cardImage.gameObject.SetActive(true);
            cardImage.sprite = cardBackSprite;

        }
        else if (!string.IsNullOrEmpty(word))   // �ܾ�ī��
        {
            isTextCard = true;
            wordText.text = word;
            cardImage.sprite = cardBackSprite;
            wordText.gameObject.SetActive(false);
            textBackground.gameObject.SetActive(false);
        }
    }

    IEnumerator Flip()
    {
        animator.SetTrigger("Flip");
        yield return new WaitForSeconds(0.6f);

        if ( isTextCard )   // �ܾ�ī��
        {
            cardImage.enabled = false;
            textBackground.gameObject.SetActive(true);
            wordText.gameObject.SetActive(true);
        }
        else // �̹���ī��
        {
            cardImage.enabled = true;
            cardImage.sprite = cardFrontSprite;
            textBackground.gameObject.SetActive(false);
            wordText.gameObject.SetActive(false);
        }

        isFlipped = true;
    }

    public void FlipBack()
    {
        if (!isFlipped) return;

        animator.SetTrigger("FlipBack");

        cardImage.enabled = true;
        cardImage.sprite = cardBackSprite;
        wordText.gameObject.SetActive(false);
        textBackground.gameObject.SetActive(false);

        isFlipped = false;
    }

    public string GetCardValue()
    {
        return isTextCard ? wordText.text : cardFrontSprite.name;
    }

    public void CardClicked()
    {
        OnCardClicked?.Invoke(this);
        StartCoroutine(Flip());
    }
}