using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 6f;
    [SerializeField] float turnSmoothTime = 0.1f;
    [SerializeField] Transform cam;
    float turnSmoothVelocity;


    public bool isIdle = true;
    public bool isWalking = false;

    void Update()
    {
        float horizontal = Input.GetAxisRaw( "Horizontal" );
        float vertical = Input.GetAxisRaw( "Vertical" );
        Vector3 direction = new Vector3 ( horizontal, 0, vertical ).normalized;

        if( direction.magnitude >= 0.1f )
        {
            isWalking = true;

            float targetAngle = Mathf.Atan2( direction.x, direction.z ) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle, ref turnSmoothVelocity, turnSmoothTime );
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0) * Vector3.forward;
            controller.Move ( moveDir.normalized * speed * Time.deltaTime ) ;
        }
        else
        {
            isWalking = false;
        }
    }
}
