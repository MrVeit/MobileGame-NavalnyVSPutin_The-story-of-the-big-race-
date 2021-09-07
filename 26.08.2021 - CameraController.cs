using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Main Components")]

    [SerializeField] private Transform _targetLook;

    [Header("Objects Distance")]

    private static Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - _targetLook.position;
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, _offset.z + _targetLook.position.z); //по первым двум осям камера будет заморожена
        transform.position = newPosition;
    }
}
