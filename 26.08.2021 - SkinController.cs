using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class SkinController : MonoBehaviour
{
    [Range(0, 2)]
    [SerializeField] private int _charId;

    public int CharId
    {
        get => _charId;
    }

    [Range(0, 3)]
    [SerializeField] private int[] _skinId, _itemId;
    [Range(0, 1)]
    [SerializeField] private int[] _skinStatus;

    [Header("UI Massive Obj")]
    
    [SerializeField] private Image[] _wayIcon, _charUltimate;
    [SerializeField] private Text[] _rubInformation;

    [Header("Group Obj")] 
    
    [SerializeField] private GameObject[] _charGroup, _roadCollection;

    [Header("Other Game Skins")]
    
    [SerializeField] private GameObject[] _leaveToMenuAtDeadPanel, _leaveToMenuAtPausePanel, _leaveMenuAtDeadPanelIcon;
    [SerializeField] private GameObject[] _characterSkinPack;
    [SerializeField] private GameObject[] _goldenErshPack, _goldenDuckPack;
    [SerializeField] private GameObject[] _navalnyPirateEye;
    [SerializeField] private GameObject[] _putinRubBackpack;

    private void Awake()
    {
        #region LoadAllGameSkins/Obj
        _charId = PlayerPrefs.GetInt("CharNumber");
        _skinId[0] = PlayerPrefs.GetInt("NavSkinInfo");
        _skinId[1] = PlayerPrefs.GetInt("PutSkinInfo");
        _itemId[0] = PlayerPrefs.GetInt("NavItemInfo");
        _itemId[1] = PlayerPrefs.GetInt("PutItemInfo");
        #endregion

        #region SkinStatus
        _skinStatus[0] = PlayerPrefs.GetInt("SoldItem_1");
        _skinStatus[1] = PlayerPrefs.GetInt("SoldItem_2");
        _skinStatus[2] = PlayerPrefs.GetInt("SoldItem_3");
        _skinStatus[3] = PlayerPrefs.GetInt("SoldItem_4");
        _skinStatus[4] = PlayerPrefs.GetInt("SoldItem_5");
        _skinStatus[5] = PlayerPrefs.GetInt("SoldItem_6");
        _skinStatus[6] = PlayerPrefs.GetInt("SoldItem_7");
        _skinStatus[7] = PlayerPrefs.GetInt("SoldItem_8");
        _skinStatus[8] = PlayerPrefs.GetInt("SoldItem_9");
        _skinStatus[9] = PlayerPrefs.GetInt("SoldItem_10");
        #endregion
    }

    private void Start()
    {
        OnCheckCharacterId();
        OnCheckSkin();
        OnCheckItem();
    }

    public void OnCheckSkin()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int u = 0; u < 4; u++)
            {
                switch (_skinId[0])
                {
                    case 0:
                        _characterSkinPack[0].SetActive(true);
                        _characterSkinPack[i].SetActive(false);
                        _charUltimate[u].gameObject.SetActive(false);
                        break;
                    case 1:
                        if (_skinStatus[0] == 1)
                        {
                            _characterSkinPack[1].SetActive(true);
                            _characterSkinPack[i].SetActive(false);
                            _charUltimate[0].gameObject.SetActive(true);
                            _charUltimate[u].gameObject.SetActive(false);
                        }
                        break;
                    case 2:
                        if (_skinStatus[1] == 1)
                        {
                            _characterSkinPack[2].SetActive(true);
                            _characterSkinPack[i].SetActive(false);
                            _charUltimate[1].gameObject.SetActive(true);
                            _charUltimate[u].gameObject.SetActive(false);
                        }
                        break;
                    case 3:
                        _characterSkinPack[0].SetActive(true);
                        _characterSkinPack[i].SetActive(false);
                        _charUltimate[u].gameObject.SetActive(false);
                        break;
                }

                switch (_skinId[1])
                {
                    case 0:
                        _characterSkinPack[3].SetActive(true);
                        _characterSkinPack[i].SetActive(false);
                        _charUltimate[u].gameObject.SetActive(false);
                        break;
                    case 1:
                        if (_skinStatus[5] == 1)
                        {
                            _characterSkinPack[4].SetActive(true);
                            _characterSkinPack[i].SetActive(false);
                            _charUltimate[2].gameObject.SetActive(true);
                            _charUltimate[u].gameObject.SetActive(false);
                        }
                        break;
                    case 2:
                        if (_skinStatus[6] == 1)
                        {
                            _characterSkinPack[i].SetActive(false);
                            _characterSkinPack[5].SetActive(true);
                            _charUltimate[u].gameObject.SetActive(false);
                            _charUltimate[3].gameObject.SetActive(true);
                        }
                        break;
                    case 3:
                        _characterSkinPack[3].SetActive(true);
                        _characterSkinPack[i].SetActive(false);
                        _charUltimate[u].gameObject.SetActive(false);
                        break;
                }
            }
        }
    }

    private void OnCheckItem()
    {
        for (int i = 0; i < 6; i++)
        {
            switch (_itemId[0])
            {
                case 1:
                    for (int p = 0; p < 3; p++)
                    {
                        if (_skinStatus[2] == 1)
                        {
                            _navalnyPirateEye[p].SetActive(true);
                        }
                    }
                    break;
                case 2:
                    if (_skinStatus[3] == 1)
                    {
                        _goldenErshPack[0].SetActive(true);
                        _goldenErshPack[1].SetActive(true);
                        _goldenErshPack[2].SetActive(true);
                        _goldenErshPack[i].SetActive(false);
                    }
                    break;
                case 3:
                    if (_skinStatus[4] == 1)
                    {
                        _goldenDuckPack[0].SetActive(true);
                        _goldenDuckPack[1].SetActive(true);
                        _goldenDuckPack[2].SetActive(true);
                        _goldenDuckPack[i].SetActive(false);
                    }
                    break;
            }

            switch (_itemId[1])
            {
                case 1:
                    for (int r = 0; r < 3; r++)
                    {
                        if (_skinStatus[7] == 1)
                        {
                            _putinRubBackpack[r].SetActive(true);
                        }
                    }
                    break;
                case 2:
                    if (_skinStatus[8] == 1)
                    {
                        _goldenErshPack[i].SetActive(false);
                        _goldenErshPack[3].SetActive(true);
                        _goldenErshPack[4].SetActive(true);
                        _goldenErshPack[5].SetActive(true);
                    }
                    break;
                case 3:
                    if (_skinStatus[9] == 1)
                    {
                        _goldenDuckPack[i].SetActive(false);
                        _goldenDuckPack[3].SetActive(true);
                        _goldenDuckPack[4].SetActive(true);
                        _goldenDuckPack[5].SetActive(true);
                    }
                    break;
            }
        }
    }

    public void OnCheckCharacterId()
    {
        switch (CharId)
        {
            case 1:
                _wayIcon[0].gameObject.SetActive(true);
                _wayIcon[1].gameObject.SetActive(false);
                _rubInformation[0].gameObject.SetActive(true);
                _rubInformation[1].gameObject.SetActive(false);
                _charGroup[0].SetActive(true);
                _charGroup[1].SetActive(false);
                _roadCollection[0].SetActive(true);
                _roadCollection[1].SetActive(false);
                _charUltimate[2].gameObject.SetActive(false);
                _charUltimate[3].gameObject.SetActive(false);
                _leaveMenuAtDeadPanelIcon[0].SetActive(true);
                _leaveMenuAtDeadPanelIcon[1].SetActive(false);
                _leaveToMenuAtDeadPanel[0].SetActive(true);
                _leaveToMenuAtDeadPanel[1].SetActive(false);
                _leaveToMenuAtPausePanel[0].SetActive(true);
                _leaveToMenuAtPausePanel[1].SetActive(false);
                break;
            case 2:
                _wayIcon[0].gameObject.SetActive(false);
                _wayIcon[1].gameObject.SetActive(true);
                _rubInformation[0].gameObject.SetActive(false);
                _rubInformation[1].gameObject.SetActive(true);
                _charGroup[0].SetActive(false);
                _charGroup[1].SetActive(true);
                _roadCollection[0].SetActive(false);
                _roadCollection[1].SetActive(true);
                _charUltimate[0].gameObject.SetActive(false);
                _charUltimate[1].gameObject.SetActive(false);
                _leaveMenuAtDeadPanelIcon[0].SetActive(false);
                _leaveMenuAtDeadPanelIcon[1].SetActive(true);
                _leaveToMenuAtDeadPanel[0].SetActive(false);
                _leaveToMenuAtDeadPanel[1].SetActive(true);
                _leaveToMenuAtPausePanel[0].SetActive(false);
                _leaveToMenuAtPausePanel[1].SetActive(true);
                break;
        }
    }
}
