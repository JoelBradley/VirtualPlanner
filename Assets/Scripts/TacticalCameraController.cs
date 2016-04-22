using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TacticalCameraController : MonoBehaviour {

    public Camera tacCam;
    public float speed;
    public float zoomSpeed;
    private float targetZoom = 5;
    public float zoom = 5;
 	// Use this for initialization
	void Start () {
	
	}


    // Update is called once per frame
    //private int timer;
    //private float startZoom = 5;
    private float rotationSpeed = 50f;

    void Update()
    {
       
        if (CrossPlatformInputManager.GetAxis("Mouse ScrollWheel") < 0 && tacCam.orthographicSize < 10) // out
        {
            targetZoom++;
        }else if (CrossPlatformInputManager.GetAxis("Mouse ScrollWheel") > 0 && tacCam.orthographicSize > 1) // in
        {
            targetZoom--;
        }

        //handle camera rotation and translation
        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.E))
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.up * speed * zoom * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(-Vector3.up * speed * zoom * Time.deltaTime);

        if(Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * speed * zoom * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            transform.Translate(-Vector3.left * speed * zoom * Time.deltaTime);
    }
    //called after targetZoom has been edited in Update(). Makes the zooming smooth.
    void LateUpdate()
    {
        zoom = Mathf.Lerp(zoom, targetZoom, Time.deltaTime * zoomSpeed);//make zooming smoother
        if (zoom < 10 && zoom > 1)
        {
            tacCam.orthographicSize = zoom;
        }
        else
        {
            if (zoom > 10) targetZoom = 10;//ensures zoom can't become greater than 10
            else if (zoom < 1) targetZoom = 1;//or less than 1
        }
       
    }
}
