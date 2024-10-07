using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    [SceneName] public string sceneFrom;
    [SceneName] public string sceneTo;
    public static bool isEnter = false;
    public void Teleport()
    {
        TransitionManager.Instance.Transition(sceneFrom, sceneTo);
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isEnter = true;
            Debug.Log(isEnter);
        }
    }
}
