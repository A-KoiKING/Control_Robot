using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using static UnityEngine.EventSystems.EventTrigger;
using TMPro;
using System.Linq;

public class InputControl : MonoBehaviour
{

    public GameInputs _gameInputs;

    [SerializeField] private Vector2 _LeftInputValue;
    [SerializeField] private Vector2 _RightInputValue;

    [SerializeField] private float _LeftOutputValueX;
    [SerializeField] private float _LeftOutputValueY;
    [SerializeField] private float _RightOutputValueX;
    [SerializeField] private float _RightOutputValueY;

    [SerializeField] private byte _buttonByte1 = 0;
    [SerializeField] private byte _buttonByte2 = 0;
    [SerializeField] private byte _buttonByte3 = 0;

    [SerializeField] private byte _LeftValueX;
    [SerializeField] private byte _LeftValueY;
    [SerializeField] private byte _RightValueX;
    [SerializeField] private byte _RightValueY; 
    [SerializeField] private byte _Up;
    [SerializeField] private byte _Left;
    [SerializeField] private byte _Down;
    [SerializeField] private byte _Right;
    [SerializeField] private byte _Triangle;
    [SerializeField] private byte _Square;
    [SerializeField] private byte _Cross;
    [SerializeField] private byte _Circle;
    [SerializeField] private byte _LSB;
    [SerializeField] private byte _RSB;
    [SerializeField] private byte _L1;
    [SerializeField] private byte _R1;
    [SerializeField] private byte _L2;
    [SerializeField] private byte _R2;

    public TextMeshProUGUI SendingMessage;
    public TextMeshProUGUI LastState;

    public string byteString;
    public string binaryString;
    public byte[] dataToSend;

    private byte[] _inputDataBytes;
    private const int DATA_SIZE = 7;

    // _buttonByte1
    private const byte BIT_UP = 0b00000001;       // 1
    private const byte BIT_LEFT = 0b00000010;     // 2
    private const byte BIT_DOWN = 0b00000100;     // 4
    private const byte BIT_RIGHT = 0b00001000;    // 8
    private const byte BIT_TRIANGLE = 0b00010000; // 16
    private const byte BIT_SQUARE = 0b00100000;   // 32
    private const byte BIT_CROSS = 0b01000000;    // 64
    private const byte BIT_CIRCLE = 0b10000000;   // 128

    // _buttonByte2
     // private const byte BIT_AUTO1 = 0b00000011;  // 3
    private const byte BIT_LSB = 0b00000100;      // 4
    private const byte BIT_RSB = 0b00001000;      // 8
    private const byte BIT_L1 = 0b00010000;       // 16
    private const byte BIT_R1 = 0b00100000;       // 32
    private const byte BIT_L2 = 0b01000000;       // 64
    private const byte BIT_R2 = 0b10000000;       // 128

    // _buttonByte2
     // private const byte BIT_AUTO2 = 0b11000000;  // 172

