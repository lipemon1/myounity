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
        return new Vector2(Input.GetAxis("HorizontalKeyboard" + ((int)playerIndex).ToString()), Input.GetAxis("VerticalKeyboard" + ((int)playerIndex).ToString()));
    }

    public bool GetButton(PlayerIndex playerIndex, Ds4Button btn)
    {
        switch (btn)
        {
            case Ds4Button.Cross:
                return (Input.GetButton("SkillOne" + ((int)playerIndex).ToString()));
            case Ds4Button.Circle:
                return (Input.GetButton("SkillTwo" + ((int)playerIndex).ToString()));
            case Ds4Button.Square:
                return (Input.GetButton("Shoot" + ((int)playerIndex).ToString()));

            default:
                break;
        }

        return false;
    }

    public bool GetButtonDown(PlayerIndex playerIndex, Ds4Button btn)
    {
        switch (btn)
        {
            case Ds4Button.Cross:
                return (Input.GetButtonDown("SkillOne" + ((int)playerIndex).ToString()));
            case Ds4Button.Circle:
                return (Input.GetButtonDown("SkillTwo" + ((int)playerIndex).ToString()));
            case Ds4Button.Square:
                return (Input.GetButtonDown("Shoot" + ((int)playerIndex).ToString()));

            default:
                break;
        }
        return false;
    }

    public bool GetButtonUp(PlayerIndex playerIndex, Ds4Button btn)
    {
        switch (btn)
        {
            case Ds4Button.Cross:
                return (Input.GetButtonUp("SkillOne" + ((int)playerIndex).ToString()));
            case Ds4Button.Circle:
                return (Input.GetButtonUp("SkillTwo" + ((int)playerIndex).ToString()));
            case Ds4Button.Square:
                return (Input.GetButtonUp("Shoot" + ((int)playerIndex).ToString()));

            default:
                break;
        }
        return false;
    }

    #endregion
}
