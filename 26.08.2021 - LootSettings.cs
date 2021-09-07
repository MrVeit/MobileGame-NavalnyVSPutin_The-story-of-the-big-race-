using UnityEngine;

public class LootSettings : MonoBehaviour
{
    [Header("World Space Parameters")]

    private const float _speedPosition = 100;

    private void FixedUpdate()
    {
        transform.Rotate(0, _speedPosition * Time.deltaTime, 0);
    }
}
