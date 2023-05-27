using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    public void OnClick_ReturnToMainMenu()
    {
        MenuManager.OpenMenu(Menu.MAINMENU, gameObject);
    }
}
