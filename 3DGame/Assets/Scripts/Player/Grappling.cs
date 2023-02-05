using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    private PlayerMovement playerMovement;
    private RaycastHit rayHit;
    public Transform grappleGunTip;
    public LayerMask grappableObject;
    public LineRenderer line;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grappleCooldown;
    private float grappleCooldownTimer;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse0;

    private bool grappling;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(grappleKey))
        {
            StartGrapple();
        }

        if (grappleCooldownTimer > 0)
        {
            grappleCooldown -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (grappling)
        {
            line.SetPosition(0, grappleGunTip.position);
        }
    }

    private void StartGrapple()
    {
        if (grappleCooldownTimer > 0)
        {
            return;
        }

        grappling = true;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out rayHit, maxGrappleDistance, grappableObject))
        {
            grapplePoint = rayHit.point;

            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = rayHit.point * maxGrappleDistance;

            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        line.enabled = true;
        line.SetPosition(1, grapplePoint);
    }

    private void ExecuteGrapple()
    {

    }

    private void StopGrapple()  
    {
        grappling = false;

        grappleCooldownTimer = grappleCooldown;

        line.enabled = false;
    }
}

