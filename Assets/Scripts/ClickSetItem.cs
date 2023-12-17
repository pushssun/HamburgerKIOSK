using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickSetItem : ClickField
{
    [SerializeField] private TextMeshProUGUI _count;
    private int _currentTypeIndex;

    protected override void SpawnItem(int index)
    {
        for (int i = 0; i < _menuDatabase.Menus.Length; i++) //�����ͺ��̽��� menus �߿���
        {
            for (int j = 0; j < _menuDatabase.Menus[i].Type.Length; j++) //���� ���� Type ��
            {
                if (_menuDatabase.Menus[i].Type[j] == _fieldType) //�ܹ��Ű� ���õǾ��ٸ�
                {
                    _itemPf.transform.GetChild(0).GetComponent<Image>().sprite = _menuDatabase.Menus[i].Image;
                    _itemPf.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _menuDatabase.Menus[i].Name;
                    _itemPf.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _menuDatabase.Menus[i].Price.ToString();

                    //��Ʈ �޴� spawn
                    Instantiate(_itemPf, _content.transform.GetChild(index)); //Instantiate �ܹ��� �޴�
                    _itemPf.GetComponent<Toggle>().group = transform.parent.GetComponent<ToggleGroup>();

                    _currentTypeIndex = index; // 0: side 1:drink

                }

            }
        }
        for(int i = 0; i < _content.transform.GetChild(_currentTypeIndex).childCount; i++)
        {
            int setItemIndex = i;
            _content.transform.GetChild(_currentTypeIndex).GetChild(i).GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => OnChangeAddItem(isOn, setItemIndex));
            
        }
        

    }

    private void OnChangeAddItem(bool isOn, int index) //��ǰ�� ������ 
    {
        int price = int.Parse(_content.transform.GetChild(_currentField).GetChild(index).GetChild(2).GetComponent<TextMeshProUGUI>().text);
        if (_currentField == 0) //���̵� �޴�
        {
            GameManager.Instance.CurrentSetSide = price;

        }
        else if(_currentField == 1) //���� �޴�
        {
            GameManager.Instance.CurrentSetDrink = price;
        }
    }

}
