using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class SerialHandler : MonoBehaviour
{
    public string portName = "/dev/ttyACM0";
    public int baudRate    = 115200;

    private SerialPort serialPort_;

    void Awake()
    {
        Open();
    }

    void OnDestroy()
    {
        Close();
    }

    private void Open()
    {
        serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
        serialPort_.Open();
    }

    private void Close()
    {
        if (serialPort_ != null && serialPort_.IsOpen) {
            serialPort_.Close();
            serialPort_.Dispose();
        }
    }

    public void Write(string message)
    {
        try
        {
            if (serialPort_ != null && serialPort_.IsOpen)
            {
                serialPort_.Write(message);
            }
            else
            {
                Debug.LogWarning("Serial port not open.");
                Open();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Serial Write Error: " + e.Message);
            Open();
        }
    }
}
