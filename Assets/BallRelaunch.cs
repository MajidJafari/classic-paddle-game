using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRelaunch : MonoBehaviour
{
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
        other.GetComponent<Ball>()?.Scored(this.relaunchPosition);
    }
}
