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
            // �ִϸ��̼� ��� �� ī�� �ո����� ����
            animator.SetTrigger("Flip");
            Invoke("ShowFront", 0.5f); // �ִϸ��̼� ��� �� ����
        }
        else
        {
            // �̹� ������ ���¸� �޸����� ����
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