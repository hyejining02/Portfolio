using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResult
{
    public List<ResultScene> Level1Result = new List<ResultScene>();

    public int totalCoins;

}

public class ResultScene
{
    public string imageUrl;
    public string correctAnswer;
    public bool isCorrect;

}
