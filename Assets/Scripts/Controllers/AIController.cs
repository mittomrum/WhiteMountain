using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private float chaseDistance = 5;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        var distance = distanceToPlayer();
        if (distanceToPlayer() < chaseDistance)
        {
            Debug.Log(this.gameObject.name + " - 'I am chasing the player because he is to close' (" + distance + ")");
        }
    }
    private float distanceToPlayer()
    {
        return Vector3.Distance(this.transform.position, player.transform.position);
    }
}
