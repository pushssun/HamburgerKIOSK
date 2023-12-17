using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickItem : MonoBehaviour
{
    private Button _itemButton;
    private Transform _spawnPoint;

    [SerializeField] private GameObject _addItemPf;

    // Start is called before the first frame update
    void Start()
    {
        _itemButton = GetComponent<Button>();
        _itemButton.onClick.AddListener(OnClickItemButton);

        _spawnPoint = GameObject.Find("AddItemContent").transform;

    }


    private void OnClickItemButton()
    {
        _addItemPf.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _itemButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
        _addItemPf.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = string.Format("{0:#,###}", _itemButton.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text);
        //_addItemPf.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = string.Format("{0:#,###}", GameManager.Instance.CurrentSetPrice + GameManager.Instance.CurrentSetSide);

        int index = IsSpawnItem(_addItemPf);
        if (index == -1)
        {
            GameManager.Instance.UpdateText(1, int.Parse(_addItemPf.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text.Replace(",", "")));
            Instantiate(_addItemPf, _spawnPoint);
        }
        else
        {
            _spawnPoint.GetChild(index).GetComponent<ClickAddItem>().OnClickPlusButton();
        }

        Menu[] menus = GameManager.Instance.MenuDatabase.Menus;
        for(int i = 0; i < menus.Length; i++)
        {
            if (_itemButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text.Equals(menus[i].Name))
            {
                for(int j = 0; j < menus[i].Type.Length; j++)
                {
                    if (menus[i].Type[j] == Type.Hamburger)
                    {
                        GameObject.Find("Popup").transform.GetChild(4).gameObject.SetActive(true);
                        GameManager.Instance.CurrentSetPrice = menus[i].Price; 
                    }

                }
            }
            
        }
        
    }

    private int IsSpawnItem(GameObject item)
    {
        int index = _spawnPoint.childCount;
        for(int i = 0; i < index; i++)
        {
            if (GetMenu(item).Equals(GetMenu(_spawnPoint.GetChild(i).gameObject)))
            {
                return i;
            }
        }
        return -1;
    }

    private string GetMenu(GameObject item)
    {
        return item.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
    }

    
}
