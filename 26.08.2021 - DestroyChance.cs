using UnityEngine;

public class DestroyChance : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float _chanceOfStaying = 0.5f;

    private void Start()
    {
        if (Random.value > _chanceOfStaying)
            Destroy(gameObject);
    }
}
