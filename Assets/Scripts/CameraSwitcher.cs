using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CameraSwitcher : MonoBehaviour
{
    public Camera fpsCamera;
    public Camera downCamera;
    public GameObject player;
    public bool isFirstPerson;
    public bool isTacticalView;
    public Canvas editor;



  
    // Use this for initialization
    void Start()
    {
        editor.enabled = false;
        fpsCamera.enabled = true;
        isFirstPerson = fpsCamera.enabled;
        downCamera.gameObject.SetActive(false);
        downCamera.enabled = false;
    }

    void changeModeForFurniture(bool isTactical)
    {
        GameObject[] furniture = GameObject.FindGameObjectsWithTag("Furniture");
        foreach (GameObject f in furniture)
        {
            f.GetComponent<Furniture>().setViewMode(isTactical);
            print("yep");
        }

    }
    public void changeCamera()
    {
        fpsCamera.enabled = !fpsCamera.enabled;
        downCamera.enabled = !downCamera.enabled;
        downCamera.gameObject.SetActive(downCamera.enabled);
        player.SetActive(fpsCamera.enabled);
        isFirstPerson = !isFirstPerson;
        Cursor.visible = !isFirstPerson;
        if(isFirstPerson)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
        editor.enabled = !editor.enabled;

    }

    // Update is called once per frame
    void Update()
    {
      
        
       
        //If the c button is pressed toggle the camera
        if (Input.GetKeyDown(KeyCode.X))
        {
            changeCamera();
        }
        //If the c button is pressed toggle the camera switch to tactical view
        if (Input.GetKeyDown(KeyCode.T) && !isFirstPerson)
        {
            isTacticalView = !isTacticalView;
            changeModeForFurniture(isTacticalView);
        }
    }
}