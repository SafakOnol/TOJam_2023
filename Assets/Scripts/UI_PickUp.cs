using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class UI_PickUp : MonoBehaviour
{
    public static bool isPickedUp { get; private set; }
    public static bool isDropped { get; private set; }
    public static GameObject dummyBox, cargoBox, valuablesBox;
    public static void Init()
    {
        GameObject canvas = GameObject.Find("PickUpUI");
        dummyBox = canvas.transform.Find("DummyBox").gameObject;
        cargoBox = canvas.transform.Find("CargoBox").gameObject; ;
        valuablesBox = canvas.transform.Find("ValuablesBox").gameObject; ;

        isPickedUp = true;
        isDropped = false;
    }

    public static void DisplayPickUpBox(PickUpItems pickedUpItem, GameObject displayingCanvas)
    {
        if (!isPickedUp) Init();

        switch (pickedUpItem)
        {
            case PickUpItems.DUMMYBOX:
                dummyBox.SetActive(true);
                Debug.Log("DummyBoxPickedUIActive");
                break;
            case PickUpItems.CARGOBOX:
                cargoBox.SetActive(true);
                Debug.Log("CargoBoxPickedUIActive");
                break;
            case PickUpItems.VALUABLESBOX:
                valuablesBox.SetActive(true);
                Debug.Log("ValuablesBoxPickedUIActive");
                break;
            default: break;
        }
        //displayingCanvas.SetActive(false);
    }

    public static void StopDisplay(PickUpItems droppedItem, GameObject displayingCanvas)
    {
        isDropped = true;
        switch (droppedItem) 
        {
            case PickUpItems.DUMMYBOX:
                dummyBox.SetActive(false);
                break;
            case PickUpItems.CARGOBOX:
                cargoBox.SetActive(false);
                break;
            case PickUpItems.VALUABLESBOX:
                valuablesBox.SetActive(false);
                break;
            default: break;
        }
    }


}
