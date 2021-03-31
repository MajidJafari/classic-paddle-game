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
    public float secondsToWaitToLaunch = 3;
    public BoundaryPosition randomPositionX = new BoundaryPosition(6, 8);
    public BoundaryPosition randomPositionY = new BoundaryPosition(-4, 3);
    private PlayerNumbers _latestTouchingPlayerNumber;
    public PlayerNumbers latestTouchingPlayerNumber { get { return this._latestTouchingPlayerNumber; } }

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
                Random.Range(this.randomPositionX.lower, this.randomPositionX.upper),
                Random.Range(this.randomPositionY.lower, this.randomPositionY.upper),
            0);
        }
    }

    private void OnCollisionEnter(Collision collisionInfo) 
    {
        Player player = collisionInfo.gameObject.GetComponent<Player>();
        if (player) {
            this._latestTouchingPlayerNumber = player.playerNumber;
        }
        GetComponent<AudioSource>().Play();
    }

    public void Relaunch(Vector3 vector) => this.transform.position = vector;
}
