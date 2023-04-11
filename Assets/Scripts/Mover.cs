using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    private Vector3 targetPosition;

    // Update is called once per frame
    void Update()
    {
        #region Click To Move
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }
        #endregion

        UpdateAnimator();
        #region Debug
        if (targetPosition !=  null)
        {
            Debug.DrawRay(transform.position, targetPosition - transform.position, Color.red, 0.1f);
        }
        #endregion

    }
    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if (hasHit)
        {
            targetPosition = hit.point;
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
    }
    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }

}
