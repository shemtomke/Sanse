using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //RESTART BUTTON
    public void Restart()
    {
        SceneManager.LoadScene(1);
       
    }

    //quit
    public void Quit()
    {
        Application.Quit();
    }
}
