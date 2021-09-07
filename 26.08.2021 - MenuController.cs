using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class MenuController : MonoBehaviour
{
    [Header("Graphic manager")]

    [SerializeField] private UniversalRenderPipelineAsset _urpManager;

    [Header("Skin Base Collection")]

    [SerializeField] private SkinInformation[] _skinDatabase;

    [Header("UI Game Components")]

    [SerializeField] private Text[] _navCyberrubInfo;
    [SerializeField] private Text[] _putCyberrubInfo;
    [SerializeField] private Text[] _navWayInfo;
    [SerializeField] private Text[] _putWayInfo;
    [SerializeField] private Button[] _buySkin;
    [SerializeField] private Button[] _selectSkin;

    [Header("Game Value")]

    private static int _navCyberRub;
    private static int _putCyberRub;
    private static float _navWay;
    private static float _putWay;

    [Range(0, 2)] [SerializeField] private int _firstLaunch, _gameStatus;

    [Header("Skin Value")]

    [Range(0, 3)]
    [SerializeField] private int[] _skinId;
    [Range(1, 2)]
    [SerializeField] private int _charNumber;

    private void Start()
    {
        LoadGameValue();
 
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        _firstLaunch = PlayerPrefs.GetInt("FirstLaunch");

        for (int i = 0; i < 2; i++)
        {
            _navWayInfo[i].text = _navWay.ToString();
        }

        for (int b = 0; b < 2; b++)
        {
            _putWayInfo[b].text = _putWay.ToString();
        }

        if (_firstLaunch == 0)
        {
            _urpManager.shadowCascadeCount = 1;
            _urpManager.shadowDistance = 0;
            _urpManager.msaaSampleCount = 0;
            _urpManager.supportsHDR = false;
            _urpManager.useSRPBatcher = true;
            _urpManager.supportsDynamicBatching = true;
            _urpManager.supportsCameraOpaqueTexture = false;
            _urpManager.supportsCameraDepthTexture = false;
        }
    }

    #region MainGameSettings

    private void FixedUpdate()
    {
        for (int i = 0; i < 2; i++)
        {
            _navCyberrubInfo[i].text = _navCyberRub.ToString();
        }

        for (int b = 0; b < 2; b++)
        {
            _putCyberrubInfo[b].text = _putCyberRub.ToString();
        }
    }

    public void AddNavalnySmallPack()
    {
        _navCyberRub += 500;
        StartCoroutine(SetSaveDataValueAfterGame(3f));
        PlayerPrefs.SetInt("NavRub", _navCyberRub);
        PlayerPrefs.Save();
    }

    public void AddNavalnyMediumPack()
    {
        _navCyberRub += 3500;
        StartCoroutine(SetSaveDataValueAfterGame(3f));
        PlayerPrefs.SetInt("NavRub", _navCyberRub);
        PlayerPrefs.Save();
    }

    public void AddNavalnyBigPack()
    {
        _navCyberRub += 7500;
        StartCoroutine(SetSaveDataValueAfterGame(3f));
        PlayerPrefs.SetInt("NavRub", _navCyberRub);
        PlayerPrefs.Save();
    }

    public void AddPutinSmallPack()
    {
        _putCyberRub += 1000;
        StartCoroutine(SetSaveDataValueAfterGame(3f));
        PlayerPrefs.SetInt("PutRub", _putCyberRub);
        PlayerPrefs.Save();
    }

    public void AddPutinMediumPack()
    {
        _putCyberRub += 5500;
        StartCoroutine(SetSaveDataValueAfterGame(3f));
        PlayerPrefs.SetInt("PutRub", _putCyberRub);
        PlayerPrefs.Save();
    }

    public void AddPutinBigPack()
    {
        _putCyberRub += 10500;
        StartCoroutine(SetSaveDataValueAfterGame(3f));
        PlayerPrefs.SetInt("PutRub", _putCyberRub);
        PlayerPrefs.Save();
    }

    private void CheckIdAndLoadValue()
    {
        StartCoroutine(SetSaveDataValueAfterGame(3f));

        _charNumber = PlayerPrefs.GetInt("CharNumber");

        if (_charNumber == 1)
        { 
            _navCyberRub += PlayerPrefs.GetInt("NavRub");
            _putCyberRub = PlayerPrefs.GetInt("PutDataRub");
            _putWay = PlayerPrefs.GetInt("PutDataRoad");

            if (PlayerPrefs.GetInt("NavRoad") > _navWay)
            {
                _navWay = PlayerPrefs.GetInt("NavRoad");
            }
        }
        else if (_charNumber == 2)
        {
            _putCyberRub += PlayerPrefs.GetInt("PutRub");
            _navCyberRub = PlayerPrefs.GetInt("NavDataRub");
            _navWay = PlayerPrefs.GetInt("NavDataRoad");

            if (PlayerPrefs.GetInt("PutRoad") > _putWay)
            {
                _putWay = PlayerPrefs.GetInt("PutRoad");
            }
        }
    }

    private IEnumerator SetSaveDataValueAfterGame(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        PlayerPrefs.SetInt("NavDataRub", _navCyberRub);
        PlayerPrefs.SetInt("NavDataRoad", (int)_navWay);
        PlayerPrefs.SetInt("PutDataRub", _putCyberRub);
        PlayerPrefs.SetInt("PutDataRoad", (int)_putWay);
    }

    private void LoadGameValue()
    {
        _gameStatus = PlayerPrefs.GetInt("GameStatus");

        if (_gameStatus == 1)
        {
            CheckIdAndLoadValue();

            Debug.Log("Валюта успешно загружена после пробега");
        }

        else if (_gameStatus == 2)
        {
            LoadBeforeLaunchGame();
        }
    }

    private void LoadBeforeLaunchGame()
    {
        _navCyberRub = PlayerPrefs.GetInt("NavDataRub");
        _putCyberRub = PlayerPrefs.GetInt("PutDataRub");
        _navWay = PlayerPrefs.GetInt("NavDataRoad");
        _putWay = PlayerPrefs.GetInt("PutDataRoad");

        Debug.Log("Выгрузка успешно произведена");
    }

    public void DeleteAllGameProgressNavalny()
    {
        PlayerPrefs.DeleteKey("NavRub");
        PlayerPrefs.DeleteKey("NavWay");
        PlayerPrefs.DeleteKey("NavDataRub");
        PlayerPrefs.DeleteKey("NavDataRoad");
        PlayerPrefs.DeleteKey("NavSkinInfo");
        PlayerPrefs.DeleteKey("NavItemInfo");
        PlayerPrefs.DeleteKey("SoldItem_1");
        PlayerPrefs.DeleteKey("SoldItem_2");
        PlayerPrefs.DeleteKey("SoldItem_3");
        PlayerPrefs.DeleteKey("SoldItem_4");
        PlayerPrefs.DeleteKey("SoldItem_5");
    }

    public void DeleteAllGameProgressPutin()
    {
        PlayerPrefs.DeleteKey("PutRub");
        PlayerPrefs.DeleteKey("PutRoad");
        PlayerPrefs.DeleteKey("PutDataRoad");
        PlayerPrefs.DeleteKey("PutDataRub");
        PlayerPrefs.DeleteKey("PutSkinInfo");
        PlayerPrefs.DeleteKey("PutItemInfo");
        PlayerPrefs.DeleteKey("SoldItem_6");
        PlayerPrefs.DeleteKey("SoldItem_7");
        PlayerPrefs.DeleteKey("SoldItem_8");
        PlayerPrefs.DeleteKey("SoldItem_9");
        PlayerPrefs.DeleteKey("SoldItem_10");
    }

    public void ReloadAllGameValues()
    {
        for (int i = 0; i < 2; i++)
        {
            _navCyberrubInfo[i].text = _navCyberRub.ToString();
            _putCyberrubInfo[i].text = _putCyberRub.ToString();
            _navWayInfo[i].text = _navWay.ToString();
            _putWayInfo[i].text = _putWay.ToString();
        }
    }

    public void OnClickPlayNavalny()
    {
        _charNumber = 1;
        _firstLaunch = 1;

        SceneManager.LoadScene("GameMode");
        PlayerPrefs.SetInt("FirstLaunch", _firstLaunch);
        PlayerPrefs.SetInt("CharNumber", _charNumber);
        PlayerPrefs.Save();
    }

    public void OnClickPlayPutin()
    {
        _charNumber = 2;
        _firstLaunch = 1;

        SceneManager.LoadScene("GameMode");
        PlayerPrefs.SetInt("FirstLaunch", _firstLaunch);
        PlayerPrefs.SetInt("CharNumber", _charNumber);
        PlayerPrefs.Save();
    }

    public void OnClickExit()
    {
        _gameStatus = 2;

        Application.Quit();
        PlayerPrefs.SetInt("GameStatus", _gameStatus);
    }

    private void OnApplicationPause(bool pause)
    {
        _gameStatus = 2;

        PlayerPrefs.SetInt("GameStatus", _gameStatus);
        PlayerPrefs.Save();
    }

    #endregion

    #region SkinManager

    #region OnSelectItemOperations
    public void SelectDefaultNavalny()
    {
        _skinId[0] = 3;

        PlayerPrefs.SetInt("NavSkinInfo", _skinId[0]);
        PlayerPrefs.Save();
    }

    public void SelectDefaultPutin()
    {
        _skinId[1] = 3;

        PlayerPrefs.SetInt("PutSkinInfo", _skinId[1]);
        PlayerPrefs.Save();
    }

    public void SelectJailNavalny()
    {
        if (_skinDatabase[0]._isSold[0] != 1)
        {
            _buySkin[0].gameObject.SetActive(true);

            if (_navCyberRub >= 3500)
            {
                _buySkin[0].interactable = true;
            }
        }

        else if (_skinDatabase[0]._isSold[0] == 1)
        {
            _buySkin[0].interactable = false;
            _buySkin[0].gameObject.SetActive(false);
            _selectSkin[0].gameObject.SetActive(true);
            _selectSkin[0].interactable = true;
        }
    }

    public void SelectGreenNavalny()
    {
        if (_skinDatabase[1]._isSold[1] != 1)
        {
            _buySkin[1].gameObject.SetActive(true);

            if (_navCyberRub >= 7500)
            {
                _buySkin[1].interactable = true;
            }
        }

        else if (_skinDatabase[1]._isSold[1] == 1)
        {
            _buySkin[1].interactable = false;
            _buySkin[1].gameObject.SetActive(false);
            _selectSkin[1].gameObject.SetActive(true);
            _selectSkin[1].interactable = true;
        }
    }

    public void SelectNavalnyPirateEye()
    {
        if (_skinDatabase[2]._isSold[2] != 1)
        {
            _buySkin[2].gameObject.SetActive(true);

            if (_navCyberRub >= 1000)
            {
                _buySkin[2].interactable = true;
            }
        }

        else if (_skinDatabase[2]._isSold[2] == 1)
        {
            _buySkin[2].interactable = false;
            _buySkin[2].gameObject.SetActive(false);
            _selectSkin[2].gameObject.SetActive(true);
            _selectSkin[2].interactable = true;
        }
    }

    public void SelectNavalnyGoldenErsh()
    {
        if (_skinDatabase[3]._isSold[3] != 1)
        {
            _buySkin[3].gameObject.SetActive(true);

            if (_navCyberRub >= 1500)
            {
                _buySkin[3].interactable = true;
            }
        }

        else if (_skinDatabase[3]._isSold[3] == 1)
        {
            _buySkin[3].interactable = false;
            _buySkin[3].gameObject.SetActive(false);
            _selectSkin[3].gameObject.SetActive(true);
            _selectSkin[3].interactable = true;
        }
    }

    public void SelectNavalnyGoldenDucks()
    {
        if (_skinDatabase[4]._isSold[4] != 1)
        {
            _buySkin[4].gameObject.SetActive(true);

            if (_navCyberRub >= 3000)
            {
                _buySkin[4].interactable = true;
            }
        }

        else if (_skinDatabase[4]._isSold[4] == 1)
        {
            _buySkin[4].interactable = false;
            _buySkin[4].gameObject.SetActive(false);
            _selectSkin[4].gameObject.SetActive(true);
            _selectSkin[4].interactable = true;
        }
    }

    public void SelectHunterPutin()
    {
        if (_skinDatabase[5]._isSold[5] != 1)
        {
            _buySkin[5].gameObject.SetActive(true);

            if (_putCyberRub >= 5500)
            {
                _buySkin[5].interactable = true;
            }
        }

        else if (_skinDatabase[5]._isSold[5] == 1)
        {
            _buySkin[5].interactable = false;
            _buySkin[5].gameObject.SetActive(false);
            _selectSkin[5].gameObject.SetActive(true);
            _selectSkin[5].interactable = true;
        }
    }

    public void SelectBiozardPutin()
    {
        if (_skinDatabase[6]._isSold[6] != 1)
        {
            _buySkin[6].gameObject.SetActive(true);

            if (_putCyberRub >= 10000)
            {
                _buySkin[6].interactable = true;
            }
        }

        else if (_skinDatabase[6]._isSold[6] == 1)
        {
            _buySkin[6].interactable = false;
            _buySkin[6].gameObject.SetActive(false);
            _selectSkin[6].gameObject.SetActive(true);
            _selectSkin[6].interactable = true;
        }
    }

    public void SelectPutinRubPack()
    {
        if (_skinDatabase[7]._isSold[7] != 1)
        {
            _buySkin[7].gameObject.SetActive(true);

            if (_putCyberRub >= 1500)
            {    
                _buySkin[7].interactable = true;
            }
        }

        else if (_skinDatabase[7]._isSold[7] == 1)
        {
            _buySkin[7].interactable = false;
            _buySkin[7].gameObject.SetActive(false);
            _selectSkin[7].gameObject.SetActive(true);
            _selectSkin[7].interactable = true;
        }
    }

    public void SelectPutinGoldenErsh()
    {
        if (_skinDatabase[8]._isSold[8] != 1)
        {
            _buySkin[8].gameObject.SetActive(true);

            if (_putCyberRub >= 2500)
            {
                _buySkin[8].interactable = true;
            }
        }

        else if (_skinDatabase[8]._isSold[8] == 1)
        {
            _buySkin[8].interactable = false;
            _buySkin[8].gameObject.SetActive(false);
            _selectSkin[8].gameObject.SetActive(true);
            _selectSkin[8].interactable = true;
        }
    }

    public void SelectPutinGoldenDucks()
    {
        if (_skinDatabase[9]._isSold[9] != 1)
        {
            _buySkin[9].gameObject.SetActive(true);

            if (_putCyberRub >= 3500)
            {
                _buySkin[9].interactable = true;
            }
        }

        else if (_skinDatabase[9]._isSold[9] != 1)
        {
            _buySkin[9].interactable = false;
            _buySkin[9].gameObject.SetActive(false);
            _selectSkin[9].gameObject.SetActive(true);
            _selectSkin[9].interactable = true;
        }
    }

    #endregion

    #region OnBuyItemOperations
    public void OnBuyJailNavalny()
    {
        if (_navCyberRub >= 3500)
        {
            if (_skinDatabase[0]._isSold[0] != 1)
            {
                _navCyberRub -= 3500;
                _skinDatabase[0]._isSold[0] = 1;
                _skinDatabase[0]._id = 1;
                Destroy(_buySkin[0].gameObject);
                StartCoroutine(SetSaveDataValueAfterGame(3f));
                PlayerPrefs.SetInt("NavSkinInfo", _skinDatabase[0]._id);
                PlayerPrefs.SetInt("SoldItem_1", _skinDatabase[0]._isSold[0]);
                PlayerPrefs.Save();
            }
        }

        else if (_skinDatabase[0]._isSold[0] == 1)
        {
            _skinDatabase[0]._id = 1;
            Destroy(_buySkin[0].gameObject);
            PlayerPrefs.SetInt("NavSkinInfo", _skinDatabase[0]._id);
            PlayerPrefs.SetInt("SoldItem_1", _skinDatabase[0]._isSold[0]);
            PlayerPrefs.Save();
        }
    }

    public void OnBuyGreenNavalny()
    {
        if (_navCyberRub >= 7500)
        {
            if (_skinDatabase[1]._isSold[1] != 1)
            {
                _navCyberRub -= 7500;
                _skinDatabase[1]._isSold[1] = 1;
                _skinDatabase[1]._id = 2;
                Destroy(_buySkin[1].gameObject);
                StartCoroutine(SetSaveDataValueAfterGame(3f));
                PlayerPrefs.SetInt("NavSkinInfo", _skinDatabase[1]._id);
                PlayerPrefs.SetInt("SoldItem_2", _skinDatabase[1]._isSold[1]);
                PlayerPrefs.Save();
            }
        }

        else if (_skinDatabase[1]._isSold[1] == 1)
        {
            _skinDatabase[1]._id = 2;
            Destroy(_buySkin[1].gameObject);
            PlayerPrefs.SetInt("NavSkinInfo", _skinDatabase[1]._id);
            PlayerPrefs.SetInt("SoldItem_2", _skinDatabase[1]._isSold[1]);
            PlayerPrefs.Save();
        }
    }

    public void OnBuyNavalnyPirateEye()
    {
        if (_navCyberRub >= 1000)
        {
            if (_skinDatabase[2]._isSold[2] != 1)
            {
                _navCyberRub -= 1000;
                _skinDatabase[2]._isSold[2] = 1;
                _skinDatabase[2]._id = 1;
                Destroy(_buySkin[2].gameObject);
                StartCoroutine(SetSaveDataValueAfterGame(3f));
                PlayerPrefs.SetInt("NavItemInfo", _skinDatabase[2]._id);
                PlayerPrefs.SetInt("SoldItem_3", _skinDatabase[2]._isSold[2]);
                PlayerPrefs.Save();
            }
        }

        else if (_skinDatabase[2]._isSold[2] == 1)
        {
            _skinDatabase[2]._id = 1;
            Destroy(_buySkin[2].gameObject);
            PlayerPrefs.SetInt("NavItemInfo", _skinDatabase[2]._id);
            PlayerPrefs.SetInt("SoldItem_3", _skinDatabase[2]._isSold[2]);
            PlayerPrefs.Save();
        }
    }

    public void OnBuyNavalnyGoldenErsh()
    {
        if (_navCyberRub >= 1500)
        {
            if (_skinDatabase[3]._isSold[3] != 1)
            {
                _navCyberRub -= 1500;
                _skinDatabase[3]._isSold[3] = 1;
                _skinDatabase[3]._id = 2;
                Destroy(_buySkin[3].gameObject);
                StartCoroutine(SetSaveDataValueAfterGame(3f));
                PlayerPrefs.SetInt("NavItemInfo", _skinDatabase[3]._id);
                PlayerPrefs.SetInt("SoldItem_4", _skinDatabase[3]._isSold[3]);
                PlayerPrefs.Save();
            }
        }


        else if (_skinDatabase[3]._isSold[3] == 1)
        {
            _skinDatabase[3]._id = 2;
            Destroy(_buySkin[3].gameObject);
            PlayerPrefs.SetInt("NavItemInfo", _skinDatabase[3]._id);
            PlayerPrefs.SetInt("SoldItem_4", _skinDatabase[3]._isSold[3]);
            PlayerPrefs.Save();
        }
    }

    public void OnBuyNavalnyGoldenDucks()
    {
        if (_navCyberRub >= 3000)
        {
            if (_skinDatabase[4]._isSold[4] != 1)
            {
                _navCyberRub -= 3000;
                _skinDatabase[4]._isSold[4] = 1;
                _skinDatabase[4]._id = 3;
                Destroy(_buySkin[4].gameObject);
                StartCoroutine(SetSaveDataValueAfterGame(3f));
                PlayerPrefs.SetInt("NavItemInfo", _skinDatabase[4]._id);
                PlayerPrefs.SetInt("SoldItem_5", _skinDatabase[4]._isSold[4]);
                PlayerPrefs.Save();
            }
        }

        else if (_skinDatabase[4]._isSold[4] == 1)
        {
            _skinDatabase[4]._id = 3;
            Destroy(_buySkin[4].gameObject);
            PlayerPrefs.SetInt("NavItemInfo", _skinDatabase[4]._id);
            PlayerPrefs.SetInt("SoldItem_5", _skinDatabase[4]._isSold[4]);
            PlayerPrefs.Save();
        }
    }

    public void OnBuyHunterPutin()
    {
        if (_putCyberRub >= 5500)
        {
            if (_skinDatabase[5]._isSold[5] != 1)
            {
                _putCyberRub -= 5500;
                _skinDatabase[5]._isSold[5] = 1;
                _skinDatabase[5]._id = 1;
                Destroy(_buySkin[5].gameObject);
                StartCoroutine(SetSaveDataValueAfterGame(3f));
                PlayerPrefs.SetInt("PutSkinInfo", _skinDatabase[5]._id);
                PlayerPrefs.SetInt("SoldItem_6", _skinDatabase[5]._isSold[5]);
                PlayerPrefs.Save();
            }
        }

        else if (_skinDatabase[5]._isSold[5] == 1)
        {
            _skinDatabase[5]._id = 1;
            Destroy(_buySkin[5].gameObject);
            PlayerPrefs.SetInt("PutSkinInfo", _skinDatabase[5]._id);
            PlayerPrefs.SetInt("SoldItem_6", _skinDatabase[5]._isSold[5]);
            PlayerPrefs.Save();
        }
    }

    public void OnBuyBiozardPutin()
    {
        if (_putCyberRub >= 10000)
        {
            if (_skinDatabase[6]._isSold[6] != 1)
            {
                _putCyberRub -= 10000;
                _skinDatabase[6]._isSold[6] = 1;
                _skinDatabase[6]._id = 2;
                Destroy(_buySkin[6].gameObject);
                StartCoroutine(SetSaveDataValueAfterGame(3f));
                PlayerPrefs.SetInt("PutSkinInfo", _skinDatabase[6]._id);
                PlayerPrefs.SetInt("SoldItem_7", _skinDatabase[6]._isSold[6]);
                PlayerPrefs.Save();
            }
        }

        else if (_skinDatabase[6]._isSold[6] == 1)
        {
            _skinDatabase[6]._id = 2;
            Destroy(_buySkin[6].gameObject);
            PlayerPrefs.SetInt("PutSkinInfo", _skinDatabase[6]._id);
            PlayerPrefs.SetInt("SoldItem_7", _skinDatabase[6]._isSold[6]);
            PlayerPrefs.Save();
        }
    }

    public void OnBuyPutinRubPack()
    {
        if (_putCyberRub >= 1500)
        {
            if (_skinDatabase[7]._isSold[7] != 1)
            {
                _putCyberRub -= 1500;
                _skinDatabase[7]._isSold[7] = 1;
                _skinDatabase[7]._id = 1;
                Destroy(_buySkin[7].gameObject);
                StartCoroutine(SetSaveDataValueAfterGame(3f));
                PlayerPrefs.SetInt("PutItemInfo", _skinDatabase[7]._id);
                PlayerPrefs.SetInt("SoldItem_8", _skinDatabase[7]._isSold[7]);
                PlayerPrefs.Save();
            }
        }

        else if (_skinDatabase[7]._isSold[7] == 1)
        {
            _skinDatabase[7]._id = 1;
            Destroy(_buySkin[7].gameObject);
            PlayerPrefs.SetInt("PutItemInfo", _skinDatabase[7]._id);
            PlayerPrefs.SetInt("SoldItem_8", _skinDatabase[7]._isSold[7]);
            PlayerPrefs.Save();
        }
    }

    public void OnBuyPutinGoldenErsh()
    {
        if (_putCyberRub >= 2500)
        {
            if (_skinDatabase[8]._isSold[8] != 1)
            {
                _putCyberRub -= 2500;
                _skinDatabase[8]._isSold[8] = 1;
                _skinDatabase[8]._id = 2;
                Destroy(_buySkin[8].gameObject);
                StartCoroutine(SetSaveDataValueAfterGame(3f));
                PlayerPrefs.SetInt("PutItemInfo", _skinDatabase[8]._id);
                PlayerPrefs.SetInt("SoldItem_9", _skinDatabase[8]._isSold[8]);
                PlayerPrefs.Save();
            }
        }

        else if (_skinDatabase[8]._isSold[8] == 1)
        {
            _skinDatabase[8]._id = 2;
            Destroy(_buySkin[8].gameObject);
            PlayerPrefs.SetInt("PutItemInfo", _skinDatabase[8]._id);
            PlayerPrefs.SetInt("SoldItem_9", _skinDatabase[8]._isSold[8]);
            PlayerPrefs.Save();
        }
    }

    public void OnBuyPutinGoldenDucks()
    {
        if (_putCyberRub >= 3500)
        {
            if (_skinDatabase[9]._isSold[9] != 1)
            {
                _putCyberRub -= 3500;
                _skinDatabase[9]._isSold[9] = 1;
                _skinDatabase[9]._id = 3;
                Destroy(_buySkin[9].gameObject);
                StartCoroutine(SetSaveDataValueAfterGame(3f));
                PlayerPrefs.SetInt("PutItemInfo", _skinDatabase[9]._id);
                PlayerPrefs.SetInt("SoldItem_10", _skinDatabase[9]._isSold[9]);
                PlayerPrefs.Save();
            }
        }

        else if (_skinDatabase[9]._isSold[9] == 1)
        {
            _skinDatabase[9]._id = 3;
            Destroy(_buySkin[9].gameObject);
            PlayerPrefs.SetInt("PutItemInfo", _skinDatabase[9]._id);
            PlayerPrefs.SetInt("SoldItem_10", _skinDatabase[9]._isSold[9]);
            PlayerPrefs.Save();
        }
    }

    #endregion
    #endregion
}
