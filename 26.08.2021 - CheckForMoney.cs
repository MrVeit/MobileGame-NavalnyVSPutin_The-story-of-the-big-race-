using System.Collections;
using UnityEngine;

public class CheckForMoney : MonoBehaviour
{
    [SerializeField] private ChangeElements _changeElements;
    [SerializeField] private MovementController _movementCharacter;
    [SerializeField] private CollectValueAnimation _collectRubAnimation;

    private void OnTriggerEnter(Collider other)
    {
        _movementCharacter.RubCollector.SetActive(false);

        if (other.CompareTag("Rub"))
        {
            Destroy(other.gameObject);
            _changeElements.NavalnyRub += 1;
            _movementCharacter.StartCoroutine(CollectRub(2.5f));
        }
    }

    public IEnumerator CollectRub(float seconds)
    {
        _movementCharacter.RubCollector.SetActive(true);
        _collectRubAnimation.CollectOperation();

        yield return new WaitForSeconds(seconds);

        _movementCharacter.RubCollector.SetActive(false);
        _collectRubAnimation.CollectOperation();
    }
}
