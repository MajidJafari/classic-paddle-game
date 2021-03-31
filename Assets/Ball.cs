using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


static class BallCoroutines {
    public static string WaitForIt{ get { return "WaitForIt"; } }
}

[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour
{
    public Scoring scoreboard;
    public float secondsToWaitToLaunch = 3;
    public Vector2 lowerBound = new Vector2(-4, 6);
    public Vector2 upperBound = new Vector2(3, 8);
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
                UnityEngine.Random.Range(this.lowerBound.x, this.upperBound.x),
                UnityEngine.Random.Range(this.lowerBound.y, this.upperBound.y),
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
