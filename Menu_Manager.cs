using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour
{
    //Canvas object to access its children without repeating the method twice.
    public GameObject menuCanvas; 

    // Start is called before the first frame update
    public void Start()
    {
        menuCanvas = transform.GetChild(0).gameObject;
        OpenStages();
    }

    //Closes all Menus
    //Before setting correct Menu to active.
    public void CloseMenus()
    {
        for (int i = 0; i < 3; i++)
        {
            menuCanvas.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    //Sets Stage_Menu to active.
    public void OpenStages()
    {
        CloseMenus();
        menuCanvas.transform.GetChild(0).gameObject.SetActive(true);
    }
    //Sets Shop_Menu to active.
    public void OpenShop()
    {
        CloseMenus();
        menuCanvas.transform.GetChild(1).gameObject.SetActive(true);
    }

    //Sets Equip_Menu to active.
    public void OpenEquip()
    {
        CloseMenus();
        menuCanvas.transform.GetChild(2).gameObject.SetActive(true);
    }

    //Automatically closes MenuScene
    //Loads stage scene depending on button pressed.
    public void LoadStage(string levelName)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }


  


}
