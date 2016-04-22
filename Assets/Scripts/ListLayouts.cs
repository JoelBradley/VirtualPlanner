using UnityEngine;
using System.Runtime.InteropServices;
using System;



public class ListLayouts : MonoBehaviour {

    [DllImport("__Internal")]
    private static extern string getAllCookies();

    public GameObject button;
    public FurnitureLoader f;
    void Start()
    {
        generateList();
    }
    private void generateList()
    {
        string json = getAllCookies();
        //string json = "{\"layouts\":[{\"key\":\"7wh-a\" ,\"time\":1460663500645},{\"key\":\"we_71\" ,\"time\":1460664460180},{\"key\":\"yyFKP\" ,\"time\":1460667303314}]}";
     
        LayoutList o = JsonUtility.FromJson<LayoutList>(json);
        Array.Sort(o.layouts, delegate (Layout a, Layout b) { return b.time.CompareTo(a.time); });
        int y = 0;
        int j = 0;
        foreach(Layout l in o.layouts)
        {
            print(l.key);
            
            GameObject b = GameObject.Instantiate(button, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            b.transform.SetParent(GameObject.FindObjectOfType<Canvas>().gameObject.transform, false);
            b.transform.position = new Vector3(b.transform.parent.position.x, b.transform.parent.position.y - y,0);
            y += 100;
         
            b.GetComponentInChildren<UnityEngine.UI.Text>().text = l.key + " - Last Edited: " + fromUnixTime(l.time/1000).ToString("dd/MM/yyyy HH:mm:ss");
   
            string key = (string)l.key.Clone();
            b.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { this.buttonClicked(key); });
            //fff.AddComponent<UnityEngine.UI.Text>();
        }
        
    }
    private void buttonClicked(string key)
    {
        
       
        f.loadInFurniture(key);
    }

    public DateTime fromUnixTime(long unixTime)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return epoch.AddSeconds(unixTime);
    }

    [System.Serializable]
    public class LayoutList
    {
        public Layout[] layouts;
    }

    [System.Serializable]
    public class Layout
    {
        public string key;
        public long time;
    }
}
