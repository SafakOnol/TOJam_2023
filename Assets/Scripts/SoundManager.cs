using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Debug
    public string TestSoundManager()
    {
        return "Sound Manager is active!";
    }

    public static SoundManager Instance;
    AudioSource audioSource;

    [SerializeField] AudioClip boxPickedUp, boxSecured, boxDropped, boxDamaged, boxDestroyed;

    private void OnEnable()
    {
        CargoBox.OnCargoBoxSecured += PlayBoxSecuredSound;
    }

    private void OnDisable()
    {
        CargoBox.OnCargoBoxSecured -= PlayBoxSecuredSound;
    }

    private void Awake()
    {
        //
    }

    // Collectable Audio
    public void PlayBoxSecuredSound()
    {
        Debug.Log("PlayBoxSecuredSound played!");
        //PlayAudioClip(boxSecured);
    }
}


