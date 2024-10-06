using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpiritManager : MonoBehaviour
{
    [SerializeField] private List<Tilemap> conceal;
    public GameObject currentPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            foreach(Tilemap obj in conceal)
            {
                obj.color = Color.black;
            }
            currentPlayer.GetComponent<Character1>().enabled = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                if(hit.collider.tag=="Player")
                {
                    foreach (Tilemap obj in conceal)
                    {
                        obj.color = Color.white;
                    }
                    currentPlayer = hit.collider.gameObject;
                    currentPlayer.GetComponent<Character1>().enabled = true;
                }
            }
        }
    }
}
