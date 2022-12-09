using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedItem : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public enum itemType
        {
            none, wheat, rice
        }
        public itemType myItemType;
        public GameObject displayGraphic;
        public Transform nextGraphPt;
        public List<GameObject> myDisplays;
        public float graphDist;
        public int amount;
    }

    public List<Item> myItems = new List<Item>();
    public bool fillable = true;
    public int totalCap;

    int totalItem;
    public void AddItem(Item newItem)
    {
        for (int i = 0; i < myItems.Count; i++)
        {
            if (newItem.myItemType == myItems[i].myItemType)
            {
                int newItemAm = newItem.amount;
                totalItem += newItemAm;
                if (totalItem > totalCap)
                {
                    newItemAm = totalCap - (totalItem - newItemAm);
                    totalItem = totalCap;
                }
                myItems[i].amount += newItemAm;
                if (totalItem >= totalCap) fillable = false;

                if (myItems[i].amount % 3 == 0)
                {
                    AddDItem(myItems[i]);
                }
                return;
            }
        }
        myItems.Add(newItem);
    }

    public void RemoveItem(Item.itemType it, int amount)
    {
        for (int i = 0; i < myItems.Count; i++)
        {
            if (it == myItems[i].myItemType)
            {
                myItems[i].amount -= amount;
                totalItem -= amount;

                Destroy(myItems[i].myDisplays[myItems[i].myDisplays.Count - 1]);
                myItems[i].myDisplays.RemoveAt(myItems[i].myDisplays.Count - 1);
                myItems[i].nextGraphPt.position -= new Vector3(0, myItems[i].graphDist, 0);

                if (totalItem < totalCap) fillable = true;
                return;
            }
        }
    }
    void AddDItem(Item it)
    {
        GameObject newG = Instantiate(it.displayGraphic, it.nextGraphPt.position, transform.rotation);
        newG.transform.parent = it.nextGraphPt.transform.parent;
        it.myDisplays.Add(newG);
        it.nextGraphPt.position += new Vector3(0, it.graphDist, 0);
    }
}
