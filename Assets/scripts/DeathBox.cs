using UnityEngine;

public class DeathBox : MonoBehaviour
{
    public float returnControlDelay = 0.05f;

    private Vector3 lastGroundedPos;
    private CharacterController playerController;
    private float disableTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        disableTimer += Time.deltaTime;
        if(!playerController.enabled && disableTimer >= returnControlDelay)
        {
            playerController.enabled = true;
        }

        if (playerController.isGrounded) {
               lastGroundedPos = playerController.transform.position + Vector3.up * 0.1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Player Fell");
            other.transform.position = lastGroundedPos;
            playerController.enabled = false;
            disableTimer = 0f;
        }
    }
}
