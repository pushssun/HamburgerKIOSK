//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;

//public class UpdatePricePrefab : MonoBehaviour
//{
//    private int _addPrice;

//    public void AddPrice()
//    {
//        if( _addPrice != 0)
//        {
//            MinusPrice(_addPrice);
//        }
//        _addPrice = int.Parse(transform.GetChild(2).GetComponent<TextMeshProUGUI>().text);
//        GameManager.Instance.CurrentPrice += _addPrice;
//        GameManager.Instance.UpdateText(0, GameManager.Instance.CurrentPrice);
//    }

//    public void MinusPrice(int amount)
//    {
//        GameManager.Instance.CurrentPrice -= amount;
//        GameManager.Instance.UpdateText(0, GameManager.Instance.CurrentPrice);
//    }
//}
