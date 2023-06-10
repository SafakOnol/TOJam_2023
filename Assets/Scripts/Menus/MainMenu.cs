using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        FindObjectOfType<GameManager>().SetGameState(GameState.MainMenu);
    }
    public void OnClick_StartGame()
    {
        FindObjectOfType<GameManager>().LoadScene("GameScene01");
        //Debug.Log("GameScene01!");
    }

    public void OnClick_ExitGame()
    {
        Application.Quit();
    }

    public void OnClick_SettingsMenu()
    {
        MenuManager.OpenMenu(Menu.SETTINGS, gameObject);
    }

    public void OnClick_Credits()
    {
        MenuManager.OpenMenu(Menu.CREDITS, gameObject);
    }
}
