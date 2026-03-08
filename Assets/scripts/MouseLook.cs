using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private InputActionAsset inputActions;
    public float lookSensitivity=100f;

    private float m_xRotation = 0f;
    private InputAction m_lookAction;
    private void Awake()
    {
        inputActions.FindActionMap("Player").Enable();

        m_lookAction = inputActions.FindAction("Look");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lookDelta = m_lookAction.ReadValue<Vector2>() * lookSensitivity * Time.deltaTime;

        m_xRotation -= lookDelta.y;
        m_xRotation = Mathf.Clamp(m_xRotation, -90, 90);

        playerCamera.localRotation = Quaternion.Euler(m_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * lookDelta.x);
    }
}
