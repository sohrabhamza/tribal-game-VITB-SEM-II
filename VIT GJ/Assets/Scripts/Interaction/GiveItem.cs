using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItem : MonoBehaviour
{
    [SerializeField] float timeToWait = 2;
    [SerializeField] float rechargeTime = 20;
    [SerializeField] bool rechargeable = true;
    [SerializeField] CollectedItem.Item item;
    [SerializeField] Transform graphic;
    [SerializeField] Vector3 graphicTopPos;
    [SerializeField] float graphicSpeed = 1;
    float timeInside;
    float rechargeTimeElapsed;
    int itemOrigAmount;

    private void Start()
    {
        itemOrigAmount = item.amount;
    }

    private void Update()
    {
        if (rechargeable && item.amount <= 0)
        {
            rechargeTimeElapsed += Time.deltaTime;
            if (rechargeTimeElapsed >= rechargeTime)
            {
                item.amount = itemOrigAmount;
            }
        }

        if (item.amount > 0)
        {
            graphic.localPosition = Vector3.Lerp(graphic.localPosition, graphicTopPos, Time.deltaTime * graphicSpeed);
        }
        else
        {
            graphic.localPosition = Vector3.Lerp(graphic.localPosition, Vector3.zero, Time.deltaTime * graphicSpeed);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && item.amount > 0 && other.GetComponent<CollectedItem>().fillable)
        {
            timeInside += Time.deltaTime;

            if (timeInside >= timeToWait)
            {
                other.GetComponent<CollectedItem>().AddItem(item);
                item.amount = 0;
                rechargeTimeElapsed = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeInside = 0;
        }
    }
}
