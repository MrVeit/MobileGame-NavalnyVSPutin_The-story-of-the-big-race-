using UnityEngine;

public class CheckDistance : MonoBehaviour
{
    [Header("Main Components")]

    [SerializeField] private GameObject _destroyModel;
    private static GameObject _objectDestroyer;

    [Header("World Distance")]

    [SerializeField] private float _distanceAtObject;
    private static int _destroyValue;

    private void FixedUpdate()
    {
        DoDestroyProcess();
        CheckOnDestroy();
    }

    private void DoDestroyProcess()
    {
        _destroyValue = PlayerPrefs.GetInt("DestroyValue");

        if (_destroyValue >= 1)
        {
            _objectDestroyer = GameObject.FindWithTag("DestructionElement");
            _distanceAtObject = Vector3.Distance(transform.position, _objectDestroyer.transform.position);
        }
    }

    public void CheckOnDestroy()
    {
        if (_distanceAtObject <= 175)
        {
            if (_destroyValue == 1)
            {
                _destroyModel.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
