using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerNumbers {
    P1, P2, P3, P4
}

enum MovementTypes {
    Forward, Backward, Idle
}

public class Player : MonoBehaviour
{
    public PlayerNumbers playerNumber;
    public float speed = 20.0f;
    public string moveForwardKeyboard;
    public string moveBackwardKeyboard;

    public string speedBallDownKeyboard = "-";
    public string speedBallUpKeyboard = "=";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovementTypes movementType = Input.GetKey(this.moveForwardKeyboard)
            ? MovementTypes.Forward
            : Input.GetKey(this.moveBackwardKeyboard) 
            ? MovementTypes.Backward 
            : MovementTypes.Idle;
        this.move(movementType);

        MassTypes ballMassType = Input.GetKey(this.speedBallUpKeyboard)
            ? MassTypes.Light
            : Input.GetKey(this.speedBallDownKeyboard)
            ? MassTypes.Heavy
            : MassTypes.Void;
            
        Ball.Instance.SetMass(ballMassType, this.playerNumber);
    }

    private void move(MovementTypes type)
    {
        var multiplier = this.getMultiplier(type);
        var translationAmount = multiplier * Time.deltaTime;
        var translationVector = new Vector3(0, translationAmount, 0);
        transform.Translate(translationVector);
    }

    private float getMultiplier(MovementTypes type) {
        switch(type) {
            case MovementTypes.Forward:
                return this.speed;
            case MovementTypes.Backward:
                return -this.speed;
            default:
                return 0;
        }
    }
}