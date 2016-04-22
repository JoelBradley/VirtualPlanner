using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class LayoutPoster : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern void setCurrentKey(string key);

    [DllImport("__Internal")]
    private static extern void saveKeyInCookie(string key);

    private string currentKey;
    public GameObject keyPopUp;
    // Use this for initialization
    void Start()
    {
        keyPopUp.SetActive(false);
        if (FurnitureLoader.loadedLevel)
        {
            currentKey = FurnitureLoader.key;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void generateAndPostLayout()
    {
        //get all the objects
        GameObject[] furnitures;
        furnitures = GameObject.FindGameObjectsWithTag("Furniture");
      
        if(furnitures.Length == 0)//if there are no items
        {//send empty json list
            postData("{\"furniture\":[]}");
            return;
        }
        ArrayList furniture = new ArrayList();
        ArrayList namesDone = new ArrayList();
        foreach (GameObject o in furnitures)
        {
            //create list of objects with necessary data
            F f = new F();
            f.type = o.ToString().Split(' ')[0];
            f.x = o.transform.position.x;
            f.y = o.transform.position.z;
            f.r = o.transform.eulerAngles.y;
            furniture.Add(f);
        }
        string j = "{\"furniture\":[";//start json string
        foreach (F f in furniture)
        {
            string name = f.type;
            if (namesDone.Contains(name)) continue;
            //add json object list for each type of furniture
            j += "{\"" + name.Split('(')[0] + "\":[";
            foreach (F ff in furniture)
            {
                //add JSON object for each instance of that type of furniture
                if (ff.type.Equals(name))
                {
                    j += "{\"x\":" + ff.x + ",\"y\":" + ff.y + ",\"r\":" + ff.r + "},";
                }
            }
            namesDone.Add(name);
            j = j.Remove(j.Length - 1);//remove unnecessary comma
            j += "]},";
        }
        j = j.Remove(j.Length - 1);
        j += "]}";
        print(j);
        postData(j);//send data to server
    }

    void postData(string data)
    {

        //Hashtable headers = new Hashtable();
        //headers.Add("Content-Type", "application/json");
        byte[] body = Encoding.UTF8.GetBytes(data);
        WWWForm form = new WWWForm();
        form.AddBinaryData("JSON", body);
        if (currentKey == null) {
            WWW www = new WWW("https://jrbradley.co.uk:8000/imbooking/api/new", form);
            StartCoroutine("PostdataEnumerator", www);
        }
        else
        {
            WWW www = new WWW("https://jrbradley.co.uk:8000/imbooking/api/furniture/set/"+currentKey, form);
            StartCoroutine("setFurnitureData", www);
        }

        



    }

    IEnumerator PostdataEnumerator(WWW www)
    {
        yield return www;

        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            
            StartCoroutine("displayKey", www.text);
            saveKeyInCookie(www.text);
            setCurrentKey(www.text);
            currentKey = www.text;
        }
    }

    IEnumerator displayKey(string key)
    {
        yield return key;

        keyPopUp.SetActive(true);
        keyPopUp.GetComponent<InputField>().text = "Saved: "+key;
        //yield return new WaitForSeconds(5);
        //keyPopUp.SetActive(false);
    }

        IEnumerator setFurnitureData(WWW www)
    {
        yield return www;

        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.text);
         
        }
    }

    private class F
    {
        public string type;
        public float x;
        public float y;
        public float r;
    }
}
