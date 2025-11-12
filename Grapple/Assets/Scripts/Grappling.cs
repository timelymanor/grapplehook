using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.AI;

public class Grappling : MonoBehaviour
{
    [Header("References")] 
    private PlayerMovementAdvanced pm;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform gunTip;
    [SerializeField] private LayerMask whatIsGrappleable;
    [SerializeField] private LineRenderer lr;

    [Header("Grappling")] 
    [SerializeField] private float maxGrappleDistance;
    [SerializeField] private float grappleDelayTime;
    public float overshootYAxis;
    [SerializeField] private GameObject grapplePoint;
    private GameObject grappledEnemy;

    [Header("Cooldown")] 
    [SerializeField] private float grapplingCd;
    private float grapplingCdTimer;

    [Header("Input")] 
    [SerializeField] private KeyCode grappleKey = KeyCode.Mouse1;

    private bool grappling;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Rigidbody rig;

    private void Start()
    {
        pm = GetComponent<PlayerMovementAdvanced>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(grappleKey)) StartGrapple();

        if (grapplingCdTimer > 0)
        {
            grapplingCdTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if(grappling)
            lr.SetPosition(0, gunTip.position);
    }

    private void StartGrapple()
    {
        if (grapplingCdTimer > 0) return;
        GetComponent<SwingingDone>().StopSwing();
        grappling = true;

        pm.freeze = true;

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            
            if (hit.collider.CompareTag("Enemy"))
            {
                grapplePoint.transform.position = hit.point;
                grapplePoint.transform.parent = hit.collider.gameObject.transform;
                grappledEnemy = hit.collider.gameObject;
                Invoke(nameof(GrapplingEnemy), grappleDelayTime);
            }
            else
            {
                grapplePoint.transform.position = hit.point;
                grapplePoint.transform.parent = hit.collider.gameObject.transform;
                Invoke(nameof(ExecuteGrapple), grappleDelayTime);
                
            }
            
            
        }
        else
        {
            grapplePoint.transform.position = cam.position + cam.forward * maxGrappleDistance;
            Invoke(nameof(StopGrapple), grappleDelayTime);
        }
        
        lr.enabled = true;
        lr.SetPosition(1, grapplePoint.transform.position);
    }

    private void ExecuteGrapple()
    {
        pm.freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.transform.position.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        pm.JumpToPosition(grapplePoint.transform.position, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        pm.freeze = false;
        
        grappling = false;
        
        grapplingCdTimer = grapplingCd;
        
        lr.enabled = false;
    }

    private void GrapplingEnemy()
    {
        pm.freeze = false;

        agent = grappledEnemy.GetComponent<NavMeshAgent>();
        agent.enabled = false;
        rig = grappledEnemy.gameObject.GetComponent<Rigidbody>();
        rig.isKinematic = false;
        StartCoroutine(GrappleEnemyWait());
        
        
    }

    private IEnumerator GrappleEnemyWait()
    {
        while (grappledEnemy != null)
        {
            Vector3 direction = (gunTip.transform.position - rig.position).normalized;
            float distance = (gunTip.transform.position - rig.position).magnitude;
            
            float pullForce = 30f;
            rig.AddForce(direction * pullForce * Time.deltaTime, ForceMode.VelocityChange);
            
            if (Input.GetKeyUp(grappleKey) || distance < 1f)
            {
                rig.linearVelocity = Vector3.zero;
                grapplePoint.transform.parent = null;
                break;
            }

            yield return null;
        }
        
        agent.enabled = true;
        rig.isKinematic = true;
    }
}
