using UnityEngine;
using System.Net.Sockets;
using System.Text;
using TMPro;
using UnityEngine.InputSystem;

public class UDPSend : MonoBehaviour
{
    private string host;
    private int IP1;
    private int IP2;
    private int IP3;
    private int IP4;
    private int port;
    private UdpClient client;

    public TextMeshProUGUI IPNumber;
    public TextMeshProUGUI PortNumber;
    public TMP_InputField IP_1_InputField;
    public TMP_InputField IP_2_InputField;
    public TMP_InputField IP_3_InputField;
    public TMP_InputField IP_4_InputField;
    public TMP_InputField Port_InputField;

    public InputControl inputControl;

    void Start()
    {
        IP1 = PlayerPrefs.GetInt("IP_1", 192);
        IP2 = PlayerPrefs.GetInt("IP_2", 168);
        IP3 = PlayerPrefs.GetInt("IP_3", 10);
        IP4 = PlayerPrefs.GetInt("IP_4", 123);
        port = PlayerPrefs.GetInt("PORT", 64222);
        host = IP1 + "." + IP2 + "." + IP3 + "." + IP4;
        IP_1_InputField.text = IP1.ToString();
        IP_2_InputField.text = IP2.ToString();
        IP_3_InputField.text = IP3.ToString();
        IP_4_InputField.text = IP4.ToString();
        IPNumber.text = host;
        Port_InputField.text = port.ToString();
        PortNumber.text = port.ToString();

        OnConnect();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            if (IP_1_InputField.isFocused)
            {
                Port_InputField.ActivateInputField();
            }
            if (IP_2_InputField.isFocused)
            {
                IP_1_InputField.ActivateInputField();
            }
            if (IP_3_InputField.isFocused)
            {
                IP_2_InputField.ActivateInputField();
            }
            if (IP_4_InputField.isFocused)
            {
                IP_3_InputField.ActivateInputField();
            }
            if (Port_InputField.isFocused)
            {
                IP_4_InputField.ActivateInputField();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (IP_1_InputField.isFocused)
            {
                IP_2_InputField.ActivateInputField();
            }
            if (IP_2_InputField.isFocused)
            {
                IP_3_InputField.ActivateInputField();
            }
            if (IP_3_InputField.isFocused)
            {
                IP_4_InputField.ActivateInputField();
            }
            if (IP_4_InputField.isFocused)
            {
                Port_InputField.ActivateInputField();
            }
            if (Port_InputField.isFocused)
            {
                IP_1_InputField.ActivateInputField();
            }
        }

        Send();
    }

    public void Send()
    {
        client.Send(inputControl.dataToSend, inputControl.dataToSend.Length);
    }

    public void OnConnect()
    {
        client = new UdpClient();
        client.Connect(host, port);
        Debug.Log(host + " : " + port);
    }

    private void OnDestroy()
    {
        client.Close();
    }

    public void SetText()
    {
        if (IP_1_InputField.text == "" || int.Parse(IP_1_InputField.text) >= 256 || int.Parse(IP_1_InputField.text) <= -1)
        {
            IP_1_InputField.text = IP1.ToString();
        }
        if (IP_2_InputField.text == "" || int.Parse(IP_2_InputField.text) >= 256 || int.Parse(IP_2_InputField.text) <= -1)
        {
            IP_2_InputField.text = IP2.ToString();
        }
        if (IP_3_InputField.text == "" || int.Parse(IP_3_InputField.text) >= 256 || int.Parse(IP_3_InputField.text) <= -1)
        {
            IP_3_InputField.text = IP3.ToString();
        }
        if (IP_4_InputField.text == "" || int.Parse(IP_4_InputField.text) >= 256 || int.Parse(IP_4_InputField.text) <= -1)
        {
            IP_4_InputField.text = IP4.ToString();
        }
        if (Port_InputField.text == "" || int.Parse(Port_InputField.text) >= 65536 || int.Parse(Port_InputField.text) <= 0)
        {
            Port_InputField.text = port.ToString();
        }

        IP1 = int.Parse(IP_1_InputField.text);
        IP2 = int.Parse(IP_2_InputField.text);
        IP3 = int.Parse(IP_3_InputField.text);
        IP4 = int.Parse(IP_4_InputField.text);
        host = IP1 + "." + IP2 + "." + IP3 + "." + IP4;
        IPNumber.text = host;
        port = int.Parse(Port_InputField.text);
        PortNumber.text = port.ToString();

        PlayerPrefs.SetInt("IP_1", IP1);
        PlayerPrefs.SetInt("IP_2", IP2);
        PlayerPrefs.SetInt("IP_3", IP3);
        PlayerPrefs.SetInt("IP_4", IP4);
        PlayerPrefs.SetInt("PORT", port);
        PlayerPrefs.Save();

        OnConnect();
    }
}