    // Start is called before the first frame update
    void Start()
    {
        _gameInputs = new GameInputs();

        // Action Event Entry
        _gameInputs.Control.LeftStick.started += OnLeftStick;
        _gameInputs.Control.LeftStick.performed += OnLeftStick;
        _gameInputs.Control.LeftStick.canceled += OnLeftStick;

        _gameInputs.Control.RightStick.started += OnRightStick;
        _gameInputs.Control.RightStick.performed += OnRightStick;
        _gameInputs.Control.RightStick.canceled += OnRightStick;

        _gameInputs.Control.Up.performed += OnUp;
        _gameInputs.Control.Up.canceled += EndUp;

        _gameInputs.Control.Left.performed += OnLeft;
        _gameInputs.Control.Left.canceled += EndLeft;

        _gameInputs.Control.Down.performed += OnDown;
        _gameInputs.Control.Down.canceled += EndDown;

        _gameInputs.Control.Right.performed += OnRight;
        _gameInputs.Control.Right.canceled += EndRight;

        _gameInputs.Control.Triangle.performed += OnTriangle;
        _gameInputs.Control.Triangle.canceled += EndTriangle;

        _gameInputs.Control.Square.performed += OnSquare;
        _gameInputs.Control.Square.canceled += EndSquare;

        _gameInputs.Control.Cross.performed += OnCross;
        _gameInputs.Control.Cross.canceled += EndCross;

        _gameInputs.Control.Circle.performed += OnCircle;
        _gameInputs.Control.Circle.canceled += EndCircle;

        _gameInputs.Control.LSB.performed += OnLSB;
        _gameInputs.Control.LSB.canceled += EndLSB;

        _gameInputs.Control.RSB.performed += OnRSB;
        _gameInputs.Control.RSB.canceled += EndRSB;

        _gameInputs.Control.L1.performed += OnL1;
        _gameInputs.Control.L1.canceled += EndL1;

        _gameInputs.Control.R1.performed += OnR1;
        _gameInputs.Control.R1.canceled += EndR1;

        _gameInputs.Control.L2.performed += OnL2;
        _gameInputs.Control.L2.canceled += EndL2;

        _gameInputs.Control.R2.performed += OnR2;
        _gameInputs.Control.R2.canceled += EndR2;

        _gameInputs.Control.Auto_1.performed += OnAuto1;
        _gameInputs.Control.Auto_1.canceled += EndAuto;

        _gameInputs.Control.Auto_2.performed += OnAuto2;
        _gameInputs.Control.Auto_2.canceled += EndAuto;

        _gameInputs.Control.Auto_3.performed += OnAuto3;
        _gameInputs.Control.Auto_3.canceled += EndAuto;

        _gameInputs.Control.Auto_4.performed += OnAuto4;
        _gameInputs.Control.Auto_4.canceled += EndAuto;

        _gameInputs.Control.Auto_5.performed += OnAuto5;
        _gameInputs.Control.Auto_5.canceled += EndAuto;

        _gameInputs.Control.Auto_6.performed += OnAuto6;
        _gameInputs.Control.Auto_6.canceled += EndAuto;

        _gameInputs.Control.Auto_7.performed += OnAuto7;
        _gameInputs.Control.Auto_7.canceled += EndAuto;

        _gameInputs.Control.Auto_8.performed += OnAuto8;
        _gameInputs.Control.Auto_8.canceled += EndAuto;

        _gameInputs.Control.Auto_9.performed += OnAuto9;
        _gameInputs.Control.Auto_9.canceled += EndAuto;

        _gameInputs.Control.Auto_10.performed += OnAuto10;
        _gameInputs.Control.Auto_10.canceled += EndAuto;

        _gameInputs.Control.Auto_11.performed += OnAuto11;
        _gameInputs.Control.Auto_11.canceled += EndAuto;

        // Input Action Enable
        _gameInputs.Enable();

        _inputDataBytes = new byte[DATA_SIZE];

        _LeftValueX = 128;
        _LeftValueY = 128;
        _RightValueX = 128;
        _RightValueY = 128;

        Debug.Log(_LeftValueX);
    }

    private void OnDestroy()
    {
        _gameInputs?.Dispose();
    }

    private void OnLeftStick(InputAction.CallbackContext context)
    {
        _LeftInputValue = context.ReadValue<Vector2>();
        _LeftOutputValueX = (_LeftInputValue.x + 1f) * 127.5f;
        _LeftValueX = (byte)Mathf.Clamp(Mathf.RoundToInt(_LeftOutputValueX), 0, 255);
        _LeftOutputValueY = (_LeftInputValue.y + 1f) * 127.5f;
        _LeftValueY = (byte)Mathf.Clamp(Mathf.RoundToInt(_LeftOutputValueY), 0, 255);
    }

    private void OnRightStick(InputAction.CallbackContext context)
    {
        _RightInputValue = context.ReadValue<Vector2>();
        _RightOutputValueX = (_RightInputValue.x + 1f) * 127.5f;
        _RightValueX = (byte)Mathf.Clamp(Mathf.RoundToInt(_RightOutputValueX), 0, 255);
        _RightOutputValueY = (_RightInputValue.y + 1f) * 127.5f;
        _RightValueY = (byte)Mathf.Clamp(Mathf.RoundToInt(_RightOutputValueY), 0, 255);
    }

