using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{

    [SerializeField] private float walkSpeed = 6.0f;
    [SerializeField] private float gravity = 20.0f;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private float lookXLimit = 85.0f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

    }

    void Update()
    {
        if (cameraTransform != null)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            transform.Rotate(Vector3.up * mouseX);
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = walkSpeed * Input.GetAxis("Vertical");
        float curSpeedY = walkSpeed * Input.GetAxis("Horizontal");

        float movementDirectionY = moveDirection.y;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (!characterController.isGrounded)
        {
            moveDirection.y = movementDirectionY - (gravity * Time.deltaTime);
        }
        else
        {
            moveDirection.y = -0.5f;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BuyZone"))
            NPCManager.instance.PlayerCass(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BuyZone"))
            NPCManager.instance.PlayerCass(null);
    }
}
