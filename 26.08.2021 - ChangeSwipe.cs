using UnityEngine;

public class ChangeSwipe : MonoBehaviour
{
    public static bool Tap,
                       SwipeLeft,
                       SwipeRight,
                       SwipeUp,
                       SwipeDown;

    private bool _isTouched = false;

    private Vector2 _startTouch, _swipeDelta;

    private void Update()
    {
        Tap = SwipeDown = SwipeUp = SwipeLeft = SwipeRight = false;

        #region PC

        if (Input.GetMouseButtonDown(0))
        {
            Tap = true;
            _isTouched = true;
            _startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isTouched = false;
            Reset();
        }
        #endregion 

        #region MobilePlatform

        if (Input.touches.Length > 0)
        {
            switch (Input.touches[0].phase)
            {
                case TouchPhase.Began:
                    Tap = true;
                    _isTouched = true;
                    _startTouch = Input.touches[0].position;
                    break;
                case TouchPhase.Ended:
                    _isTouched = false;
                    Reset();
                    break;
                case TouchPhase.Canceled:
                    _isTouched = false;
                    Reset();
                    break;
            }
        }
        #endregion

        _swipeDelta = Vector2.zero;

        if (_isTouched) //определение дистанции свайпа
        {
            if (Input.touches.Length < 0)
                _swipeDelta = Input.touches[0].position - _startTouch;
            else if (Input.GetMouseButton(0))
                _swipeDelta = (Vector2)Input.mousePosition - _startTouch;
        }

        if (_swipeDelta.magnitude > 100) //проверка на совершение свайпа
        {
            //определение направления
            float x = _swipeDelta.x;
            float y = _swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                    SwipeLeft = true;
                else
                    SwipeRight = true;
            }

            else
            {
                if (y < 0)
                    SwipeDown = true;
                else
                    SwipeUp = true;
            }

            Reset();
        }
    }
    private void Reset()
    {
        _startTouch = _swipeDelta = Vector2.zero;
        _isTouched = false;
    }
}
