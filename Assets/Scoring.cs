using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerTypes {
    P1, P2
}
public class Scoring : MonoBehaviour
{
    public Player player1;   
    public Player player2;   
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
        this.DisplayScore(10, PlayerTypes.P1);
        this.DisplayScore(Screen.width - 250, PlayerTypes.P2);
    }

    private void DisplayScore(float xPosition, PlayerTypes player)
    {
        var playerProp = player == PlayerTypes.P1 ? this.player1 : this.player2;
        string playerText = player == PlayerTypes.P1 ? "Player 1 Score: " : "Player 2 Score: ";
        GUI.Box(new Rect(xPosition, 10, 200, 30), playerText + playerProp.score);
    }
}
