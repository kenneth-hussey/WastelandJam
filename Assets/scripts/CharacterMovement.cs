using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private AudioSource stepSound; // this should not be here but whatev
    [SerializeField] private CharacterController controller;
    [SerializeField] private float gravity=1f;
    [SerializeField] private float jumpPower=6.5f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveDamping;
    [SerializeField] private bool useDamping = true;
    [SerializeField] private bool enableJumps = false;

    [HideInInspector] public Vector3 m_velo;
    private Vector2 m_moveAmt;
    private InputAction m_moveAction;
    private InputAction m_jumpAction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inputActions.FindActionMap("Player").Enable();
        m_moveAction = inputActions.FindAction("Move");
        m_jumpAction = inputActions.FindAction("Jump");

        if(!enableJumps)
        {
            print("Jumping is disabled...");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!stepSound.isPlaying && m_moveAction.IsPressed())
        {
            stepSound.Play();
        }
        if(stepSound.isPlaying && !m_moveAction.IsPressed())
        {
            stepSound.Stop();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var _ = hit.gameObject.GetComponent<Terminal>();
        var __ = hit.gameObject.GetComponent<EndTerminal>();
        if (_ || __)
        {
            hit.gameObject.BroadcastMessage("ActivateTerminal");
        }
    }

    private void FixedUpdate()
    {
        m_moveAmt = m_moveAction.ReadValue<Vector2>().normalized * moveSpeed;

        var lastFrameVelo = m_velo; 
        m_velo = transform.right * m_moveAmt.x + transform.forward * m_moveAmt.y + transform.up*m_velo.y;
        if (useDamping)
        {
            m_velo = new Vector3(Mathf.Clamp(m_velo.x, lastFrameVelo.x - moveDamping, lastFrameVelo.x + moveDamping),
            m_velo.y,
            Mathf.Clamp(m_velo.z, lastFrameVelo.z - moveDamping, lastFrameVelo.z + moveDamping));
        }
        

        //m_velo = new Vector3( Mathf.Clamp( m_moveAmt.x, m_velo.x - moveDamping , m_velo.x + moveDamping) , 
        //    m_velo.y, 
        //    Mathf.Clamp(m_moveAmt.y, m_velo.z - moveDamping, m_velo.z + moveDamping) );


        if (!controller.isGrounded)
        {
            m_velo.y -= gravity * Time.deltaTime;
        }
        else
        {
            m_velo.y = 0f;
        }
        
        if (enableJumps && m_jumpAction.IsPressed() && controller.isGrounded)
        {
            m_velo.y = jumpPower;
            print("yump");
        }
        
        controller.Move(m_velo * Time.deltaTime);
    }
}
