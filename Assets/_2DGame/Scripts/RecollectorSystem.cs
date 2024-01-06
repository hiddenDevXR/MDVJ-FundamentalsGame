using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecollectorSystem : MonoBehaviour
{
    public Image sodaBar;
    private float sodaCount;
    private float fillAmount = 0;
    public delegate void Contact();
    public event Contact OnReportCollected;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Item"))
        {
            other.gameObject.GetComponent<Item>().Remove();
            sodaCount++;
            fillAmount = sodaCount / 10;
            
            sodaBar.fillAmount = fillAmount;
        }

        if(other.CompareTag("Report"))
        {
            GameManager.remainingTime += 10;
            other.gameObject.GetComponent<Item>().PoolRemove();
            OnReportCollected();
        }
    }

    public float GetFillProgress()
    { 
        return fillAmount; 
    }

}
