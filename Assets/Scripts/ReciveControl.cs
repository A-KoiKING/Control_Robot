using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReciveControl : MonoBehaviour
{
    public UDPReceive UDPReceive;

    public TextMeshProUGUI C_Message;
    public TextMeshProUGUI C_T_Message;
    public TextMeshProUGUI R_Message;

    public Image M1;
    public Image M2;
    public Image M3;
    public Image M4;

    public Sprite death;
    public Sprite live;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        C_Message.text = UDPReceive.Condition;
        C_T_Message.text = UDPReceive.ConversionCondition;
        R_Message.text = UDPReceive.byteString + "\n" + UDPReceive.binaryString;

        if (UDPReceive.M1 == 1)
        {
            M1.sprite = live;
        }
        else
        {
            M1.sprite = death;
        }

        if (UDPReceive.M2 == 1)
        {
            M2.sprite = live;
        }
        else
        {
            M2.sprite = death;
        }

        if (UDPReceive.M3 == 1)
        {
            M3.sprite = live;
        }
        else
        {
            M3.sprite = death;
        }

        if (UDPReceive.M4 == 1)
        {
            M4.sprite = live;
        }
        else
        {
            M4.sprite = death;
        }
    }
}
