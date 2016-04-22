using UnityEngine;
using System.Collections;

public class TextureSizer : MonoBehaviour {

	// Use this for initialization
    public float xScale;
    public float yScale;
    private Renderer renderer;
	void Start () {
        renderer = GetComponent<Renderer>();
        
    }
	
	// Update is called once per frame
	void Update () {
        renderer.material.mainTextureScale = new Vector2(transform.localScale.x * xScale, transform.localScale.y * yScale);
    }
}
