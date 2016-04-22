using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FurnitureLoader : MonoBehaviour {

    public Button b;
    public GameObject[] furnitureObjects;
    public GameObject alert;
    public static FurniturePosition[] obsToAdd;
    public static bool loadedLevel = false;
    public static string key;
    
    // Use this for initialization
    void Start () {
        alert.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
   
    public void checkKey(InputField i)
    {
        alert.SetActive(false);
        string text = i.text;
        if (text.Length == 5)
        {
            b.interactable = true;
        }
        else
        {
           b.interactable = false;
        }
        
    }
    private void populateRoom(string jsonString) {

        SceneManager.LoadScene("room");
        ArrayList furniture = new ArrayList();
        string[] types = jsonString.Split('[');
        for(int i=2;i< types.Length; i++)
        {
            string[] names = types[i - 1].Split('"');
            string name = names[names.Length - 2];
            string data = types[i].Split(']')[0];
            string[] pos = data.Split('}');
            foreach(string s in pos)
            {
                if (s.Length < 4) continue;
                FurniturePosition f = new FurniturePosition();
                f.name = name;
                f.x = float.Parse(s.Split(':')[1].Split(',')[0]);
                f.y = float.Parse(s.Split(':')[2].Split(',')[0]);
                f.r = float.Parse(s.Split(':')[3].Split('}')[0]);
                furniture.Add(f);
            }
        }
        obsToAdd = (FurniturePosition[])furniture.ToArray(typeof(FurniturePosition));
        loadedLevel = true;
    }

    public void loadOnButtonPress(InputField i)
    {
 
        WWW w = new WWW("https://jrbradley.co.uk:8000/imbooking/api/exist/" + i.text);
        StartCoroutine("checkKeyData", new object[] { w,i.text});
    }


    public void loadInFurniture(string k)
    {
        key = k;
        WWW w = new WWW("https://jrbradley.co.uk:8000/imbooking/api/get/"+key+"/furniture");
        StartCoroutine("getData",w);
    }

    private void handleKeysExistance(string e,string key)
    {
    
        if (e == "true")
        {
            loadInFurniture(key);
        }
        else
        {
            alert.SetActive(true);
            //EditorUtility.DisplayDialog("Key not recognised!", "The key you entered was not recognised. Please try another.", "Try Again");
        }

    }

    IEnumerator getData(WWW www)
    {
        yield return www;
        if (www.error != null)
        {
            Debug.Log(www.error);
            //populateRoom(www.text);
        }
        else
        {
            populateRoom(www.text);
        }
    }
    IEnumerator checkKeyData(object[] ob)
    {
        WWW www = (WWW) ob[0];
        string key = (string)ob[1];
 
        yield return www;
        if (www.error != null)
        {
            Debug.Log(www.error);
            handleKeysExistance(www.text,key);
        }
        else
        {
      
            handleKeysExistance(www.text,key);
        }
    }
 

    [System.Serializable]
    public class FurniturePosition
    {
        public float x, y, r;
        public string name;

        public FurniturePosition(string name, float x, float y, float r) {
            this.x = x;
            this.y = y;
            this.r = r;
            this.name = name;
      
        }

        public FurniturePosition()
        {
           
        }

    }
}

