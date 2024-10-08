using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public Transform playerTransform;
    public float shadowSizeFloat = 0.5f;
    private float highDifference;
    private Vector3 scale;
    private PlayerParentMove playerMove;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerParentMove>();
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        highDifference = playerTransform.position.y-transform.position.y;
        transform.localScale = new Vector3(Mathf.Clamp(scale.x - (highDifference / playerMove.jumpHigh) * scale.x, scale.x *
            shadowSizeFloat, scale.x), Mathf.Clamp(scale.y - (highDifference / playerMove.jumpHigh) * scale.y, scale.y *
            shadowSizeFloat, scale.y), scale.z);
    }
}
