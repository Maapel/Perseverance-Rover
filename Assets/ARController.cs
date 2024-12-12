using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.XR.ARFoundation;

public class ARController : MonoBehaviour
{
    ARRaycastManager raycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    GameObject obj = null;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject marker;
    private float distance;
    private float diff;
    private float prev_dist;
    // Start is called before the first frame update
    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits))
        {
            if ((Input.touchCount >1))
            {
                if (obj)
                {
                    Vector2 touch0, touch1;
                    touch0 = Input.GetTouch(0).position;
                    touch1 = Input.GetTouch(1).position;
                    distance = Vector2.Distance(touch0, touch1);
                    diff = distance - prev_dist;
                    obj.transform.localScale *= 1 + (diff / 100);
                    prev_dist = distance;
                }
            }

            else if (Input.touchCount == 1)
            {
                if (!obj)
                {
                    obj = Instantiate(prefab);
                }
                else
                {
                    obj.transform.position = hits[0].pose.position;
                }

            }
            else
            {
                marker.transform.position = hits[0].pose.position;


            }
        }
    }
}
