using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour {

    public SceneManagerScript AudioSource;

    private static SceneManagerScript instance = null;

    public static SceneManagerScript Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null & instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public static void LoadScene(int AddBuildIndexToCurrent)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + AddBuildIndexToCurrent);
    }
}
