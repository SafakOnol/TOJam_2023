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

    [SerializeField] private int CargoBoxCountToCollect;
    [SerializeField] private int ValuablesBoxCountToCollect;

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
        objectiveText.text = $"Objective: {CargoBoxCountToCollect}";
        cargoBoxText.text = $"Cargo Boxes Secured: {cargoBoxCount} / {CargoBoxCountToCollect}";
        valuablesBoxText.text = $"Valuables Box Secured: {valuablesBoxCount} / {ValuablesBoxCountToCollect}";
    }

    public void IncrementCargoBoxCount()
    {
        cargoBoxCount++;
        cargoBoxText.text = $"Cargo Boxes Secured: {cargoBoxCount} / {CargoBoxCountToCollect}";
        if (cargoBoxCount == CargoBoxCountToCollect) OnAllCargoBoxesCollected?.Invoke();
    }

    public void IncrementValuablesBoxCount()
    {
        valuablesBoxCount++;
        valuablesBoxText.text = $"Valuables Box Secured: {valuablesBoxCount} / {ValuablesBoxCountToCollect}";
        if (valuablesBoxCount == ValuablesBoxCountToCollect) OnAllValuablesBoxCollected?.Invoke();
    }

}
