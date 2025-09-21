using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SerialSend : MonoBehaviour
{
    public SerialHandler serialHandler;

    public InputControl inputControl;

    void Start()
    {
        Time.fixedDeltaTime = 0.02f; // 20ms
    }

    void FixedUpdate()
    {
        string message = "s" + BitConverter.ToString(inputControl.dataToSend).Replace("-", "") + "\n";
        serialHandler.Write(message);
        //Debug.Log(message);
    }
}