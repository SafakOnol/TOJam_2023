using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public void OnClick_ReturnToMainMenu()
    {
        MenuManager.OpenMenu(Menu.MAINMENU, gameObject);
    }
}
