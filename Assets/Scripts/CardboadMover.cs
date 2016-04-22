using UnityEngine;
using System.Collections;

public class CardboadMover : MonoBehaviour {

    private bool moving = false;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected GameObject head;
    private Rigidbody body;
    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Cardboard.SDK.Triggered) moving = !moving;
        Vector3 movement = new Vector3(0,0,0);
        if (moving) movement = head.transform.forward *speed;
        movement.y = 0;
        //print(movement);
        body.MovePosition(movement+body.position);//.position += movement;
    }
}
