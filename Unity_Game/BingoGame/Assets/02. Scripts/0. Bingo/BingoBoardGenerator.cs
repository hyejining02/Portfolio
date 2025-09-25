using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable IDE0051

public class BingoBoardGenerator : MonoBehaviour
{
    public BingoCell[] cells;

    private void Awake()
    {
        cells = GetComponentsInChildren<BingoCell>();
        if ( cells.Length != 16 )
        {
            Debug.LogError($"Invalid number of cells. Expected 16, got {cells.Length}.");
        }
    }

    public void PopulateBingoBoard(List<ImageWordPair> pairs, bool isUserBoard)
    {
        if ( pairs == null || pairs.Count < cells.Length)
        {
            Debug.LogError("Invalid pairs data for populating bingo board");
            return;
        }

        for ( int i = 0; i < cells.Length; i++ )
        {
            cells[i].Initialize(pairs[i], isUserBoard);
        }
    }

    public List<ImageWordPair> GetBoardPairs()
    {
        List<ImageWordPair> boardPairs = new List<ImageWordPair>();
        foreach ( var cell in cells )
        {
            if ( cell.Pair != null )
            {
                boardPairs.Add( cell.Pair );
            }
        }
        return boardPairs;
    }

    public void MarkCorrect(string pairNum)
    {
        foreach (var cell in cells)
        {
            if (cell.Pair != null && cell.Pair.Num == pairNum)
            {
                cell.MarkAsCorrectWithSprite("answer");
            }
        }
    }

    public int CheckBingoLines()
    {
        int bingoLines = 0;

        for ( int row = 0; row < 4; row++ )
        {
            if ( CheckRow(row) ) bingoLines++;
        }

        for ( int col = 0; col < 4; col++ )
        {
            if ( CheckColumn(col) ) bingoLines++;
        }

        if (CheckDiagonal(true)) bingoLines++;
        if (CheckDiagonal(false)) bingoLines++;

        return bingoLines;
    }

    private bool CheckRow ( int row )
    {
        for ( int i = 0; i < 4;  i++ )
        {
            if (!cells[ row * 4 + i].IsMarked) return false;
        }
        return true;
    }

    private bool CheckColumn ( int col )
    {
        for ( int i = 0; i < 4; i++ )
        {
            if (!cells[col + i * 4].IsMarked) return false;
        }
        return true;
    }

    private bool CheckDiagonal ( bool isLeftToRight  )
    {
        for ( int i = 0; i < 4; i++ )
        {
            int index = isLeftToRight ? i * 5 : (i + 1) * 3;
            if (!cells[index].IsMarked) return false;
        }
        return true;
    }

    private void Shuffle(List<ImageWordPair> pairs )
    {
        for ( int i = pairs.Count-1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            ImageWordPair temp = pairs[i];
            pairs[i] = pairs[randomIndex];
            pairs[randomIndex] = temp;
        }
    }

}
