using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class CollectorManager : MonoBehaviour
{
    public TextMeshProUGUI cargoBoxText;
    public TextMeshProUGUI valuablesBoxText;
    public TextMeshProUGUI objectiveText;
    int cargoBoxCount = 0;
    int valuablesBoxCount = 0;

    [SerializeField] private int BoxCountToCollect;

    public static event Action OnAllCargoBoxesCollected;
    public static event Action OnAllValuablesBoxCollected;


    private void OnEnable()
    {
        CargoBox.OnCargoBoxSecured += IncrementCargoBoxCount;
        ValuablesBox.OnValuablesBoxCollected += IncrementValuablesBoxCount;
    }

    private void OnDisable()
    {
        CargoBox.OnCargoBoxSecured -= IncrementCargoBoxCount;
        ValuablesBox.OnValuablesBoxCollected -= IncrementValuablesBoxCount;
    }

    private void Awake()
    {
        objectiveText.text = $"Objective: {BoxCountToCollect}";
    }

    public void IncrementCargoBoxCount()
    {
        cargoBoxCount++;
        cargoBoxText.text = $"Cargo Boxes Secured: {cargoBoxCount}";
        if (cargoBoxCount == BoxCountToCollect)
        {
            OnAllCargoBoxesCollected?.Invoke();
        }
    }

    public void IncrementValuablesBoxCount()
    {
        valuablesBoxCount++;
        valuablesBoxText.text = $"Valuables Box Secured!";
        if (valuablesBoxCount == 1) OnAllValuablesBoxCollected?.Invoke();
    }
}
