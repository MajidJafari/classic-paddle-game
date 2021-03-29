using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum MovementTypes
{
    Up, Down, Idle
}

public class Player : MonoBehaviour
{
    public float translationMultiplier = 20.0f;
    public string moveUpKeyboard = "w";
    public string moveDownKeyboard = "s";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovementTypes movementType = Input.GetKey(this.moveUpKeyboard)
            ? MovementTypes.Up 
            : Input.GetKey(this.moveDownKeyboard) 
            ? MovementTypes.Down 
            : MovementTypes.Idle;
        this.move(movementType);
    }

    private void move(MovementTypes type)
    {
        var multiplier = this.getMultiplier(type);
        transform.Translate(0, multiplier * Time.deltaTime, 0);
    }

    private float getMultiplier(MovementTypes type) {
        switch(type) {
            case MovementTypes.Up:
                return this.translationMultiplier;
            case MovementTypes.Down:
                return -this.translationMultiplier;
            default:
                return 0;    
        }
    }
}