using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothing;
    public static Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if(target!=null)
        {
            if(transform.position!=target.position)
            {
                Vector3 targetPos = new Vector3(target.position.x, target.position.y +2, target.position.z - 9);
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
            }
        }
    }
}
