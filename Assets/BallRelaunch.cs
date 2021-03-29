using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRelaunch : MonoBehaviour
{
    public Ball ball;
    public Player player1;
    public Player player2;
    public Vector3 relaunchPosition = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var scoringPlayer = other.transform.position.x > 0 ? this.player1 : this.player2;
        scoringPlayer.score++;
        this.ball.Relaunch(this.relaunchPosition);
    }
}
