using UnityEngine;
using UnityEngine.UI;

public class CardFlip : MonoBehaviour
{
    public Sprite cardBack;
    public Sprite cardFront;
    public bool isFlipped = false;

    private Image cardImage;
    private Animator animator;

    private void Start()
    {
        cardImage = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    public void OnCardClick()
    {
        if (!isFlipped)
        {
            // 애니메이션 재생 후 카드 앞면으로 변경
            animator.SetTrigger("Flip");
            Invoke("ShowFront", 0.5f); // 애니메이션 재생 후 반전
        }
        else
        {
            // 이미 뒤집힌 상태면 뒷면으로 변경
            animator.SetTrigger("FlipBack");
            Invoke("ShowBack", 0.5f);
        }
        isFlipped = !isFlipped;
    }

    private void ShowFront()
    {
        cardImage.sprite = cardFront;
    }

    private void ShowBack()
    {
        cardImage.sprite = cardBack;
    }
}