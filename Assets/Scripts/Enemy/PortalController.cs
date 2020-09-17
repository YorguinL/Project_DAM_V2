using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public static float time;
    float nextPosition = 0.0f;
    public GameObject portal;

    // Start is called before the first frame update
    void Start()
    {
        PositionsController.NewPosition(portal);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextPosition){
            nextPosition = Time.time + time;
            PositionsController.NewPosition(portal);
        }
    }
}
