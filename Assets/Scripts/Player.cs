﻿using System.Collections;
using System.Linq;
using UnityEngine;
using XInputDotNetPure;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public PlayerIndex Index = PlayerIndex.One;

    [Header("Movimentation Settings")] public float MovSpeed = 10;

    [Header("Rotation Settings")] public float RotSlerpSpeed = 12;


    [Header("Dash Settings")]
    public AnimationCurve DashAnimationCurve;
    public float DashCooldown { get { return DashAnimationCurve.keys.Last().time + 0.5f; } }
    private bool _canDash = true;

    [Header("States")] 
    public bool IsIdle;
    public bool IsRunning;
    public bool IsDashing;
    public bool IsAiming;
    
    //Input
    private JoystickInputController _joyInputController;

    #region Unity Calls

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _joyInputController = JoystickInputController.Instance;
    }

    private void Update()
    {
        HandleMovement(_joyInputController.LeftStick(Index));
        HandleRotation(_joyInputController.LeftStick(Index));
        HandleDash();
    }

    #endregion

    private void HandleMovement(Vector2 stick)
    {
        transform.Translate(new Vector3(stick.x, 0, stick.y) * RotSlerpSpeed * Time.deltaTime, Space.World);
    }

    private void HandleRotation(Vector2 stick)
    {
        transform.forward = Vector3.Slerp(transform.forward, new Vector3(stick.x, 0, stick.y), Time.deltaTime * RotSlerpSpeed);
    }

    private void HandleDash()
    {
        if (_joyInputController.GetButtonDown(Index, Ds4Button.Cross) && _canDash)
        {
            StopAllCoroutines();
            StartCoroutine(DashCo());
            _canDash = false;
            IsDashing = true;
            Invoke("ResetCanDash", DashCooldown);
        }
    }

    private void ResetCanDash()
    {
        _canDash = true;
    }

    private IEnumerator DashCo()
    {
        float timer = 0;
        Debug.Log(DashAnimationCurve.keys.Last().time);
        while (timer < DashAnimationCurve.keys.Last().time)
        {
            float value = DashAnimationCurve.Evaluate(timer);
            transform.Translate(transform.forward * value, Space.World);
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        IsDashing = false;
    }
}