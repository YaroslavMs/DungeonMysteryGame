using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewBehaviourScript1 : MonoBehaviour
{
    int levelUnLock;
    public Button[] buttons;

    void Start()
    {
        levelUnLock = PlayerPrefs.GetInt("levels", 1);

        for (int i = 1; i < buttons.Length; i++)
            buttons[i].interactable = false;


        for (int i = 0; i < levelUnLock; i++)
            buttons[i].interactable = true;
    }
    
    public void loadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void rt()
    {
        for (int i = 1; i < buttons.Length; i++)
            buttons[i].interactable = false;

        PlayerPrefs.DeleteKey("levels");
    }
}