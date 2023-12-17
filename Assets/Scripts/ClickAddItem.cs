using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickAddItem : MonoBehaviour
{
    public int Price;
    [SerializeField] private Button _minusButton;
    [SerializeField] private Button _plusButton;
    [SerializeField] private Button _deleteButton;
    // Start is called before the first frame update
    void Start()
    {
        Price = int.Parse(transform.GetChild(5).GetComponent<TextMeshProUGUI>().text.Replace(",", ""));
        _minusButton.onClick.AddListener(OnClickMinusButton);
        _plusButton.onClick.AddListener(OnClickPlusButton);
        _deleteButton.onClick.AddListener(OnClickDeleteButton);
    }

    private void OnClickMinusButton()
    {
        int count = int.Parse(transform.GetChild(4).GetComponent<TextMeshProUGUI>().text.Replace(",",""));
        count--;
        if (count == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = count.ToString();
            transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = (count * Price).ToString();
        }
        GameManager.Instance.UpdateText(-1, -Price);
    }

    public void OnClickPlusButton()
    {
        int count = int.Parse(transform.GetChild(4).GetComponent<TextMeshProUGUI>().text);
        count++;
        transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = count.ToString();
        transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = (count * Price).ToString();
        GameManager.Instance.UpdateText(1, Price);
    }

    public void OnClickDeleteButton()
    {
        int count = int.Parse(transform.GetChild(4).GetComponent<TextMeshProUGUI>().text);
        int price = int.Parse(transform.GetChild(5).GetComponent<TextMeshProUGUI>().text.Replace(",", ""));
        GameManager.Instance.UpdateText(-count, -price);
        Destroy(gameObject);
    }
}
