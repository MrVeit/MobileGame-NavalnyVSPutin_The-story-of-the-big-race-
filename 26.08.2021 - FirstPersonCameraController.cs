using UnityEngine;

public class FirstPersonCameraController : MonoBehaviour
{
    [Header("World Position Camera")]

    private float _positionX, _positionY, _positionZ;

    [Header("Speed Rotate")]

    [SerializeField] private int _speedRotate;

    private void FixedUpdate()
    {
        _positionX += Input.GetAxis("Mouse X") * _speedRotate * Time.deltaTime;
        _positionY += Input.GetAxis("Mouse Y") * _speedRotate * Time.deltaTime;
                                                                                        //Осмотр территории камерой от первого лица.
        if (Input.GetKey(KeyCode.Q) || ChangeSwipe.SwipeLeft)
            _positionZ += 1f * _speedRotate * Time.deltaTime;
        if (Input.GetKey(KeyCode.E) || ChangeSwipe.SwipeRight)
            _positionZ -= 1f * _speedRotate * Time.deltaTime;

        transform.rotation = Quaternion.Euler(_positionY, _positionX, _positionZ);
    }
}
