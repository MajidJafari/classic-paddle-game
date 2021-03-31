using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MassTypes {
    Heavy, Light, Void
}

static class BallCoroutines {
    public static string WaitForIt{ get { return "WaitForIt"; } }
}

[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour
{
    private static Ball _instance;
    public static Ball Instance{ get { return Ball._instance; } }
    public float heavyMass = 0.01f;
    public float lightMass = 0.00f;

    public Scoring scoreboard;
    public float secondsToWaitToLaunch = 3;
    public Vector2 lowerBound = new Vector2(-4, 6);
    public Vector2 upperBound = new Vector2(3, 8);
    private Nullable<PlayerNumbers> toucher = null;
    private Nullable<PlayerNumbers> speedController = null;

     private void Awake() {
        if (_instance != null && _instance != this)
        {
        Destroy(this.gameObject);
        return;//Avoid doing anything else
        }
    
        _instance = this;
        DontDestroyOnLoad( this.gameObject );
    }
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
        Player player = collisionInfo.gameObject.GetComponent<Player>();
        if (player) {
            this.toucher = player.playerNumber;
        }
        GetComponent<AudioSource>().Play();
    }

    public void Scored(Vector3 relaunchPosition) 
    {
        this.scoreboard.AddScore(this.toucher);
        this.speedController = this.toucher;
        this.toucher = null;
        this.transform.position = relaunchPosition;
    }

    public void SetMass(MassTypes massType, PlayerNumbers playerNumber)
    {
        if (
            !(massType is MassTypes.Void) &&
            playerNumber == this.speedController
        ) 
        {
            GetComponent<Rigidbody>().mass = massType == MassTypes.Heavy
                ? this.heavyMass
                : this.lightMass;   
        }
    }
}
