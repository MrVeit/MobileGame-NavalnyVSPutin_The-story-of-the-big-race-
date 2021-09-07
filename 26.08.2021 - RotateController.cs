using UnityEngine;

public class RotateController : MonoBehaviour
{
    [Header("Speed Rotate")]

    [SerializeField] private float _swipeSpeed;

    [Header("Main Elements Position")]

    private float _positionX, _positionY, _positionZ;

    private void FixedUpdate()
    {
        _positionX += Input.GetAxis("Mouse X") * _swipeSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(_positionY, _positionX, _positionZ);
    }
}
