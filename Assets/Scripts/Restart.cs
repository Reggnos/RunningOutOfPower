using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {


    void Start()
    {
        Invoke("LoadFirst", 1);
    }

    void LoadFirst()
    {
        SceneManager.LoadScene("Boot");
    }
}
