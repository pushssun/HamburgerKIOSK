using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddItemPrice : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    private Button button;
    
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickAddItemPrice);
    }

    private void OnClickAddItemPrice()
    {
        _spawnPoint.GetChild(_spawnPoint.childCount - 1).gameObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = string.Format("{0:#,###}", GameManager.Instance.CurrentSetPrice + GameManager.Instance.CurrentSidePrice);
        _spawnPoint.GetChild(_spawnPoint.childCount - 1).gameObject.GetComponent<ClickAddItem>().Price = GameManager.Instance.CurrentSetPrice + GameManager.Instance.CurrentSidePrice;
    }

}
