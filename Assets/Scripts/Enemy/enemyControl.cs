using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pathfinding;
using System.Threading;

public class enemyControl : MonoBehaviour
{
    public UnityEvent<Vector2> OnMoveMentInput;
    public UnityEvent Ontrigger;
    [SerializeField] private Transform player;
    [SerializeField] private float chaseDistance = 3.0f;
    [SerializeField] private float triggerDistance = 0.5f;
    private Seeker seeker;
    private List<Vector3> pathPointList;
    private int currentIndex = 0;
    private float pathGenerateInteval = 0.5f;
    private float pathGenerateTimer = 0f;
    public float highOfABox = 0.4f;
    private void Awake()
    {
        seeker = GetComponent<Seeker>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        float distance = Vector2.Distance(player.position, transform.position);
        if (distance < chaseDistance && player.position.z <= 2 * highOfABox)
        {
            AutoPath();
            if (pathPointList == null)
                return;
            //ÔÚ´¥·¢·¶Î§Àï×öÊ²Ã´£¿
            if (distance < triggerDistance)
            {
                OnMoveMentInput?.Invoke(Vector2.zero);
                Ontrigger?.Invoke();
            }
            else
            {
                //Vector2 direction = player.position - transform.position;
                Vector2 direction = (pathPointList[currentIndex]-transform.position).normalized;
                OnMoveMentInput?.Invoke(direction.normalized);
            }
        }
        else
        {
            OnMoveMentInput?.Invoke(Vector2.zero);//Í£Ö¹×·»÷
        }
    }
    private void AutoPath()
    {
        pathGenerateTimer += Time.deltaTime;
        if(pathGenerateTimer>=pathGenerateInteval)
        {
            GeneratePath(player.position);
            pathGenerateTimer = 0;
        }
        if(pathPointList == null||pathPointList.Count<=0)
        {
            GeneratePath(player.position);
        }
        else if (Vector2.Distance(transform.position, pathPointList[currentIndex])<=0.1f)
        {
            currentIndex++;
            if (currentIndex >= pathPointList.Count)
                GeneratePath(player.position);
        }
    }
    private void GeneratePath(Vector3 target)
    {
        currentIndex = 0;
        seeker.StartPath(transform.position, target, Path =>
        {
            pathPointList = Path.vectorPath;
        });
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
