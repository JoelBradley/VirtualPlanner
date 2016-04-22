using UnityEngine;
using System.Collections;

public class Furniture : MonoBehaviour {

    public bool isTacticalView;
    public bool isMovable;
    private bool isBeingMoved = false;
    public Rigidbody body;
    public Camera tacCam;
    private Vector3 velocity = new Vector3(0,0,0);
	// Use this for initialization
	void Start () {
        this.body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))//when the left mouse button is pressed
            this.setBeingMoved(false);//fix the object in the scene
        
        if (this.isBeingMoved)//if it's not fixed in place
        {
            if (Input.GetMouseButton(1))//if the right mouse button is pressed
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                
                //rotate object by moving mouse on its X-axis
                Vector3 angle = new Vector3(0,Input.GetAxis("Mouse X"),0);
               
                Quaternion deltaRotation = Quaternion.Euler(angle*3);
                body.MoveRotation(body.rotation * deltaRotation);//rotate object
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                body.MovePosition(getMouseInWorld());//moves object to 
                //where the mouse is the world so it follows the mouse
            }
        }    
    }

    private Vector3 getMouseInWorld()
    {
        Vector3 v3 = Input.mousePosition;
        v3.z = 4.9f;
        v3 = Camera.current.ScreenToWorldPoint(v3);
        return v3;
    }
    public void move(Vector3 m)
    {
        
        if (this.isBeingMoved)
        {
            velocity = m;
         
        }
    }
    public void setBeingMoved(bool isMove)
    {
        this.isBeingMoved = isMove;
    }
    public bool getMoving()
    {
        return this.isBeingMoved;
    }
    public void setViewMode(bool t)
    {
        isTacticalView = t;

    }
}
