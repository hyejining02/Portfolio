using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BingoCell : MonoBehaviour
{
    public Button button;
    public TMP_Text cellText;

    public ImageWordPair Pair { get; set; }
    public bool IsMarked { get; private set; }

    public void Initialize(ImageWordPair pair, bool isUserBoard)
    {
        Pair = pair;
        cellText.text = pair.Eng;
        cellText.gameObject.SetActive(isUserBoard);
        IsMarked = false;

        if ( isUserBoard )
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnCellClicked);
        }
        else
        {
           // button.interactable = false;
        }
    }

    public void MarkAsCorrectWithSprite(string spriteName)
    {
        IsMarked = true;

        Transform cellOnImage = transform.Find("AnswerOverlay");
        if ( cellOnImage != null)
        {
            Image overlayImage = cellOnImage.GetComponent<Image>();
            if ( overlayImage != null )
            {
                Sprite loadedSprite = Resources.Load<Sprite>(spriteName);
                if ( overlayImage != null )
                {
                    overlayImage.sprite = loadedSprite;
                    cellOnImage.gameObject.SetActive(true);
                }
                else
                {
                    Debug.LogError($"Failed to load sprite: {spriteName}");
                }
            }
            else
            {
                Debug.LogWarning($"Image component missing on AnswerOverlay in cell: {name}");
            }
        }
        else
        {
            Debug.LogWarning($"AnswerOverlay object not found in cell: {name}");
        }

        Debug.Log($"Cell marked as correct: {Pair.Eng}");

    }

    private void OnCellClicked()
    {
        Debug.Log($"Cell clicked: {Pair?.Eng ?? "Unknown"}");
    }
}
