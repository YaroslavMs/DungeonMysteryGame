using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Nmascene : MonoBehaviour
{ 
public string levelIndex;
    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
            SceneManager.LoadScene(levelIndex);
    }
}
        
   

