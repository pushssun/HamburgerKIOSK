using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalUpdate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalCount;
    [SerializeField] private TextMeshProUGUI _totalPrice;

    // Start is called before the first frame update
    void Start()
    {
        if (_totalCount != null)
        {
            _totalCount.text = string.Format("{0:#,###}", GameManager.Instance.TotalCount);
        }
        if(_totalPrice != null) 
        {
            _totalPrice.text = string.Format("{0:#,###}", GameManager.Instance.TotalPrice);
        }
    }

}