    private void OnUp(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte1, BIT_UP, true);
    }

    private void EndUp(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte1, BIT_UP, false);
    }

    private void OnLeft(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte1, BIT_LEFT, true);
    }

    private void EndLeft(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte1, BIT_LEFT, false);
    }

    private void OnDown(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte1, BIT_DOWN, true);
    }

    private void EndDown(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte1, BIT_DOWN, false);
    }

    private void OnRight(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte1, BIT_RIGHT, true);
    }

    private void EndRight(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte1, BIT_RIGHT, false);
    }

    private void OnTriangle(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte1, BIT_TRIANGLE, true);
    }

    private void EndTriangle(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte1, BIT_TRIANGLE, false);
    }

    private void OnSquare(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte1, BIT_SQUARE, true);
    }

    private void EndSquare(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte1, BIT_SQUARE, false);
    }

    private void OnCross(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte1, BIT_CROSS, true);
    }

    private void EndCross(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte1, BIT_CROSS, false);
    }

    private void OnCircle(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte1, BIT_CIRCLE, true);
    }

    private void EndCircle(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte1, BIT_CIRCLE, false);
    }

    private void OnLSB(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte2, BIT_LSB, true);
    }

    private void EndLSB(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte2, BIT_LSB, false);
    }

    private void OnRSB(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte2, BIT_RSB, true);
    }

    private void EndRSB(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte2, BIT_RSB, false);
    }

    private void OnL1(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte2, BIT_L1, true);
    }

    private void EndL1(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte2, BIT_L1, false);
    }

    private void OnR1(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte2, BIT_R1, true);
    }

    private void EndR1(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte2, BIT_R1, false);
    }

    private void OnL2(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte2, BIT_L2, true);
    }

    private void EndL2(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte2, BIT_L2, false);
    }

    private void OnR2(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte2, BIT_R2, true);
    }

    private void EndR2(InputAction.CallbackContext context)
    {
        SetButtonBit(ref _buttonByte2, BIT_R2, false);
    }

    private void OnAuto1(InputAction.CallbackContext context)
    {
        ResetAuto();
        _buttonByte3 |= 0b01000000;
        LastState.text = "1";
    }

    private void OnAuto2(InputAction.CallbackContext context)
    {
        ResetAuto();
        _buttonByte3 |= 0b10000000;
        LastState.text = "2";
    }

    private void OnAuto3(InputAction.CallbackContext context)
    {
        ResetAuto();
        _buttonByte3 |= 0b11000000;
        LastState.text = "3";
    }

    private void OnAuto4(InputAction.CallbackContext context)
    {
        ResetAuto();
        _buttonByte2 |= 0b00000001;
        LastState.text = "4";
    }

    private void OnAuto5(InputAction.CallbackContext context)
    {
        ResetAuto();
        _buttonByte2 |= 0b00000001;
        _buttonByte3 |= 0b01000000;
        LastState.text = "5";
    }

    private void OnAuto6(InputAction.CallbackContext context)
    {
        ResetAuto();
        _buttonByte2 |= 0b00000001;
        _buttonByte3 |= 0b10000000;
        LastState.text = "6";
    }

    private void OnAuto7(InputAction.CallbackContext context)
    {
        ResetAuto();
        _buttonByte2 |= 0b00000001;
        _buttonByte3 |= 0b11000000;
        LastState.text = "7";
    }

    private void OnAuto8(InputAction.CallbackContext context)
    {
        ResetAuto();
        _buttonByte2 |= 0b00000010;
        _buttonByte3 |= 0b00000000;
        LastState.text = "8";
    }

    private void OnAuto9(InputAction.CallbackContext context)
    {
        ResetAuto();
        _buttonByte2 |= 0b00000010;
        _buttonByte3 |= 0b01000000;
        LastState.text = "9";
    }

    private void OnAuto10(InputAction.CallbackContext context)
    {
        ResetAuto();
        _buttonByte2 |= 0b00000010;
        _buttonByte3 |= 0b10000000;
        LastState.text = "10";
    }

    private void OnAuto11(InputAction.CallbackContext context)
    {
        ResetAuto();
        _buttonByte2 |= 0b00000010;
        _buttonByte3 |= 0b11000000;
        LastState.text = "11";
    }

    private void EndAuto(InputAction.CallbackContext context)
    {
        _buttonByte2 &= 0b11111100;
        _buttonByte3 &= 0b00111111;
    }

    private void ResetAuto()
    {
        _buttonByte2 &= 0b11111100;
        _buttonByte3 &= 0b00111111;
    }

    private void SetButtonBit(ref byte targetByte, byte bitMask, bool setState)
    {
        if (setState)
        {
            targetByte |= bitMask; // Bit Set (OR)
        }
        else
        {
            targetByte &= (byte)~bitMask; // Bit Clear (AND NOT)
        }
    }

    public byte[] GetPackedInputData()
    {
        _inputDataBytes[0] = _LeftValueX;
        _inputDataBytes[1] = _LeftValueY;
        _inputDataBytes[2] = _RightValueX;
        _inputDataBytes[3] = _RightValueY;

        _inputDataBytes[4] = _buttonByte1;
        _inputDataBytes[5] = _buttonByte2;
        _inputDataBytes[6] = _buttonByte3;

        return _inputDataBytes;
    }

    // Update is called once per frame
    void Update()
    {
        dataToSend = GetPackedInputData();

        byteString = BitConverter.ToString(dataToSend).Replace("-", "");
        binaryString = string.Join(" ",
            dataToSend.Select(b => Convert.ToString(b, 2).PadLeft(8, '0'))
        );

        SendingMessage.text = byteString + "\n" + binaryString;
    }
}
