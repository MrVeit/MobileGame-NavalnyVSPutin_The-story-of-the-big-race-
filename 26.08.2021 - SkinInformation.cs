using UnityEngine;

public class SkinInformation : MonoBehaviour
{
    [Header("Skin DataBase")]

    [SerializeField] private SkinBase _skinData;

    [Header("DataBase Information")]

    [Range(0, 11)]
    [SerializeField] public int _id;

    [Range(0, 10000)]
    [SerializeField] public int _priceCount;

    [Range(0, 1)]
    [SerializeField] public int[] _isSold;

    private void Start()
    {
        OnSkin(_skinData);
        CheckDataBase();
    }

    private void OnSkin(SkinBase skinbase)
    {
        _id = skinbase.Id;
        _priceCount = skinbase.PriceCount;
    }

    private void CheckDataBase()
    {
        _isSold[0] = PlayerPrefs.GetInt("SoldItem_1");
        _isSold[1] = PlayerPrefs.GetInt("SoldItem_2");
        _isSold[2] = PlayerPrefs.GetInt("SoldItem_3");
        _isSold[3] = PlayerPrefs.GetInt("SoldItem_4");
        _isSold[4] = PlayerPrefs.GetInt("SoldItem_5");
        _isSold[5] = PlayerPrefs.GetInt("SoldItem_6");
        _isSold[6] = PlayerPrefs.GetInt("SoldItem_7");
        _isSold[7] = PlayerPrefs.GetInt("SoldItem_8");
        _isSold[8] = PlayerPrefs.GetInt("SoldItem_9");
        _isSold[9] = PlayerPrefs.GetInt("SoldItem_10");
    }
}
