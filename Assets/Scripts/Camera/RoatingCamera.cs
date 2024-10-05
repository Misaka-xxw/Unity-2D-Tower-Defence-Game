using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoatingCamera : MonoBehaviour
{
    public float rotateTime = 0.2f;
    private Transform player;
    private bool isRoating = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
        Rotate();
    }
    void Rotate()
    {
        if(Input.GetKeyDown(KeyCode.Q)&&!isRoating)
        {
            StartCoroutine(RotateAound(-45, rotateTime));
        }
        if(Input.GetKeyDown(KeyCode.E)&&!isRoating)
        {
            StartCoroutine(RotateAound(45, rotateTime));
        }
    }
    IEnumerator RotateAound(float angle,float time)
    {
        float number = 60 * time;
        float nextAngle = angle / number;
        isRoating = true;
        for(int i=0;i<number;i++)
        {
            transform.Rotate(new Vector3(0,0,nextAngle));
            yield return new WaitForFixedUpdate();
        }
        isRoating = false;
    }
}
