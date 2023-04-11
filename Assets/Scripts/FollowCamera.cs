using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.AI;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class FollowCamera : MonoBehaviour
{

    [SerializeField] public GameObject target;
    [SerializeField] private Camera cam;

    public float xSpeed = 12.0f;
    public float ySpeed = 12.0f;
    public float scrollSpeed = 10.0f;

    public float zoomMin = 1.0f;
    public float zoomMax = 20.0f;
    public float distance;
    public bool isActivated;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization

    void Awake()
    {
        if (target == null)
        {
            Debug.Log("No target set for camera to follow");
            target = GameObject.FindWithTag("Player"); // or any other way to find the target GameObject
        }
    }

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

    }
    void Update()
    {
        this.transform.position = target.transform.position;
    }
    void LateUpdate()
    {
        
        // only update if the mousebutton is held down
        if (Input.GetMouseButtonDown(2))
        {
            isActivated = true;
        }
        // if mouse button is let UP then stop rotating camera
        if (Input.GetMouseButtonUp(2))
        {
            isActivated = false;
        }

        if (target && isActivated)
        {
            //  get the distance the mouse moved in the respective direction
            x += Input.GetAxis("Mouse X") * xSpeed;

            // when mouse moves left and right we actually rotate around local y axis	
            transform.RotateAround(target.transform.position, Vector3.up, x);


            // reset back to 0 so it doesn't continue to rotate while holding the button
            x = 0;
            y = 0;
        } else
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                // Get the current distance between the camera and target
                float currentDistance = Vector3.Distance(cam.transform.position, target.transform.position);

                // Calculate the new distance based on the mouse wheel input
                float deltaDistance = -Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
                float newDistance = ZoomLimit(currentDistance + deltaDistance, zoomMin, zoomMax);

                // Calculate the current camera-to-target vector
                Vector3 cameraToTarget = cam.transform.position - target.transform.position;

                // Scale the vector by the new distance and add it back to the target position to get the new camera position
                Vector3 newCameraPosition = target.transform.position + cameraToTarget.normalized * newDistance;

                // Move the camera to the new position and make it look at the target
                cam.transform.position = newCameraPosition;
                cam.transform.LookAt(target.transform.position);

                // Keep the camera's position in sync with the target
                this.transform.position = target.transform.position;
            }

        }
    }

    public static float ZoomLimit(float dist, float min, float max)
    {
        if (dist < min)
            dist = min;
        if (dist > max)
            dist = max;
        return dist;
    }
}