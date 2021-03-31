using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    private static int[] scores = {0, 0, 0, 0};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        var widthPortion = Screen.width / 4;
        foreach (var player in System.Enum.GetValues(typeof(PlayerNumbers))) {
            int playerNumber = (int) player;
            var xPosition = playerNumber * widthPortion + 10;
            var playerText = "Player " + (playerNumber + 1) + " Score: ";
            GUI.Box(new Rect(xPosition, 10, 200, 30), playerText + this.GetPlayerScore(playerNumber));
        }
    }
    public static void AddScore(PlayerNumbers playerNumber)
    {
        Scoring.scores[(int) playerNumber]++;
    }

    private int GetPlayerScore(int playerNumber)
    {
        return Scoring.scores[playerNumber];
    }
}
