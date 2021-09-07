using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CollectValueAnimation : MonoBehaviour
{
    [Header("Move targets")]
    
    [SerializeField] private Transform _endPosition;
    private static Vector3 _startPosition;

    public void CollectOperation()
    {
        StartCoroutine(DoGetCollect());
    }

    private IEnumerator DoGetCollect()
    {
        _startPosition = transform.position;
        transform.DOMove(_endPosition.transform.position, 3);

        yield return new WaitForSeconds(0.5f);

        transform.DOMove(_startPosition, 0.5f);

        StartCoroutine(DoGetCollect());
    }
}
