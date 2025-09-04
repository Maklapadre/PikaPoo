using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Runtime Info:")]
    [SerializeField] private Vector2 _moveValue = Vector2.zero;
    [SerializeField] private Vector2 _lookValue = Vector2.zero;
    [SerializeField] private float _basicAttackValue = 0f;
    [SerializeField] private float _heavyAttackValue = 0f;
    [SerializeField] private float _interactValue = 0f;
    [SerializeField] private float _crouchValue = 0f;
    [SerializeField] private float _jumpValue = 0f;
    [SerializeField] private float _sprintValue = 0f;

    public static PlayerInputManager Instance;
    private PlayerInput _playerInput = null;

    // Public accessors
    public Vector2 MoveValue => _moveValue;
    public Vector2 LookValue => _lookValue;
    public float BasicAttackValue => _basicAttackValue;
    public float HeavyAttackValue => _heavyAttackValue;
    public float InteractValue => _interactValue;
    public float CrouchValue => _crouchValue;
    public float JumpValue => _jumpValue;
    public float SprintValue => _sprintValue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.activeSceneChanged += OnSceneChange;

        Instance.enabled = true; // Disable player input until we are in the world scene
    }

    private void OnSceneChange(Scene oldScene, Scene newScene)
    {
        if (newScene.buildIndex == WorldSaveGameManager.Instance.GetWorldSceneIndex()) // If we are loading in the world scene, enable player input
        {
            Instance.enabled = true;
        }
        else
        {
            Instance.enabled = false; // Disable player input in other scenes (which should be menu scenes)
        }
    }

    private void OnEnable()
    {
        EnableInputActionAsset();
        SubscribeToInputEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromInputEvents();
        DisableInputActionAsset();
    }

    private void EnableInputActionAsset()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
    }

    private void DisableInputActionAsset()
    {
        _playerInput.Disable();
    }

    private void SubscribeToInputEvents()
    {
        _playerInput.Player.Move.performed += OnMovePerformed;
        _playerInput.Player.Move.canceled += OnMoveCanceled;

        _playerInput.Player.Look.performed += OnLookPerformed;
        _playerInput.Player.Look.canceled += OnLookCanceled;

        _playerInput.Player.BasicAttack.performed += OnBasicAttackPerformed;
        _playerInput.Player.BasicAttack.canceled += OnBasicAttackCanceled;

        _playerInput.Player.HeavyAttack.performed += OnHeavyAttackPerformed;
        _playerInput.Player.HeavyAttack.canceled += OnHeavyAttackCanceled;

        _playerInput.Player.Interact.performed += OnInteractPerformed;
        _playerInput.Player.Interact.canceled += OnInteractCanceled;

        _playerInput.Player.Crouch.performed += OnCrouchPerformed;
        _playerInput.Player.Crouch.canceled += OnCrouchCanceled;

        _playerInput.Player.Jump.performed += OnJumpPerformed;
        _playerInput.Player.Jump.canceled += OnJumpCanceled;

        _playerInput.Player.Sprint.performed += OnSprintPerformed;
        _playerInput.Player.Sprint.canceled += OnSprintCanceled;
    }

    private void UnsubscribeFromInputEvents()
    {
        _playerInput.Player.Move.performed -= OnMovePerformed;
        _playerInput.Player.Move.canceled -= OnMoveCanceled;

        _playerInput.Player.Look.performed -= OnLookPerformed;
        _playerInput.Player.Look.canceled -= OnLookCanceled;

        _playerInput.Player.BasicAttack.performed -= OnBasicAttackPerformed;
        _playerInput.Player.BasicAttack.canceled -= OnBasicAttackCanceled;

        _playerInput.Player.HeavyAttack.performed -= OnHeavyAttackPerformed;
        _playerInput.Player.HeavyAttack.canceled -= OnHeavyAttackCanceled;

        _playerInput.Player.Interact.performed -= OnInteractPerformed;
        _playerInput.Player.Interact.canceled -= OnInteractCanceled;

        _playerInput.Player.Crouch.performed -= OnCrouchPerformed;
        _playerInput.Player.Crouch.canceled -= OnCrouchCanceled;

        _playerInput.Player.Jump.performed -= OnJumpPerformed;
        _playerInput.Player.Jump.canceled -= OnJumpCanceled;

        _playerInput.Player.Sprint.performed -= OnSprintPerformed;
        _playerInput.Player.Sprint.canceled -= OnSprintCanceled;
    }

    // Input Callbacks

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _moveValue = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _moveValue = Vector2.zero;
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        _lookValue = context.ReadValue<Vector2>();
    }

    private void OnLookCanceled(InputAction.CallbackContext context)
    {
        _lookValue = Vector2.zero;
    }

    private void OnBasicAttackPerformed(InputAction.CallbackContext context)
    {
        _basicAttackValue = 1f;
    }

    private void OnBasicAttackCanceled(InputAction.CallbackContext context)
    {
        _basicAttackValue = 0f;
    }

    private void OnHeavyAttackPerformed(InputAction.CallbackContext context)
    {
        _heavyAttackValue = 1f;
    }

    private void OnHeavyAttackCanceled(InputAction.CallbackContext context)
    {
        _heavyAttackValue = 0f;
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        _interactValue = 1f;
    }

    private void OnInteractCanceled(InputAction.CallbackContext context)
    {
        _interactValue = 0f;
    }

    private void OnCrouchPerformed(InputAction.CallbackContext context)
    {
        _crouchValue = 1f;
    }

    private void OnCrouchCanceled(InputAction.CallbackContext context)
    {
        _crouchValue = 0f;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        _jumpValue = 1f;
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        _jumpValue = 0f;
    }

    private void OnSprintPerformed(InputAction.CallbackContext context)
    {
        _sprintValue = 1f;
    }

    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        _sprintValue = 0f;
    }
}
