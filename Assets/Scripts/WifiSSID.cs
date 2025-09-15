using UnityEngine;
using System.Diagnostics;
using TMPro;

public class WifiSSID : MonoBehaviour
{
    public TMP_Text ssidText;
    private string currentSSID = "";

    void Start()
    {
        InvokeRepeating(nameof(CheckWifiSSID), 0f, 4f);
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

        if (ssid != "hakorobo-box")
        {
            UnityEngine.Debug.Log("SSID is not hakorobo-box. Restarting Wi-Fi...");
            RestartWifi();
        }
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

    void RestartWifi()
    {
        try
        {
            RunCommand("nmcli radio wifi off");
            System.Threading.Thread.Sleep(2000); // 2秒待つ
            RunCommand("nmcli radio wifi on");
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError("Error restarting Wi-Fi: " + e.Message);
        }
    }

    void RunCommand(string command)
    {
        ProcessStartInfo psi = new ProcessStartInfo();
        psi.FileName = "/bin/bash";
        psi.Arguments = "-c \"" + command + "\"";
        psi.RedirectStandardOutput = true;
        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;

        Process process = Process.Start(psi);
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        UnityEngine.Debug.Log("Command output: " + output);
    }
}
