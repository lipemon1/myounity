﻿using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using XInputDotNetPure;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public PlayerIndex Index = PlayerIndex.One;

    [Header("Debug")]
    [SerializeField] private bool _playerCanControl;

    [Header("Class Base")]
    public PlayerClassCreatorScriptable Class;

    [Header("Rendering Settings")]
    public Renderer[] ColoredRenderers;

    [Header("Dash Settings")]
    public AnimationCurve DashAnimationCurve;
    [SerializeField] private ParticleSystem _dashParticle;

    [Header("Animator Controller")]
    private PlayerAnimController _playerAnimController;

    public float DashCooldown
    {
        get { return DashAnimationCurve.keys.Last().time + 0.5f; }
    }

    private bool _canDash = true;

    [Header("States")]
    public bool IsIdle;
    public bool IsRunning;
    public bool IsDashing;
    public bool IsAiming;

    [Header("Aim Settings")]
    public float AimDuration;
    public LineRenderer Aim;

    [Header("Energy Handler")]
    [HideInInspector] private EnergyHandler _energyHandler;

    //Input
    //private JoystickInputController _joyInputController;
    private MasterInput _masterInput;
    private CharacterController _characterController;

    #region Unity Calls

    private void Awake()
    {
        Instance = this;
        _energyHandler = GetComponent<EnergyHandler>();
        _playerAnimController = GetComponent<PlayerAnimController>();
    }

    private void Start()
    {
        //_joyInputController = JoystickInputController.Instance;
        _masterInput = MasterInput.Instance;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_playerCanControl)
        {
            if (!IsDashing && !IsAiming) HandleMovement(_masterInput.LeftStick(Index));
            if (!IsDashing) HandleRotation(_masterInput.LeftStick(Index));
            if (!IsAiming) HandleDash();
            HandleAim();
        }
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z); //freezing Y position
    }

    #endregion

    private void HandleMovement(Vector2 stick)
    {
        _characterController.Move(new Vector3(stick.x, 0, stick.y) * Class.BaseInfo.MoveSpeed * Time.deltaTime);
        _playerAnimController.UpdateMoveAnimations(stick.magnitude);
    }

    private void HandleRotation(Vector2 stick)
    {
        if (stick.magnitude > 0.1f)
            transform.forward = Vector3.Lerp(transform.forward, new Vector3(stick.x, 0, stick.y), Time.deltaTime * Class.BaseInfo.RotateSlerpSpeed);
        //transform.forward = Vector3.Slerp(transform.forward, new Vector3(stick.x, 0, stick.y), Time.deltaTime * RotSlerpSpeed);
    }

    private void HandleAim()
    {
        if(Global.Player[(int)Index].FireController.GetCanShoot())
        {
            if (_masterInput.GetButtonDown(Index, Ds4Button.Square))
            {
                AimDuration = 0;
                IsAiming = true;
            }
            if (_masterInput.GetButton(Index, Ds4Button.Square))
            {
                AimDuration += Time.deltaTime;
                Aim.SetPositions(new[] { Vector3.zero, new Vector3(0, 0, AimDuration) });

                _energyHandler.SetHoldingShot(true, AimDuration);
                IsAiming = false;
                _energyHandler.TryToShoot();
            }
            else if (_masterInput.GetButtonUp(Index, Ds4Button.Square))
            {
                Aim.SetPositions(new[] { Vector3.zero, Vector3.zero });
                IsAiming = false;

                _energyHandler.TryToShoot();
                AimDuration = 0;
            }
        }
        
    }

    private void HandleDash()
    {
        if (_masterInput.GetButtonDown(Index, Ds4Button.Cross) && _canDash)
        {
            StopAllCoroutines();
            StartCoroutine(DashCo());
            _canDash = false;
            IsDashing = true;
            Invoke("ResetCanDash", DashCooldown);
            _dashParticle.Clear();
            _dashParticle.Play();
            SoundManager.Instance.PlaySomeAudio("Dash");
        }
    }

    private void ResetCanDash()
    {
        _canDash = true;
    }

    private IEnumerator DashCo()
    {
        NavMeshAgent _agent = GetComponent<NavMeshAgent>();
        _agent.enabled = false;
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
        _agent.enabled = true;
    }

    public void EnablePlayerControl()
    {
        _playerCanControl = true;
        Global.Player[(int)Index].FireController.EnableShoot();
    }

    public void DisablePlayerControl()
    {
        _playerCanControl = false;
    }

    public void ResetPlayer()
    {
        IsIdle = true;
        IsRunning = false;
        IsDashing = false;
        IsAiming = false;
        AimDuration = 0f;
    }
}