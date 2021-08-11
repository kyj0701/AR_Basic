using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlaceOnPlane : MonoBehaviour
{
    public ARRaycastManager arRaycaster;
    public GameObject placeObject;
    private Text cubePos;
    
    // Start is called before the first frame update
    void Start()
    {
        cubePos = GameObject.Find("Pose").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCenterObject();
    }

    private void UpdateCenterObject()
    {
        Vector3 screenCenter  = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arRaycaster.Raycast(screenCenter, hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            Pose placementPose = hits[0].pose;
            placeObject.SetActive(true);
            placeObject.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            cubePos.text = "X:" + placementPose.position.x.ToString() + " Y: " + placementPose.position.y.ToString() + " Z: " + placementPose.position.z.ToString();
        }
        else
        {
            placeObject.SetActive(false);
        }
    }
}