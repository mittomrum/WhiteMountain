using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 targetPosition;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("No Target added in the inspector!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        #region Click To Move
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }
        #endregion
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


}
