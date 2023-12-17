//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class UpdatePrice : MonoBehaviour
//{
//    [SerializeField] private int amount;
//    [SerializeField] private TextMeshProUGUI text;

//    public void AddPrice()
//    {
//        GameManager.Instance.CurrentPrice += amount;
//        text.text = GameManager.Instance.CurrentPrice.ToString();
//        GameManager.Instance.UpdateText(0, GameManager.Instance.CurrentPrice);
//    }

//    public void MinusPrice()
//    {
//        GameManager.Instance.CurrentPrice -= amount;
//        text.text = GameManager.Instance.CurrentPrice.ToString();
//        GameManager.Instance.UpdateText(0, -GameManager.Instance.CurrentPrice);
//    }
//}
