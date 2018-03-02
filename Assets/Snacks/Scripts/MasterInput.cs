﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class MasterInput : MonoBehaviour
{

    #region Variables
    private static MasterInput _instance;
    private JoystickInputController _joystickInstance;
    private KeyboardInputController _keyboardInstance;
    #endregion

    #region Properties
    public static MasterInput Instance
    {
        get
        {
            if (_instance) return _instance;
            _instance = new GameObject("MasterInputController").AddComponent<MasterInput>();
            DontDestroyOnLoad(_instance.gameObject);
            return _instance;
        }
    }
    #endregion

    #region UnityCalls
    // Use this for initialization
    void Start()
    {
        _joystickInstance = JoystickInputController.Instance;
        _keyboardInstance = KeyboardInputController.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Getters

    public Vector2 LeftStick(PlayerIndex playerIndex)
    {
        if (playerIndex == PlayerIndex.Two || playerIndex == PlayerIndex.One)
            return _joystickInstance.LeftStick(playerIndex);
        else
            return (_joystickInstance.LeftStick(playerIndex).magnitude > _keyboardInstance.LeftStick(playerIndex).magnitude) ? _joystickInstance.LeftStick(playerIndex) : _keyboardInstance.LeftStick(playerIndex);

    }

    public bool GetButtonDown(PlayerIndex playerIndex, Ds4Button btn)
    {
        if (playerIndex == PlayerIndex.Two || playerIndex == PlayerIndex.One)
            return _joystickInstance.GetButtonDown(playerIndex, btn);
        else
            return _joystickInstance.GetButtonDown(playerIndex, btn) || _keyboardInstance.GetButtonDown(playerIndex, btn);
    }

    public bool GetButton(PlayerIndex playerIndex, Ds4Button btn)
    {
        if (playerIndex == PlayerIndex.Two || playerIndex == PlayerIndex.One)
            return _joystickInstance.GetButton(playerIndex, btn);
        else
            return _joystickInstance.GetButton(playerIndex, btn) || _keyboardInstance.GetButton(playerIndex, btn);
    }

    public bool GetButtonUp(PlayerIndex playerIndex, Ds4Button btn)
    {
        if (playerIndex == PlayerIndex.Two || playerIndex == PlayerIndex.One)
            return _joystickInstance.GetButtonUp(playerIndex, btn);
        else
            return _joystickInstance.GetButtonUp(playerIndex, btn) || _keyboardInstance.GetButtonUp(playerIndex, btn);
    }

    #endregion
}