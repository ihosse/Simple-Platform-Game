using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterGrounding : MonoBehaviour
{
    [SerializeField]
    private Transform[] sidePositions;
    [SerializeField]
    private Transform[] footPositions;

    [SerializeField]
    private float maxDistance;

    [SerializeField]
    private LayerMask layerMask;

    private Transform contactObject;
    private Vector3? groundedObjectLastPosition;

    public bool isTouchingGround { get; private set; }
    public bool IsTouchingWall { get; private set; }
    public Vector2 ContactDirection { get; private set; }

    private void Update()
    {
        IsTouchingWall = CheckContact(sidePositions);
        isTouchingGround = CheckContact(footPositions);

        StickToMovingObjects();
    }

    private bool CheckContact(Transform[] foots)
    {
        bool isContact;
        foreach (var position in foots)
        {
            isContact = CheckContact(position);
            if (isContact)
                return true;
        }
        return false;
    }

    private void StickToMovingObjects()
    {
        if(contactObject != null) 
        {
            if(groundedObjectLastPosition.HasValue && 
               groundedObjectLastPosition.Value != contactObject.position)
            {
                Vector3 delta = contactObject.position - groundedObjectLastPosition.Value;
                transform.position += delta;
            }
            groundedObjectLastPosition = contactObject.position;
        }
        else 
        {
            groundedObjectLastPosition = null;
        }
    }

    private bool CheckContact(Transform foot) 
    {
        var raycastHit = Physics2D.Raycast(foot.position, foot.forward, maxDistance, layerMask);
        Debug.DrawRay(foot.position, foot.forward * maxDistance, Color.red);

        if (raycastHit.collider != null)
        {
            if(contactObject != raycastHit.collider.transform)
            { 
                groundedObjectLastPosition = raycastHit.collider.transform.position;
            }
            contactObject = raycastHit.collider.transform;
            ContactDirection = foot.forward;
            return true;
        }
        else
        {
            contactObject = null;
            return false;
        }
    }
}
