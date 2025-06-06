using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputController : MonoBehaviour
{
    private PlayerInputActions inputActions;
    [SerializeField] private MinigameController minigameController;

    public event Action<Vector2> OnTap;
    public event Action OnSwipeUp;
    public event Action<float> OnJoystickRoll;
    public event Action OnHoldStart;
    public event Action OnHoldEnd;
    public event Action<Vector3> OnLift;
    private Vector2 screenPosition;
    private bool isHolding = false;

    [Header("lift_Config")]
    private Vector3 lastAcceleration = Vector3.zero;
    private float liftThreshold = 0.25f; // tune this value
    private float cooldownTime = 0.5f;
    private float lastLiftTime = -1f;

    [Header("Swipe_Config")]
    private float swipeCooldown = 1f;       // Cooldown duration in seconds
    private float lastSwipeTime = -Mathf.Infinity; // Time of the last swipe

    [Header("JoySticks")]
    public FixedJoystick fixedJoystick;
    private float totalRotation = 0f;
    private float lastAngle = 0f;
    private bool isRotating = false;

    public float rotationTriggerThreshold = 360f;


    private void Awake()
    {
        minigameController = GetComponent<MinigameController>();
        inputActions = new PlayerInputActions();
    }

    private void Update()
    {
        if (Accelerometer.current != null)
        {
            Vector3 acc = Accelerometer.current.acceleration.ReadValue();
            Debug.DrawRay(transform.position, acc * 5, Color.green); // Visualize force
        }

        if (fixedJoystick != null)
        {
            Vector2 dir = new Vector2(fixedJoystick.Horizontal, fixedJoystick.Vertical);
            if (dir.magnitude > 0.5f) // Consider movement only if stick is pushed enough
            {
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                if (!isRotating)
                {
                    isRotating = true;
                    lastAngle = angle;
                }

                float deltaAngle = Mathf.DeltaAngle(lastAngle, angle);
                totalRotation += deltaAngle;
                lastAngle = angle;

                //Debug.Log($"Accumulated Rotation: {totalRotation}");

                if (Mathf.Abs(totalRotation) >= rotationTriggerThreshold)
                {
                    bool isClockwise = totalRotation > 0;
                    Debug.Log(isClockwise ? "Full Clockwise Rotation!" : "Full Counter-Clockwise Rotation!");

                    OnJoystickRoll?.Invoke(isClockwise ? 1f : -1f); // Trigger with +1 or -1 depending on direction

                    // Reset for next rotation
                    totalRotation = 0f;
                    isRotating = false;
                }
            }
            else
            {
                isRotating = false;
            }
        }

    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.Tap.performed += OnTapPerformed;
        inputActions.Player.SwipeUp.performed += OnSwipeUpPerformed;
        inputActions.Player.JoystickRoll.performed += OnJoystickRollPerformed;
        inputActions.Player.Hold.performed += OnHoldStarted;
        inputActions.Player.Hold.canceled += OnHoldEnded;
        inputActions.Player.Lift.performed += OnLiftPerformed;
    }

    private void OnDisable()
    {
        inputActions.Player.Tap.performed -= OnTapPerformed;
        inputActions.Player.SwipeUp.performed -= OnSwipeUpPerformed;
        inputActions.Player.JoystickRoll.performed -= OnJoystickRollPerformed;
        inputActions.Player.Hold.performed -= OnHoldStarted;
        inputActions.Player.Hold.canceled -= OnHoldEnded;
        inputActions.Player.Lift.performed -= OnLiftPerformed;

        inputActions.Disable();
    }

    private void OnTapPerformed(InputAction.CallbackContext context)
    {
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            screenPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            screenPosition = Mouse.current.position.ReadValue();
        }
        else
        {
            return;
        }

        //Debug.Log($"Tap/click at screen position: {screenPosition}");
        minigameController.HandelTap();
        OnTap?.Invoke(screenPosition);
    }

    private void OnSwipeUpPerformed(InputAction.CallbackContext context)
    {
        Vector2 swipe = context.ReadValue<Vector2>();

        if (Time.time - lastSwipeTime >= swipeCooldown && swipe.y > 200f)
        {
            Debug.Log("Swipe Up Detected");
            lastSwipeTime = Time.time;
            OnSwipeUp?.Invoke();
        }
    }


    private void OnJoystickRollPerformed(InputAction.CallbackContext context)
    {
        float x = context.ReadValue<float>();
        Debug.Log($"Joystick Roll X: {x}");
        OnJoystickRoll?.Invoke(x);
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0) angle += 360f;
        return angle;
    }

    private void OnHoldStarted(InputAction.CallbackContext context)
    {
        if (!isHolding)
        {
            isHolding = true;
            Debug.Log("Hold Started");
            OnHoldStart?.Invoke();
        }
    }

    private void OnHoldEnded(InputAction.CallbackContext context)
    {
        if (isHolding)
        {
            isHolding = false;
            Debug.Log("Hold Ended");
            OnHoldEnd?.Invoke();
        }
    }

    private void OnLiftPerformed(InputAction.CallbackContext context)
    {
        if (Accelerometer.current == null)
        {
            Debug.LogWarning("Accelerometer not available on this device.");
            return;
        }

        Vector3 currentAcceleration = Accelerometer.current.acceleration.ReadValue();
        Vector3 delta = currentAcceleration - lastAcceleration;

        float timeNow = Time.time;

        // Check for sudden upward spike (delta Y)

        //Debug.Log(delta);
        if (delta.y > liftThreshold && (timeNow - lastLiftTime > cooldownTime))
        {
            Debug.Log($"Lift gesture detected! Delta Y: {delta.y}");
            lastLiftTime = timeNow;
            OnLift?.Invoke(currentAcceleration);
        }

        lastAcceleration = currentAcceleration;
    }
}
