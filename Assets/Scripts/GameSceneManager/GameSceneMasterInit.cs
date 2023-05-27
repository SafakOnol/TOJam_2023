using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneMasterInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<GameSceneMaster>().LoadScene("MenuScene");
    }

}
