using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameStep
{
    None,
    Step1,
    Step2,
    Step3
}
[System.Serializable]
public class GameData
{
    public string member_id;
    public string kiosk_category_id;
    public string play_date;
    public int play_stage;
    public int play_time;
    public int is_success;
    public int is_game;
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int TotalCount;
    public int TotalPrice;
    public int TotalOrderPrice;
    public int TotalDiscountPrice;
    public int CurrentSetPrice; //세트 가격
    public int CurrentSetDrink; //추가 음료가겨
    public int CurrentSetSide; //추가 사이드 가격
    public int CurrentSetBun; //추가 빵 가격
    public int CurrentSidePrice; //추가 사이드 총가격
    public MenuDatabase MenuDatabase;

    [SerializeField] private GameObject TotalCountText;
    [SerializeField] private GameObject TotalOrderPriceText;
    [SerializeField] private GameObject TotalPriceText;
    [SerializeField] private Button _allClearButton;
    [SerializeField] private Button _cancelOrderButton;
    [SerializeField] private Button _payButton;
    [SerializeField] private Button _cuponBarcodeButton;
    [SerializeField] private Button _cuponConfirmButton;
    [SerializeField] public Button _setButton;
    [SerializeField] private Button _setBunButton;
    [SerializeField] private TextMeshProUGUI TotalDiscountText;
    [SerializeField] private TextMeshProUGUI TotalMainPriceText;
    [SerializeField] private TextMeshProUGUI _cupontext;
    [SerializeField] private Transform _spawnPoint;


    [SerializeField] private GameObject _finishUI;
    [SerializeField] private TextMeshProUGUI _playTimeTxt;
    [SerializeField] private GameObject _successPanel;
    [SerializeField] private GameObject _failPanel;

    private GameData _gameData;
    private GameStep _gameStep;
    private Stopwatch sw;
    private string _sceneNameType;
    private int playTime;
    private bool isSuccess;
    private bool _saveData;

    private void Awake()
    {
        Instance = this;
        _gameData = new GameData();
    }
    private void Start()
    {

        _allClearButton.onClick.AddListener(OnClickAllClearButton);
        _cancelOrderButton.onClick.AddListener(OnClickAllClearButton);
        _cuponBarcodeButton.onClick.AddListener(OnClickCuponButton);
        _cuponConfirmButton.onClick.AddListener(OnClickCuponButton);
        _setButton.onClick.AddListener(OnClickSetSelectButton);
        _setBunButton.onClick.AddListener(OnClickSetBunButton);

        Application.ExternalCall("unityFunction", _gameData.member_id);
        
        _gameData.play_date = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
        //Game
        string sceneName = SceneManager.GetActiveScene().name;
        _sceneNameType = sceneName.Substring(10, 5);

        _gameData.kiosk_category_id = sceneName.Substring(0, 9);
        UnityEngine.Debug.Log(sceneName.Substring(14, 1));
        _gameData.play_stage = int.Parse(sceneName.Substring(14, 1));

        if (_sceneNameType.StartsWith("Prac"))
        {
            _gameStep = GameStep.None;
            _gameData.is_game = 0;
        }
        else if (_sceneNameType.StartsWith("Test"))
        {
            _gameData.is_game = 1;
        }
        _gameStep = (GameStep)char.GetNumericValue(sceneName[sceneName.Length - 1]);

        sw = new Stopwatch();
        sw.Start();
    }

