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

    public Image FL;
    public Image FR;
    public Image RL;
    public Image RR;

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

        if (UDPReceive.FL == 1)
        {
            FL.sprite = live;
        }
        else
        {
            FL.sprite = death;
        }

        if (UDPReceive.FR == 1)
        {
            FR.sprite = live;
        }
        else
        {
            FR.sprite = death;
        }

        if (UDPReceive.RL == 1)
        {
            RL.sprite = live;
        }
        else
        {
            RL.sprite = death;
        }

        if (UDPReceive.RR == 1)
        {
            RR.sprite = live;
        }
        else
        {
            RR.sprite = death;
        }
    }
}
