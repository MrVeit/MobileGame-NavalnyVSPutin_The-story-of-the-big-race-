using UnityEngine;

public class ChangeListBase : MonoBehaviour
{
    [Header("List Components")]

    [SerializeField]
    private GameObject[] _dataBaseList;

    [Range(0, 7)]
    [SerializeField] private int _index;

    private void LateUpdate()
    {
        switch (_index)
        {
            case 7:
                _index = 6;
                break;
            case -1:
                _index = 0;
                break;
        }
    }

    public void LeftClick()
    {
        _index++;

        for (int i = 0; i < 7; i++)
        {
            switch (_index)
            {
                case 0:
                    _dataBaseList[i].SetActive(false);
                    _dataBaseList[0].SetActive(true);
                    break;

                case 1:
                    _dataBaseList[i].SetActive(false);
                    _dataBaseList[1].SetActive(true);
                    break;

                case 2:
                    _dataBaseList[i].SetActive(false);
                    _dataBaseList[2].SetActive(true);
                    break;

                case 3:
                    _dataBaseList[i].SetActive(false);
                    _dataBaseList[3].SetActive(true);
                    break;

                case 4:
                    _dataBaseList[i].SetActive(false);
                    _dataBaseList[4].SetActive(true);
                    break;

                case 5:
                    _dataBaseList[i].SetActive(false);
                    _dataBaseList[5].SetActive(true);
                    break;

                case 6:
                    _dataBaseList[i].SetActive(false);
                    _dataBaseList[6].SetActive(true);
                    break;
            }
        }
    }

    public void RightClick()
    {
        _index -= 1;

        for (int i = 0; i < 7; i++)
        {
            switch (_index)
            {
                case 6:
                    _dataBaseList[i].SetActive(false);
                    _dataBaseList[6].SetActive(true);
                    break;

                case 5:
                    _dataBaseList[i].SetActive(false);
                    _dataBaseList[5].SetActive(true);
                    break;

                case 4:
                    _dataBaseList[i].SetActive(false);
                    _dataBaseList[4].SetActive(true);
                    break;

                case 3:
                    _dataBaseList[i].SetActive(false);
                    _dataBaseList[3].SetActive(true);
                    break;

                case 2:
                    _dataBaseList[i].SetActive(false);
                    _dataBaseList[2].SetActive(true);
                    break;

                case 1:
                    _dataBaseList[i].SetActive(false);
                    _dataBaseList[1].SetActive(true);
                    break;

                case 0:
                    _dataBaseList[i].SetActive(false);
                    _dataBaseList[0].SetActive(true);
                    break;
            }
        }
    }
}

