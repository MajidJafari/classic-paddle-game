using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


static class BallCoroutines {
    public static string WaitForIt{ get { return "WaitForIt"; } }
}

public class BoundaryPosition {
    public int lower{ get; set; }
    public int upper{ get; set; }

    public BoundaryPosition(int lower, int upper) {
        this.lower = lower;
        this.upper = upper;
    }
}

[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour
{
    public Scoring scoreboard;
    public float secondsToWaitToLaunch = 3;
    public BoundaryPosition randomPositionX = new BoundaryPosition(6, 8);
    public BoundaryPosition randomPositionY = new BoundaryPosition(-4, 3);
    private Nullable<PlayerNumbers> latestTouchingPlayerNumber = null;

    // Start is called before the first frame update
    void Start()
    {
        var rigidBody = GetComponent<Rigidbody>();
        if(rigidBody) {
            rigidBody.freezeRotation = true;
        }
        StartCoroutine(BallCoroutines.WaitForIt);
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator WaitForIt() {
        var rigidBody = GetComponent<Rigidbody>();
        yield return new WaitForSeconds(this.secondsToWaitToLaunch);
        if (rigidBody) {
            rigidBody.AddForce(
                UnityEngine.Random.Range(this.randomPositionX.lower, this.randomPositionX.upper),
                UnityEngine.Random.Range(this.randomPositionY.lower, this.randomPositionY.upper),
            0);
        }
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        this.latestTouchingPlayerNumber = (PlayerNumbers) collisionInfo.gameObject.GetComponent<Player>()?.playerNumber;
        GetComponent<AudioSource>().Play();
    }

    public void Scored(Vector3 relaunchPosition) {
        this.scoreboard.AddScore(this.latestTouchingPlayerNumber);
        this.latestTouchingPlayerNumber = null;
        this.transform.position = relaunchPosition;
    }
}
