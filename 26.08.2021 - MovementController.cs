using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MovementController : MonoBehaviour
{
    [Header("Cosmetic and Game Elements")]

    [SerializeField] private ChangeElements _gameGuiElements;
    [SerializeField] private SkinController _skinController;

    [Header("Main Ultimate Components")]

    [SerializeField] private CollectValueAnimation _collectAnimation;
    private readonly CheckDistance _getDistance;

    [Header("UI Game Components")]

    [SerializeField] private GameObject _rubCollector;

    public GameObject RubCollector
    {
        get { return _rubCollector; }
    }

    [SerializeField] private GameObject[] _gamePanel, _abilityGameObjects;
    [SerializeField] private Button _continueForRubValue;

    [Header("VFX Particles Container")]

    [SerializeField] private GameObject _leftFootEffect, _rightFootEffect;

    [Header("Ultimate Buttons")]

    public Button[] UltimateIcons;
    public Image[] ReloadIcons, VerticalReload;

    [Header("Main Player Settings")]

    public Animator[] CharAnimator;
    private static CharacterController _playerController;
    private static Vector3 _direction, _gravity;
    [SerializeField] private float _jumpSpeed;

    [Header("Settings Line Position")]

    [SerializeField] private float _firstLinePos, _lineDistance, _sideSpeed;

    private static int _lineNumber = 1, _lineCount = 2;

    [Header("Character State Parameters")]

    [SerializeField] private static bool _isRolling, _isPaused, _isDead;

    private readonly Vector3 _characterControllerCenterNorm = new Vector3(0, 7, -0.25f),
                            _characterControllerCenterCrouch = new Vector3(0, 3, -0.25f);
    private readonly float _characterControllerHeightNorm = 14, _characterControllerHeightCrouch = 8;

    public static int SelectedCharacter = 0;

    [Header("Ultimate Elements")]

    [Range(0f, 6f)]
    public float UltimateValue;
    [Range(0, 2)]
    [SerializeField] private int _gameStatus;
    [Range(1, 2)]
    private static int _bonusRub = 1;
    [Range(0, 1)]
    private static int _magnetValue;
    [Range(0, 2)]
    private static int _destroyValue;

    [Header("Audio Database Effects")]
    
    [SerializeField] private AudioClip[] _deactivateUltimate, _collectUltimateResources;
    [SerializeField] private AudioClip _collectRub;

    private void Start()
    {
        _playerController = GetComponent<CharacterController>();
        _direction = new Vector3(1, 0, 0);
        _gravity = Vector3.zero;

        for (int i = 0; i < 4; i++)
        {
            VerticalReload[i].fillAmount = 0;
            VerticalReload[i].gameObject.SetActive(true);
            ReloadIcons[i].fillAmount = 0;
            ReloadIcons[i].gameObject.SetActive(false);
            VerticalReload[i].fillAmount = UltimateValue;
            UltimateIcons[i].interactable = false;
        }

        PlayerPrefs.SetInt("MagnetValue", _magnetValue);
        PlayerPrefs.SetInt("DestroyValue", _destroyValue);
        PlayerPrefs.Save();
    }

    private void LateUpdate()
    {
        for (int i = 0; i < 2; i++)
        {
            if (_gameGuiElements.NavalnyRub >= 150 || _gameGuiElements.PutinRub >= 150)
            {
                _continueForRubValue.interactable = true;
            }
            else
                _continueForRubValue.interactable = false;
        }
    }
    private void Update()
    {
        CheckInput();

        if (_playerController.isGrounded)
        {
            _gravity = Vector3.zero;

            for (int i = 0; i < 3; i++)
            {
                CharAnimator[i].ResetTrigger("Jump");
            }

            if (!_isRolling)
            {
                if (Input.GetAxisRaw("Vertical") > 0 || ChangeSwipe.SwipeUp)
                {
                    _gravity.y = _jumpSpeed;

                    StartCoroutine(DoJump(2f));
                }

                else if (Input.GetAxisRaw("Vertical") < 0 || ChangeSwipe.SwipeDown)
                    StartCoroutine(DoCrouch(1.5f));
            }
        }
        else
            _gravity += Physics.gravity * Time.deltaTime * 3;

        _playerController.Move(_direction);
        _direction.x = _gameGuiElements.MoveSpeed;
        _direction += _gravity;
        _direction *= Time.deltaTime;

        Vector3 newPos = transform.position;
        newPos.z = Mathf.Lerp(newPos.z, _firstLinePos + (_lineNumber * _lineDistance), Time.deltaTime * _sideSpeed);
        transform.position = newPos;
    }

    private void FixedUpdate()
    {
        if (UltimateValue >= 6)
        {
            for (int i = 0; i < 4; i++)
            {
                UltimateIcons[i].interactable = true;
                VerticalReload[i].fillAmount = 1;
            }
        }

        _magnetValue = PlayerPrefs.GetInt("MagnetValue");
    }

    IEnumerator DoJump(float seconds)
    {
        for (int i = 0; i < 3; i++)
        {
            CharAnimator[i].SetTrigger("Jump");
        }

        yield return new WaitForSeconds(seconds);

        for (int i = 0; i < 3; i++)
        {
            CharAnimator[i].ResetTrigger("Jump");
        }
    }

    IEnumerator DoCrouch(float seconds)
    {
        _isRolling = true;

        for (int i = 0; i < 3; i++)
        {
            CharAnimator[i].SetBool("Crouch", true);
        }

        _playerController.center = _characterControllerCenterCrouch;
        _playerController.height = _characterControllerHeightCrouch;

        yield return new WaitForSeconds(seconds);

        for (int i = 0; i < 3; i++)
        {
            CharAnimator[i].SetBool("Crouch", false);
        }

        _playerController.center = _characterControllerCenterNorm;
        _playerController.height = _characterControllerHeightNorm;

        yield return new WaitForSeconds(0.3f);

        _isRolling = false;
    }

    IEnumerator EnableLeftFoot(float seconds)
    {
        _leftFootEffect.SetActive(true);

        yield return new WaitForSeconds(seconds);

        _leftFootEffect.SetActive(false);
    }

    IEnumerator EnableRightFoot(float seconds)
    {
        _rightFootEffect.SetActive(true);

        yield return new WaitForSeconds(seconds);

        _rightFootEffect.SetActive(false);
    }

    public void CheckInput()
    {
        int sign = 0;

        if (_isRolling)
            return;

        if (Input.GetKeyDown(KeyCode.A) || ChangeSwipe.SwipeRight)
        {
            sign = -1;
            StartCoroutine(EnableRightFoot(1.5f));
        }

        else if (Input.GetKeyDown(KeyCode.D) || ChangeSwipe.SwipeLeft)
        {
            sign = 1;
            StartCoroutine(EnableLeftFoot(1.5f));
        }

        else if (Input.GetKeyDown(KeyCode.A) || ChangeSwipe.SwipeRight)
        {
            sign = 0;
            StartCoroutine(EnableRightFoot(1.5f));
        }

        else if (Input.GetKeyDown(KeyCode.A) || ChangeSwipe.SwipeLeft)
        {
            sign = 0;
            StartCoroutine(EnableLeftFoot(1.5f));
        }

        else
            return;

        _lineNumber += sign;
        _lineNumber = Mathf.Clamp(_lineNumber, 0, _lineCount);
    }

    public void OnSelectPause()
    {
        _isPaused = true;
        Time.timeScale = 0;
        AudioListener.volume = 0.55f;
        _gamePanel[0].SetActive(true);
        gameObject.GetComponent<CharacterController>().enabled = false;
    }

    public void OnDeselectPause()
    {
        _isPaused = false;
        Time.timeScale = 1;
        AudioListener.volume = 1f;
        _gamePanel[0].SetActive(false);
        gameObject.GetComponent<CharacterController>().enabled = true;
    }

    public void Ultimate_LongJump()
    {
        StartCoroutine(LongJumper(10f));
    }

    public void Ultimate_MagnitudeDance()
    {
        StartCoroutine(MagneteDance(10f));
    }

    public void Ultimate_DoubleMoney()
    {
        StartCoroutine(DoubleCollector(10f));
    }

    public void Ultimate_ImmortalBaby()
    {
        StartCoroutine(TimeToDestroy(10f));
    }


    public IEnumerator CollectingRub(float seconds)
    {
        _rubCollector.SetActive(true);
        _collectAnimation.CollectOperation();

        yield return new WaitForSeconds(seconds);

        _rubCollector.SetActive(false);
    }

    IEnumerator LongJumper(float seconds)
    {
        gameObject.TryGetComponent(out AudioSource _audioController);

        if (UltimateValue >= 6)
        {
            _jumpSpeed = 70;

            for (int i = 0; i < 4; i++)
            {
                ReloadIcons[i].gameObject.SetActive(true);
                ReloadIcons[i].GetComponent<Animator>().SetTrigger("Loading");
                UltimateIcons[i].interactable = false;
                VerticalReload[i].gameObject.SetActive(false);
            }
        }

        else
        {
            for (int i = 0; i < 4; i++)
            {
                ReloadIcons[i].gameObject.SetActive(false);
                UltimateIcons[i].interactable = false;
            }
        }

        yield return new WaitForSeconds(seconds);

        _audioController.PlayOneShot(_deactivateUltimate[0]);
        _audioController.Play();

        if (UltimateValue >= 6)
        {
            _jumpSpeed = 60;
            UltimateValue -= 6;

            for (int i = 0; i < 4; i++)
            {
                ReloadIcons[i].GetComponent<Animator>().ResetTrigger("Loading");
                ReloadIcons[i].gameObject.SetActive(false);
                UltimateIcons[i].interactable = false;
                VerticalReload[i].gameObject.SetActive(true);
                VerticalReload[i].fillAmount = UltimateValue;
                VerticalReload[i].fillAmount = 0;
            }
        }
    }

    IEnumerator MagneteDance(float seconds)
    {
        gameObject.TryGetComponent(out AudioSource _audioController);

        if (UltimateValue >= 6)
        {
            _magnetValue = 1;

            PlayerPrefs.SetInt("MagnetValue", _magnetValue);
            _abilityGameObjects[0].SetActive(true);

            for (int i = 0; i < 4; i++)
            {
                ReloadIcons[i].gameObject.SetActive(true);
                ReloadIcons[i].GetComponent<Animator>().SetTrigger("Loading");
                UltimateIcons[i].interactable = false;
                VerticalReload[i].gameObject.SetActive(false);
            }
        }

        else
        {
            for (int i = 0; i < 4; i++)
            {
                ReloadIcons[i].gameObject.SetActive(false);
                UltimateIcons[i].interactable = false;
            }
        }

        yield return new WaitForSeconds(seconds);

        _audioController.PlayOneShot(_deactivateUltimate[0]);
        _audioController.Play();

        if (UltimateValue >= 6)
        {
            _magnetValue = 0;
            PlayerPrefs.SetInt("MagnetValue", _magnetValue);
            _abilityGameObjects[0].SetActive(false);
            Debug.Log(_magnetValue);

            UltimateValue -= 6;

            for (int i = 0; i < 4; i++)
            {
                ReloadIcons[i].GetComponent<Animator>().ResetTrigger("Loading");
                ReloadIcons[i].gameObject.SetActive(false);
                UltimateIcons[i].interactable = false;
                VerticalReload[i].gameObject.SetActive(true);
                VerticalReload[i].fillAmount = UltimateValue;
                VerticalReload[i].fillAmount = 0;
            }
        }
    }

    IEnumerator DoubleCollector(float seconds)
    {
        gameObject.TryGetComponent(out AudioSource _audioController);

        if (UltimateValue >= 6)
        {
            _bonusRub = 2;

            for (int i = 0; i < 4; i++)
            {
                ReloadIcons[i].gameObject.SetActive(true);
                ReloadIcons[i].GetComponent<Animator>().SetTrigger("Loading");
                UltimateIcons[i].interactable = false;
                VerticalReload[i].gameObject.SetActive(false);
            }
        }

        else
        {
            for (int i = 0; i < 4; i++)
            {
                ReloadIcons[i].gameObject.SetActive(false);
                UltimateIcons[i].interactable = false;
            }
        }

        yield return new WaitForSeconds(seconds);

        _audioController.PlayOneShot(_deactivateUltimate[1]);
        _audioController.Play();

        if (UltimateValue >= 6)
        {
            _bonusRub = 1;
            UltimateValue -= 6;

            for (int i = 0; i < 4; i++)
            {
                ReloadIcons[i].GetComponent<Animator>().ResetTrigger("Loading");
                ReloadIcons[i].gameObject.SetActive(false);
                UltimateIcons[i].interactable = false;
                VerticalReload[i].gameObject.SetActive(true);
                VerticalReload[i].fillAmount = UltimateValue;
                VerticalReload[i].fillAmount = 0;
            }
        }
    }

    IEnumerator TimeToDestroy(float seconds)
    {
        gameObject.TryGetComponent(out AudioSource _audioController);

        if (UltimateValue >= 6)
        {
            _destroyValue = 1;
            _abilityGameObjects[1].SetActive(true);
            PlayerPrefs.SetInt("DestroyValue", _destroyValue);

            for (int i = 0; i < 4; i++)
            {
                ReloadIcons[i].gameObject.SetActive(true);
                ReloadIcons[i].GetComponent<Animator>().SetTrigger("Loading");
                UltimateIcons[i].interactable = false;
                VerticalReload[i].gameObject.SetActive(false);
            }
        }

        else
        {
            for (int i = 0; i < 4; i++)
            {
                UltimateIcons[i].interactable = false;
                ReloadIcons[i].gameObject.SetActive(false);
            }
        }

        yield return new WaitForSeconds(seconds);

        _audioController.PlayOneShot(_deactivateUltimate[1]);
        _audioController.Play();

        if (UltimateValue >= 6)
        {
            UltimateValue -= 6;

            for (int i = 0; i < 4; i++)
            {
                ReloadIcons[i].GetComponent<Animator>().ResetTrigger("Loading");
                ReloadIcons[i].gameObject.SetActive(false);
                UltimateIcons[i].interactable = false;
                VerticalReload[i].gameObject.SetActive(true);
                VerticalReload[i].fillAmount = UltimateValue;
                VerticalReload[i].fillAmount = 0;
            }
  
            _destroyValue = 0;
            PlayerPrefs.SetInt("DestroyValue", _destroyValue);
            _abilityGameObjects[1].SetActive(false);
        }
    }

    public void OnClickPayment()
    {
        for (int i = 0; i < 2; i++)
        {
            if (_gameGuiElements.PutinRub >= 150)
            {
                _continueForRubValue.interactable = true;
                _gameGuiElements.PutinRub -= 150;

                _isDead = false;
                _gamePanel[1].SetActive(false);
                _gameGuiElements.CyberRubSprite.gameObject.SetActive(true);
                _gameGuiElements.MenuPauseSprite.gameObject.SetActive(true);
                gameObject.GetComponent<CharacterController>().enabled = true;
                AudioListener.volume = 1f;
                Time.timeScale = 1;

                switch (_skinController.CharId)
                {
                    case 1:
                        _gameGuiElements.WayIcon[0].gameObject.SetActive(true);
                        break;
                    case 2:
                        _gameGuiElements.WayIcon[1].gameObject.SetActive(true);
                        break;
                }
            }
            else
                _continueForRubValue.interactable = false;

            if (_gameGuiElements.NavalnyRub >= 150)
            {
                _continueForRubValue.interactable = true;
                _gameGuiElements.NavalnyRub -= 150;
                _isDead = false;
                _gamePanel[1].SetActive(false);
                _gameGuiElements.CyberRubSprite.gameObject.SetActive(true);
                _gameGuiElements.MenuPauseSprite.gameObject.SetActive(true);
                gameObject.GetComponent<CharacterController>().enabled = true;
                AudioListener.volume = 1f;
                Time.timeScale = 1;

                switch (_skinController.CharId)
            {
                case 1:
                    _gameGuiElements.WayIcon[0].gameObject.SetActive(true);
                    break;
                case 2:
                    _gameGuiElements.WayIcon[1].gameObject.SetActive(true);
                    break;
            }
            }
            else
                _continueForRubValue.interactable = false;
        }
    }

    public void OnClickAds()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1f;
        gameObject.GetComponent<CharacterController>().enabled = true;

        for (int i = 0; i < 2; i++)
        {
            _isDead = false;
            _gamePanel[1].SetActive(false);
            _gameGuiElements.CyberRubSprite.gameObject.SetActive(true);
            _gameGuiElements.MenuPauseSprite.gameObject.SetActive(true);

            switch (_skinController.CharId)
            {
                case 1:
                    _gameGuiElements.WayIcon[0].gameObject.SetActive(true);
                    break;
                case 2:
                    _gameGuiElements.WayIcon[1].gameObject.SetActive(true);
                    break;
            }
        }
    }

    public void OnClickMenu()
    {
        _gameStatus = 1;
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        PlayerPrefs.SetInt("GameStatus", _gameStatus);

        switch (_skinController.CharId)
        {
            case 1:
                PlayerPrefs.SetInt("NavRub", _gameGuiElements.NavalnyRub);
                PlayerPrefs.SetInt("NavRoad", (int)_gameGuiElements.NavalnyRoad);
                PlayerPrefs.Save();
                break;

            case 2:
                PlayerPrefs.SetInt("PutRub", _gameGuiElements.PutinRub);
                PlayerPrefs.SetInt("PutRoad", (int)_gameGuiElements.PutinRoad);
                PlayerPrefs.Save();
                break;
        }
    }

    private void OnApplicationPause(bool pause)
    {
        _isPaused = pause;
        _gameStatus = 1;
        Time.timeScale = 1;
        PlayerPrefs.SetInt("GameStatus", _gameStatus);

        switch (_skinController.CharId)
        {
            case 1:
                PlayerPrefs.SetInt("NavRub", _gameGuiElements.NavalnyRub);
                PlayerPrefs.SetInt("NavRoad", (int)_gameGuiElements.NavalnyRoad);
                PlayerPrefs.Save();
                break;

            case 2:
                PlayerPrefs.SetInt("PutRub", _gameGuiElements.PutinRub);
                PlayerPrefs.SetInt("PutRoad", (int)_gameGuiElements.PutinRoad);
                PlayerPrefs.Save();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _rubCollector.SetActive(false);

        if (other.tag == "Rub")
        {
            _gameGuiElements.NavalnyRub += 1;
            _gameGuiElements.PutinRub += 1 * _bonusRub;
            gameObject.TryGetComponent(out AudioSource _audioController);
            _audioController.PlayOneShot(_collectRub);
            _audioController.Play();

            Destroy(other.gameObject);
            StartCoroutine(CollectingRub(2.5f));
        }

        if (other.tag == "GoldenErsh")
        {
            UltimateValue += 1;
            Destroy(other.gameObject);
            gameObject.TryGetComponent(out AudioSource _audioController);
            _audioController.PlayOneShot(_collectUltimateResources[Random.Range(0, _collectUltimateResources.Length)]);
            _audioController.Play();

            for (int i = 0; i < 4; i++)
            {
                VerticalReload[i].fillAmount += 0.17f;
            }
        }

        if (other.tag == "GoldenDuck")
        {
            UltimateValue += 3;
            Destroy(other.gameObject);
            gameObject.TryGetComponent(out AudioSource _audioController);
            _audioController.PlayOneShot(_collectUltimateResources[Random.Range(0, _collectUltimateResources.Length)]);
            _audioController.Play();

            for (int i = 0; i < 4; i++)
            {
                VerticalReload[i].fillAmount += 0.5f;
            }
        }

        if (other.tag == "PirateEye")
        {
            UltimateValue += 1;
            Destroy(other.gameObject);
            gameObject.TryGetComponent(out AudioSource _audioController);
            _audioController.PlayOneShot(_collectUltimateResources[Random.Range(0, _collectUltimateResources.Length)]);
            _audioController.Play();

            for (int i = 0; i < 4; i++)
            {
                VerticalReload[i].fillAmount += 0.17f;
            }
        }

        if (other.tag == "DeadObj")
        {
            _isDead = true;
            Destroy(other.gameObject);

            Time.timeScale = 0;
            AudioListener.volume = 0.55f;
            gameObject.GetComponent<CharacterController>().enabled = false;

            for (int i = 0; i < 2; i++)
            {
                _gamePanel[1].SetActive(true);
                _gameGuiElements.CyberRubSprite.gameObject.SetActive(false);
                _gameGuiElements.MenuPauseSprite.gameObject.SetActive(false);
                _gameGuiElements.WayIcon[i].gameObject.SetActive(false);
            }
        }
    }
}
