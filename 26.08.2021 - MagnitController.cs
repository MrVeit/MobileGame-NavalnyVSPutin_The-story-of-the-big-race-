using System.Collections;
using UnityEngine;

public class MagnitController : MonoBehaviour
{
    [Header("Main Elements")]

    private GameObject _magnetTarget;

    [Header("Value Apply")]

    private int _magnetValue;
    private const float _forceFactor = 20;

    [Range(0f, 450)]
    [SerializeField] private float _distance;

    private void Awake()
    {
        _magnetTarget = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        MoveMoneyPack();
        StartCoroutine(CheckVisibleValue(5f));
    }

    private IEnumerator CheckVisibleValue(float seconds)
    {
        _magnetValue = PlayerPrefs.GetInt("MagnetValue");
        _distance = Vector3.Distance(_magnetTarget.transform.position, transform.position);

        yield return new WaitForSeconds(seconds);

        StartCoroutine((CheckVisibleValue(5f)));
    }

    private void MoveMoneyPack()
    {
        if (_distance <= 450 && _magnetValue == 1)
            GetComponent<Rigidbody>().AddForce((_magnetTarget.transform.position - transform.position) * _forceFactor * Time.smoothDeltaTime);
    }
}

