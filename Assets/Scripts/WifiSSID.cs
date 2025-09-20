using UnityEngine;
using System.Diagnostics;
using TMPro;

public class WifiSSID : MonoBehaviour
{
    public TMP_Text ssidText;
    private string currentSSID = "";

    public UDPSend UDPSend;
    public UDPReceive UDPReceive;

    void Start()
    {
        InvokeRepeating(nameof(CheckWifiSSID), 0f, 2f);
    }

    void CheckWifiSSID()
    {
        string ssid = GetWifiSSID();
        if (ssid != currentSSID)
        {
            currentSSID = ssid;
            UnityEngine.Debug.Log("Wi-Fi Changed: " + ssid);

            if (ssidText != null)
            {
                ssidText.text = ssid;
            }
        }

        /*
        if (ssid != "hakorobo-box")
        {
            UnityEngine.Debug.Log("SSID is not hakorobo-box. Restarting Wi-Fi...");
        }
        */
    }

    string GetWifiSSID()
    {
        try
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "/bin/bash";
            psi.Arguments = "-c \"iwgetid -r\"";
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;

            Process process = Process.Start(psi);
            string output = process.StandardOutput.ReadToEnd().Trim();
            process.WaitForExit();

            return string.IsNullOrEmpty(output) ? "Not connected" : output;
        }
        catch (System.Exception e)
        {    
            UnityEngine.Debug.LogError("Error getting Wi-Fi SSID: " + e.Message);
            return "Error";
        }
    }
}
