using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask ground;

    public float moveSpeed = 5f;
    public float rotateSpeed = 180f;


    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;

    private Camera camera;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        camera = Camera.main;
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();

        var dir = new Vector3(playerInput.moveH, 0f, playerInput.moveV);

        playerAnimator.SetFloat("Move", dir.magnitude);
    }

    private void Move()
    {

        var forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();
        var right = Camera.main.transform.right;
        right.y = 0;
        right.Normalize();

        var dir = forward * playerInput.moveV;
        dir += right * playerInput.moveH;

        if (dir.magnitude > 1)
        {
            dir.Normalize();
        }
        var delta = dir * moveSpeed * Time.deltaTime;

        playerRigidbody.MovePosition(playerRigidbody.position + delta);
    }

    private void Rotate()
    {
        RaycastHit hit;
        var ray = camera.ScreenPointToRay(playerInput.mousePos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground", "Ground")))
        {
            var forward = hit.point - transform.position;
            forward.y = 0;
            forward.Normalize();

            transform.rotation = Quaternion.LookRotation(forward);
        }
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue);
    }
}
