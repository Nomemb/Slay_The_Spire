using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public event Action playerTurnEvent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlayerTurnEvent()
    {
        playerTurnEvent?.Invoke();
    }
}
