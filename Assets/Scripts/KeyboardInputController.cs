using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class KeyboardInputController : MonoBehaviour {

    #region Variables

    private static KeyboardInputController _instance;

    #endregion

    #region Properties

    public static KeyboardInputController Instance
    {
        get
        {
            if (_instance) return _instance;
            _instance = new GameObject("KeyboardInputController").AddComponent<KeyboardInputController>();
            DontDestroyOnLoad(_instance.gameObject);
            return _instance;
        }
    }

    #endregion

    #region UnityCalls


    #endregion

    #region Getters

    public Vector2 LeftStick(PlayerIndex playerIndex)
    {
        int indexToUse = GetCorrectIndex(playerIndex);

        Debug.Log("H: " + Input.GetAxis("HorizontalKeyboard" + indexToUse.ToString()));
        Debug.Log("V: " + Input.GetAxis("VerticalKeyboard" + indexToUse.ToString()));

        if (playerIndex == PlayerIndex.Three || playerIndex == PlayerIndex.Four)
            return new Vector2(Input.GetAxis("HorizontalKeyboard" + indexToUse.ToString()), Input.GetAxis("VerticalKeyboard" + indexToUse.ToString()));
        else
            return Vector2.zero;
    }

    public bool GetButton(PlayerIndex playerIndex, Ds4Button btn)
    {
        int indexToUse = GetCorrectIndex(playerIndex);

        switch (btn)
        {
            case Ds4Button.Cross:
                return (Input.GetButton("SkillOne" + indexToUse.ToString()));
            case Ds4Button.Circle:
                return (Input.GetButton("SkillTwo" + indexToUse.ToString()));
            case Ds4Button.Square:
                return (Input.GetButton("Shoot" + indexToUse.ToString()));

            default:
                break;
        }

        return false;
    }

    public bool GetButtonDown(PlayerIndex playerIndex, Ds4Button btn)
    {
        if (playerIndex == PlayerIndex.One || playerIndex == PlayerIndex.Two)
            return false;

        int indexToUse = GetCorrectIndex(playerIndex);

        switch (btn)
        {
            case Ds4Button.Cross:
                return (Input.GetButtonDown("SkillOne" + indexToUse.ToString()));
            case Ds4Button.Circle:
                return (Input.GetButtonDown("SkillTwo" + indexToUse.ToString()));
            case Ds4Button.Square:
                return (Input.GetButtonDown("Shoot" + indexToUse.ToString()));

            default:
                break;
        }
        return false;
    }

    public bool GetButtonUp(PlayerIndex playerIndex, Ds4Button btn)
    {
        int indexToUse = GetCorrectIndex(playerIndex);

        switch (btn)
        {
            case Ds4Button.Cross:
                return (Input.GetButtonUp("SkillOne" + indexToUse.ToString()));
            case Ds4Button.Circle:
                return (Input.GetButtonUp("SkillTwo" + indexToUse.ToString()));
            case Ds4Button.Square:
                return (Input.GetButtonUp("Shoot" + indexToUse.ToString()));

            default:
                break;
        }
        return false;
    }

    private int GetCorrectIndex(PlayerIndex playerIndex)
    {
        return ((int)playerIndex + 1);
    }

    #endregion
}
