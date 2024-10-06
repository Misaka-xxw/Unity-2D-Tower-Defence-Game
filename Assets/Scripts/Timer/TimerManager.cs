using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float timer = 60.0f;
    public Text timerText;
    private bool isTimeOut = false;
    //public GameObject overPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }
    private void Timer()
    {
        if(!isTimeOut)
        {
            timer -= Time.deltaTime;
            //F2������λС��
            timerText.text = timer.ToString("F2");
            if(timer<=0)
            {
                isTimeOut = true;
                timerText.text = "Over";
            }
            //��ʾ���㻭��
            StartCoroutine("ShowPanel");
        }
    }
    private IEnumerator ShowPanel()
    {
        yield return null;
    }

}
