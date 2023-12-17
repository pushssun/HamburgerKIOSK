using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickField : MonoBehaviour
{
    [SerializeField] private int startToggle;
    [SerializeField] private int toggleCount;
    [SerializeField] private ToggleGroup _field;
    [SerializeField] protected GameObject _content;
    [SerializeField] protected GameObject _itemPf;

    private ScrollRect _scrollRect;
    protected int _currentField;
    protected Type _fieldType;
    protected MenuDatabase _menuDatabase;

    // Start is called before the first frame update
    void Start()
    {
        _menuDatabase = GameManager.Instance.MenuDatabase;
        _scrollRect = _content.GetComponentInParent<ScrollRect>();
        
        for(int i = startToggle; i < startToggle + toggleCount; i++)
        {
            _fieldType = (Type)i; //토글 각각에
            int index = i - startToggle;
            SpawnItem(index); //아이템 spawn 

            _field.transform.GetChild(index).GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => OnClickedField(isOn, index));

        }
    }

    private void OnClickedField(bool isOn, int index)
    {
        if (isOn)
        {
            _currentField = index;
            _content.transform.GetChild(index).gameObject.SetActive(true); //해당 content setactive true
            _scrollRect.content = _content.transform.GetChild(index).GetComponent<RectTransform>();

        }
        else
        {
            _content.transform.GetChild(index).gameObject.SetActive(false); //해당 content setactive true

        }
        
    }

    protected virtual void SpawnItem(int index)
    {
        for (int i = 0; i < _menuDatabase.Menus.Length; i++) //데이터베이스의 menus 중에서
        {
            for (int j = 0; j < _menuDatabase.Menus[i].Type.Length; j++) //여러 개의 Type 중
            {
                if (_menuDatabase.Menus[i].Type[j] == _fieldType) //선택된 토글이 있다면
                {
                    _itemPf.transform.GetChild(0).GetComponent<Image>().sprite = _menuDatabase.Menus[i].Image; 
                    _itemPf.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _menuDatabase.Menus[i].Name; 
                    _itemPf.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = string.Format("{0:#,###}", _menuDatabase.Menus[i].Price); 
                    
                    Instantiate(_itemPf, _content.transform.GetChild(index)); //Instantiate
                }

            }
        }

    }
}
