using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool IsOpen = false;
    [SerializeField] float Speed = 1f;
    [Header("Sliding Configs")]
    [SerializeField] private Vector3 SlideDirection = Vector3.back;
    [SerializeField] private float SlideAmount = 1.9f;

    private Vector3 StartPosition;

    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        StartPosition = transform.position;
    }

    private void OnEnable()
    {
        GameManager.OnState_Level01_Special += GameManager_OnState_Level01_Special;
    }

    private void OnDisable()
    {
        GameManager.OnState_Level01_Special -= GameManager_OnState_Level01_Special;
    }

    private void GameManager_OnState_Level01_Special()
    {
        Open();
        //throw new System.NotImplementedException();
    }

    public void Open()
    {
        if (!IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }
            else
            {
                AnimationCoroutine = StartCoroutine(SlideOpen());
            }
            
        }
    }

    private IEnumerator SlideOpen()
    {
        Vector3 endPosition = StartPosition + SlideAmount * SlideDirection;
        Vector3 startPosition = transform.position;

        float time = 0;
        IsOpen = true;
        while (time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }

    public void Close()
    {
        if (IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            AnimationCoroutine = StartCoroutine(SlideClose());
        }
    }

    private IEnumerator SlideClose()
    {
        Vector3 endPosition = StartPosition;
        Vector3 startPosition = transform.position;
        float time = 0;

        IsOpen = false;

        while (time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }

}
