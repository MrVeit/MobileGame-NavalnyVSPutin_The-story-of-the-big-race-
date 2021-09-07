using UnityEngine;

[CreateAssetMenu(fileName = "SkinDatabase", menuName = "DataBase/SkinCollection", order = 0)]
public class SkinBase : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private int _priceCount;
    [SerializeField] private int _isSold;

    public int Id
    {
        get { return _id; }
    }

    public int PriceCount
    {
        get { return _priceCount; }
    }

    public int IsSold
    {
        get { return _isSold; }
    }
}
