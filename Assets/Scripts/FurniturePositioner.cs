using UnityEngine;
using System.Collections;

public class FurniturePositioner : MonoBehaviour {

    private Furniture currentFurniture;
    private Rigidbody body;
    public Camera tacCam;
    public GameObject[] furnitureObjects;
    private Vector3 camPos;
	// Use this for initialization
	void Start () {
       
        foreach (FurnitureLoader.FurniturePosition f in FurnitureLoader.obsToAdd)
        {
           
            foreach (GameObject fo in furnitureObjects)
            {

                if (fo.name == f.name)
                {
                    
                    GameObject o = GameObject.Instantiate(fo) as GameObject;
                  
                    o.transform.Rotate(new Vector3(0, f.r, 0));
                    o.transform.position = new Vector3(f.x,0,f.y);
                    
                    break;
                }
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
        float speed = tacCam.GetComponent<TacticalCameraController>().speed;
        float zoom = tacCam.GetComponent<TacticalCameraController>().zoom;
        float m = speed * zoom * Time.deltaTime;
        Vector3 movement = new Vector3();
        if (Input.mousePosition.y >= Screen.height * 0.95)
        {
            movement = new Vector3(0, m, 0);

        }
        if (Input.mousePosition.y <= Screen.height - (Screen.height * 0.95))
        {
            movement = new Vector3(0, -m, 0);

        }
        if (Input.mousePosition.x >= Screen.width * 0.95)
        {
            movement = new Vector3(m, 0, 0);

        }
        if (Input.mousePosition.x <= Screen.width - (Screen.width * 0.95))
        {
            movement = new Vector3(-m, 0, 0);

        }
        if(tacCam.isActiveAndEnabled)
            tacCam.gameObject.transform.Translate(movement);
       
        if(currentFurniture!=null)
        if (!currentFurniture.getMoving())
        {
            Vector3 mpos = getMouseInWorld();
            GameObject[] fs;
            mpos.y = 0.3f;
            //get all the objects placed by user
            fs = GameObject.FindGameObjectsWithTag("Furniture");
            foreach (GameObject f in fs)//check all items placed
            {
                if (pointInOABB(mpos,f.GetComponent<BoxCollider>()))
                {//check if mouse if in collision box
                    if (Input.GetMouseButtonUp(1))
                    {//and if the right mouse button is pressed
                        f.GetComponent<Furniture>().setBeingMoved(true);
                        this.currentFurniture = f.GetComponent<Furniture>();
                            //enable the furniture to be moved again
                    }     
                    
                }
            }
        }  

       
        
	}

    private Vector3 getMouseInWorld()
    {
        Vector3 v3 = Input.mousePosition;
        v3.z = 4.9f;
        
        v3 = tacCam.ScreenToWorldPoint(v3);
        return v3;
    }

    private bool pointInOABB(Vector3 point, BoxCollider box)
    {
        point = box.transform.InverseTransformPoint(point) - box.center;

        float halfX = (box.size.x * 0.5f);
        float halfY = (box.size.y * 0.5f);
        float halfZ = (box.size.z * 0.5f);
        if (point.x < halfX && point.x > -halfX &&
           point.y < halfY && point.y > -halfY &&
           point.z < halfZ && point.z > -halfZ)
            return true;
        else
            return false;
    }
    public void addNewFurniture(GameObject furniture)
    {
        if(currentFurniture!=null)
            currentFurniture.setBeingMoved(false);
        GameObject o = GameObject.Instantiate(furniture, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        o.GetComponent<Furniture>().setBeingMoved(true);
        currentFurniture = o.GetComponent<Furniture>();
    }
}
