using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;
using UniRx;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using System;

public class UDPReceive : MonoBehaviour
{
    private int R_port;
    public TextMeshProUGUI R_PortNumber;
    public TMP_InputField R_Port_InputField;

    public string byteString;
    public string binaryString;

    public string Condition;
    public string ConversionCondition;

    public int M1;
    public int M2;
    public int M3;
    public int M4;

    private UdpClient udpClient;

    void Start()
    {
        R_port = PlayerPrefs.GetInt("R_PORT", 64276);
        R_Port_InputField.text = R_port.ToString();
        R_PortNumber.text = R_port.ToString();

        udpClient = new UdpClient(R_port);
        udpClient.BeginReceive(OnReceived, udpClient);
    }

    private void OnReceived(System.IAsyncResult result)
    {
        try
        {
            UdpClient getUdp = (UdpClient)result.AsyncState;
            IPEndPoint ipEnd = null;

            // UDP Receive
            byte[] getByte = getUdp.EndReceive(result, ref ipEnd);

            byteString = BitConverter.ToString(getByte).Replace("-", "");
            binaryString = string.Join(" ",
                getByte.Select(b => Convert.ToString(b, 2).PadLeft(8, '0'))
            );

            if (getByte.Length >= 4)
            {
                byte X = getByte[0];
                byte Y = getByte[1];
                byte R = getByte[2];
                byte mFlags = getByte[3];

                M1 = (mFlags >> 7) & 1;
                M2 = (mFlags >> 6) & 1;
                M3 = (mFlags >> 5) & 1;
                M4 = (mFlags >> 4) & 1;

                Condition = $"X={X}, Y={Y}, R={R}";

                int T_X = (int)(X * 41.17647);
                int T_Y = (int)(Y * 41.17647);
                int T_R = (int)(X * 1.41176);

                ConversionCondition = $"X={T_X}, Y={T_Y}, R={T_R}";
            }
            else
            {
                Debug.LogWarning("Packet too short, ignoring.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"UDP Receive Error: {ex}");
        }
        finally
        {
            // 例外が起きても必ず次を受信
            UdpClient getUdp = (UdpClient)result.AsyncState;
            getUdp.BeginReceive(OnReceived, getUdp);
        }
    }


    private void OnDestroy()
    {
        udpClient.Close();
    }

    public void SetText()
    {
        if (udpClient != null)
        {
            udpClient.Close();
            udpClient = null;
        }

        if (R_Port_InputField.text == "" || int.Parse(R_Port_InputField.text) >= 65536 || int.Parse(R_Port_InputField.text) <= -1)
        {
            R_Port_InputField.text = R_port.ToString();
        }

        R_port = int.Parse(R_Port_InputField.text);
        R_Port_InputField.text = R_port.ToString();
        R_PortNumber.text = R_port.ToString();

        PlayerPrefs.SetInt("R_PORT", R_port);
        PlayerPrefs.Save();

        udpClient = new UdpClient(R_port);
        udpClient.BeginReceive(OnReceived, udpClient);
    }
}