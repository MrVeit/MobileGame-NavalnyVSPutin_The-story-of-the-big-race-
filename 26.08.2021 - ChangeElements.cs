using UnityEngine;
using UnityEngine.UI;

public class ChangeElements : MonoBehaviour
{
    [Header("Main GameObject")]

    [SerializeField] private MovementController _movementController;

    [Header("All Visible Scores")]

    [SerializeField] private Text[] _cyberTextInfo, _wayScore;

    public Text[] WayScore
    {
        get => _wayScore;
    }

    public Text[] CyberRubTextInfo
    {
        get => _cyberTextInfo;
    }

    [Header("UI Main Components")]

    [SerializeField] private Image _cyberRubSprite, _menuPauseSprite;
    [SerializeField] private Image[] _wayIcon;

    public Image CyberRubSprite
    {
        get => _cyberRubSprite;
    }

    public Image MenuPauseSprite
    {
        get => _menuPauseSprite;
    }

    public Image[] WayIcon
    {
        get => _wayIcon;
    }

    [Header("Speed Parameters")]

    [Range(15, 75)]
    private float _moveSpeed = 15;
    [Range(4, 10)]
    private float _animationSpeed = 4;
    [Range(1, 30)]
    private float _wayMultiplier;

    private const float _wayBaseValue = 1f;

    public float MoveSpeed
    {
        get => _moveSpeed;
    }

    [Header("UI Scores")]
    
    private int _navalnyRub, _putinRub;
    private float _navalnyRoad, _putinRoad;

    public int NavalnyRub
    {
        get => _navalnyRub;

        set
        {
            _navalnyRub = value;
        }
    }

    public int PutinRub
    {
        get => _putinRub;

        set
        {
            _putinRub = value;
        }
    }

    public float NavalnyRoad
    {
        get => _navalnyRoad;

        set
        {
            _navalnyRoad = value;
        }
    }

    public float PutinRoad
    {
        get => _putinRoad;

        set
        {
            _putinRoad = value;
        }
    }

    private void FixedUpdate()
    {
        _cyberTextInfo[0].text = _navalnyRub.ToString();
        _cyberTextInfo[1].text = _putinRub.ToString();
        _wayScore[0].text = ((int)_navalnyRoad).ToString();
        _wayScore[1].text = ((int)_putinRoad).ToString();

        _navalnyRoad += _wayBaseValue * _wayMultiplier * Time.deltaTime;
        _putinRoad += _wayBaseValue * _wayMultiplier * Time.deltaTime;

        _wayMultiplier += 0.05f * Time.deltaTime;
        _wayMultiplier = Mathf.Clamp(_wayMultiplier, 1, 50);

        _moveSpeed += 0.035f * Time.deltaTime;
        _moveSpeed = Mathf.Clamp(MoveSpeed, 15, 75);

        _animationSpeed += 0.0055f * Time.deltaTime;
        _animationSpeed = Mathf.Clamp(_animationSpeed, 4, 12);

        for (int i = 0; i < 2; i++)
        {
            _movementController.CharAnimator[i].SetFloat("Speed", _animationSpeed);
        }
    }
}

