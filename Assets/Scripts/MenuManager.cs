using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MenuManager
{
    public static bool isInitialised { get; private set; }
    public static GameObject mainMenu, settingsMenu, creditsMenu;
    public static void Init()
    {
        GameObject canvas = GameObject.Find("MenuCanvas");
        mainMenu = canvas.transform.Find("MainMenu").gameObject;
        settingsMenu = canvas.transform.Find("SettingsMenu").gameObject;
        creditsMenu = canvas.transform.Find("CreditsMenu").gameObject;

        isInitialised = true;
    }

    public static void OpenMenu(Menu menu, GameObject callingMenu)
    {
        if(!isInitialised) Init();

        switch(menu)
        {
            case Menu.MAINMENU:
                mainMenu.SetActive(true);
                break;
            case Menu.SETTINGS:
                settingsMenu.SetActive(true);
                break;
            case Menu.CREDITS:
                creditsMenu.SetActive(true);
                break;
        }

        callingMenu.SetActive(false);
    }
}
