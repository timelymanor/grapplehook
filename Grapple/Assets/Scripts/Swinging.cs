using UnityEngine;

public class Swinging : MonoBehaviour
{
    [Header("Input")] 
    public KeyCode swingKey = KeyCode.Mouse0;

    [Header("References")] 
    [SerializeField] private LineRenderer lr;
    [SerializeField] private Transform gunTip, cam, player;
    [SerializeField] private LayerMask whatIsGrappleable;

    [Header("Swinging")]
    private float maxSwingDistance = 25f;
    private Vector3 swingPoint;
    private SpringJoint joint;

    void Update()
    {
        if (Input.GetKeyDown(swingKey)) StartSwing();
        if (Input.GetKeyUp(swingKey)) StopSwing();
    }
    private void StartSwing()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxSwingDistance, whatIsGrappleable))
        {
            swingPoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;
            
            float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;
            
            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
    }

    void StopSwing()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }
    
    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        if (!joint) return;
        
        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }
}
