using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadscene : MonoBehaviour
{

    
    

   

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag=="house")
        SceneManager.LoadScene(1);
    }
}


