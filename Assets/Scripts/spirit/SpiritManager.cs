using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpiritManager : MonoBehaviour
{
    [SerializeField] private List<Tilemap> conceal;
    public GameObject currentPlayer;
    private bool canClick;
    public Camera viewCamera;
    public Camera MainCamera;
    private Vector3 mouseWorldPos => viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private Collider2D ObjectAtMousePosition()
    {
        //Debug.Log(mouseWorldPos);
        return Physics2D.OverlapPoint(mouseWorldPos);
        //return Physics2D.OverlapPoint(characterTest.player.position);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            foreach(Tilemap obj in conceal)
            {
                obj.color = Color.black;
            }
            currentPlayer.GetComponent<characterTest>().enabled = false;
            viewCamera.gameObject.SetActive(true);
            MainCamera.gameObject.SetActive(false);
            Time.timeScale = 0.5f;
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if(Physics.Raycast(ray,out hit))
        //    {
        //        if(hit.collider.tag=="Player" || hit.collider.tag=="Enemy")
        //        {
        //            foreach (Tilemap obj in conceal)
        //            {
        //                obj.color = Color.white;
        //            }
        //            currentPlayer = hit.collider.gameObject;
        //            Animator anim=currentPlayer.GetComponent<Animator>();
        //            anim.SetTrigger("controlStart");
        //            if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        //            {
        //                currentPlayer.GetComponent<characterTest>().enabled = true;
        //                anim.SetTrigger("controlCancel");
        //            }
        //            Time.timeScale = 1.0f;
        //        }
        //    }
        //}
        canClick = ObjectAtMousePosition();
        //if (hand.gameObject.activeInHierarchy)
        //{
        //    hand.position = Input.mousePosition;
        //}
        if (canClick && Input.GetMouseButtonDown(0))
            //if (teleport.isEnter && Input.GetKeyDown(key: KeyCode.F))
        {
            ClickAction(ObjectAtMousePosition().gameObject);
        }

    }
    private void ClickAction(GameObject clickObject)
    {
        if (clickObject.tag == "Player" || clickObject.tag == "Enemy")
        {
            Debug.Log("get Input");
            foreach (Tilemap obj in conceal)
            {
                obj.color = Color.white;
            }
            currentPlayer = clickObject.gameObject;
            Animator anim = currentPlayer.GetComponent<Animator>();
            anim.SetTrigger("controlStart");
            if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
            {
                currentPlayer.GetComponent<characterTest>().enabled = true;
                anim.SetTrigger("controlCancel");
            }
            viewCamera.gameObject.SetActive(false);
            MainCamera.gameObject.SetActive(true);
            Time.timeScale = 1.0f;
        }
    }
}
