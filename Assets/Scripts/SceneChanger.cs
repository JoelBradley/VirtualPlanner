using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public void changeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
