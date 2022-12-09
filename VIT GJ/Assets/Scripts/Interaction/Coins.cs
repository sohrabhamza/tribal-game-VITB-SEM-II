using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinTex;
    int coins;
    public void AddCoin(int am)
    {
        coins += am;
        coinTex.text = coins.ToString();
    }

    public int GiveCoin(int am)
    {
        if (coins - am >= 0)
        {
            coins -= am;
            return am;
        }
        else
        {
            return 0;
        }
    }
}
