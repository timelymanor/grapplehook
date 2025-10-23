using UnityEngine;

public class Swinging : MonoBehaviour
{
    [Header("Input")] 
    public KeyCode swingKey = KeyCode.Mouse0;

    [Header("References")] 
    [SerializeField] private LineRenderer lr;
    [SerializeField] private Transform gunTip, cam, player;
    [SerializeField] private LayerMask whatIsGrappleable;
    [SerializeField] private PlayerMovementAdvanced pm;

    [Header("Swinging")]
    private float maxSwingDistance = 25f;
    private Vector3 swingPoint;
    private SpringJoint joint;

    [Header("Odmgear")] 
    [SerializeField] private Transform orientation;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float horizontalThrustForce;
    [SerializeField] private float forwardThrustForce;
    [SerializeField] private float extendCableSpeed;
    

    void Update()
    {
        if (Input.GetKeyDown(swingKey)) StartSwing();
        if (Input.GetKeyUp(swingKey)) StopSwing();

        if (joint != null) OdmGearMovement();

    }
    private void StartSwing()
    {
        pm.swinging = true;
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxSwingDistance, whatIsGrappleable))
        {
            DrawRope();
            swingPoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;
            
            float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 12f;   // stronger pull toward anchor
            joint.damper = 4.5f;  // smoother motion
            joint.massScale = 1f; // less sluggishness
            
            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
        else
        {
            pm.swinging = false; // if no grapple hit, cancel swing
        }
    }

    void StopSwing()
    {
        pm.swinging = false;
        lr.positionCount = 0;
        Destroy(joint);
    }
    
    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        if (!joint || lr.positionCount < 2) return;  // <-- Add this line

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }
    
    private void OdmGearMovement()
    {
        // right
        if (Input.GetKey(KeyCode.D)) 
            rb.AddForce(orientation.right * horizontalThrustForce * Time.deltaTime);

        // left
        if (Input.GetKey(KeyCode.A)) 
            rb.AddForce(-orientation.right * horizontalThrustForce * Time.deltaTime);

        // forward
        if (Input.GetKey(KeyCode.W)) 
            rb.AddForce(orientation.forward * forwardThrustForce * Time.deltaTime);

        // shorten cable
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 directionToPoint = swingPoint - transform.position;
            rb.AddForce(directionToPoint.normalized * forwardThrustForce * Time.deltaTime);

            float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
        }

        // extend cable
        if (Input.GetKey(KeyCode.S))
        {
            float extendedDistanceFromPoint = Vector3.Distance(transform.position, swingPoint) + extendCableSpeed;
            joint.maxDistance = extendedDistanceFromPoint * 0.8f;
            joint.minDistance = extendedDistanceFromPoint * 0.25f;
        }
    }

}
