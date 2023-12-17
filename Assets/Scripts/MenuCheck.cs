using TMPro;
using UnityEngine;

public class MenuCheck : MonoBehaviour
{
    [SerializeField] private Transform _selectSpawnPoint;
    [SerializeField] private Transform _checkSpawnPoint;
    [SerializeField] private GameObject _checkPf;


    private void Start()
    {
        foreach(Transform transform in _selectSpawnPoint)
        {

            _checkPf.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = transform.GetChild(1).GetComponent<TextMeshProUGUI>().text; //Name
            _checkPf.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = transform.GetChild(4).GetComponent<TextMeshProUGUI>().text; //Count
            _checkPf.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = transform.GetChild(5).GetComponent<TextMeshProUGUI>().text; //Price


            Instantiate(_checkPf,_checkSpawnPoint);
        }

        
    }

}
