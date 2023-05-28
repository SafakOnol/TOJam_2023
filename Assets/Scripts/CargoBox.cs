using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using TMPro;

public class CargoBox : MonoBehaviour, ICollectible
{
    public static event Action OnCargoBoxSecured;
    public int damageCounter;   // damagecounter can be used on UI
    public string condition = "Good";
    public bool pickedUp = false;
    public bool vulnerability = false;
	public TextMeshProUGUI BoxDamageText;
	public TextMeshProUGUI VulnText;


	public void Collect()
    {
        //throw new System.NotImplementedException();
        UnityEngine.Debug.Log("Box Secured!");
        Destroy(gameObject);
        OnCargoBoxSecured?.Invoke();
    }

	private void OnCollisionEnter(UnityEngine.Collision collision)
	{
        if(pickedUp && vulnerability)
        {
			damageCounter++;
			// box damage code goes here
			if (damageCounter == 1) { condition = "Cracked"; }
			else if (damageCounter == 2) { condition = "Damaged"; }
		}

		BoxDamageText.text = condition;
	}

    private void DamageToBox()
    {
        vulnerability = true;
		VulnText.text = vulnerability.ToString();

	}

}