    private void Update()
    {
        if (int.TryParse(_cupontext.text, out int result))
        {
            _cuponConfirmButton.interactable = true;
        }
        else
        {
            _cuponConfirmButton.interactable = false;
        }

        if (_finishUI.activeSelf == true)
        {
            sw.Stop();

            switch (_gameStep)
            {
                case GameStep.Step1:
                    isSuccess = (TotalCount > 0);
                    break;
                case GameStep.Step2:
                    isSuccess = (TotalCount > 1);
                    break;
                case GameStep.Step3:
                    isSuccess = TotalDiscountPrice > 0;
                    break;
            }


            if (isSuccess)
            {
                _successPanel.SetActive(true);
            }
            else
            {
                _failPanel.SetActive(true);
            }

            _gameData.is_success = Convert.ToInt32(isSuccess); //정수로 값보내기
            UnityEngine.Debug.Log(_gameData.is_success);
            if (!_saveData)
            {
                SaveData(); //끝나면 정보 보내기

            }

            

        }

        // �ð� ���
        if (_playTimeTxt != null)
        {
            playTime = (int)sw.ElapsedMilliseconds / 1000;
            int minutes;
            int seconds;

            minutes = playTime / 60;
            seconds = playTime % 60;

            if (minutes > 0)
            {
                _playTimeTxt.text = "소요 시간 : " + minutes.ToString() + "분 " + seconds.ToString() + "초";
            }
            else
            {
                _playTimeTxt.text = "소요 시간 : " + seconds.ToString() + "초";
            }

            _gameData.play_time = playTime;//�ҿ�ð� ����
        }

    }

    private void SaveData()
    {
        _saveData = true;
        //����ȭ
        string jsonData = JsonUtility.ToJson(_gameData);

        string url = "https://003operation.shop/kiosk/insertData";

        StartCoroutine(SendDataToWeb(jsonData, url));
    }

    private IEnumerator SendDataToWeb(string jsonData, string url)
    {
        // �����͸� ����Ʈ �迭�� ��ȯ
        byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

        // POST ��û ������
        UnityWebRequest www = UnityWebRequest.PostWwwForm(url, "POST");
        www.uploadHandler = new UploadHandlerRaw(dataBytes);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("withCredentials", "true");

        // ��û ������ �� ���� ��ٸ���
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            UnityEngine.Debug.LogError("Failed to send data to the web server: " + www.error);
        }
        else
        {
            UnityEngine.Debug.Log("Data sent successfully!");
            // ���������� ������ www.downloadHandler.text ���� ���� Ȯ���� �� �ֽ��ϴ�.
            SetQuit();
        }
    }
    private void OnClickSetBunButton()
    {
        CurrentSetBun = 500;
    }

    private void OnClickSetSelectButton()
    {
        CurrentSidePrice = CurrentSetBun + CurrentSetDrink + CurrentSetSide;
        UpdateText(0, CurrentSidePrice);

        //마지막으로 spawn된 item
        _spawnPoint.GetChild(_spawnPoint.childCount - 1).GetChild(5).GetComponent<TextMeshProUGUI>().text = string.Format("{0:#,###}", CurrentSetPrice + CurrentSidePrice);
    }
    private void OnClickCuponButton()
    {
        int random = UnityEngine.Random.Range(1000, 3000);
        TotalDiscountPrice = random -  random % 100;
        TotalDiscountText.text = TotalDiscountPrice.ToString();
    }
    private void OnClickAllClearButton()
    {
        foreach(Transform child in _spawnPoint)
        {
            child.GetComponent<ClickAddItem>().OnClickDeleteButton();
            Destroy(child.gameObject);
        }
    }

    public void UpdateText(int count, int price)
    {
        TotalCount += count;
        TotalOrderPrice += price;
        TotalPrice = TotalOrderPrice - TotalDiscountPrice;
        if(TotalPrice < 0)
        {
            TotalPrice = 0;
        }

        TotalCountText.GetComponent<TextMeshProUGUI>().text = string.Format("{0:#,###}",TotalCount);
        TotalOrderPriceText.GetComponent<TextMeshProUGUI>().text = string.Format("{0:#,###}", TotalOrderPrice) ;
        TotalMainPriceText.GetComponent<TextMeshProUGUI>().text = string.Format("{0:#,###}", TotalOrderPrice);
        TotalPriceText.GetComponent<TextMeshProUGUI>().text = string.Format("{0:#,###}", TotalPrice);

        if(TotalCount > 0)
        {
            _payButton.interactable = true;
        }
        else
        {
            _payButton.interactable = false;    
        }
    }

    public void ReceiveData(string message)
    {
        _gameData.member_id = message;
        UnityEngine.Debug.Log("Received message from JavaScript: " + message);
    }

    public void SetQuit()
    {
        #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}