using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemRequirement : MonoBehaviour
{
    [SerializeField] float timeToWait = 2;
    [SerializeField] float rechargeTime = 20;
    [SerializeField] Vector2 rechargeTimeVar = new Vector2(-10, 10);
    [SerializeField] int maxRequestable = 27;
    [SerializeField] List<CollectedItem.Item.itemType> requestableItems;
    [SerializeField] FadeInOut reqBack;
    [SerializeField] FadeInOut questionMark;
    [SerializeField] TextMeshPro numberTex;
    [SerializeField] GameObject[] itemIcon;
    [SerializeField] GameObject backG;
    float timeInside;
    float rechargeTimeElapsed;
    float rechargeTimeLocal;
    CollectedItem.Item requestedItem = new CollectedItem.Item();
    CollectedItem collector;
    bool hasItems;
    int itemIndex;
    Vector3 redGraphicPos;

    private void Start()
    {
        collector = FindObjectOfType<CollectedItem>();
        rechargeTimeLocal = rechargeTime + Random.Range((float)rechargeTimeVar.x, (float)rechargeTimeVar.y);
        redGraphicPos = backG.transform.localPosition;
    }

    private void Update()
    {
        if (timeInside == 0 && requestedItem.myItemType == CollectedItem.Item.itemType.none)
        {
            rechargeTimeElapsed += Time.deltaTime;
            if (rechargeTimeElapsed >= rechargeTimeLocal)
            {
                questionMark.FadeIn();
            }
        }

        Vector3 whereToGraphic;
        if (rechargeTimeElapsed >= rechargeTimeLocal || requestedItem.myItemType != CollectedItem.Item.itemType.none)
        {
            whereToGraphic = new Vector3(redGraphicPos.x, 0, redGraphicPos.z); ;
        }
        else
        {
            whereToGraphic = redGraphicPos;
        }
        backG.transform.localPosition = Vector3.Lerp(backG.transform.localPosition, whereToGraphic, Time.deltaTime * 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (requestedItem.myItemType != CollectedItem.Item.itemType.none)
            {
                for (int i = 0; i < collector.myItems.Count; i++)
                {
                    if (requestedItem.myItemType == collector.myItems[i].myItemType && collector.myItems[i].amount > 0)
                    {
                        hasItems = true;
                        itemIndex = i;
                        break;
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeInside += Time.deltaTime;

            if (timeInside >= timeToWait)
            {
                if (requestedItem.myItemType == CollectedItem.Item.itemType.none && rechargeTimeElapsed >= rechargeTimeLocal)
                {
                    int randReq = Random.Range(0, requestableItems.Count);
                    requestedItem.myItemType = requestableItems[randReq];
                    requestedItem.amount = Random.Range(1, maxRequestable / 3) * 3;

                    reqBack.FadeIn();
                    questionMark.FadeOut();
                    numberTex.text = requestedItem.amount.ToString();
                    showItem(requestedItem.myItemType);
                    // tmep_itemName.text = requestedItem.myItemType.ToString();

                    timeInside = 0;
                    rechargeTimeElapsed = 0;
                    rechargeTimeLocal = rechargeTime + Random.Range((float)rechargeTimeVar.x, (float)rechargeTimeVar.y);
                }
                if (hasItems)
                {
                    if (requestedItem.amount <= 0)
                    {
                        requestedItem.myItemType = CollectedItem.Item.itemType.none;
                        reqBack.FadeOut();
                        hasItems = false;
                        return;
                    }
                    else if (collector.myItems[itemIndex].amount <= 0)
                    {
                        hasItems = false;
                        return;
                    }

                    collector.RemoveItem(requestedItem.myItemType, 3);
                    requestedItem.amount -= 3;

                    other.GetComponent<Coins>().AddCoin(10);

                    numberTex.text = requestedItem.amount.ToString();

                    timeInside = timeToWait - 0.5f;
                }
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

    void showItem(CollectedItem.Item.itemType it)
    {
        switch (it)
        {
            case CollectedItem.Item.itemType.wheat:
                reqBack.en = new List<int>() { 0, 1 };
                break;
            case CollectedItem.Item.itemType.rice:
                reqBack.en = new List<int>() { 0, 2 };
                break;
        }
    }
}
